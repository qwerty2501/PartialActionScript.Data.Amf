using Xunit;
using Xunit.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PartialActionScript.Data.Amf.UnitTest
{

    public class Int29Test
    {
        [Fact]
        public void CreateTest()
        {
            Int29 test = new Int29();

            Assert.Equal(0,(Int32) test);
        }

        [Fact]
        public void MaxValueTest()
        {
            Assert.Equal(268435455, Int29.MaxValue);
        }

        [Fact]
        public void MinValueTest()
        {
            Assert.Equal(-268435456, Int29.MinValue);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(268435455)]
        [InlineData(-268435456)]
        public void SetTest(Int32 input)
        {
            Int29 actual = input;

            Assert.Equal(input,(Int32) actual);
        }

        [Theory]
        [InlineData(268435456)]
        [InlineData(-268435457)]
        public void BadSetTest(Int32 input)
        {
            Assert.Throws<OverflowException>(() =>
            {
                Int29 actual = input;
            });
            
        }

        [Theory]
        [InlineData(0, UInt29.MinValue)]
        [InlineData(-1, UInt29.MaxValue)]
        [InlineData(Int29.MaxValue, (UInt32)Int29.MaxValue)]
        public void CastTest(Int32 input,UInt32 expected)
        {
            Int29 actual = input;
            Assert.Equal<UInt29>(expected, (UInt29)actual);
        }

        [Fact]
        public void ComparisonTest()
        {
            Int29 val1 = 5;
            Int29 val2 = 5;

            Assert.Equal( val1 , val2);
        }

        [Theory]
        [InlineData(0, "0x00")]
        [InlineData(-1, "0xFF,0xFF,0xFF,0xFF")]
        [InlineData(268435455, "0xBF,0xFF,0xFF,0xFF")]
        [InlineData(127, "0x7F")]
        [InlineData(128, "0x81,0x00")]
        [InlineData(16383, "0xFF,0x7F")]
        [InlineData(16384, "0x81,0x80,0x00")]
        [InlineData(2097151, "0xFF,0xFF,0x7F")]
        [InlineData(2097152, "0x80,0xC0,0x80,0x00")]
        public void WriteToTest(Int32 input,string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);

            Int29 actual = input;

            var writer = new DataWriter();
            actual.WriteTo(writer);
            Assert.Equal(expectArray, writer.DetachBuffer().ToArray());
        }
    }
}
