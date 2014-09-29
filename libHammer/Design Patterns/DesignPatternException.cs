using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// 
    /// </summary>
    [global::System.Serializable]
    public class DesignPatternException : Exception
    {

        /// <summary>
        /// Initialises a new instance of the <see cref="DesignPatternException" /> class.
        /// </summary>
        public DesignPatternException() { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DesignPatternException" /> class.
        /// </summary>
        /// <param name="exception"></param>
        public DesignPatternException(string exceptionDescription) : base(exceptionDescription) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DesignPatternException" /> class.
        /// </summary>
        /// <param name="exceptionDescription"></param>
        /// <param name="innerException"></param>
        public DesignPatternException(string exceptionDescription, Exception innerException) : base(exceptionDescription, innerException) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="DesignPatternException" /> class.
        /// </summary>
        /// <param name="serializationInfo">The <see cref="SerializationInfo"/> holding the serialized object data about the exception being thrown.</param>
        /// <param name="streamingContext">The <see cref="StreamingContext"/> that contains the contextual information about the source or destination.</param>
        protected DesignPatternException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }

    }

}
