using CommunityToolkit.WinUI.Notifications;
using MagistradosApp.Data;
using MagistradosApp.Views.CrearArchivoSueldos;
using Org.BouncyCastle.Asn1.Ocsp;
using SqlOrganize;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Utils;

namespace MagistradosApp.Views;

public partial class CrearArchivoSueldosPage : Page, INotifyPropertyChanged
{
    private ObservableCollection<Int32> periodoAnioOC = new();
    private ObservableCollection<Data_organo> organoOC = new();

    public CrearArchivoSueldosPage()
    {
        InitializeComponent();
        DataContext = this;

        tipoComboBox.SelectedValuePath = "Key";
        tipoComboBox.DisplayMemberPath = "Value";
        tipoComboBox.Items.Add(new KeyValuePair<string, string>("afiliacion", "Registro 40"));
        tipoComboBox.Items.Add(new KeyValuePair<string, string>("tramite_excepcional", "Registro 80"));

        organoComboBox.SelectedValuePath = "id";
        organoComboBox.DisplayMemberPath = "descripcion";
        organoComboBox.ItemsSource = organoOC;
        IEnumerable<Dictionary<string, object?>> data2 = ContainerApp.db.Query("organo").
            ColOfDictCache();
        organoOC.Clear();
        organoOC.AddRange(data2);


        formGroupBox.DataContext = new Data_CrearArchivoSueldos();
    }




    private void CrearButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var request = (Data_CrearArchivoSueldos)formGroupBox.DataContext;

            #region verificar si ya se encuentran procesados
            //no se realiza verificacion, se crea archivo de sueldos con todos los registros de estado "Creado".
            /*var cantidad = ContainerApp.db.Query("importe_"+request.tipo).
                Select("COUNT(*) as cantidad").
                Where("YEAR($periodo) = @0 AND MONTH($periodo) = @1 AND $" + request.tipo + "-organo = @2").
                Parameters(request.periodo_anio, request.periodo_mes, request.organo).
                Column<Int64>(0).ElementAt(0);

            if (cantidad > 0)
                new ToastContentBuilder()
                .AddText("Crear Archivo de Sueldos")
                .AddText("ERROR: Los datos ingresados, ya se encuentran procesados")
                .Show();*/
            #endregion

            #region obtener registros creados que seran incorporados al archivo de sueldos
            EntityQuery query = ContainerApp.db.Query(request.tipo).
                Where("$modificado IS NULL AND $estado = 'Creado' AND $motivo IN ('Alta', 'Baja', 'Modificación') AND $organo = @0").
                Parameters(request.organo);

            if (request.tipo.Equals("tramite_excepcional"))
                query.Where(" AND $desde IS NOT NULL AND $hasta IS NOT NULL");

            IEnumerable<Dictionary<string, object?>> registrosCreados = query.ColOfDictCache();

            if (registrosCreados.IsNullOrEmpty())
                throw new Exception("No existen registros creados para el órgano solicitado");
            #endregion

            #region definir variables y paths
            DateTime now = DateTime.Now;
            string organoDescripcion = (string)registrosCreados.ElementAt(0)["organo-descripcion"]!;
            string tipoShort = "40";
            string tipo = "Registro 40";

            if (request.tipo.Equals("tramite_excepcional")){
                tipoShort = "80";
                tipo = "Registro 80";
            }

            string fileName = ContainerApp.config.archivoSueldosPath + now.ToString("yyyy'-'MM'-'dd") + "_" + organoDescripcion.Acronym() + "_" + tipoShort + ".dat";
            string fileNameDetail = ContainerApp.config.archivoSueldosPath + now.ToString("yyyy'-'MM'-'dd") + "_" + organoDescripcion.Acronym() + "_" + tipoShort + "_detalle.txt";

            if (File.Exists(fileName))
                File.Delete(fileName);

            if (File.Exists(fileNameDetail))
                File.Delete(fileNameDetail);
            #endregion

            #region crear y escribir archivo
            
