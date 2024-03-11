using CommunityToolkit.WinUI.Notifications;
using Google.Protobuf.WellKnownTypes;
using MagistradosApp.Data;
using MagistradosApp.Views.ProcesarArchivoSueldos;
using MySql.Data.MySqlClient;
using SqlOrganize;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Utils;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

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
    private DateTime evaluado = DateTime.Now;

    private Dictionary<string, List<string>> legajosProcesados; //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
    private Dictionary<string, Dictionary<string, ObservableCollection<Data_Registro>>> respuesta; //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
    IDictionary<string, Data_codigo_departamento> codigosDepartamento; //Lista de departamentos clasificados por codigo
    List<string> errors; //errores en el procesamiento 
    List<EntityPersist> persists = new();
    Data_Longitud longitud;
    IDictionary<string, Data_persona_r> personasDeRegistrosRestantes;
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

        #region Inicializar atributos de respuesta que se visualizaran en el XAML
        respuesta = new() { //debido a la similitud de procesamiento entre afiliaciones y tramites excepcionales, se almacenan los datos en una misma variable
            { "afiliacion", new()
                {
                    { "altas_existentes", new() },
                    { "bajas_automaticas", new() },
                    { "altas_aprobadas", new() },
                    { "altas_rechazadas", new() },
                    { "bajas_aprobadas", new() },
                    { "bajas_rechazadas", new() },
                    { "altas_automaticas", new() }

                }
            },
            { "tramite_excepcional", new()
                {
                    { "altas_existentes", new() },
                    { "bajas_automaticas", new() },
                    { "altas_aprobadas", new() },
                    { "altas_rechazadas", new() },
                    { "bajas_aprobadas", new() },
                    { "bajas_rechazadas", new() },
                    { "altas_automaticas", new() }
                }
            },
        };

        altasExistentes40DataGrid.ItemsSource = respuesta["afiliacion"]["altas_existentes"];
        altasAprobadas40DataGrid.ItemsSource = respuesta["afiliacion"]["altas_aprobadas"];
        altasRechazadas40DataGrid.ItemsSource = respuesta["afiliacion"]["altas_rechazadas"];
        altasAutomaticas40DataGrid.ItemsSource = respuesta["afiliacion"]["altas_automaticas"];
        bajasAprobadas40DataGrid.ItemsSource = respuesta["afiliacion"]["bajas_aprobadas"];
        bajasRechazadas40DataGrid.ItemsSource = respuesta["afiliacion"]["bajas_rechazadas"];
        bajasAutomaticas40DataGrid.ItemsSource = respuesta["afiliacion"]["bajas_automaticas"];

        altasExistentes80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["altas_existentes"];
        altasAprobadas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["altas_aprobadas"];
        altasRechazadas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["altas_rechazadas"];
        altasAutomaticas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["altas_automaticas"];
        bajasAprobadas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["bajas_aprobadas"];
        bajasRechazadas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["bajas_rechazadas"];
        bajasAutomaticas80DataGrid.ItemsSource = respuesta["tramite_excepcional"]["bajas_automaticas"];
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
                registro.codigo = Convert.ToInt32(lines[i].Substring(longitud.inicioCodigoAfiliacion, longitud.longitudCodigoAfiliacion).Trim());
                registro.descripcion = lines[i].Substring(longitud.inicioDescripcionAfiliacion, longitud.longitudDescripcionAfiliacion).Trim();
                registro.persona__legajo = lines[i].Substring(longitud.inicioLegajo, longitud.longitudLegajo).Trim();
                registro.monto = decimal.Parse(lines[i].Substring(longitud.inicioMonto, longitud.longitudMonto), CultureInfo.InvariantCulture);
                //registro.numero = lines[i].Substring(longitud.inicioNumero, longitud.longitudNumero).Trim();
                #endregion

                #region Registros archivo > definir nombres y apellidos de registro
                string[] nombre = lines[i].Substring(longitud.inicioNombre, longitud.longitudNombre).Trim().Split(",");
                registro.persona__apellidos = nombre[0];

                if (nombre.Length > 1 && !nombre[1].Trim().IsNullOrEmpty())
                    registro.persona__nombres = nombre[1];

                if(!registro.persona__apellidos.IsNullOrEmpty())
                    for (int j = 0; j < registro.persona__apellidos.Length; j++)
                        if (!Char.IsLetter(registro.persona__apellidos, j) && !registro.persona__apellidos[j].Equals(' '))
                            registro.persona__apellidos = registro.persona__apellidos.Remove(j, 1).Insert(j, "Ñ");

                if (!registro.persona__nombres.IsNullOrEmpty())
                    for (int j = 0; j < registro.persona__nombres.Length; j++)
                        if (!Char.IsLetter(registro.persona__nombres, j) && !registro.persona__nombres[j].Equals(' '))
                            registro.persona__nombres = registro.persona__nombres.Remove(j, 1).Insert(j, "Ñ");
                #endregion

                #region Registros archivo > Verificar codigo de departamento de registro
                if (!codigosDepartamento.ContainsKey(registro.codigo_departamento))
                {
                    errors.Add(string.Format("REGISTRO {0} SIN PROCESAR: No existe departamento judicial para legajo {1}", i, registro.persona__legajo));
                    continue;
                }
                registro.departamento_judicial = codigosDepartamento[registro.codigo_departamento].departamento_judicial;
                registro.departamento_judicial_informado = codigosDepartamento[registro.codigo_departamento].departamento_judicial;
                #endregion

                #region Registros archivo > Analizar codigo de afiliacion
                string indiceRegistro = registro.persona__legajo + "~" + registro.codigo;

                switch (registro.codigo)
                {
                    
                    case 161: case 162: case 1621: case 1622: //afiliacion
                        VerificarLegajoRepetido("afiliacion", registro);
                        if(SumarImportesOAgregarRegistro("afiliacion", indiceRegistro, registro))
                            continue;
                        
                        break;

                    case 1631: case 1632: //tramite excepcional
                        #region Registros archivo > Analizar codigo de tramite excepcional > Verificar si existe afiliacion para el tramite excepcional
                        if (!legajosProcesados["afiliacion"].Contains(registro.persona__legajo))
                            errors.Add("Legajo " + registro.persona__legajo + " posee registro 80 y no registro 40.");
                        #endregion

                        VerificarLegajoRepetido("tramite_excepcional", registro);
                        SumarImportesOAgregarRegistro("tramite_excepcional", indiceRegistro, registro);

                        break;

                    default: 
                        errors.Add("Registro " + indiceRegistro + " tiene codigo de registro invalido, será ignorado: " + registro.codigo + ".");
                        continue;

                }
                #endregion

                if (archivo["afiliacion"].Count() == 0 && archivo["tramite_excepcional"].Count() == 0)
                    throw new Exception("No existen registros para procesar");

            }
            #endregion

            #region Procesar registros existentes, altas enviadas y bajas enviadas
            ProcesarRegistrosExistentes("afiliacion");
            ProcesarRegistrosExistentes("tramite_excepcional");

            ProcesarRegistrosAltasEnviadas("afiliacion");
            ProcesarRegistrosAltasEnviadas("tramite_excepcional");

            ProcesarRegistrosBajasEnviadas("afiliacion");
            ProcesarRegistrosBajasEnviadas("tramite_excepcional");
            #endregion

            #region Procesar registros restantes en el archivo
            ConsultarPersonasDeRegistrosRestantes();
            ProcesarRegistrosRestantes("afiliacion");
            ProcesarRegistrosRestantes("tramite_excepcional");
            #endregion

            

        }
        catch (Exception ex)
        {
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var lineNumber = frame.GetFileLineNumber();

            string fileName = frame.GetFileName();
            int columnNumber = frame.GetFileColumnNumber();
            var method = frame.GetMethod();
            var type = frame.GetMethod().DeclaringType;

            new ToastContentBuilder()
                .AddText(this.Title)
                .AddText("ERROR " + fileName +"-"+lineNumber.ToString()   + ": " + ex.Message)
            .Show();
        }

        
    }

    private void Commit()
    {
        var connection = new MySqlConnection(ContainerApp.config.connectionString);
        connection.Open();
        using DbTransaction tran = connection!.BeginTransaction();
        string sql = "";
        try
        {
            foreach (EntityPersist p in persists)
            {
                p.SetConn(connection!);
                p.Exec();
            }

            tran.Commit();
            ContainerApp.db.Cache.Clear();
        }

        catch (Exception ex)
        {
            tran.Rollback();
            throw new Exception(ex.Message + " - " + sql);
        }
        connection.Close();
    }

    private void ConsultarPersonasDeRegistrosRestantes()
    {
        if (archivo["afiliacion"].IsNullOrEmptyOrDbNull() && archivo["tramite_excepcional"].IsNullOrEmptyOrDbNull())
            return;

        List<object> legajos = new();

        foreach(var (identifierRegistro, registro) in archivo["afiliacion"])
            legajos.Add(registro.persona__legajo);

        foreach (var (identifierRegistro, registro) in archivo["tramite_excepcional"])
            legajos.Add(registro.persona__legajo);


        personasDeRegistrosRestantes = ContainerApp.db.Query("persona").
            Where("$legajo IN ( @0 )").
            Parameters(legajos).
            Size(0).
            ColOfObj<Data_persona_r>().
            DictOfObjByPropertyNames("legajo");
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

            
        errors = new(); //errores en el procesamiento 

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
        if (legajosProcesados[tipo].Contains(registro.persona__legajo))
            errors.Add("Legajo " + registro.persona__legajo + " posee mas de un registro de " + tipo);
        else
            legajosProcesados[tipo].Add(registro.persona__legajo);

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
    /// Procesar registros existentes
    /// </summary>
    private void ProcesarRegistrosExistentes(string tipo)
    {
        IDictionary<string, Data_Registro> registrosExistentes = ConsultarRegistrosDb(tipo, "Aprobado", "Alta", "Modificacion");

        #region Recorrer registros existentes para analizar si es alta_existente o baja_automatica
        foreach (var (identifierRegistro, registroExistenteData) in registrosExistentes)
        {
            if (archivo[tipo].ContainsKey(identifierRegistro)) //el registro existente se encuentra en el archivo
            {
                #region Inicializar variables para procesar altas existentes
                respuesta[tipo]["altas_existentes"].Add(registroExistenteData);
                var registroArchivo = archivo[tipo][identifierRegistro];
                Data_persona personaDelRegistro = ContainerApp.db.Values("persona", "persona").SetObj(registroExistenteData).Values().Obj<Data_persona>();
                #endregion

                VerificarNombreSiEsDistinto(tipo, identifierRegistro, personaDelRegistro);
                
                ActualizarDepartamentoJudicialInformadoSiEsDistinto(tipo, identifierRegistro, registroExistenteData);

                VerificarCoincidenciaMontoTramiteExcepcional(tipo, identifierRegistro, registroExistenteData.monto);

                #region insertar importe del registro
                EntityValues importe = ContainerApp.db.Values("importe_" + tipo).
                    Set(tipo, registroExistenteData.id).
                    Set("valor", registroArchivo.monto).
                    Set("periodo", periodo).
                    Default();

                EntityPersist p = ContainerApp.db.Persist().Insert(importe);
                persists.Add(p);
                #endregion

                #region Borrar registro del archivo porque ya fue procesado
                archivo[tipo].Remove(identifierRegistro);
                #endregion
            }
            else //el registro existente NO se encuentra en el archivo
            {
                #region Inicializar variables para procesar bajas automaticas
                respuesta[tipo]["bajas_automaticas"].Add(registroExistenteData);
                #endregion

                ModificarRegistrosExistentes(tipo, registroExistenteData.persona!, (int)registroExistenteData.codigo!);

                #region Insertar registro de baja automatica
                EntityValues registroAInsertar = ContainerApp.db.Values(tipo).
                    Set("persona", registroExistenteData.persona!).
                    Set("creado", evaluado).
                    Set("evaluado", evaluado).
                    Set("motivo", "Baja").
                    Set("estado", "Aprobado").
                    Set("codigo", (int)registroExistenteData.codigo!).
                    Set("departamento_judicial", registroExistenteData.departamento_judicial!).
                    Set("departamento_judicial_informado", registroExistenteData.departamento_judicial_informado!).
                    Set("organo", registroExistenteData.organo!).
                    Set("monto", registroExistenteData.monto).
                    Set("observaciones", "Baja automática").
                    Default().Reset();

                var p = ContainerApp.db.Persist().Insert(registroAInsertar);
                persists.Add(p);
                #endregion
            }
        }
        #endregion
    }

    private void ProcesarRegistrosAltasEnviadas(string tipo)
    {
        IDictionary<string, Data_Registro> registrosAltasEnviadas = ConsultarRegistrosDb(tipo, "Enviado", "Alta", "Modificacion");

        foreach (var (identifierRegistro, registroAltaEnviadaData) in registrosAltasEnviadas)
        {
            var p = ContainerApp.db.Persist();

            if (archivo[tipo].ContainsKey(identifierRegistro)) //el registro alta enviada se encuentra en el archivo
            {
                #region Inicializar variables para procesar altas existentes
                respuesta[tipo]["altas_aprobadas"].Add(registroAltaEnviadaData);
                var registroArchivo = archivo[tipo][identifierRegistro];
                Data_persona_r personaDelRegistro = ContainerApp.db.Values("persona", "persona").SetObj(registroAltaEnviadaData).Values().Obj<Data_persona_r>();
                #endregion region

                ActualizarDepartamentoJudicialInformadoSiEsDistinto(tipo, identifierRegistro, registroAltaEnviadaData);

                VerificarNombreSiEsDistinto(tipo, identifierRegistro, personaDelRegistro);

                VerificarCoincidenciaMontoTramiteExcepcional(tipo, identifierRegistro, (decimal?)registroAltaEnviadaData.monto);

                #region Aprobar alta enviada
                EntityValues registroValue = ContainerApp.db.Values(tipo).
                    Set("id", registroAltaEnviadaData.id).
                    Set("estado", "Aprobado").
                    Set("evaluado", evaluado);

                p.Update(registroValue);
                #endregion

                #region insertar importe del registro
                EntityValues importe = ContainerApp.db.Values("importe_" + tipo).
                    Set(tipo, registroAltaEnviadaData.id).
                    Set("valor", registroArchivo.monto).
                    Set("periodo", periodo).
                    Default();

                p.Insert(importe);
                #endregion

                #region Borrar registro del archivo porque ya fue procesado
                archivo[tipo].Remove(identifierRegistro);
                #endregion
            }
            else //el registro existente NO se encuentra en el archivo
            {
                #region Inicializar variables para procesar bajas automaticas
                respuesta[tipo]["altas_rechazadas"].Add(registroAltaEnviadaData);
                #endregion

                #region Rechazar alta enviada
                EntityValues registroValue = ContainerApp.db.Values(tipo).
                    Set("id", registroAltaEnviadaData.id).
                    Set("estado", "Rechazado").
                    Set("evaluado", evaluado);

                p.Update(registroValue);
                #endregion
            }

            persists.Add(p);

        }
    }

    /// <summary>
    /// El resultado del procesamiento genera bajas_aprobadas y bajas_rechazadas
    /// </summary>
    /// <param name="tipo"></param>
    private void ProcesarRegistrosBajasEnviadas(string tipo)
    {
        IDictionary<string, Data_Registro> registrosBajasEnviadas =  ConsultarRegistrosDb(tipo, "Enviado", "Baja");

        foreach (var (identifierRegistro, registroBajaEnviadaData) in registrosBajasEnviadas)
        {
            var persist = ContainerApp.db.Persist();

            if (archivo[tipo].ContainsKey(identifierRegistro)) //el registro baja enviada se encuentra en el archivo, lo que significa que no ha sido aprobado
            {
                #region Inicializar variables para procesar bajas rechazadas
                respuesta[tipo]["bajas_rechazadas"].Add(registroBajaEnviadaData);
                var registroArchivo = archivo[tipo][identifierRegistro];
                Data_persona personaDelRegistro = ContainerApp.db.Values("persona", "persona").SetObj(registroBajaEnviadaData).Values().Obj<Data_persona>();
                #endregion region

                ActualizarDepartamentoJudicialInformadoSiEsDistinto(tipo, identifierRegistro, registroBajaEnviadaData);

                VerificarNombreSiEsDistinto(tipo, identifierRegistro, personaDelRegistro);

                VerificarCoincidenciaMontoTramiteExcepcional(tipo, identifierRegistro, (decimal?)registroBajaEnviadaData.monto!);

                #region Rechazar baja enviada
                EntityValues registroValue = ContainerApp.db.Values(tipo).
                    Set("id", registroBajaEnviadaData.id).
                    Set("estado", "Rechazado").
                    Set("evaluado", evaluado);

                persist.Update(registroValue);
                #endregion

                ModificarRegistrosExistentes(tipo, registroBajaEnviadaData.persona!, (int)registroBajaEnviadaData.codigo!);

                #region Insertar registro de nueva alta correspondiente a la baja rechazada
                EntityValues registroAInsertar = ContainerApp.db.Values(tipo).
                    Set("persona", registroBajaEnviadaData.persona!).
                    Set("creado", evaluado).
                    Set("evaluado", evaluado).
                    Set("motivo", "Alta").
                    Set("estado", "Aprobado").
                    Set("codigo", (int)registroBajaEnviadaData.codigo!).
                    Set("departamento_judicial", registroBajaEnviadaData.departamento_judicial!).
                    Set("departamento_judicial_informado", registroArchivo.departamento_judicial_informado!).
                    Set("organo", dataForm.organo!).
                    Set("monto", registroBajaEnviadaData.monto).
                    Set("observaciones", "Nueva alta por baja rechazada").
                    Default().Reset();

                persist.Insert(registroAInsertar);
                #endregion

                #region Insertar importe del registro
                EntityValues importe = ContainerApp.db.Values("importe_" + tipo).
                    Set(tipo, registroAInsertar.Get("id")).
                    Set("valor", registroArchivo.monto).
                    Set("periodo", periodo).
                    Default();

                persist.Insert(importe);
                #endregion

                #region Borrar registro del archivo porque ya fue procesado
                archivo[tipo].Remove(identifierRegistro);
                #endregion
            }
            else //la baja enviada NO se encuentra en el archivo, significa que fue aprobada
            {
                #region Inicializar variables para procesar bajas automaticas
                respuesta[tipo]["bajas_aprobadas"].Add(registroBajaEnviadaData);
                #endregion

                #region Aprobar baja enviada
                EntityValues registroValue = ContainerApp.db.Values(tipo).
                    Set("id", registroBajaEnviadaData.id).
                    Set("estado", "Aprobado").
                    Set("evaluado", evaluado);

                persist.Update(registroValue);
                #endregion
            }

            persists.Add(persist);
        }

    }

    /// <summary>
    /// Consultar registros de la base de datos
    /// </summary>
    /// <param name="tipo">afiliacion o tramite_excepcional</param>
    /// <param name="estado">Aprobado, Modificado, Rechazado, Enviado, Creado</param>
    /// <param name="motivo">Alta, Baja, Modificación</param>
    /// <returns></returns>
    protected IDictionary<string, Data_Registro> ConsultarRegistrosDb(string tipo, string estado, params string[] motivo)
    {
        return ContainerApp.db.Query(tipo).
            Where(@"$modificado IS NULL 
                AND $estado = @0
                AND $motivo IN (@1)
                AND $organo = @2"
            ).
            Size(0).
            Parameters(estado, motivo, dataForm.organo).
            ColOfDictCache().
            ColOfObj<Data_Registro>().
            DictOfObjByPropertyNames("persona__legajo", "codigo");
    }

    /// <summary>
    /// Actualizar departamento judicial informado si es distinto
    /// </summary>
    /// <remarks>Se actualiza el valor actual, deberia definirse uno nuevo!!!</remarks>
    protected void ActualizarDepartamentoJudicialInformadoSiEsDistinto(string tipo, string identifierRegistro, Data_Registro registro)
    {
        string departamentoJudicialArchivo = archivo[tipo][identifierRegistro].departamento_judicial_informado;
        

        if (!departamentoJudicialArchivo.Equals(registro.departamento_judicial_informado))
        {
            var persist = ContainerApp.db.Persist().UpdateValueIds(
                tipo,
                "departamento_judicial_informado",
                departamentoJudicialArchivo,
                registro.id!
            );
            persists.Add(persist);
            errors.Add("Se actualizo el departamento judicial informado del registro " + identifierRegistro);
        }
    }


    /// <summary>
    /// Procesar registros restantes
    /// </summary>
    private void ProcesarRegistrosRestantes(string tipo)
    {
        if (archivo[tipo].IsNullOrEmptyOrDbNull())
            return;

        foreach (var (identifierRegistro, registroArchivo) in archivo[tipo])
        {

            var persist = ContainerApp.db.Persist();

            #region Verificar existencia de persona
            string legajo = archivo[tipo][identifierRegistro].persona__legajo!;
            EntityValues personaVal = ContainerApp.db.Values("persona", "persona").Default().SetNotNull(registroArchivo.Dict());
            Data_persona personaDeArchivo = personaVal.Values().Obj<Data_persona>();

            if (personasDeRegistrosRestantes.ContainsKey(legajo)) //si existe persona se verifica nombre
            {
                personaDeArchivo.id = personasDeRegistrosRestantes[legajo].id;
                VerificarNombreSiEsDistinto(tipo, identifierRegistro, personaDeArchivo);
            }
            else //si no existe persona se crea
            {
                if (!personaVal.Reset().Check())
                {
                    errors.Add("Error al procesar persona de registro " + identifierRegistro + " " + personaVal.Logging.ToString());
                    continue;
                }
                persist.Insert(personaVal);
                personaDeArchivo.id = (string)personaVal.Get("id");
            }
            #endregion

            #region Insertar registro de baja automatica
            ModificarRegistrosExistentes(tipo, personaDeArchivo.id!, (int)registroArchivo.codigo!);

            EntityValues registroAInsertar = ContainerApp.db.Values(tipo).
                Set("persona", personaDeArchivo.id!).
                Set("creado", evaluado).
                Set("evaluado", evaluado).
                Set("motivo", "Alta").
                Set("estado", "Aprobado").
                Set("codigo", registroArchivo.codigo).
                Set("departamento_judicial", registroArchivo.departamento_judicial!).
                Set("departamento_judicial_informado", registroArchivo.departamento_judicial_informado!).
                Set("organo", dataForm.organo).
                Set("monto", registroArchivo.monto).
                Set("observaciones", "Alta automática").
                Default().Reset();

            persist.Insert(registroAInsertar);

            EntityValues importe = ContainerApp.db.Values("importe_" + tipo).
                    Set(tipo, registroAInsertar.Get("id")).
                    Set("valor", registroArchivo.monto).
                    Set("periodo", periodo).
                    Default().Reset();

            persist.Insert(importe);
            #endregion

            persists.Add(persist);
            respuesta[tipo]["altas_automaticas"].Add(registroArchivo);
        }
    }

    /// <summary>
    /// Marcar como modificados los registros existentes
    /// </summary>
    /// <param name="tipo"></param>
    /// <param name="idPersona"></param>
    /// <param name="motivo"></param>
    /// <param name="codigoRegistro"></param>
    /// <param name="idOrgano"></param>
    /// <param name="idDepartamentoJudicial"></param>
    /// <param name="idDepartamentoJudicialInformado"></param>
    /// <param name="monto"></param>
    protected void ModificarRegistrosExistentes(string tipo, string idPersona, int codigoRegistro)
    {
        IEnumerable<string> idRegistrosAModificar = ContainerApp.db.Query(tipo).
            Where("$persona = @0 AND $modificado IS NULL AND $codigo = @1 AND $organo = @2").
            Parameters(idPersona, codigoRegistro, dataForm.organo).Column<string>("id");

        if (!idRegistrosAModificar.IsNullOrEmpty()) { 
            var persist = ContainerApp.db.Persist().UpdateValueIds(tipo, "modificado", evaluado, idRegistrosAModificar.ToArray());
            persists.Add(persist);
        }
    }

    
    protected void VerificarNombreSiEsDistinto(string tipo, string identifierRegistro, Data_persona registro)
    {
        string nombre1 = "";
        if (!registro.nombres.IsNullOrEmptyOrDbNull()) nombre1 += registro.nombres + " ";
        nombre1 += registro.apellidos;
        string nombre2 = "";
        if (!archivo[tipo][identifierRegistro].persona__nombres.IsNullOrEmptyOrDbNull())
            nombre2 += archivo[tipo][identifierRegistro].persona__nombres + " ";
        nombre2 += archivo[tipo][identifierRegistro].persona__apellidos;
        if(!nombre1.similarTo(nombre2))
            errors.Add("Los nombres son diferentes en el registro " + identifierRegistro);
    }

    /// <summary>
    /// Verificar si coincide monto informado con el de archivo para registro 80
    /// </summary>
    /// <param name="tipo"></param>
    /// <param name="identifierRegistro"></param>
    /// <param name="montoArchivo"></param>
    /// <param name="montoRegistro"></param>
    protected void VerificarCoincidenciaMontoTramiteExcepcional(string tipo, string identifierRegistro, decimal? montoRegistro)
    {
        if (tipo.Equals("tramite_excepcional"))
            if(montoRegistro! != archivo[tipo][identifierRegistro].monto!)
                errors.Add("El monto del archivo no coincide con el monto informado: " + identifierRegistro);
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
