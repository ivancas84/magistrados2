#nullable enable
using SqlOrganize;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using Utils;

namespace MagistradosApp.Data
{
    public class Data_codigo_departamento : SqlOrganize.Data
    {

        public Data_codigo_departamento ()
        {
            Initialize();
        }

        public Data_codigo_departamento(DataInitMode mode)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Null)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("codigo_departamento").Default("id").Get("id");
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
        protected string? _codigo = null;
        public string? codigo
        {
            get { return _codigo; }
            set { _codigo = value; NotifyPropertyChanged(); }
        }
        protected string? _departamento_judicial = null;
        public string? departamento_judicial
        {
            get { return _departamento_judicial; }
            set { _departamento_judicial = value; NotifyPropertyChanged(); }
        }
        protected string? _organo = null;
        public string? organo
        {
            get { return _organo; }
            set { _organo = value; NotifyPropertyChanged(); }
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

                case "codigo":
                    if (_codigo == null)
                        return "Debe completar valor.";
                    return "";

                case "departamento_judicial":
                    if (_departamento_judicial == null)
                        return "Debe completar valor.";
                    return "";

                case "organo":
                    if (_organo == null)
                        return "Debe completar valor.";
                    return "";

            }

            return "";
        }
    }
}
