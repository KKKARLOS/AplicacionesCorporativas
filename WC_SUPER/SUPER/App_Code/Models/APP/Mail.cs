using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IB.SUPER.APP.Models
{
    public class Mail
    {
        #region Public Properties
        public String codred { get; set; }//destinatario
        public String asunto { get; set; }
        public String mensaje { get; set; }

        #endregion
    }
}