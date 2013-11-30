﻿using System;
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
            
            return new InvalidOperationException(string.Format(Languages.Language.InvalidTypeErrorMessageFormat, actualType));
        }

        internal static InvalidOperationException CreateInvalidRemainingValueException(UInt29 val)
        {
            return new InvalidOperationException(string.Format(Languages.Language.InvalidRemainingValueErrorMessageFormat, val));
        }

        internal static InvalidOperationException CreateInvalidOperationStringValueTooLong(string val)
        {
            return new InvalidOperationException(string.Format(Languages.Language.StringValueTooLongErrorMessageFormat, val.Length));
        }

        internal static OverflowException CreateOutOfUInt29Exception(UInt32 input)
        {
            return new OverflowException(string.Format(Languages.Language.OutOfUInt29ErrorMessageFormat, input));
        }

        internal static OverflowException CreateOutOfInt29Exception(Int32 input)
        {
            return new OverflowException(string.Format(Languages.Language.OutOfInt29ErrorMessageFormat, input));
        }

        internal static IndexOutOfRangeException CreateOutOfStringRemainLengthException()
        {
            return createOutOfRemainLengthException( "String");
        }

        private static IndexOutOfRangeException createOutOfRemainLengthException(string remainType)
        {
            return new IndexOutOfRangeException(string.Format(Languages.Language.OutOfRemainLengthErrorMessageFormat, remainType));
        }


        private static string getloaderString(string resource)
        {
            var resourceLoarder = new ResourceLoader();
            return resourceLoarder.GetString(resource);
        }

    }
}
