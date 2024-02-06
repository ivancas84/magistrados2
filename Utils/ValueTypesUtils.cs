﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class ValueTypesUtils
    {

        private static Random random = new Random();

        public static string ToTitleCase(this string str)
        {
            TextInfo textInfo = new CultureInfo("es-AR", false).TextInfo;
            return textInfo.ToTitleCase(str);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static char GetNextChar(this char c)
        {
            // convert char to ascii
            int ascii = (int)c;
            // get the next ascii
            int nextAscii = ascii + 1;
            // convert ascii to char
            char nextChar = (char)nextAscii;

            return nextChar;
        }

        public static string RemoveLastChar(this string s, char c)
        {
            int index = s.LastIndexOf(c); //remover ultima coma
            if (index > -1)
                return s.Remove(index, 1);
            return s;
        }

        public static string RemoveDigits(this string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.IndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        public static bool ToBool(this string @this)
        {
            string s = @this.Substring(0, 1).ToLower();
            if (s == "t" || s == "1" || s == "s" || s == "y" || s == "o") return true;
            return false;
        }

        public static bool ToBool(this int @this)
        {
            return @this.ToString().ToBool();
        }

        public static char ToChar(this string @this)
        {
            return @this.ToCharArray()[0];
        }

        public static string RemoveMultipleSpaces(this string @this)
        {
            return Regex.Replace(@this, @"\s+", " ");
        }

        /// <summary>
        /// https://www.dotnetperls.com/between-before-after
        /// </summary>
        /// <param name="value"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string SubstringBetween(this string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        /// <summary>
        /// Acronym from string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/4000304/get-an-acronym-from-a-string-in-c-sharp-using-linq</remarks>
        public static string Acronym(this string @this)
        {
            return string.Join(string.Empty,
                @this.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])
            );
        }

        /// <summary>
        /// Open url in default browser
        /// </summary>
        /// <param name="url"></param>
        /// <remarks>
        /// - https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/<br/>
        /// - https://stackoverflow.com/questions/7888871/hyperlink-keeps-failing
        /// </remarks>
        /// <example>
        /// private void DescargarAdjunto_Click(object sender, RoutedEventArgs e)
        /// {
        ///     e.Handled = true;
        ///     var url = ((Hyperlink)e.OriginalSource).NavigateUri.OriginalString;
        ///     ValueTypesUtils.OpenBrowser(url);
        /// }
        /// </example>
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Quita los caracteres no numericos de un string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/19167669/keep-only-numeric-value-from-a-string</remarks>
        public static string? CleanStringOfNonDigits(this string? s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            StringBuilder sb = new StringBuilder(s);
            int j = 0, i = 0;
            while (i < sb.Length)
            {
                if (char.IsDigit(sb[i]))
                    sb[j++] = sb[i++];
                else
                    ++i;
            }
            sb.Length = j;
            return sb.ToString();
        }

        /// <summary>
        /// Recibe un string y verifica si tiene caracteres que no sean numeros
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasNonDigits(this string? s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            StringBuilder sb = new StringBuilder(s);
            for (var i = 0; i < sb.Length; i++){
                if (!char.IsDigit(sb[i]))
                    return false;
            }
            return true;
        }

        public static bool similarTo(this string name1, string name2, int len = 4)
        {
            string[] n1 = name1.Trim().RemoveMultipleSpaces().ToUpper().Split(" ");
            string[] n2 = name2.Trim().RemoveMultipleSpaces().ToUpper().Split(" ");

            foreach(string nn1 in n1)
            {
                foreach(string nn2 in n2)
                {
                    string n = nn1.Substring(0, len);
                    if(nn2.Contains(n)) 
                        return true;
                }
            }

            return false;
        }

    }
}
