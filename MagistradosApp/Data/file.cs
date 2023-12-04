#nullable enable
using SqlOrganize;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using Utils;

namespace MagistradosApp.Data
{
    public class Data_file : SqlOrganize.Data
    {

        public Data_file ()
        {
            Initialize();
        }

        public Data_file(DataInitMode mode = DataInitMode.Default)
        {
            Initialize(mode);
        }

        protected virtual void Initialize(DataInitMode mode = DataInitMode.Default)
        {
            switch(mode)
            {
                case DataInitMode.Default:
                case DataInitMode.DefaultMain:
                    _id = (string?)ContainerApp.db.Values("file").Default("id").Get("id");
                    _created = (DateTime?)ContainerApp.db.Values("file").Default("created").Get("created");
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
        protected string? _name = null;
        public string? name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged(); }
        }
        protected string? _type = null;
        public string? type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged(); }
        }
        protected string? _content = null;
        public string? content
        {
            get { return _content; }
            set { _content = value; NotifyPropertyChanged(); }
        }
        protected uint? _size = null;
        public uint? size
        {
            get { return _size; }
            set { _size = value; NotifyPropertyChanged(); }
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

                case "name":
                    if (_name == null)
                        return "Debe completar valor.";
                    return "";

                case "type":
                    if (_type == null)
                        return "Debe completar valor.";
                    return "";

                case "content":
                    if (_content == null)
                        return "Debe completar valor.";
                    return "";

                case "size":
                    if (_size == null)
                        return "Debe completar valor.";
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
