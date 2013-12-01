using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    public sealed class Amf3Reader
    {
        #region Constractor

        public Amf3Reader(IInputStream stream) : this(new DataReader(stream)) { }
        private Amf3Reader(IDataReader reader)
        {
            this.reader_ = reader;
        }

        #endregion


        #region Method

        internal IAsyncOperation<IAmfValue> LoadAmfValueAsync()
        {
            throw new NotImplementedException();
        }

        internal static IAsyncOperation<IAmfValue> LoadAmfValueFromSstreamAsync(IInputStream stream)
        {
            var reader = new Amf3Reader(stream);

            return reader.LoadAmfValueAsync();
        }

        #endregion


        #region Private

        private IDataReader reader_;

        #endregion
    }
}
