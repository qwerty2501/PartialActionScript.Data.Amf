using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;

namespace PartialActionScript.Data.Amf
{
    public sealed class Amf3Writer
    {
        #region Constructor


        public Amf3Writer(IOutputStream stream) : this(new DataWriter(stream)) { }

        private Amf3Writer(IDataWriter writer)
        {
            this.objectRemains_ = new List<object>();
            this.stringRemains_ = new List<string>();
            this.writer_ = writer; 
        }


        #endregion

        #region Finalizer

        ~Amf3Writer()
        {

        }

        #endregion

        #region Property


        #endregion

        #region Method

        internal static IBuffer Sequencify( IAmfValue input)
        {
            var writer = new DataWriter();
            var amfWriter = new Amf3Writer(writer);

            amfWriter.writeValue(input);

            return writer.DetachBuffer();
        }

        public void WriteString(string value)
        {
            this.writeAmf3Type(Amf3Type.String);
            this.partialWrite(value);
        }

        public void WriteNumber(double value)
        {
            if (Int29.ValidInt29(value))
            {
                this.writeInteger((Int29)value);
            }
            else
            {
                this.writeDouble(value);
            }
        }

        public void WriteBoolean(bool value)
        {
            this.writeAmf3Type(value ? Amf3Type.True : Amf3Type.False);
        }

        public IAsyncOperation<uint> StoreAsync()
        {
            return this.writer_.StoreAsync();
        }

        public IAsyncOperation<bool> FlushAsync()
        {
            return this.writer_.FlushAsync();
        }
        

        #endregion

        #region Private

        private List<object> objectRemains_;

        private List<string> stringRemains_;

        private IDataWriter writer_;

        private void writeValue(IAmfValue input)
        {
            switch (input.ValueType)
            {
                case AmfValueType.String:
                    writeStringValue(input);
                    break;
                case AmfValueType.Number:
                    writeNumberValue(input);
                    break;

                case AmfValueType.Boolean:
                    writeBooleanValue(input);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }

        private void writeStringValue(IAmfValue input)
        {
           
            var value = input.GetString();

            this.WriteString(value);
            
        }

        private void writeNumberValue(IAmfValue input)
        {
            var value = input.GetNumber();
            this.WriteNumber(value);
        }

        private void writeBooleanValue(IAmfValue input)
        {
            var value = input.GetBoolean();

        }

        private void writeInteger(Int29 value)
        {
            this.writeAmf3Type(Amf3Type.Integer);
            value.WriteTo(this.writer_);
        }

        private void writeDouble(double value)
        {
            this.writeAmf3Type(Amf3Type.Double);
            this.writer_.WriteDouble(value);
        }

        private void writeAmf3Type(Amf3Type value)
        {
            this.writer_.WriteByte((byte)value);
        }

        private void partialWrite(string value)
        {


            if (value.Length < 0 || (!UInt29.ValidUInt29((UInt32)value.Length)))
                throw ExceptionHelper.CreateInvalidOperationStringValueTooLong(value);

            var remainIndex = this.stringRemains_.IndexOf(value);

            if (remainIndex < 0)
            {

                UInt29 length = (UInt29)value.Length;
                length.WriteAsRefTo(false, this.writer_);
                this.writer_.WriteString(value);
                this.remain(value);

            }
            else
            {
                ((UInt29)remainIndex).WriteAsRefTo(true, this.writer_);

            }
            
        }

        private void remain(string value)
        {
            if (this.stringRemains_.Count > UInt29.MaxRemainingValue)
                throw ExceptionHelper.CreateOutOfStringRemainLengthException();

            this.stringRemains_.Add(value);
        }

        #endregion
    }
}
