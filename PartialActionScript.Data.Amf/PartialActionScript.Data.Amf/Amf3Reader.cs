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



        internal async Task<byte> ReadByteAsync()
        {
            await this.appendLoadAssync(1).ConfigureAwait(false);

            return this.reader_.ReadByte();
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
                    return  ReadStringValue();


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

        

        private  IAmfValue ReadStringValue()
        {
            return AmfValue.CreteStringValue( this.ReadString());
        }

        

        private async Task<uint> appendLoadAssync(uint count)
        {
            if (this.reader_.UnconsumedBufferLength < count)
                return await this.reader_.LoadAsync(count).AsTask().ConfigureAwait(false);
            
            return 0;
        }

        #endregion
    }
}
