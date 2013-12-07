using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal class Amf3ArrayReadItem
    {

        internal bool IsDenseArrayItem
        {
            get
            {
                return this.DenseIndex <= 0 && this.PropertyName == null;
            }
        }

        internal IValueOrRemainedIndex<string> PropertyName { get; private set; }

        internal int DenseIndex { get; private set; }
    }
}
