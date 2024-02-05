using CommunityToolkit.WinUI.Notifications;
using MagistradosApp.Data;
using MagistradosApp.Views.ProcesarArchivoSueldos;
using SqlOrganize;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using Utils;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MagistradosApp.Views;

public partial class ProcesarArchivoSueldosPage : Page, INotifyPropertyChanged
{
    #region Atributos para formulario
    private ObservableCollection<int> periodoAnioOC = new();
    private ObservableCollection<Data_organo> organoOC = new();
    private Data_Formulario dataForm = new();
    #endregion

    #region Atributos de procesamiento
    private Dictionary<string, Dictionary<string, Data_Registro>> archivo; //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
    private DateTime periodo;
    private Dictionary<string, List<string>> legajosProcesados; //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
    private Dictionary<string, Dictionary<string, List<Data_RegistroDb>>> respuesta; //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
    IDictionary<string, Data_codigo_departamento> codigosDepartamento; //Lista de departamentos clasificados por codigo
    List<string> errors; //errores en el procesamiento 
    EntityPersist persist;
    Data_Longitud longitud;

    #endregion

    public ProcesarArchivoSueldosPage()
    {
        InitializeComponent();
        DataContext = this;

        formGroupBox.DataContext = dataForm;

        #region Inicializar periodoAnioComboBox
        periodoAnioComboBox.ItemsSource = periodoAnioOC;
        IEnumerable<int> data = ContainerApp.db.Query("afiliacion").
            Select("YEAR($enviado) AS enviado_anio").
            Where("$enviado IS NOT NULL").
            Order("$enviado DESC").
            Column<int>(0);

        int anioAdicional = data.ElementAt(0) + 1;

        periodoAnioOC.Clear();
        periodoAnioOC.AddRange(data);
        periodoAnioOC.Insert(0, anioAdicional);
        #endregion

        #region Inicializar periodoMesComboBox
        periodoMesComboBox.SelectedValuePath = "Key";
        periodoMesComboBox.DisplayMemberPath = "Value";
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(1, "1"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(2, "2"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(3, "3"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(4, "4"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(5, "5"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(6, "6"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(7, "7"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(8, "8"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(9, "9"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(10, "10"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(11, "11"));
        periodoMesComboBox.Items.Add(new KeyValuePair<int, string>(12, "12"));
        #endregion

        #region Inicializar organoComboBox
        organoComboBox.SelectedValuePath = "id";
        organoComboBox.DisplayMemberPath = "descripcion";
        organoComboBox.ItemsSource = organoOC;
        IEnumerable<Dictionary<string, object?>> data2 = ContainerApp.db.Query("organo").Order("$descripcion ASC").
            ColOfDictCache();
        organoOC.Clear();
        organoOC.AddRange(data2);
        #endregion
    }

    private void BuscarArchivoButton_Click(object sender, RoutedEventArgs e)
    {
        // Create OpenFileDialog
        Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

        // Launch OpenFileDialog by calling ShowDialog method
        Nullable<bool> result = openFileDlg.ShowDialog();
        // Get the selected file name and display in a TextBox.
        // Load content of file in a TextBlock
        if (result == true)
        {
            archivoTextBox.Text = openFileDlg.FileName;
            //TextBlock1.Text = 
        }
    }

    private void ProcesarButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            InicializarAtributosProcesamiento();

            #region Analizar si ya se encuentra el periodo procesado (se verifican si existen importes de afiliaciones o de tramites exepcionales)
            AnalizarPeriodoProcesado("afiliacion");
            AnalizarPeriodoProcesado("tramite_excepcional");
            #endregion

            #region Definir registros de archivo
            string fileContent = System.IO.File.ReadAllText(archivoTextBox.Text);

            string[] lines = fileContent.Split("\r\n"); //separar archivo en lineas

            for (var i = 0; i < lines.Length; i++)
            {
                #region Registros archivo > analizar longitudes de la linea
                lines[i] = WebUtility.HtmlDecode(lines[i]).Trim(); //decodificar para procesar caracteres especiales del archivo (eñes, acentos y otrs=

                if (lines[i].Length < longitud.longitudFilaMinima) 
                    continue; //ignorar si es inferior a minimo

                if (lines[i].Length > longitud.longitudFilaMaxima) //guardar error si es superior a máximo
                {
                    errors.Add(string.Format("La longitud de la fila {0} supera el máximo permitido", (i + 1)));
                    continue;
                }
                #endregion

                #region Registros archivo > definir datos iniciales de registro a procesar
                Data_Registro registro = new();
                registro.codigo_departamento = lines[i].Substring(longitud.inicioCodigoDepartamento, longitud.longitudCodigoDepartamento).Trim().PadLeft(longitud.longitudCodigoDepartamento, '0');
                registro.codigo_afiliacion = lines[i].Substring(longitud.inicioCodigoAfiliacion, longitud.longitudCodigoAfiliacion).Trim();
                registro.descripcion_afiliacion = lines[i].Substring(longitud.inicioDescripcionAfiliacion, longitud.longitudDescripcionAfiliacion).Trim();
                registro.legajo = lines[i].Substring(longitud.inicioLegajo, longitud.longitudLegajo).Trim();
                registro.monto = decimal.Parse(lines[i].Substring(longitud.inicioMonto, longitud.longitudMonto), CultureInfo.InvariantCulture);
                //registro.numero = lines[i].Substring(longitud.inicioNumero, longitud.longitudNumero).Trim();
                #endregion

                #region Registros archivo > definir nombres y apellidos de registro
                string[] nombre = lines[i].Substring(longitud.inicioNombre, longitud.longitudNombre).Trim().Split(",");
                registro.apellidos = nombre[0];

                if (nombre.Length > 1 && !nombre[1].Trim().IsNullOrEmpty())
                    registro.nombres = nombre[1];

                if(!registro.apellidos.IsNullOrEmpty())
                    for (int j = 0; j < registro.apellidos.Length; j++)
                        if (!Char.IsLetter(registro.apellidos, j) && !registro.apellidos[j].Equals(' '))
                            registro.apellidos = registro.apellidos.Remove(j, 1).Insert(j, "Ñ");

                if (!registro.nombres.IsNullOrEmpty())
                    for (int j = 0; j < registro.nombres.Length; j++)
                        if (!Char.IsLetter(registro.nombres, j) && !registro.nombres[j].Equals(' '))
                            registro.nombres = registro.nombres.Remove(j, 1).Insert(j, "Ñ");
                #endregion

                #region Registros archivo > Verificar codigo de departamento de registro
                if (!codigosDepartamento.ContainsKey(registro.codigo_departamento))
                {
                    errors.Add(string.Format("REGISTRO {0} SIN PROCESAR: No existe departamento judicial para legajo {1}", i, registro.legajo));
                    continue;
                }
                #endregion

                #region Registros archivo > Analizar codigo de afiliacion
                string indiceRegistro = registro.legajo + "~" + registro.codigo_afiliacion;

                switch (registro.codigo_afiliacion)
                {
                    
                    case "161": case "162": case "1621": case "1622":
                        VerificarLegajoRepetido("afiliacion", registro);
                        if(SumarImportesOAgregarRegistro("afiliacion", indiceRegistro, registro))
                            continue;
                        
                        break;

                    case "1631": case "1632":
                        #region Registros archivo > Analizar codigo de tramite excepcional > Verificar si existe afiliacion para el tramite excepcional
                        if (!legajosProcesados["afiliacion"].Contains(registro.legajo))
                            errors.Add("Legajo " + registro.legajo + " posee registro 80 y no registro 40.");
                        #endregion

                        VerificarLegajoRepetido("tramite_excepcional", registro);
                        SumarImportesOAgregarRegistro("tramite_excepcional", indiceRegistro, registro);

                        break;

                    default: 
                        errors.Add("Registro " + indiceRegistro + " tiene codigo de registro invalido, será ignorado: " + registro.codigo_afiliacion + ".");
                        continue;

                }
                #endregion

                if (archivo["afiliacion"].Count() == 0 && archivo["tramite_excepcional"].Count() == 0)
                    throw new Exception("No existen registros para procesar");

            }
            #endregion

            #region Procesar registros de archivo
            ProcesarRegistrosExistentes("afiliacion");
            ProcesarRegistrosExistentes("tramite_excepcional");
            #endregion

            #region Ejecutar SQL
            MySqlConnection connection = new MySqlConnection(ContainerApp.config.connectionString);
            connection.Open();
            using DbTransaction tran = connection.BeginTransaction();
            try
            {
                string[] sqls = persist.sql.Split(";");
                foreach (string s in sqls)
                {
                    if (s.Trim().IsNullOrEmpty())
                        continue;
                    var qu = ContainerApp.db.Query();
                    qu.connection = connection;
                    qu.sql = s;
                    qu.parameters = persist.parameters;
                    qu.Exec();
                }
                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
            #endregion


        }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Procesar Archivo de Sueldos")
                .AddText("ERROR: " + ex.Message)
            .Show();
        }

        
    }
    private void InicializarAtributosProcesamiento()
    {
        longitud = new(dataForm.organo);

        archivo = new() { //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
            { "afiliacion", new() }, //afiliaciones obtenidas del archivo
            { "tramite_excepcional", new() } //tramites obtenidos del archivo
        };

    
        legajosProcesados = new() { //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
            { "afiliacion", new() }, //afiliaciones obtenidas del archivo
            { "tramite_excepcional", new() }, //tramites obtenidos del archivo
        };

        respuesta = new() { //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
            { "afiliacion", new()
                {
                    { "altas_existentes", new() },
                    { "bajas_automaticas", new() }
                }
            },
            { "tramite_excepcional", new()
                {
                    { "altas_existentes", new() },
                    { "bajas_automaticas", new() }
                }
            },
        };
            
        errors = new(); //errores en el procesamiento 

        persist = ContainerApp.db.Persist();

        periodo = new DateTime(dataForm.periodo_anio, dataForm.periodo_mes, DateTime.Now.Day);

        #region Obtener codigos de departamentos judiciales del organo para definir registros de archivo
        codigosDepartamento = ContainerApp.db.Query("codigo_departamento").
            Where("$organo = @0").
            Parameters(dataForm.organo).
            ColOfDictCache().
            ColOfObj<Data_codigo_departamento>().
            DictOfObjByPropertyNames("codigo");
        #endregion
    }

    private void AnalizarPeriodoProcesado(string tipo)
    {
        Int64 cantidad = ContainerApp.db.Query("importe_" + tipo).
            Select("COUNT(*)").
            Where("YEAR($periodo) = @0 AND MONTH($periodo) = @1 AND $" + tipo + "-organo = @2").
            Parameters(periodo.Year, periodo.Month, dataForm.organo).
            Value<Int64>(0);

        if (cantidad > 0)
            throw new Exception("Existen importes de " + tipo + " para el período ingresado");

    }

    /// <summary>
    /// Verificar si el legajo del registro ya se encuentra procesado
    /// </summary>
    /// <param name="tipo">Tipo de registro</param>
    /// <param name="registro">Registro a analizar</param>
    private void VerificarLegajoRepetido(string tipo, Data_Registro registro)
    {
        if (legajosProcesados[tipo].Contains(registro.legajo))
            errors.Add("Legajo " + registro.legajo + " posee mas de un registro de " + tipo);
        else
            legajosProcesados[tipo].Add(registro.legajo);

    }

    /// <summary>
    /// Sumar importes si el indiceRegistro ya existe en el archivo, agregar nuevo registro si no existe en el archivo
    /// </summary>
    /// <param name="tipo">tipo de registro que se encuentra procesando</param>
    /// <param name="indiceRegistro">indice de registro</param>
    /// <param name="registro">registro a procesar</param>
    /// <returns>Existencia (true existe, false no existe)</returns>
    private bool SumarImportesOAgregarRegistro(string tipo, string indiceRegistro, Data_Registro registro)
    {
        if (archivo["afiliacion"].ContainsKey(indiceRegistro))
        {
            errors.Add("Registro repetido " + indiceRegistro + ": Se sumarán los importes.");
            archivo["afiliacion"][indiceRegistro].monto += registro.monto;
            return true;
        }

        archivo["afiliacion"][indiceRegistro] = registro;
        return false;

    }
    
    /// <summary>
    /// 
    /// </summary>
    private void ProcesarRegistrosExistentes(string tipo)
    {
        #region Consultar registros existentes
        EntityQuery q = ContainerApp.db.Query(tipo).
            Where(@"$modificado IS NULL 
                AND $estado = 'Aprobado' 
                AND $motivo = 'Alta' 
                AND $organo = @0"
            ).
            Size(0).
            Parameters(dataForm.organo);
           

        if(tipo == "tramite_excepcional")
        {
            q.Where(@" AND (EXTRACT(YEAR_MONTH FROM $desde) <= @1
                AND EXTRACT(YEAR_MONTH FROM $hasta) >= @1) 
                OR ($desde IS NULL AND $hasta IS NULL) ").
                Parameters(int.Parse(periodo.ToString("yyyyMM")));
        }

        IDictionary<string, Data_RegistroDb> registrosExistentes = q.ColOfDictCache().ColOfObj<Data_RegistroDb>().DictOfObjByPropertyNames("persona__legajo", "codigo");
        #endregion

        #region Recorrer registros para definir
        foreach (var (identifierRegistro, registroExistenteData) in registrosExistentes)
        {
            if (archivo[tipo].ContainsKey(identifierRegistro)) //el registro existente se encuentra en el archivo
            {
                respuesta[tipo]["altas_existentes"].Add(registroExistenteData);
                var registroArchivo = archivo[tipo][identifierRegistro];

                string departamentoJudicialArchivo = codigosDepartamento[registroArchivo.codigo_departamento].departamento_judicial!;
                
                #region Actualizar departamento judicial si es distinto (se actualiza el valor actual, deberia definirse uno nuevo!!!)
                if (!departamentoJudicialArchivo.Equals(registroExistenteData.departamento_judicial_informado)) { 
                    persist.UpdateValueIds(
                        tipo, 
                        "departamento_judicial_informado",
                        departamentoJudicialArchivo, 
                        registroExistenteData.id!
                    );

                    errors.Add("Se actualizo el departamento judicial informado del registro " + identifierRegistro);
                }
                #endregion

                #region Verificar si coincide monto informado con el de archivo para registro 80
                if (tipo == "tramite_excepcional" && registroExistenteData.monto! != registroArchivo.monto)
                    errors.Add("El monto del archivo no coincide con el monto informado: " + identifierRegistro);
                #endregion

                #region insertar importe del registro
                EntityValues importe = ContainerApp.db.Values("importe_" + tipo).
                    Set(tipo, registroExistenteData.id).
                    Set("valor", registroArchivo.monto).
                    Set("periodo", periodo).
                    Default();

                persist.Insert(importe);
                #endregion
            }
            else //el registro existente NO se encuentra en el archivo
            {
                respuesta[tipo]["bajas_automaticas"].Add(registroExistenteData);


            }


        }
        #endregion
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
