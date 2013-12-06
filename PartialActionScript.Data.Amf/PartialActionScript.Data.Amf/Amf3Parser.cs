using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal sealed class Amf3Parser
    {
        #region Constractor

        internal Amf3Parser(IBuffer buffer)
        {
            this.reader_ = new Amf3Reader(buffer);
            this.stringRemains_ = new List<string>();
            this.objectRemains_ = new List<IAmfValue>();
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

        private List<IAmfValue> objectRemains_;

        private IAmfValue readAmfValue()
        {
            
            this.reader_.Read();
            switch (this.reader_.Amf3Type)
            {
                case Amf3Type.String:
                    return this.getStringValue();

                case Amf3Type.Integer:
                    return this.getIntegerValue();

                case Amf3Type.Double:
                    return this.getDoubleValue();

                case Amf3Type.True:
                case Amf3Type.False:
                    return this.getBooleanValue();

                case Amf3Type.Xml:
                    return this.getXmlValue();

                case Amf3Type.XmlDocument:
                    return this.getXmlDocumentValue();

                case Amf3Type.Null:
                    return this.getNullValue();

                case Amf3Type.Date:
                    return this.getDateValue();

                case Amf3Type.Undefined:
                    return this.getUndefinedValue();

                default:
                    throw new NotImplementedException();
            }
        }

        private IAmfValue getDateValue()
        {
            return this.getObjectReferenceOrValue(() => AmfValue.CreateDateValue(this.reader_.GetDate()));
        }

        private IAmfValue getNullValue()
        {
            return AmfValue.CreateNullValue();
        }

        private IAmfValue getStringValue()
        {
            
            if (this.reader_.RemainedValue)
                return AmfValue.CreteStringValue(this.stringRemains_[this.reader_.GetRemainIndex()]);
            

            return AmfValue.CreteStringValue(this.reader_.GetString());
        }



        private IAmfValue getIntegerValue()
        {
            return AmfValue.CreteNumberValue(this.reader_.GetInteger());
        }


        private IAmfValue getDoubleValue()
        {
            return AmfValue.CreteNumberValue(this.reader_.GetDouble());
        }



        private IAmfValue getBooleanValue()
        {
            return AmfValue.CreateBooleanValue(this.reader_.GetBoolean());
        }

        private IAmfValue getXmlValue()
        {
            return getObjectReferenceOrValue(() => AmfValue.AsXmlValue(this.reader_.GetXml()));
        }

        private IAmfValue getUndefinedValue()
        {
            return AmfValue.CreateUndefinedValue();
        }

        private IAmfValue getXmlDocumentValue()
        {
            return getObjectReferenceOrValue(()=> AmfValue.AsLegacyXmlValue(this.reader_.GetXml()));
        }

        private IAmfValue getObjectReferenceOrValue(Func< IAmfValue> createFunc)
        {
            if (this.reader_.RemainedValue)
                return this.objectRemains_[this.reader_.GetRemainIndex()];

            return createFunc();
        }

        #endregion
    }
}
