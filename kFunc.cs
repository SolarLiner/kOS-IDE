using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kOS_IDE
{
    class kFunc
    {
        /// <summary>
        /// Array of parameters.
        /// </summary>
        public string[] Parameters { get; private set; }

        /// <summary>
        /// Global initializer.
        /// </summary>
        /// <param name="parameters">Inputs the parameters of the function.</param>
        public kFunc(params string[] parameters)
        {
            Parameters = parameters;
        }
    }
}
