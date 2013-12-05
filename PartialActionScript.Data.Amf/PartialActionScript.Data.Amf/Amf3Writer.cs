using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Data.Xml.Dom;

namespace PartialActionScript.Data.Amf
{
    public sealed class Amf3Writer
    {
        #region Constructor

        public Amf3Writer(IDataWriter writer)
        {
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

        public IBuffer DetatchBuffer()
        {
            return this.writer_.DetachBuffer();
        }

        public void WirteRemainString(uint remainIndex)
        {
            this.writeAmf3Type(Amf3Type.String);
            this.writeRemainIndex(remainIndex);
        }

        public void WriteString(string value)
        {
            

            this.writeAmf3Type(Amf3Type.String);
            this.partialWriteString(value);
        }

        public void WriteRemainXml(uint remainIndex)
        {
            this.writeAmf3Type(Amf3Type.Xml);
            this.writeRemainIndex(remainIndex);
        }

        public void WriteXml(XmlDocument document)
        {
            this.writeAmf3Type(Amf3Type.Xml);
            this.partialWriteString(document.GetXml());
        }

        public void WriteRemainXmlDocument(uint remainIndex)
        {
            this.writeAmf3Type(Amf3Type.XmlDocument);
            this.writeRemainIndex(remainIndex);
        }

        public void WriteXmlDocument(XmlDocument document)
        {
            this.writeAmf3Type(Amf3Type.XmlDocument);
            var xml = document.GetXml();
            this.partialWriteString(xml == string.Empty ? "<>" : xml);
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

        


        private IDataWriter writer_;


        private void partialWriteString(string value)
        {
            if (value.Length < 0 || (!UInt29.ValidUInt29((UInt32)value.Length)))
                throw ExceptionHelper.CreateInvalidOperationStringValueTooLong(value);

            UInt29 length = (UInt29)value.Length;
            length.WriteAsRefTo(false, this.writer_);
            this.writer_.WriteString(value);
        }

        private void writeRemainIndex(uint remainIndex)
        {
            ((UInt29)remainIndex).WriteAsRefTo(true, this.writer_);
        }


        private void writeAmf3Type(Amf3Type value)
        {
            this.writer_.WriteByte((byte)value);
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

        #endregion
    }
}
