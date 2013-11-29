
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf.UnitTest
{
    internal class TestHelper
    {
        internal static byte[] CreateByteArray(string input)
        {
            var strArray = input.Split(',');

            var result = new byte[strArray.Length];

            for (int i = 0; i < strArray.Length; i++)
            {
                var target = strArray[i];

                if (target.Contains("0x"))
                {
                    result[i] = byte.Parse(target.Replace("0x", ""), NumberStyles.HexNumber);
                }
                else
                {
                    result[i] = byte.Parse(target);
                }
                
            }

            return result;
        }


        internal static string CreateString<T>(IEnumerable<T> array)
        {
            var builder = new StringBuilder();

            foreach (var item in array)
            {
                builder.Append(item);
                builder.Append(",");
            }

            return builder.ToString();
        }


    }
}
