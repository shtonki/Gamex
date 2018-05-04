using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gamex.src.Util
{
    abstract class GamexException : Exception
    {
        #region cruft
        public GamexException()
        {
        }
        public GamexException(string message) : base(message)
        {
        }
        public GamexException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public GamexException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion
    }

    class GameBrokenException : GamexException
    {
        public GameBrokenException(string message) : base(message)
        {
        }
    }
}
