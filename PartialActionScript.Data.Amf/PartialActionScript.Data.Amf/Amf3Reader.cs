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

        public Amf3Reader(IInputStream stream) : this(new DataReader(stream)) { }
        private Amf3Reader(IDataReader reader)
        {
            this.reader_ = reader;
            this.typeLoaded_ = false;
            this.stringRemains_ = new List<string>();
            this.amf3Type_ = Amf3Type.Undefined;
        }

        #endregion


        #region Method

        public  IAsyncOperation<Amf3Type> ReadAmf3TypeAsync()
        {
            return this.readAmf3TypeAsync().AsAsyncOperation();

        }

        public IAsyncOperation<string> ReadStringAsync()
        {
            return readStringAsync().AsAsyncOperation();
        }


        internal async Task<byte> ReadByteAsync()
        {
            await this.appendLoadAssync(1).ConfigureAwait(false);

            return this.reader_.ReadByte();
        }

        internal static IAsyncOperation<IAmfValue> LoadAmfValueFromSstreamAsync(IInputStream stream)
        {
            var reader = new Amf3Reader(stream);

            return reader.readAmfValueAsync().AsAsyncOperation();
        }

        #endregion


        #region Private

        private IDataReader reader_;

        private bool typeLoaded_;

        private Amf3Type amf3Type_;

        private List<string> stringRemains_;


        private Task<string> readStringAsync()
        {
            return  this.partialReadAmfValueAsync(async() =>
            {
                var length = await this.readUInt29Async().ConfigureAwait(false);

                if (length.RemainedValue)
                {
                    return this.stringRemains_[length.ToRemainIndex()];
                }
                else
                {
                    var result = this.reader_.ReadString(length.ToNoneFlagValue());

                    this.stringRemains_.Add(result);

                    return result;
                }

            },Amf3Type.String);
        }

        private async Task<UInt29> readUInt29Async()
        {
            return await UInt29.ReadFromAsync(this).ConfigureAwait(false);
        }

        private async Task<IAmfValue> readAmfValueAsync()
        {
            switch (await this.readAmf3TypeAsync().ConfigureAwait(false))
            {
                case Amf3Type.String:
                    return await readStringValueAsync().ConfigureAwait(false);


                default:
                    throw new NotImplementedException();
            }
        }

        private async Task<T> partialReadAmfValueAsync<T>(Func<Task<T>> func,Amf3Type amf3Type)
        {
            if ((!this.typeLoaded_) || (this.amf3Type_ != amf3Type))
                throw new InvalidOperationException();

            var result = await func().ConfigureAwait(false);

            this.typeLoaded_ = false;

            return result;
        }

        private async Task<Amf3Type> readAmf3TypeAsync()
        {
            if (this.typeLoaded_)
                throw new InvalidOperationException();

            await this.appendLoadAssync(1).ConfigureAwait(false);

            var type = (Amf3Type)this.reader_.ReadByte();

            this.typeLoaded_ = true;

            return type;
        }

        private async Task<IAmfValue> readStringValueAsync()
        {
            return AmfValue.CreteStringValue(await this.readStringAsync().ConfigureAwait(false));
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