            using (StreamWriter sw = File.CreateText(fileName))
            {
                using (StreamWriter swd = File.CreateText(fileNameDetail))
                {
                    swd.WriteLine("COLEGIO DE MAGISTRADOS Y FUNCIONARIOS DEL PODER JUDICIAL DE LA PROVINCIA DE BUENOS AIRES");
                    swd.WriteLine(now.ToString("dd'/'MM'/'yyyy"));
                    swd.WriteLine(tipo);

                    EntityPersist persist = ContainerApp.db.Persist(request.tipo);
                    foreach(var reg in registrosCreados)
                    {

                        Dictionary<string, object?> updateData = new();
                        updateData["id"] = reg["id"];
                        updateData["enviado"] = DateTime.Now;
                        updateData["estado"] = "Enviado";
                        persist.Update(updateData);


                        string codigoDepartamento = (string)ContainerApp.db.Query("codigo_departamento").
                            Where("$departamento_judicial = @0 AND $organo = @1").
                            Parameters(reg["departamento_judicial"]!, reg["organo"]!).DictCache()!["codigo"]!;

                        string codigo = "";
                        string detalle = "";

                        if (request.tipo == "afiliacion")
                        {
                            Data_afiliacion_r afiliacionObj = reg.Obj<Data_afiliacion_r>();

                            codigo = "010"; //3 1..3 
                            codigo += afiliacionObj.persona__legajo; //6 4..9 legajo
                            codigo += "40"; //2 10..11
                            codigo += "00000000000"; //11 12..22
                            codigo += afiliacionObj.motivo.Equals("Alta") ? "3" : "4"; //1 23
                            codigo += "                                                     "; //53 24..76
                            codigo += afiliacionObj.codigo.Equals(162) ? "1" : " ";  //1 77
                            codigo += "                     "; //21 78..98
                            codigo += afiliacionObj.codigo.Equals(162) ? "1" : " "; //1 99
                            
                            detalle = afiliacionObj.persona__legajo;
                            detalle += (afiliacionObj.persona__apellidos + ", " + afiliacionObj.persona__nombres).PadRight(28, ' ').Substring(0,28) + " ";
                            detalle += codigoDepartamento.PadRight(4, ' ').Substring(0, 4) + " ";
                            detalle += afiliacionObj.departamento_judicial__nombre.PadRight(15, ' ').Substring(0, 15) + " ";
                            detalle += afiliacionObj.motivo.PadRight(13, ' ').Substring(0, 13) + " ";
                            detalle += afiliacionObj.codigo.ToString().PadRight(4, ' ').Substring(0, 4) + " ";
                        }
                        else
                        {
                            Data_tramite_excepcional_r tramiteExcepcionalObj = reg.Obj<Data_tramite_excepcional_r>();

                            if (tramiteExcepcionalObj.organo == "1")
                            {

                                string motivo = "";

                                switch (tramiteExcepcionalObj.motivo)
                                {
                                    case "Alta":
                                        motivo = "0";
                                        break;

                                    case "Modificación":
                                        motivo = "3";
                                        break;

                                    case "Baja":
                                        motivo = "5";
                                        break;

                                    default:
                                        throw new Exception("Motivo incorrecto");

                                }

                                codigo = "010"; //3 1..3 empresa (ministerio publico 010)
                                codigo += tramiteExcepcionalObj.persona__legajo; //6 4..9 legajo
                                codigo += "80"; //2 10..11 registro
                                codigo += "163"; //3 12..14 concepto (moreno 156)
                                codigo += "2"; //1 15 subconcepto (para aj es 2, ¿para mp es 2?)
                                codigo += "00000"; //5 16..20 ceros
                                codigo += "  "; //2 21..22 orden/secuencia: 
                                /**
                                * numero que se utiliza para identificar 
                                * que un agente posee mas de un descuento
                                * para el mismo concepto, subconcepto, fecha de proceso y entidad
                                * ej 01, 02, etc
                                */

                                if (tramiteExcepcionalObj.organo.Equals("1"))
                                {
                                    codigo += motivo; //1 23 motivo
                                    codigo += ((DateTime)tramiteExcepcionalObj.desde!).Year.ToString().Substring(3, 1); //1 24 año desde
                                    codigo += ((DateTime)tramiteExcepcionalObj.desde).Month.ToString().PadLeft(2, '0'); //2 25..26 mes desde
                                    codigo += "0"; //1 27 completar con 0
                                    codigo += ((DateTime)tramiteExcepcionalObj.hasta!).Year.ToString().Substring(3, 1); //1 28 año hasta
                                    codigo += ((DateTime)tramiteExcepcionalObj.hasta).Month.ToString().PadLeft(2, '0'); //2 29..30 mes hasta
                                    codigo += "00"; //2 32..33 completar con 0
                                    codigo += ((decimal)tramiteExcepcionalObj.monto!).ToString("0000000000:0.00"); //10 34..43 monto a descontar
                                    codigo += "            "; //12 44..55 blancos
                                    codigo += tramiteExcepcionalObj.sucursal__descripcion!.PadRight(15); //16 56..71 descripcion
                                } else
                                {
                                    codigo += ((DateTime)tramiteExcepcionalObj.desde!).Year.ToString().Substring(2, 2); //2 23..24 año desde
                                    codigo += ((DateTime)tramiteExcepcionalObj.desde).Month.ToString().PadLeft(2, '0'); //2 25..26 mes desde
                                    codigo += motivo; //1 27 motivo (no estoy seguro si aca va el motivo)
                                    codigo += ((DateTime)tramiteExcepcionalObj.hasta!).Year.ToString().Substring(2, 2); //2 28..29 año hasta
                                    codigo += ((DateTime)tramiteExcepcionalObj.hasta).Month.ToString().PadLeft(2, '0'); //2 30..31 mes hasta
                                    codigo += "00"; //2 32..33 completar con 0
                                    codigo += ((decimal)tramiteExcepcionalObj.monto!).ToString("0000000000:0.00"); //10 34..43 monto a descontar
                                    codigo += "        "; //8 44..51 ceros 3. La descripcion dice 0 pero en los ejemplos esta en blanco
                                    codigo += " "; //1 52 automatico 
                                    codigo += "   "; //3 53..55 ceros 4. La descripcion dice 0 pero en los ejemplos esta en blanco
                                    codigo += tramiteExcepcionalObj.sucursal__descripcion!.PadRight(16); //16 56..71 descripcion
                                    codigo += "000"; //3 72..74 (puede ser ceros o blancos)

                                }

                                detalle = tramiteExcepcionalObj.persona__legajo;
                                detalle += (tramiteExcepcionalObj.persona__apellidos + ", " + tramiteExcepcionalObj.persona__nombres).PadRight(28, ' ').Substring(0, 28) + " ";
                                detalle += codigoDepartamento.PadRight(4, ' ').Substring(0, 4) + " ";
                                detalle += tramiteExcepcionalObj.departamento_judicial__nombre.PadRight(15, ' ').Substring(0, 15) + " ";
                                detalle += tramiteExcepcionalObj.motivo.PadRight(13, ' ').Substring(0, 13) + " ";
                                detalle += tramiteExcepcionalObj.codigo.ToString().PadRight(4, ' ').Substring(0, 4) + " ";
                                detalle += ((decimal)tramiteExcepcionalObj.monto).ToString("0000000000:0.00");
                            }
                        }
                        
                        sw.WriteLine(codigo);
                        swd.WriteLine(detalle);
                        persist.Transaction().RemoveCache();
                        new ToastContentBuilder()
                            .AddText("Crear Archivo de Sueldos")
                            .AddText("Se han creado los archivos: " + fileName + " y " + fileNameDetail)
                            .Show();
                    }
                }
            }
            #endregion


    



                /*causaOC.Clear();

                CausaDatos[] data = causaDAO.BuscarWS(request);
                if (data.IsNullOrEmptyOrDbNull())
                    new ToastContentBuilder()
                        .AddText("Búsqueda de Causas del WS")
                        .AddText("La consulta no arrojó resultados")
                        .Show();
                else
                    new ToastContentBuilder()
                        .AddText("Búsqueda de Causas del WS")
                        .AddText("La consulta devolvió " + data.Count() + " registros.")
                        .Show();

                causaOC.AddRange(data);*/
                            }
        catch (Exception ex)
        {
            new ToastContentBuilder()
                .AddText("Crear Archivo de Sueldos")
                .AddText("ERROR: " + ex.Message)
                .Show();
        }
    }


    private void DetalleCodigo(Data_afiliacion_r afiliacionObj)
    {


        
    }






    public event PropertyChangedEventHandler PropertyChanged;

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
