using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Clase para gestionar documentos temporales hasta que se asignan a su padre
/// De momento la usamos para Certificados y Exámenes de CVT
/// </summary>
/// 
namespace SUPER.BLL
{
    public class DocuAux
    {
        #region Propiedades y Atributos

        private int _t686_iddocuaux;
        public int t686_iddocuaux
        {
            get { return _t686_iddocuaux; }
            set { _t686_iddocuaux = value; }
        }

        private string _t686_usuticks;
        public string t686_usuticks
        {
            get { return _t686_usuticks; }
            set { _t686_usuticks = value; }
        }

        private int _t001_idficepi;
        public int t001_idficepi
        {
            get { return _t001_idficepi; }
            set { _t001_idficepi = value; }
        }

        private int _t686_clave;
        public int t686_clave
        {
            get { return _t686_clave; }
            set { _t686_clave = value; }
        }

        private string _t686_tipo;
        public string t686_tipo
        {
            get { return _t686_tipo; }
            set { _t686_tipo = value; }
        }

        private string _t686_nombre;
        public string t686_nombre
        {
            get { return _t686_nombre; }
            set { _t686_nombre = value; }
        }

        private DateTime _t686_fecha;
        public DateTime t686_fecha
        {
            get { return _t686_fecha; }
            set { _t686_fecha = value; }
        }

        private long? _t2_iddocumento;
        public long? t2_iddocumento
        {
            get { return _t2_iddocumento; }
            set { _t2_iddocumento = value; }
        }

        private bool _t686_asignado;
        public bool t686_asignado
        {
            get { return _t686_asignado; }
            set { _t686_asignado = value; }
        }

        #endregion
        public DocuAux()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T686_DOCUAUX.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	18/10/2010 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t001_idficepi, string t686_tipo, string t686_nombre,
                                 string t686_usuticks, long idContentServer, bool t686_asignado)
        {
            //En el caso de que ya se haya seleccionado un documento y cargamos otro, hay que eliminar de la tabla auxiliar
            //el primer documento
            SUPER.DAL.DocuAux.BorrarDocumento(tr, t686_tipo, t686_usuticks);
            return SUPER.DAL.DocuAux.Insert(tr, t001_idficepi, t686_tipo, t686_nombre, t686_usuticks, idContentServer, t686_asignado);
        }
        public static DocuAux GetDocumento(SqlTransaction tr, string t686_usuticks)
        {
            SUPER.DAL.DocuAux oDocumento = SUPER.DAL.DocuAux.GetDocumento(tr, t686_usuticks);

            DocuAux oDoc = new DocuAux();
            oDoc.t2_iddocumento = oDocumento.t2_iddocumento;
            oDoc.t686_nombre = oDocumento.t686_nombre;

            return oDoc;
        }
    }
}
