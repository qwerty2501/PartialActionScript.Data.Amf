using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal  static class AmfWriter
    {
        internal static IAsyncOperation<uint> WriteAsync(IOutputStream stream, IAmfValue input, AmfEncodingType encodingType)
        {
            var writer = new DataWriter(stream);
            switch (encodingType)
            {
                case AmfEncodingType.Amf0:
                    throw new NotImplementedException();


                case AmfEncodingType.Amf3:
                        Amf3Writer.Write(writer,input);
                        break;

                default:
                        throw new NotImplementedException();
            }

            return writer.StoreAsync();

        }
    }
}
