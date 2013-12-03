using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal static class AmfReader
    {
        internal static IAmfValue Parse(IBuffer buffer, AmfEncodingType encodingType)
        {
            switch (encodingType)
            {
                case AmfEncodingType.Amf0:
                    throw new NotImplementedException();


                case AmfEncodingType.Amf3:
                    return Amf3Parser.Parse(buffer);


                default:
                    throw new NotImplementedException();
            }
        }
    }
}
