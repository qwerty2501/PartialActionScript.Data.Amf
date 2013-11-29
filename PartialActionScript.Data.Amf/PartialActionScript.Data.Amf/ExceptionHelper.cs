using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace PartialActionScript.Data.Amf
{
    internal static class ExceptionHelper
    {
        internal static InvalidOperationException CreateInvalidTypeException(AmfValueType actualType)
        {
            
            return new InvalidOperationException(string.Format(getloaderString("InvalidTypeErrorMessageFormat"), actualType));
        }

        internal static InvalidOperationException CreateInvalidRemainingValueException(UInt29 val)
        {
            return new InvalidOperationException(string.Format(getloaderString("InvalidRemainingValueErrorMessageFormat"),  val));
        }

        internal static InvalidOperationException CreateInvalidOperationStringValueTooLong(string val)
        {

            return new InvalidOperationException(string.Format(getloaderString("StringValueTooLongErrorMessageFormat") , val.Length));
        }

        internal static OverflowException CreateOutOfUInt29Exception(UInt32 input)
        {
            return new OverflowException(string.Format(getloaderString("OutOfUInt29ErrorMessageFormat"), input));
        }

        internal static OverflowException CreateOutOfInt29Exception(Int32 input)
        {
            return new OverflowException(string.Format(getloaderString("OutOfInt29ErrorMessageFormat"), input));
        }


        private static string getloaderString(string resource)
        {
#if NETFX_CORE
            var resourceLoarder = new ResourceLoader();
            return resourceLoarder.GetString(resource);
#else
            return resource;
#endif
        }

    }
}
