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
            this.objectRemains_ = new List<object>();
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

        private List<object> objectRemains_;

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


                default:
                    throw new NotImplementedException();
            }
        }

        private string getString()
        {
            return this.reader_.RemainedValue ? this.stringRemains_[this.reader_.GetRemainIndex()] : this.reader_.GetString();
        }

        private XmlDocument getXml()
        {
            return this.reader_.RemainedValue ?((XmlContext) this.objectRemains_[this.reader_.GetRemainIndex()]).Document :  this.reader_.GetXml();
        }

        private IAmfValue getNullValue()
        {
            return AmfValue.CreateNullValue();
        }

        private IAmfValue getStringValue()
        {
            return AmfValue.CreteStringValue(getString());
        }

        private int getInteger()
        {
            return this.reader_.GetInteger();
        }

        private IAmfValue getIntegerValue()
        {
            return AmfValue.CreteNumberValue(this.getInteger());
        }

        private double getDouble()
        {
            return this.reader_.GetDouble();
        }

        private IAmfValue getDoubleValue()
        {
            return AmfValue.CreteNumberValue(this.getDouble());
        }

        private bool getBoolean()
        {
            return this.reader_.GetBoolean();
        }

        private IAmfValue getBooleanValue()
        {
            return AmfValue.CreateBooleanValue(this.getBoolean());
        }

        private IAmfValue getXmlValue()
        {
            return AmfValue.AsXmlValue(this.getXml());
        }

        private IAmfValue getXmlDocumentValue()
        {
            return AmfValue.AsLegacyXmlValue(this.getXml());
        }

        #endregion
    }
}
