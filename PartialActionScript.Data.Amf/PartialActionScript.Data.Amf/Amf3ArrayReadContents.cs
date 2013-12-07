using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal sealed class Amf3ArrayReadContents
    {
        internal Amf3ArrayReadContents(string keyName,int numericIndex, Amf3Reader reader)
        {
            this.KeyName = keyName;
            this.NumericIndex = numericIndex;
            this.Reader = reader;
            
        }

        internal bool IsNumericContent
        {
            get
            {
                return string.IsNullOrEmpty(this.KeyName);
            }
        }

        internal string KeyName { get; private set; }

        internal int NumericIndex { get; private set; }

        internal Amf3Reader Reader { get; private set; }
    }
}
