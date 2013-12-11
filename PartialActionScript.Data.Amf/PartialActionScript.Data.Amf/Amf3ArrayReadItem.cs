using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal class Amf3ArrayReadItem
    {
        internal Amf3ArrayReadItem(IValueOrRemainedIndex<string> propertyName, Amf3Reader reader) : this(propertyName,-1, reader) { }

        internal Amf3ArrayReadItem(int denseArrayIndex, Amf3Reader reader) : this(null, denseArrayIndex, reader) { }

        private Amf3ArrayReadItem(IValueOrRemainedIndex<string> propertyName, int denseArrayIndex, Amf3Reader reader)
        {
            this.PropertyName = propertyName;
            this.DenseArrayIndex = denseArrayIndex;
            this.Reader = reader;
        }

        internal bool IsDenseArrayItem
        {
            get
            {
                return this.DenseArrayIndex <= 0 && this.PropertyName == null;
            }
        }

        internal IValueOrRemainedIndex<string> PropertyName { get; private set; }

        internal int DenseArrayIndex { get; private set; }

        internal Amf3Reader Reader { get; private set; }
    }
}
