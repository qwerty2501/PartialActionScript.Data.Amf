using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal struct ValueOrRemainedIndex<T>:IValueOrRemainedIndex<T>
    {
        internal ValueOrRemainedIndex(T value, int remainedIndex)
        {
            this.Value = value;
            this.RemainedIndex = remainedIndex;
        }

        public bool IsReference
        {
            get { return this.Value.Equals( default(T)) && this.RemainedIndex > 0; }
        }

        public T Value
        {
            get;
            private set;
        }

        public int RemainedIndex
        {
            get;
            private set;
        }
    }
}
