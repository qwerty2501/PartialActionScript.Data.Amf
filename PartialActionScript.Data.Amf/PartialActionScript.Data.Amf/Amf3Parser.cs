using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal class Amf3Parser
    {
        #region Constractor

        internal Amf3Parser(IBuffer buffer)
        {
            this.reader_ = new Amf3Reader(buffer);
            this.stringRemains_ = new List<string>();
        }

        #endregion

        #region Method

        internal static IAmfValue Parse(IBuffer buffer)
        {
            var parser = new Amf3Parser(buffer);

            return parser.readAmfValue();
        }

        #endregion

        #region Private

        private Amf3Reader reader_;

        private List<string> stringRemains_;

        private IAmfValue readAmfValue()
        {
            this.reader_.Read();
            switch (this.reader_.Amf3Type)
            {
                case Amf3Type.String:
                    return this.readStringValue();

                case Amf3Type.Integer:
                    return this.readIntegerValue();

                case Amf3Type.Double:
                    return this.readDoubleValue();

                case Amf3Type.True:
                case Amf3Type.False:
                    return this.readBooleanValue();

                default:
                    throw new NotImplementedException();
            }
        }

        private IAmfValue readStringValue()
        {
            return AmfValue.CreteStringValue(this.reader_.ReadingRemainedValue ? this.stringRemains_[this.reader_.GetRemainIndex()] : this.reader_.GetString());
        }

        private IAmfValue readIntegerValue()
        {
            return AmfValue.CreteNumberValue(this.reader_.GetInteger());
        }

        private IAmfValue readDoubleValue()
        {
            return AmfValue.CreteNumberValue(this.reader_.GetDouble());
        }

        private IAmfValue readBooleanValue()
        {
            return AmfValue.CreateBooleanValue(this.reader_.GetBoolean());
        }

        #endregion
    }
}
