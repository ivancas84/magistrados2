﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Utils
{
    public static class CollectionUtils
    {

        /// <summary>
        /// Copiar valores de IDictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <remarks>https://stackoverflow.com/questions/8702603/merging-two-objects-in-c-sharp</remarks>
        public static void Copy(this IDictionary<string, object?> target, IDictionary<string, object?> source, bool targetNull = true, bool sourceNotNull = false, bool createKey = false, bool compareNotNull = false, IEnumerable<string>? ignoreKeys = default)
        {
            if(ignoreKeys == default)
                ignoreKeys = new List<string>();

            foreach(var (key, value) in source)
            {
                if (ignoreKeys.Contains(key)) 
                    continue;

                if(!target.ContainsKey(key))
                { 
                    if(!createKey)
                        continue;
                    target[key] = null;
                }

                if (!target[key].IsNullOrEmptyOrDbNull() && targetNull)
                    continue;

                if (source[key].IsNullOrEmptyOrDbNull() && sourceNotNull)
                    continue;

                if (compareNotNull && source[key] != null && target[key] != null)
                    if (!source[key]!.ToString()!.Equals(target[key]!.ToString()))
                        throw new Exception("Valores diferentes");

                target[key] = source[key];
            }
        }

        /// <summary>
        /// Copiar valores de objectos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <remarks>https://stackoverflow.com/questions/8702603/merging-two-objects-in-c-sharp</remarks>
        public static void CopyValues<T>(this T target, T source, bool targetNull = true, bool sourceNotNull = false)
        {
            Type t = typeof(T);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var propT = target.GetType().GetProperty(prop.Name);
                var valorTarget = propT!.GetValue(target, null);
                var valorSource = prop.GetValue(source, null);

                if (sourceNotNull && valorSource == null)
                    continue;

                if (targetNull && valorTarget != null)
                    continue;

                prop.SetValue(target, valorSource, null);
            }
        }

        /// <summary>
        /// Copiar valores de tipos diferentes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="targetNull">La copia se realiza solamente si el campo del target es null</param>
        /// <param name="sourceNotNull">La copia se realiza solamente si el campo del surce es not null</param>
        /// <param name="compareNotNull">Se realiza comparacion de valores no nulos, si son distintos, dispara excepcion</param>



        public static void CopyValues<T, W>(this T target, W source, bool targetNull = true, bool sourceNotNull = false, bool compareNotNull = false)
        {
            Type t = typeof(W);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var propT = target.GetType().GetProperty(prop.Name);
                if (propT == null)
                    continue;

                var valorTarget = propT!.GetValue(target, null);
                var valorSource = prop.GetValue(source, null);

                if (
                    propT.IsNullOrEmpty() || (
                        targetNull
                        && (!valorTarget.IsNullOrEmptyOrDbNull())
                    )
                )
                    continue;


                if (compareNotNull && valorSource != null && valorTarget != null)
                    if (!valorSource.ToString()!.Equals(valorTarget.ToString()))
                        throw new Exception("Valores diferentes");

                var value = prop.GetValue(source, null);

                if (sourceNotNull && value.IsNullOrEmptyOrDbNull())
                    continue;

                propT.SetValue(target, value, null);
            }
        }

        public static void Merge(this IDictionary<string, object> dictionary1, IDictionary<string, object> dictionary2, string prefix = "")
        {
            dictionary2.ToList().ForEach(pair => dictionary1[prefix + pair.Key] = pair.Value);
        }

        public static void MergeNotNull(this IDictionary<string, object> dictionary1, IDictionary<string, object> dictionary2, string prefix = "")
        {
            dictionary2.ToList().ForEach(pair => {
                if (pair.Value.IsNullOrEmpty()) { dictionary1[prefix + pair.Key] = pair.Value; } 
            } );
        }

        public static bool IsNullOrEmpty(this IList? List)
        {
            return List == null || List.Count < 1;
        }

        public static bool IsNullOrEmpty(this IDictionary? Dictionary)
        {
            return (Dictionary == null || Dictionary.Count < 1);
        }

        public static bool IsNullOrEmpty(this object? o)
        {
            if (o is null)
            {
                return true;
            }

            if (o is IDictionary)
            {
                return ((IDictionary)o).IsNullOrEmpty();
            }
            if (o is IList) return ((IList)o).IsNullOrEmpty();
            if (o is string)
            {
                return String.IsNullOrEmpty((string)o);
            }
            return false;
        }

        public static bool IsList(this object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsTuple(this object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Tuple<>));
        }

        public static bool IsDictionary(this object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        public static bool IsArray(this object o)
        {
            return o.GetType().IsArray;
        }


        /// <summary>
        /// Lista de valores de una entrada del diccionario
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="rows">Lista de diccionarios</param>
        /// <param name="key">Llave del diccionario</param>
        /// <returns>Lista de valores de una entrada del diccionario</returns>
        public static IEnumerable<T> ColOfVal<T>(this IEnumerable<Dictionary<string, object?>> rows, string key)
        {
            List<T> response = new();
            foreach (Dictionary<string, object?> row in rows)
                foreach (var (k, v) in row)
                    if (k == key && v != null)
                        response.Add((T)v);

            return response;
        }

        public static IEnumerable<T?> ColOfProp<T, V>(this IEnumerable<V> source, string key)
        {
            Type t = typeof(V);

            var p = t.GetProperty(key);

            List<T?> response = new();

            foreach (var item in source)
                response.Add((T?)p.GetValue(item));

            return response;
        }

        public static IEnumerable<T> ColOfObj<T>(this IEnumerable<Dictionary<string, object?>> rows) where T : class, new()
        {
            var results = new List<T>();

            foreach(var row in rows)
                results.Add(row.Obj<T>());

            return results;
        }

        public static void SetData(this object someObject, IDictionary<string, object?> source)
        {
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                string fieldName = item.Key.Replace("-", "__");
                if (someObjectType.GetProperty(fieldName) != null)
                    if (!item.Value.IsNullOrEmptyOrDbNull())
                        someObjectType
                            .GetProperty(fieldName)!
                            .SetValue(someObject, item.Value, null);
                    else
                        someObjectType
                            .GetProperty(fieldName)!
                            .SetValue(someObject, null, null);
            }
        }

        public static T Obj<T>(this IDictionary<string, object?> source) where T : class, new()
        {
            var someObject = new T();
            someObject.SetData(source);
            return someObject;
        }

        /// <summary>
        /// Convertir objeto a diccionario
        /// </summary>
        /// <param name="source"></param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static IDictionary<string, object?> Dict(this object source, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
        {
            var properties = source.GetType().GetProperties();
            Dictionary<string, object?> response = new();
            foreach (var propInfo in properties)
            {
                try
                {
                    response[propInfo.Name.Replace("__", "-")] = propInfo.GetValue(source, null);
                }catch(TargetParameterCountException ex) {
                    continue;
                }
            }
            return response;
        }

        public static IEnumerable<Dictionary<string, object?>> ColOfDict(this IEnumerable<object> source, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
        {
            List<Dictionary<string, object>> response = new();
            foreach (var s in source)
            {
                response.Add((Dictionary<string, object>)Dict(s, bindingAttr)!);
            }
            return response;
        }


        public static void MergeByKeys(this IEnumerable<Dictionary<string, object>> source, IEnumerable<Dictionary<string, object?>> source2, string key1, string? key2 = null, string prefix = "")
        {
            key2 = key2 ?? key1;

            var s = source2.DictOfDictByKeys(key2);

            foreach (var item in source)
            {
                if (s.ContainsKey(item[key1].ToString()))
                    item.Merge(s[item[key1].ToString()], prefix);
            }
        }

        public static void MergeByKeysFirst(this IEnumerable<Dictionary<string, object>> source, IEnumerable<Dictionary<string, object>> source2, string key1, string? key2 = null)
        {
            key2 = key2 ?? key1;

            var s = source2.DictOfDictByKeys(key2);

            foreach (var item in source)
            {
                if (s.ContainsKey(item[key1]!.ToString()!))
                {
                    item.Merge(s[item[key1]!.ToString()!]);
                    break;
                }
            }
        }

        public static IDictionary<string, List<Dictionary<string, object?>>> DictOfListByKey(this IEnumerable<Dictionary<string, object?>> source, string key)
        {
            Dictionary<string, List<Dictionary<string, object?>>> response = new();
            foreach(Dictionary<string, object?> row in source)
            {
                if (!response.ContainsKey(key))
                    response[key] = new();
                response[key].Add(row);
            }
            return response;
        }

        public static IDictionary<string, List<Dictionary<string, object?>>> DictOfListByKeys(this IEnumerable<Dictionary<string, object?>> source, params string[] keys)
        {
            Dictionary<string, List<Dictionary<string, object?>>> response = new();
            foreach (Dictionary<string, object?> row in source)
            {
                List<string> val = new();
                foreach (var k in keys)
                    val.Add(row[k].ToString()!);

                string key = String.Join("~", val.ToArray());

                if (!response.ContainsKey(key))
                    response[key] = new();
                response[key].Add(row);
            }
            return response;
        }





        

        public static IDictionary<string, T> DictOfObjByPropertyNames<T>(this IEnumerable<T> source, params string[] propertyNames)
        {
            Dictionary<object, T> response = new();
            foreach (T obj in source)
            {
                List<string> val = new();
                foreach (string propName in propertyNames)
                {
                    val.Add(obj.GetPropertyValue(propName)!.ToString()!);
                }
                string key = String.Join("~", val.ToArray());
                response[key] = obj;
            }

            return (IDictionary<string, T>)response;
        }

        public static IDictionary<string, Dictionary<string, object?>> DictOfDictByKeys(this IEnumerable<Dictionary<string, object?>> source, params string[] keys)
        {
            Dictionary<object, Dictionary<string, object?>> response = new();
            foreach (Dictionary<string, object?> row in source) {
                List<string> val = new();
                foreach (var k in keys)
                    val.Add(row[k]!.ToString()!);

                string key = String.Join("~", val.ToArray());
                response[key] = row;
            }

            return (IDictionary<string, Dictionary<string, object?>>)response;
        }

        public static IDictionary<string, object> DictOfDictByKeysValue(this IEnumerable<Dictionary<string, object?>> source, string keyValue, params string[] keys)
        {
            Dictionary<string, object> response = new();
            foreach (Dictionary<string, object> row in source)
            {
                List<string> val = new();
                foreach (var k in keys)
                    val.Add(row[k].ToString()!);

                string key = String.Join("~", val.ToArray());
                response[key] = row[keyValue];
            }

            return response;
        }


        public static IDictionary<string, T> Dict<T>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
            return dictionary;
        }

        public static IDictionary<string, object?> AddPrefixToKeysOfDict(this IDictionary<string, object?> source, string prefix)
        {
            Dictionary<string, object?> response = new();
            foreach(var (key, obj) in source)
                response[prefix + key] = obj;

            return response;
        }

        public static List<Dictionary<string, object?>> AddPrefixToKeysOfDicts(this IEnumerable<Dictionary<string, object?>> source, string prefix)
        {
            List<Dictionary<string, object?>> response = new();
            foreach(Dictionary<string, object?> row in source)
                response.Add((Dictionary<string, object?>)row.AddPrefixToKeysOfDict(prefix));
            return response;
        }

        /// <summary>
        /// Add prefix to each element of list of strings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static IEnumerable<string> AddPrefixToEnum(this IEnumerable<string> source, string prefix)
        {
            List<string> response = new();
            foreach (string e in source)
                response.Add(prefix + e);
            return response;
        }

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> items)
        {
            foreach (var item in items)
                oc.Add(item);
        }

        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<Dictionary<string, object?>> items) where T : class, new()
        {
            foreach (var item in items) { 
                T o = item.Obj<T>();
                oc.Add(o);
            }
        }

        /// <summary>
        /// https://www.dotnetperls.com/sort-strings-length
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static IEnumerable<string> SortByLength(this IEnumerable<string> e, string sort = "ASC")
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = (sort == "DESC") ?
                from s in e orderby s.Length ascending select s : from s in e orderby s.Length descending select s;

            return sorted;
        }

        public static string ToStringDict(this IDictionary<string, object?> param)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, object?> keyValues in param)
            {
                dictionaryString += keyValues.Key + " : " + keyValues.Value?.ToString() + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }

        /// <summary>
        ///     A T extension method that sets property value.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The @this to act on.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <remarks>https://csharp-extension.com/en/method/1002738/object-setpropertyvalue</remarks>
        public static void SetPropertyValue<T>(this T @this, string propertyName, object? value)
        {
            Type type = @this.GetType();
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            property.SetValue(@this, value, null);
        }

        public static object? GetPropertyValue<T>(this T @this, string propertyName)
        {
            Type type = @this.GetType();
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            return property.GetValue(@this);
        }

        /// <summary>
        /// Clonar objeto 
        /// </summary>
        /// <remarks>https://www.wwt.com/article/how-to-clone-objects-in-dotnet-core</remarks>
        public static T? Clone<T>(this T self)
        {
            var serialized = JsonConvert.SerializeObject(self);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

}


   


