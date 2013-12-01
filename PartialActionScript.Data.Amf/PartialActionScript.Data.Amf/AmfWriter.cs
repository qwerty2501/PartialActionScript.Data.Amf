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
        internal static IAsyncOperation<uint> WriteAmfValueToStreamAsync(IOutputStream stream, IAmfValue input, AmfEncodingType encodingType)
        {
            switch (encodingType)
            {
                case AmfEncodingType.Amf0:
                    throw new NotImplementedException();


                case AmfEncodingType.Amf3:
                    return Amf3Writer.WriteAmfValueToStreamAsync(stream, input);

                default:
                    throw new NotImplementedException();
            }


        }
    }
}
