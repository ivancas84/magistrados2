#nullable enable
using SqlOrganize;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using Utils;

namespace MagistradosApp.Data
{
    public class Data_log : SqlOrganize.Data
    {

        public Data_log ()
        {
            Initialize();
        }

        public Data_log(DataInitMode mode)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Null)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("log").Default("id").Get("id");
                    _created = (DateTime?)ContainerApp.db.Values("log").Default("created").Get("created");
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
        protected string? _type = null;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        protected string? _description = null;
        public string? description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(); }
        }
        protected string? _user = null;
        public string? user
        {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged(); }
        }
        protected DateTime? _created = null;
        public DateTime? created
        {
            get { return _created; }
            set { _created = value; NotifyPropertyChanged(); }
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

                case "type":
                    return "";

                case "description":
                    return "";

                case "user":
                    return "";

                case "created":
                    if (_created == null)
                        return "Debe completar valor.";
                    return "";

            }

            return "";
        }
    }
}
