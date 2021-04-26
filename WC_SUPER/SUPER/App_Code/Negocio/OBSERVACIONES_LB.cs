using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SUPER.DAL;

namespace SUPER.BLL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER.BLL
    /// Class	 : OBSERVACIONES_LB
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T693_OBSERVACIONES_LB
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	03/06/2014 9:09:53	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class OBSERVACIONES_LB
    {
        #region Propiedades y Atributos

        private int _t693_idobservacion;
        public int t693_idobservacion
        {
            get { return _t693_idobservacion; }
            set { _t693_idobservacion = value; }
        }

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private DateTime _t693_fecha;
        public DateTime t693_fecha
        {
            get { return _t693_fecha; }
            set { _t693_fecha = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private DateTime? _t693_fecha_modif;
        public DateTime? t693_fecha_modif
        {
            get { return _t693_fecha_modif; }
            set { _t693_fecha_modif = value; }
        }

        private int? _t001_idficepi_modif;
        public int? t001_idficepi_modif
        {
            get { return _t001_idficepi_modif; }
            set { _t001_idficepi_modif = value; }
        }

        private bool _t693_automatico;
        public bool t693_automatico
        {
            get { return _t693_automatico; }
            set { _t693_automatico = value; }
        }

        private string _t693_observaciones;
        public string t693_observaciones
        {
            get { return _t693_observaciones; }
            set { _t693_observaciones = value; }
        }
        #endregion

        #region Constructor

        public OBSERVACIONES_LB()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        public static void Insertar(int t305_idproyectosubnodo, int t001_idficepi, bool t693_automatico, string t693_observaciones)
        {
            SUPER.DAL.OBSERVACIONES_LB.Insert(null, t305_idproyectosubnodo, t001_idficepi, t693_automatico, t693_observaciones);
        }

        public static void Modificar(int t693_idobservacion, string t693_observaciones)
        {
            SUPER.DAL.OBSERVACIONES_LB.Update(null, t693_idobservacion, t693_observaciones);
        }

        public static void Eliminar(int t693_idobservacion)
        {
            SUPER.DAL.OBSERVACIONES_LB.Delete(null, t693_idobservacion);
        }

        public static string ObtenerCatalogo(int t305_idproyectosubnodo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                SqlDataReader dr = SUPER.DAL.OBSERVACIONES_LB.Catalogo(t305_idproyectosubnodo);
                string sImage = "", sFormato = "";
                while (dr.Read())
                {
                    if (dr["foto"] == DBNull.Value)
                        sImage = "../../../Images/imgSmile.gif";
                    else
                    {
                        Image image = null;
                        sFormato = "";
                        using (MemoryStream stream = new MemoryStream((byte[])dr["foto"]))
                        {
                            image = System.Drawing.Image.FromStream(stream);
                        }
                        if (ImageFormat.Jpeg.Equals(image.RawFormat))
                        {
                            sFormato = "jpeg";
                        }
                        else if (ImageFormat.Png.Equals(image.RawFormat))
                        {
                            sFormato = "png";
                        }
                        else if (ImageFormat.Gif.Equals(image.RawFormat))
                        {
                            sFormato = "gif";
                        }
                        else if (ImageFormat.Bmp.Equals(image.RawFormat))
                        {
                            sFormato = "bmp";
                        }
                        if (sFormato != "")
                            sImage = "data:image/" + sFormato + ";base64," + System.Convert.ToBase64String((byte[])dr["foto"]);
                        else
                            sImage = "../../../Images/imgSmile.gif";
                    }
                    
                    sb.Append(@"<li class='in' 
                                id='" + dr["t693_idobservacion"].ToString() + @"' 
                                idficepiautor = '" + dr["t001_idficepi"].ToString() + @"' 
                                automatico='" + (((bool)dr["t693_automatico"]) ? "1" : "0") + @"'>
			                    <span class='spanavatar'>
                                <img class='avatar' src=" + (char)34 + sImage + (char)34 + @" />
			                    <img class='edit' src='../../../Images/imgComentario_edit.png' title='Modificar observación' />
			                    <img class='delete' src='../../../Images/imgComentario_delete.png' title='Eliminar observación' />
                                </span>
			                    <div class='message'>
				                    <span class='arrow'></span>
				                    <span class='name'>" + dr["Profesional"].ToString() + @"</span>
				                    <span class='datetime'>" + dr["t693_fecha"].ToString().Substring(0, dr["t693_fecha"].ToString().Length - 3) + @"</span>
                                    <span class='body'>" + dr["t693_observaciones"].ToString().Replace(((char)10).ToString(), "<br>") + @"</span>
			                    </div>
		                    </li>");
                }
                dr.Close();
                dr.Dispose();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
