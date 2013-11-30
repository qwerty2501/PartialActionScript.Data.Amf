using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal struct EnglishLanguage:ILanguage
    {
        public string InvalidTypeErrorMessageFormat
        {
            get { return "{0} of Amf Type can not this operation."; }
        }

        public string InvalidRemainingValueErrorMessageFormat
        {
            get { return "{0} is invalid remaining UInt29 type value."; }
        }

        public string StringValueTooLongErrorMessageFormat
        {
            get { return "String length is too long."; }
        }

        public string OutOfUInt29ErrorMessageFormat
        {
            get { return "{0} is out of UInt29 type value."; }
        }

        public string OutOfInt29ErrorMessageFormat
        {
            get { return "{0} is out of Int29 type value."; }
        }

        public string OutOfRemainLengthErrorMessageFormat
        {
            get { return "Out of {0} remaining table length."; }
        }
    }
}
