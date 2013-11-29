using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal static class ExceptionHelper
    {
        internal static InvalidOperationException CreateInvalidTypeException(AmfValueType actualType)
        {
            return new InvalidOperationException("Bud amf type: " + actualType);
        }

        internal static InvalidOperationException CreateInvalidRemainingValueException(UInt29 val)
        {
            return new InvalidOperationException("Invalid remaining value:" +  val);
        }

        internal static InvalidOperationException CreateInvalidOperationStringValueTooLong(string val)
        {
            return new InvalidOperationException("string length too long:" + val.Length);
        }

        internal static OverflowException CreateOutOfUInt29Exception(UInt32 input)
        {
            return new OverflowException("Over  UInt29 :" + input);
        }

        internal static OverflowException CreateOutOfInt29Exception(Int32 input)
        {
            return new OverflowException("Over Int29:" + input);
        }


    }
}
