using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IB.Services.Super
{
    [ServiceContract]
    public interface ISuper
    {
        [OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        string getDatosSuper(string sXml);

        [OperationContract]
        //[TransactionFlow(TransactionFlowOption.Allowed)]
        string getFechaCierreIAP(string sXml);

    }

}
