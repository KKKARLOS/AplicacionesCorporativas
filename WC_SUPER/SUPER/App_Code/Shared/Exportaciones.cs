using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Data;
using System.Globalization;

namespace IB.SUPER.Shared
{
    public class Exportaciones
    {
        public Exportaciones()
        {

        }

        public static byte[] exportarExcel(List<Dictionary<string, string>> schema, List<Dictionary<string, string>> datos, string filename, string tituloExcel)
        {
            try
            {
                byte[] bytearr = null;  

                DataTable dtHead = new DataTable();
                dtHead.TableName = "HEAD-datos";
                DataColumn dtc = new DataColumn("clave");
                dtHead.Columns.Add(dtc);
                dtc = new DataColumn("valor");
                dtHead.Columns.Add(dtc);

                DataRow oRow = dtHead.NewRow();
                oRow[0] = "title";
                oRow[1] = tituloExcel;
                dtHead.Rows.InsertAt(oRow, 0);

                DataTable dtBody = new DataTable();
                dtBody.TableName = "BODY-datos";
                dtc = null;

                for (var i = 0; i < schema.Count; i++)
                {
                    dtc = new DataColumn(schema[i]["data-text"], Type.GetType("System." + schema[i]["data-type"]));                    
                    dtBody.Columns.Add(dtc);
                }

                DataRow oRowFila;

                for (var j = 0; j < datos.Count; j++)
                {
                    oRowFila = dtBody.NewRow();

                    for (var k = 0; k < datos[j].Count; k++)
                    {
                        if (datos[j].Values.ElementAt(k) != null)
                        {
                            switch (schema[k]["data-type"])
                            {
                                case "Double":
                                    oRowFila[k] = Math.Round(double.Parse(datos[j].Values.ElementAt(k), CultureInfo.InvariantCulture), 2);
                                    //oRowFila[k] = Math.Round(double.Parse(datos[j].Values.ElementAt(k), CultureInfo.InvariantCulture), 2).ToString("N");
                                    break;
                                case "DateTime":
                                    oRowFila[k] = new DateTime(Convert.ToInt64(datos[j].Values.ElementAt(k).Substring(6, 13)) * 10000 + 621355968000000000).ToLocalTime();
                                    break;
                                default:
                                    oRowFila[k] = datos[j].Values.ElementAt(k);
                                    break;
                            }
                        }                        
                    }

                    dtBody.Rows.Add(oRowFila);

                }

                //Si no hay filas devolver null, 
                if (dtBody.Rows.Count == 0) return bytearr;

                DataSet ds = new DataSet();
                ds.Tables.Add(dtHead);
                ds.Tables.Add(dtBody);                

                //Llamada al ibservioffice para obtener el excel
                svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
                try
                {
                    osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
                    return osvcEXCEL.getExcelFromDataSet(ds, filename);
                }
                catch (FaultException<svcEXCEL.IBOfficeException> cex)
                {
                    throw new Exception(cex.Detail.Message);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
                    {
                        if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                        else osvcEXCEL.Abort();
                    }
                }
            }
            catch (Exception ex)
            {

                LogError.LogearError("Error al exportar a excel", ex);
                throw new Exception(System.Uri.EscapeDataString("Error al exportar a excel"));
            }
        }
    }
}