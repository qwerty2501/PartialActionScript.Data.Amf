using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System.Threading;

namespace PartialActionScript.Data.Amf
{
    public sealed class Amf3Reader
    {
        #region Constractor

        public Amf3Reader(IBuffer buffer) : this(DataReader.FromBuffer(buffer)) { }

        internal Amf3Reader(IDataReader reader)
        {
            this.reader_ = reader;
            this.Amf3Type = Amf3Type.Undefined;
        }

        #endregion


        #region Property

        public Amf3Type Amf3Type
        {
            get;
            private set;
        }

        public bool ReadingRemainedValue
        {
            get
            {
                return this.remainIndexOrLength_.RemainedValue;
            }
        }

        

        #endregion

        #region Method

        public bool Read()
        {
            if (this.reader_.UnconsumedBufferLength <= 0)
                return false;

            this.readValue_ = null;
            this.resetRemainIndex();

            try
            {
                this.Amf3Type = (Amf3Type)this.reader_.ReadByte();
            }
            catch (InvalidCastException e)
            {
                throw ExceptionHelper.CreateInvalidTypeException(e);
            }

            switch (this.Amf3Type)
            {
                case Amf3Type.String:
                    this.prepareGetStringValue();
                    break;

                case Amf3Type.Integer:
                    this.prepareGetIntegerValue();
                    break;

                case Amf3Type.Double:
                    this.prepareGetDoubleValue();
                    break;
            }

            return true;

        }

        public int GetRemainIndex()
        {


            if (!this.ReadingRemainedValue)
                throw ExceptionHelper.CreateInvalidRemainingValueException(this.remainIndexOrLength_);

            return this.remainIndexOrLength_.ToRemainIndex();


            
        }


        public string GetString()
        {
            return this.partialGetAmfValue(() =>
            {
                return this.partialGetStringValue();
            }, Amf3Type.String);
        }



        public int GetInteger()
        {
            return this.partialGetAmfValue(() =>
            {
                return (int)this.readValue_;
            },Amf3Type.Integer);
        }

        public double GetDouble()
        {
            return this.partialGetAmfValue(() =>
            {
                return (double)this.readValue_;
            }, Amf3Type.Double);
        }

        public bool GetBoolean()
        {
            switch (this.Amf3Type)
            {
                case Amf.Amf3Type.True:
                    return true;

                case Amf.Amf3Type.False:
                    return false;

                default:
                    throw ExceptionHelper.CreateInvalidTypeException();
            }
        }

        public  IAsyncOperation<uint> LoadAssync(uint count)
        {

            return  this.reader_.LoadAsync(count);

        }

        

        

        #endregion


        #region Private

        private IDataReader reader_;


        private UInt29 remainIndexOrLength_;

        private object readValue_;

        private void resetRemainIndex()
        {
            this.remainIndexOrLength_ = 0x01;
        }

        private void readRemainIndexOrLength()
        {
            this.remainIndexOrLength_ = readUInt29();
        }

        private  UInt29 readUInt29()
        {
            return UInt29.ReadFrom(this.reader_);
        }


        private string partialGetStringValue()
        {
            return (string)this.readValue_;
        }

        private void prepareGetStringValue()
        {
            this.readRemainIndexOrLength();

            if (!this.ReadingRemainedValue)
                this.readValue_ = this.reader_.ReadString(this.remainIndexOrLength_.ToNoneFlagValue());
        }

        private void prepareGetIntegerValue()
        {
            this.readValue_ = (int)this.readUInt29();
        }

        private void prepareGetDoubleValue()
        {
            this.readValue_ = (double)this.reader_.ReadDouble();
        }

        private  T partialGetAmfValue<T>(Func<T> func,Amf3Type amf3Type)
        {
            if ((this.Amf3Type != amf3Type))
                throw new InvalidOperationException();

            var result = func();

            return result;
        }



        #endregion
    }
}
