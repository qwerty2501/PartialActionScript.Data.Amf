using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    public sealed class AmfValue:IAmfValue
    {
        #region Constructor

        private AmfValue(object value,AmfValueType valueType)
        {
            this.value_ = value;
            this.ValueType = valueType;
        }


        #endregion

        #region Finalizer

        ~AmfValue()
        {

        }

        #endregion

        #region Property

        public AmfValueType ValueType
        {
            get;
            private set;
        }

        #endregion

        #region Method

        public string GetString()
        {
            if (this.ValueType != AmfValueType.String)
                throw ExceptionHelper.CreateInvalidTypeException( );
            
            return (string) this.value_;
        }


        public bool GetBoolean()
        {
            if (this.ValueType != AmfValueType.Boolean)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (bool)this.value_;
        }

        

        public double GetNumber()
        {
            if (this.ValueType != AmfValueType.Number)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (double)this.value_;
        }

        public DateTimeOffset GetDate()
        {
            if (this.ValueType != AmfValueType.Date)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (DateTimeOffset)this.value_;
        }

        public XmlDocument GetXml()
        {
            if (this.ValueType != AmfValueType.Xml)
                throw ExceptionHelper.CreateInvalidTypeException();

            return ((XmlContext)this.value_).Document;
        }

        public AmfArray GetArray()
        {
            throw ExceptionHelper.CreateInvalidTypeException( );
        }


        public AmfObject GetObject()
        {
            throw ExceptionHelper.CreateInvalidTypeException();
        }

        public IReadOnlyList<byte> GetByteArray()
        {
            if (this.ValueType != AmfValueType.ByteArray)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (byte[])this.value_;
        }

        public IReadOnlyList<int> GetVectorInt()
        {
            if (this.ValueType != AmfValueType.VectorInt)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (IReadOnlyList<int>)this.value_;
        }

        public IReadOnlyList<uint> GetVectorUInt()
        {
            if (this.ValueType != AmfValueType.VectorUInt)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (IReadOnlyList<uint>)this.value_;
        }

        public IReadOnlyList<double> GetVectorDouble()
        {
            if (this.ValueType != AmfValueType.VectorDouble)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (IReadOnlyList<double>)this.value_;
        }

        public IReadOnlyList<IAmfValue> GetVectorObject()
        {
            if (this.ValueType != AmfValueType.VectorObject)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (IReadOnlyList<IAmfValue>)this.value_;
        }

        internal XmlContext GetXmlContext()
        {
            if (this.ValueType != AmfValueType.Xml)
                throw ExceptionHelper.CreateInvalidTypeException();

            return (XmlContext)this.value_;
        }

        public static IAmfValue Parse(IBuffer buffer,AmfEncodingType encodingType )
        {
            return AmfReader.Parse(buffer, encodingType);
        }

        public IBuffer Sequencify( AmfEncodingType encodingType)
        {
             return AmfSequencer.Sequencify(this, encodingType);
        }

        public override string ToString()
        {
            return this.value_.ToString();
        }

        #endregion

        #region Create Methods

        public static AmfValue CreateUndefinedValue()
        {
            return new AmfValue(null, AmfValueType.Undefined);
        }

        public static AmfValue CreateNullValue()
        {
            return new AmfValue(null, AmfValueType.Null);
        }

        public static AmfValue CreteStringValue(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            return new AmfValue(input, AmfValueType.String);
        }

        public static AmfValue CreteNumberValue(double input)
        {
            return new AmfValue(input, AmfValueType.Number);
        }

        public static AmfValue CreateBooleanValue(bool input)
        {
            return new AmfValue(input, AmfValueType.Boolean);
        }

        public static AmfValue CreateDateValue(DateTimeOffset input)
        {
            return new AmfValue(input, AmfValueType.Date);
        }

        public static AmfValue AsXmlValue(XmlDocument input)
        {
            if (input == null)
                throw new ArgumentNullException();

            return new AmfValue(new XmlContext(input, true), AmfValueType.Xml);
        }

        public static AmfValue AsLegacyXmlValue(XmlDocument input)
        {
            if (input == null)
                throw new ArgumentNullException();

            return new AmfValue(new XmlContext(input, false), AmfValueType.Xml);
        }

        #endregion

        #region Private

        private object value_;

        #endregion


    }
}
