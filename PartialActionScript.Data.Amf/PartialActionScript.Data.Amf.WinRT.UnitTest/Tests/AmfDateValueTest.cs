using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.IO;

namespace PartialActionScript.Data.Amf.UnitTest.Tests
{
    [TestClass]
    public class AmfDateValueTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = createGeneralAmfValue();
            Assert.AreEqual(AmfValueType.Date, val.ValueType);


        }

        [TestMethod]
        public void GetStringTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetString();
            });
        }

        [TestMethod]

        public void GetBooleanTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetBoolean();
            });

        }

        [TestMethod]
        public void GetArrayTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetArray();
            });

        }

        [TestMethod]
        public void GetDateTest()
        {
            var val = createGeneralAmfValue();

            Assert.AreEqual(default(DateTimeOffset), val.GetDate());

        }

        [TestMethod]
        public void GetXmlTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetXml();
            });
        }

        [TestMethod]
        public void GetByteArrayTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetByteArray();
            });

        }

        [TestMethod]
        public void GetNumberTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetNumber();
            });
        }

        [TestMethod]
        public void GetObjectTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetObject();
            });
        }

        [TestMethod]
        public void GetVectorIntTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorInt();
            });
        }

        [TestMethod]
        public void GetVectorUIntTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorUInt();
            });
        }

        [TestMethod]
        public void GetVectorDoubleTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorDouble();
            });
        }



        [TestMethod]
        public void GetVectorObjectTest()
        {
            var val = createGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorObject();
            });
        }

        [TestMethod]
        public void ToStringTest()
        {
            var expected = default(DateTimeOffset);
            var val = AmfValue.CreateDateValue(expected);

            Assert.AreEqual(expected.ToString(), val.ToString());

        }

        [DataTestMethod]
        [DataRow("0x8,0x1,0x42,0x73,0xcd,0x50,0x47,0x29,0xa0,0x0", 2013, 2, 14, 4, 25, 4, 26)]
        public void Amf3ParseTest(string input, int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            var actualArray = TestHelper.CreateByteArray(input);


            var actual = AmfValue.Parse(actualArray.AsBuffer(), AmfEncodingType.Amf3);

            var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Local);
            var expected = new DateTimeOffset(dateTime);
            Assert.AreEqual(expected, actual.GetDate());

        }

        [DataTestMethod]
        [DataRow(2013, 2, 14, 4, 25, 4, 26, "0x8,0x1,0x42,0x73,0xcd,0x50,0x47,0x29,0xa0,0x0")]
        public void Amf3SequencifyTest(int year,int month,int day,int hour,int minute,int second,int millisecond, string expect)
        {
            
            var expectArray = TestHelper.CreateByteArray(expect);

         
            var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Local);
            var actual = AmfValue.CreateDateValue(new DateTimeOffset(dateTime));


            var buffer = actual.Sequencify(AmfEncodingType.Amf3);
            var actualArray = buffer.ToArray();
            CollectionAssert.AreEqual(expectArray, actualArray);

        }


        private AmfValue createGeneralAmfValue()
        {
            return AmfValue.CreateDateValue(default(DateTimeOffset));
        }
    }
}