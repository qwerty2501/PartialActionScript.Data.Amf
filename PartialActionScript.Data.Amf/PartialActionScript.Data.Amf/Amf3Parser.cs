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

        internal Amf3Parser(IBuffer buffer) : this(new Amf3Reader(buffer)) { }

        private Amf3Parser(Amf3Reader reader)
        {
            this.reader_ = reader;
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
            return this.getAmfValue();
            
        }

        private IAmfValue getAmfValue()
        {
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

                case Amf3Type.Array:
                    return this.getArray();

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
            if (this.reader_.Amf3Type != Amf3Type.Null)
                throw new InvalidOperationException();

            return AmfValue.CreateNullValue();
        }

        private IAmfValue getStringValue()
        {


            return AmfValue.CreateStringValue(this.getString());
        }



        private IAmfValue getIntegerValue()
        {
            return AmfValue.CreateNumberValue(this.reader_.GetInteger());
        }


        private IAmfValue getDoubleValue()
        {
            return AmfValue.CreateNumberValue(this.reader_.GetDouble());
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

        private IAmfValue getArray()
        {
            return getObjectReferenceOrValue(() =>
            {
                var array = new AmfArray();

                Amf3Parser parser = null;
                foreach (var readItem in this.reader_.GetArray())
                {
                    parser  = new Amf3Parser(readItem.Reader);
                    if (readItem.IsDenseArrayItem)
                    {

                        array.Add(readItem.DenseArrayIndex, parser.readAmfValue());
                    }
                    else
                    {
                        array.Add(getRemainedString(readItem.PropertyName), parser.readAmfValue());
                    }
                }

                

                return array;
            });

            
        }

        private string getRemainedString(IValueOrRemainedIndex<string> str)
        {
            if (str.IsRemained)
                return this.stringRemains_[str.RemainedIndex];

            return str.Value;
        }

        private string getString()
        {
            if (this.reader_.RemainedValue)
                return this.stringRemains_[this.reader_.GetRemainIndex()];

            return this.reader_.GetString();
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
