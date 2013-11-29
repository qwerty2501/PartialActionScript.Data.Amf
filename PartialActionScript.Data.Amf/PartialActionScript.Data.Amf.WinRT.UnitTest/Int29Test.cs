
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PartialActionScript.Data.Amf.UnitTest
{
    [TestClass]
    public class Int29Test
    {
        [TestMethod]
        public void CreateTest()
        {
            Int29 test = new Int29();

            Assert.AreEqual(0,(Int32) test);
        }

        [TestMethod]
        public void MaxValueTest()
        {
            Assert.AreEqual(268435455, Int29.MaxValue);
        }

        [TestMethod]
        public void MinValueTest()
        {
            Assert.AreEqual(-268435456, Int29.MinValue);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(268435455)]
        [DataRow(-268435456)]
        public void SetTest(Int32 input)
        {
            Int29 actual = input;

            Assert.AreEqual(input,(Int32) actual);
        }

        [DataTestMethod]
        [DataRow(268435456)]
        [DataRow(-268435457)]
        public void BadSetTest(Int32 input)
        {
            Assert.ThrowsException<OverflowException>(() =>
            {
                Int29 actual = input;
            });
            
        }

        [DataTestMethod]
        [DataRow(0, UInt29.MinValue)]
        [DataRow(-1, UInt29.MaxValue)]
        [DataRow(Int29.MaxValue, (UInt32)Int29.MaxValue)]
        public void CastTest(Int32 input,UInt32 expected)
        {
            Int29 actual = input;
            Assert.AreEqual<UInt29>(expected, (UInt29)actual);
        }

        [TestMethod]
        public void ComparisonTest()
        {
            Int29 val1 = 5;
            Int29 val2 = 5;

            Assert.AreEqual( val1 , val2);
        }

        [DataTestMethod]
        [DataRow(0, "0x00")]
        [DataRow(-1, "0xFF,0xFF,0xFF,0xFF")]
        [DataRow(268435455, "0xBF,0xFF,0xFF,0xFF")]
        [DataRow(127, "0x7F")]
        [DataRow(128, "0x81,0x00")]
        [DataRow(16383, "0xFF,0x7F")]
        [DataRow(16384, "0x81,0x80,0x00")]
        [DataRow(2097151, "0xFF,0xFF,0x7F")]
        [DataRow(2097152, "0x80,0xC0,0x80,0x00")]
        public void WriteToTest(Int32 input,string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);

            Int29 actual = input;

            var writer = new DataWriter();
            actual.WriteTo(writer);
            CollectionAssert.AreEqual(expectArray, writer.DetachBuffer().ToArray());
        }
    }
}
