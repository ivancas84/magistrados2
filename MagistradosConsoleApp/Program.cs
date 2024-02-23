using ModelOrganize;
using ModelOrganizeMy;
using System.Configuration;

var c = new Config()
{
    connectionString = ConfigurationManager.AppSettings.Get("connectionString")!,
    docPath = ConfigurationManager.AppSettings.Get("docPath")!,
    configPath = ConfigurationManager.AppSettings.Get("configPath")!,
    dbName = ConfigurationManager.AppSettings.Get("dbName")!,
    dataClassesPath = ConfigurationManager.AppSettings.Get("dataClassesPath")!,
    dataClassesNamespace = ConfigurationManager.AppSettings.Get("dataClassesNamespace")!,
    schemaClassPath = ConfigurationManager.AppSettings.Get("schemaClassPath")!,
    schemaClassNamespace = ConfigurationManager.AppSettings.Get("schemaClassNamespace")!,
    idSource = "field_name",
    id = "id",
};

BuildModelMy t = new(c);
foreach (var (key, field) in t.fields)
{
    foreach (var (k, f) in field)
    {
        if (f.name == "id")
        {
            f.defaultValue = "guid";
        }
    }

}

t.CreateFileEntitites();

t.CreateFileFields();

t.CreateFileData();

t.CreateClassModel();