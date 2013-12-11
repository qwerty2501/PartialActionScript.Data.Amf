using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal class Amf3ArrayRange:IEnumerable<Amf3ArrayReadItem>
    {
        internal Amf3ArrayRange(Amf3Reader reader,uint denseArrayCount)
        {
            this.reader_ = reader;
            this.denseArrayCount_ = denseArrayCount;
        }

        private Amf3Reader reader_;

        private uint denseArrayCount_;

        public IEnumerator<Amf3ArrayReadItem> GetEnumerator()
        {
            IValueOrRemainedIndex<string> propertyName = null;
            do
            {
                propertyName = this.reader_.ReadPropertyName();

                if (propertyName.Value == string.Empty) 
                    break;

                yield return new Amf3ArrayReadItem(propertyName, this.reader_);

            } while (true);

            for (int denseArrayIndex = 0; denseArrayIndex < this.denseArrayCount_; denseArrayIndex++)
            {
                yield return new Amf3ArrayReadItem(denseArrayIndex, this.reader_);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
