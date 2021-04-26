using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace IB.Progress.Shared
{
    [Serializable]
    public class IBException : System.Exception
    {

        public int ErrorCode { get { return _errorCode; } }
        private int _errorCode;

        public IBException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public IBException(int errorCode, string message, Exception inner)
            : base(message, inner)
        {
            _errorCode = errorCode;
        }

        public IBException(int errorCode, string message)
            : base(message)
        {
            _errorCode = errorCode;
        }

        public IBException(string message)
            : base(message)
        {
            _errorCode = 0;
        }

        public IBException(string message, Exception inner)
            : base(message, inner)
        {
            _errorCode = 0;
        }
    }
}
