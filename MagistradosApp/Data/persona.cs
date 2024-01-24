#nullable enable
using SqlOrganize;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using Utils;

namespace MagistradosApp.Data
{
    public class Data_persona : SqlOrganize.Data
    {

        public Data_persona ()
        {
            Initialize();
        }

        public Data_persona(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("persona").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("persona").Default("creado").Get("creado");
                    _tipo_documento = (string?)ContainerApp.db.Values("persona").Default("tipo_documento").Get("tipo_documento");
                break;
            }
        }

        public string? Label { get; set; }

        protected string? _id = null;
        public string? id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(); }
        }
        protected string? _nombres = null;
        public string? nombres
        {
            get { return _nombres; }
            set { _nombres = value; NotifyPropertyChanged(); }
        }
        protected string? _apellidos = null;
        public string? apellidos
        {
            get { return _apellidos; }
            set { _apellidos = value; NotifyPropertyChanged(); }
        }
        protected string? _legajo = null;
        public string? legajo
        {
            get { return _legajo; }
            set { _legajo = value; NotifyPropertyChanged(); }
        }
        protected string? _numero_documento = null;
        public string? numero_documento
        {
            get { return _numero_documento; }
            set { _numero_documento = value; NotifyPropertyChanged(); }
        }
        protected string? _telefono_laboral = null;
        public string? telefono_laboral
        {
            get { return _telefono_laboral; }
            set { _telefono_laboral = value; NotifyPropertyChanged(); }
        }
        protected string? _telefono_particular = null;
        public string? telefono_particular
        {
            get { return _telefono_particular; }
            set { _telefono_particular = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _fecha_nacimiento = null;
        public DateTime? fecha_nacimiento
        {
            get { return _fecha_nacimiento; }
            set { _fecha_nacimiento = value; NotifyPropertyChanged(); }
        }
        protected string? _email = null;
        public string? email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged(); }
        }
        protected string? _tribunal = null;
        public string? tribunal
        {
            get { return _tribunal; }
            set { _tribunal = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _creado = null;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _eliminado = null;
        public DateTime? eliminado
        {
            get { return _eliminado; }
            set { _eliminado = value; NotifyPropertyChanged(); }
        }
        protected string? _cargo = null;
        public string? cargo
        {
            get { return _cargo; }
            set { _cargo = value; NotifyPropertyChanged(); }
        }
        protected string? _tipo_documento = null;
        public string? tipo_documento
        {
            get { return _tipo_documento; }
            set { _tipo_documento = value; NotifyPropertyChanged(); }
        }
        protected string? __Id = null;
        public string? _Id
        {
            get { return __Id; }
            set { __Id = value; NotifyPropertyChanged(); }
        }
        protected override string ValidateField(string columnName)
        {

            switch (columnName)
            {

                case "id":
                    if (_id == null)
                        return "Debe completar valor.";
                    return "";

                case "nombres":
                    return "";

                case "apellidos":
                    return "";

                case "legajo":
                    if (_legajo == null)
                        return "Debe completar valor.";
                    if (!_legajo.IsNullOrEmptyOrDbNull()) {
                        var row = ContainerApp.db.Query("persona").Where("$legajo = @0").Parameters(_legajo).DictCache();
                        if (!row.IsNullOrEmpty() && !__Id.ToString().Equals(row!["_Id"]!.ToString()))
                            return "Valor existente.";
                    }
                    return "";

                case "numero_documento":
                    if (!_numero_documento.IsNullOrEmptyOrDbNull()) {
                        var row = ContainerApp.db.Query("persona").Where("$numero_documento = @0").Parameters(_numero_documento).DictCache();
                        if (!row.IsNullOrEmpty() && !__Id.ToString().Equals(row!["_Id"]!.ToString()))
                            return "Valor existente.";
                    }
                    return "";

                case "telefono_laboral":
                    return "";

                case "telefono_particular":
                    return "";

                case "fecha_nacimiento":
                    return "";

                case "email":
                    return "";

                case "tribunal":
                    return "";

                case "creado":
                    if (_creado == null)
                        return "Debe completar valor.";
                    return "";

                case "eliminado":
                    return "";

                case "cargo":
                    return "";

                case "tipo_documento":
                    return "";

            }

            return "";
        }
    }
}
