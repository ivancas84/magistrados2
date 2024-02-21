#nullable enable
using SqlOrganize;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using Utils;

namespace MagistradosApp.Data
{
    public class Data_importe_afiliacion : SqlOrganize.Data
    {

        public Data_importe_afiliacion ()
        {
            Initialize();
        }

        public Data_importe_afiliacion(DataInitMode mode)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Null)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("importe_afiliacion").Default("id").Get("id");
                    _creado = (DateTime?)ContainerApp.db.Values("importe_afiliacion").Default("creado").Get("creado");
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
        protected DateTime? _creado = null;
        public DateTime? creado
        {
            get { return _creado; }
            set { _creado = value; NotifyPropertyChanged(); }
        }
        protected string? _afiliacion = null;
        public string? afiliacion
        {
            get { return _afiliacion; }
            set { _afiliacion = value; NotifyPropertyChanged(); }
        }
        protected decimal? _valor = null;
        public decimal? valor
        {
            get { return _valor; }
            set { _valor = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _periodo = null;
        public DateTime? periodo
        {
            get { return _periodo; }
            set { _periodo = value; NotifyPropertyChanged(); }
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

                case "creado":
                    if (_creado == null)
                        return "Debe completar valor.";
                    return "";

                case "afiliacion":
                    if (_afiliacion == null)
                        return "Debe completar valor.";
                    return "";

                case "valor":
                    if (_valor == null)
                        return "Debe completar valor.";
                    return "";

                case "periodo":
                    if (_periodo == null)
                        return "Debe completar valor.";
                    return "";

            }

            return "";
        }
    }
}
