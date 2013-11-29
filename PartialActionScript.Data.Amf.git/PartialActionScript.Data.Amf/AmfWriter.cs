using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal sealed static class AmfWriter
    {
        internal static void Write(IDataWriter writer ,IAmfValue input, AmfEncodingType encodingType)
        {
            try
            {
                switch (encodingType)
                {
                    case AmfEncodingType.Amf0:
                        throw new NotImplementedException();


                    case AmfEncodingType.Amf3:
                         Amf3Writer.Write(writer,input);
                         break;

                    default:
                        throw new NotSupportedException();
                }

            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
            
        }
    }
}
