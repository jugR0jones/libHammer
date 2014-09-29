using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libHammer.Design_Patterns
{

    /// <summary>
    /// Specifies the implementing class supports lazy initialization.
    /// </summary>
    public interface ISupportLazyInitialization
    {

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        void Initialize();

    }

}
