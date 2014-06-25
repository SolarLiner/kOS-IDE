using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kOS_IDE
{
    class kFile
    {
        /// <summary>
        /// File name of the script.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Unstripped file name of the script, if found (returns string.Empty if not).
        /// </summary>
        public string stripFName { get; private set; }

        /// <summary>
        /// Check if the script is stripped by checking a strip file corresponding.
        /// </summary>
        public bool IsStripped { get { return String.IsNullOrWhiteSpace(stripFName); } }

        /// <summary>
        /// Check if the script is a function by scanning through the file for "declare parameter".
        /// </summary>
        public bool IsFunction { get { return Function == null; } }

        /// <summary>
        /// Pointer to the function.
        /// </summary>
        public kFunc Function { get; private set; }

        /// <summary>
        /// Global initializer.
        /// </summary>
        /// <param name="FileName">FileName input (full path).</param>
        public kFile(string FileName)
        {

        }
    }
}
