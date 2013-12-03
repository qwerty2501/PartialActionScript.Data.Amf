using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    internal  static class AmfSequencer
    {
        internal static IBuffer Sequencify( IAmfValue input, AmfEncodingType encodingType)
        {
            switch (encodingType)
            {
                case AmfEncodingType.Amf0:
                    throw new NotImplementedException();


                case AmfEncodingType.Amf3:
                    return Amf3Sequencer.Sequencify(input);

                default:
                    throw new NotImplementedException();
            }


        }
    }
}
