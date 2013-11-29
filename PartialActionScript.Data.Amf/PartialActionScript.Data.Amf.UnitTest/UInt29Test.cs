using Xunit.Extensions;
using Xunit;
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
    public class UInt29Test
    {
        [Fact]
        public void MaxValueTest()
        {
            Assert.Equal(536870911U, UInt29.MaxValue);
        }

        [Fact]
        public void MinValueTest()
        {
            Assert.Equal(0U, UInt29.MinValue);
        }

        [Fact]
        public void CreateTest()
        {
            UInt29 target = new UInt29();
            Assert.Equal<UInt32>(0U, target);
        }
        [Theory]
        [InlineData(0U)]
        [InlineData(25U)]
        [InlineData(536870911U)]
        public void SetTest(UInt32 expect)
        {
            UInt29 target = expect;
            Assert.Equal<UInt32>(expect, target);
        }

        [Theory]
        [InlineData(536870912U)]
        [InlineData(unchecked((UInt32)(-1)))]
        public void BadSetTest(UInt32 expect)
        {

            Assert.Throws<OverflowException>(() =>
            {
                UInt29 target = expect;
            });
            
        }

        [Theory]
        [InlineData(536870911U, 1U)]
        public void BadCalcTest(UInt32 target1,UInt32 target2)
        {
            Assert.Throws<OverflowException>(() =>
            {
                UInt29 actual = target1 + target2;
            });
            
        }

        [Theory]
        [InlineData(123U, 256U, 379U)]
        public void CalcTest(UInt32 target1,UInt32 target2,UInt32 expect)
        {
            UInt29 target29 = target1;
            var actual = target29 + target2;

            Assert.Equal<UInt32>(expect, actual);
        }

        [Theory]
        [InlineData(0U, "0x00")]
        [InlineData(53U, "0x35")]
        [InlineData(268435455U, "0xBF,0xFF,0xFF,0xFF")]
        [InlineData(536870911U, "0xFF,0xFF,0xFF,0xFF")]
        [InlineData(127U, "0x7F")]
        [InlineData(128U, "0x81,0x00")]
        [InlineData(16383U, "0xFF,0x7F")]
        [InlineData(16384U, "0x81,0x80,0x00")]
        [InlineData(2097151U, "0xFF,0xFF,0x7F")]
        [InlineData(2097152U, "0x80,0xC0,0x80,0x00")]
        public void WriteToTest(UInt32 input,string expect)
        {
            UInt29 actual = input;
            var expectArray = TestHelper.CreateByteArray(expect);

            var writer = new DataWriter();
            actual.WriteTo(writer);

            Assert.Equal(expectArray, writer.DetachBuffer().ToArray());

        }

        [Theory]
        [InlineData(0U, "0x01", false)]
        [InlineData(0U, "0x00", true)]
        [InlineData(268435455U, "0xFF,0xFF,0xFF,0xFF", false)]
        [InlineData(268435455U, "0xFF,0xFF,0xFF,0xFE", true)]
        [InlineData(127U, "0x81,0x7F",false)]
        [InlineData(127U, "0x81,0x7E", true)]
        [InlineData(16383U, "0x81,0xFF,0x7F",false)]
        [InlineData(16383U, "0x81,0xFF,0x7E",true)]
        [InlineData(2097151U, "0x80,0xFF,0xFF,0xFF",false)]
        [InlineData(2097151U, "0x80,0xFF,0xFF,0xFE",true)]
        public void WriteAsRefToTest(UInt32 input, string expect, bool remaining)
        {
            UInt29 actual = input;
            var expectArray = TestHelper.CreateByteArray(expect);

            var writer = new DataWriter();
            actual.WriteAsRefTo(remaining, writer);
            var buffer = writer.DetachBuffer();

            Assert.Equal(expectArray, buffer.ToArray());
        }
        
    }
}
