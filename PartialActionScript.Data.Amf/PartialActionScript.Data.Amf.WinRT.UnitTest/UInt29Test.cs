
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
    public class UInt29Test
    {
        [TestMethod]
        public void MaxValueTest()
        {
            Assert.AreEqual(536870911U, UInt29.MaxValue);
        }

        [TestMethod]
        public void MinValueTest()
        {
            Assert.AreEqual(0U, UInt29.MinValue);
        }

        [TestMethod]
        public void CreateTest()
        {
            UInt29 target = new UInt29();
            Assert.AreEqual<UInt32>(0U, target);
        }
        [DataTestMethod]
        [DataRow(0U)]
        [DataRow(25U)]
        [DataRow(536870911U)]
        public void SetTest(UInt32 expect)
        {
            UInt29 target = expect;
            Assert.AreEqual<UInt32>(expect, target);
        }

        [DataTestMethod]
        [DataRow(536870912U)]
        [DataRow(unchecked((UInt32)(-1)))]
        public void BadSetTest(UInt32 expect)
        {

            Assert.ThrowsException<OverflowException>(() =>
            {
                UInt29 target = expect;
            });
            
        }

        [DataTestMethod]
        [DataRow(536870911U, 1U)]
        public void BadCalcTest(UInt32 target1,UInt32 target2)
        {
            Assert.ThrowsException<OverflowException>(() =>
            {
                UInt29 actual = target1 + target2;
            });
            
        }

        [DataTestMethod]
        [DataRow(123U, 256U, 379U)]
        public void CalcTest(UInt32 target1,UInt32 target2,UInt32 expect)
        {
            UInt29 target29 = target1;
            var actual = target29 + target2;

            Assert.AreEqual<UInt32>(expect, actual);
        }

        [DataTestMethod]
        [DataRow(0U, "0x00")]
        [DataRow(53U, "0x35")]
        [DataRow(268435455U, "0xBF,0xFF,0xFF,0xFF")]
        [DataRow(536870911U, "0xFF,0xFF,0xFF,0xFF")]
        [DataRow(127U, "0x7F")]
        [DataRow(128U, "0x81,0x00")]
        [DataRow(16383U, "0xFF,0x7F")]
        [DataRow(16384U, "0x81,0x80,0x00")]
        [DataRow(2097151U, "0xFF,0xFF,0x7F")]
        [DataRow(2097152U, "0x80,0xC0,0x80,0x00")]
        public void WriteToTest(UInt32 input,string expect)
        {
            UInt29 actual = input;
            var expectArray = TestHelper.CreateByteArray(expect);

            var writer = new DataWriter();
            actual.WriteTo(writer);

            CollectionAssert.AreEqual(expectArray, writer.DetachBuffer().ToArray());

        }

        [DataTestMethod]
        [DataRow(0U, "0x01", false)]
        [DataRow(0U, "0x00", true)]
        [DataRow(268435455U, "0xFF,0xFF,0xFF,0xFF", false)]
        [DataRow(268435455U, "0xFF,0xFF,0xFF,0xFE", true)]
        [DataRow(127U, "0x81,0x7F",false)]
        [DataRow(127U, "0x81,0x7E", true)]
        [DataRow(16383U, "0x81,0xFF,0x7F",false)]
        [DataRow(16383U, "0x81,0xFF,0x7E",true)]
        [DataRow(2097151U, "0x80,0xFF,0xFF,0xFF",false)]
        [DataRow(2097151U, "0x80,0xFF,0xFF,0xFE",true)]
        public void WriteAsRefToTest(UInt32 input, string expect, bool remaining)
        {
            UInt29 actual = input;
            var expectArray = TestHelper.CreateByteArray(expect);

            var writer = new DataWriter();
            actual.WriteAsRefTo(remaining, writer);
            var buffer = writer.DetachBuffer();

            CollectionAssert.AreEqual(expectArray, buffer.ToArray());
        }
        
    }
}
