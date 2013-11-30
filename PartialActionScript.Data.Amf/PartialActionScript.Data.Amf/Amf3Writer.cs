using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PartialActionScript.Data.Amf
{
    internal sealed class Amf3Writer
    {
        #region Constructor

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

        internal static void Write(IDataWriter writer, IAmfValue input)
        {

            var sequencer = new Amf3Writer(writer);

            sequencer.writeValue(input);
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

                default:
                    throw new NotSupportedException();
            }
        }

        private void writeStringValue(IAmfValue input)
        {
           
            var value = input.GetString();

            this.write(Amf3Type.String);
            this.write(value);
            
        }


        private void write(Amf3Type value)
        {
            this.writer_.WriteByte((byte)value);
        }

        private void write(string value)
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
