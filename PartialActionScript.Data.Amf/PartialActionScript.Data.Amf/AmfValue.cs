using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                throw ExceptionHelper.CreateInvalidTypeException( this.ValueType);
            
            return (string) this.value_;
        }


        public bool GetBoolean()
        {
            if (this.ValueType != AmfValueType.Boolean)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (bool)this.value_;
        }

        

        public double GetNumber()
        {
            if (this.ValueType != AmfValueType.Number)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (double)this.value_;
        }

        public DateTimeOffset GetDate()
        {
            if (this.ValueType != AmfValueType.Date)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (DateTimeOffset)this.value_;
        }

        public AmfArray GetArray()
        {
            throw ExceptionHelper.CreateInvalidTypeException( this.ValueType);
        }


        public AmfObject GetObject()
        {
            throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);
        }

        public IReadOnlyList<byte> GetByteArray()
        {
            if (this.ValueType != AmfValueType.ByteArray)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (byte[])this.value_;
        }

        public IReadOnlyList<int> GetVectorInt()
        {
            if (this.ValueType != AmfValueType.VectorInt)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (IReadOnlyList<int>)this.value_;
        }

        public IReadOnlyList<uint> GetVectorUInt()
        {
            if (this.ValueType != AmfValueType.VectorUInt)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (IReadOnlyList<uint>)this.value_;
        }

        public IReadOnlyList<double> GetVectorDouble()
        {
            if (this.ValueType != AmfValueType.VectorDouble)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (IReadOnlyList<double>)this.value_;
        }

        public IReadOnlyList<IAmfValue> GetVectorObject()
        {
            if (this.ValueType != AmfValueType.VectorObject)
                throw ExceptionHelper.CreateInvalidTypeException(this.ValueType);

            return (IReadOnlyList<IAmfValue>)this.value_;
        }

        public void WriteTo(IDataWriter writer, AmfEncodingType encodingType)
        {
             AmfWriter.Write(writer,this, encodingType);
        }

        public override string ToString()
        {
            return this.value_.ToString();
        }

        #endregion

        #region Create Methods

        public static AmfValue CreteStringValue(string input)
        {
            if (input == null)
                throw new ArgumentNullException();

            return new AmfValue(input ?? string.Empty, AmfValueType.String);
        }

        #endregion

        #region Private

        private object value_;

        #endregion


    }
}
