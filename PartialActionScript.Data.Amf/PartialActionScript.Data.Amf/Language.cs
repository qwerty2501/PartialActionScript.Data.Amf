using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal static class Languages
    {
        private static EnglishLanguage english = default(EnglishLanguage);

        internal static ILanguage Language
        {
            get
            {
                return english;
            }
        }
    }


    internal interface ILanguage
    {
        string InvalidTypeErrorMessageFormat { get; }

        string InvalidRemainingValueErrorMessageFormat { get; }

        string StringValueTooLongErrorMessageFormat { get; }

        string OutOfUInt29ErrorMessageFormat { get; }

        string OutOfInt29ErrorMessageFormat { get; }

        string OutOfRemainLengthErrorMessageFormat { get; }
    }
}
