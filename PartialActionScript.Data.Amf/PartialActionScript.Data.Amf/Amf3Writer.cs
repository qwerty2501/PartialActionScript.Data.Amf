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
            this.objectReferences_ = new List<object>();
            this.stringReferences_ = new List<string>();
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

        private List<object> objectReferences_;

        private List<string> stringReferences_;

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
           
            var val = input.GetString();

            this.write(Amf3Type.String);
            this.write(val);
            
        }


        private void write(Amf3Type val)
        {
            this.writer_.WriteByte((byte)val);
        }

        private void write(string val)
        {


            if (val.Length < 0 || (!UInt29.ValidUInt29((UInt32)val.Length)))
                throw ExceptionHelper.CreateInvalidOperationStringValueTooLong(val);

            var refIndex = this.stringReferences_.IndexOf(val);

            if (refIndex < 0)
            {

                UInt29 length = (UInt29)val.Length;
                length.WriteAsRefTo(false, this.writer_);
                this.writer_.WriteString(val);


            }
            else
            {
                ((UInt29)refIndex).WriteAsRefTo(true, this.writer_);

            }
            
        }


        #endregion
    }
}
