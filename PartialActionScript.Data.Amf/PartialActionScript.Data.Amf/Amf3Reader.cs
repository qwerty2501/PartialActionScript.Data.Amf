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

        private Amf3Reader(IDataReader reader)
        {
            this.reader_ = reader;
            this.stringRemains_ = new List<string>();
            this.Amf3Type = Amf3Type.UnInitialized;
        }

        #endregion


        #region Property

        internal bool TypeLoaded
        {
            get
            {
                return this.Amf3Type != Amf3Type.UnInitialized;
            }
        }

        public Amf3Type Amf3Type
        {
            get;
            private set;
        }

        #endregion

        #region Method

        public void ReadAmf3Type()
        {
            if (this.TypeLoaded)
                throw new InvalidOperationException();


            this.Amf3Type = (Amf3Type)this.reader_.ReadByte();

        }
        public string ReadString()
        {
            return this.partialReadAmfValue(() =>
            {
                var length = this.readUInt29();
                if (length.RemainedValue)
                {
                    return this.stringRemains_[length.ToRemainIndex()];
                }
                else
                {
                    var noneRefLength = length.ToNoneFlagValue();

                    var result = this.reader_.ReadString(noneRefLength);

                    this.stringRemains_.Add(result);

                    return result;
                }

            }, Amf3Type.String);
        }

        public int ReadInteger()
        {
            return this.partialReadAmfValue(() =>
            {
                return (int)this.readUInt29();
            },Amf3Type.Integer);
        }

        public double ReadDouble()
        {
            return this.partialReadAmfValue(() =>
            {
                return this.reader_.ReadDouble();
            }, Amf3Type.Double);
        }

        public bool ReadBoolean()
        {
            switch (this.Amf3Type)
            {
                case Amf.Amf3Type.True:
                    return true;

                case Amf.Amf3Type.False:
                    return false;

                default:
                    throw new InvalidOperationException();
            }
        }

        public  IAsyncOperation<uint> LoadAssync(uint count)
        {

            return  this.reader_.LoadAsync(count);

        }



        internal static IAmfValue Parse(IBuffer buffer)
        {
            var reader = new Amf3Reader(DataReader.FromBuffer(buffer));

            return reader.readAmfValueAsync();
        }

        #endregion


        #region Private

        private IDataReader reader_;

        

        private List<string> stringRemains_;


        
        private  UInt29 readUInt29()
        {
            return UInt29.ReadFrom(this.reader_);
        }

        private  IAmfValue readAmfValueAsync()
        {
            this.ReadAmf3Type();
            switch (this.Amf3Type)
            {
                case Amf3Type.String:
                    return  this.readStringValue();

                case Amf3Type.Integer:
                    return this.readIntegerValue();

                case Amf3Type.Double:
                    return this.readDoubleValue();

                case Amf3Type.True:
                case Amf3Type.False:
                    return this.readBooleanValue();

                default:
                    throw new NotImplementedException();
            }
        }

        private  T partialReadAmfValue<T>(Func<T> func,Amf3Type amf3Type)
        {
            if ((this.Amf3Type != amf3Type))
                throw new InvalidOperationException();

            var result = func();

            return result;
        }

        

        private  IAmfValue readStringValue()
        {
            return AmfValue.CreteStringValue( this.ReadString());
        }

        private IAmfValue readIntegerValue()
        {
            return AmfValue.CreteNumberValue(this.ReadInteger());
        }

        private IAmfValue readDoubleValue()
        {
            return AmfValue.CreteNumberValue(this.ReadDouble());
        }

        private IAmfValue readBooleanValue()
        {
            return AmfValue.CreateBooleanValue(this.ReadBoolean());
        }

        

        #endregion
    }
}
