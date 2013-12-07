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
    public class AmfNumberValueTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.AreEqual(AmfValueType.Number, val.ValueType);


        }

        [TestMethod]
        public void GetStringTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetString();
            });
        }

        [TestMethod]

        public void GetBooleanTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetBoolean();
            });

        }

        [TestMethod]
        public void GetArrayTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetArray();
            });

        }

        [TestMethod]
        public void GetDateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetDate();
            });

        }

        [TestMethod]
        public void GetByteArrayTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetByteArray();
            });

        }

        [TestMethod]
        public void GetNumberTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.AreEqual(5.5, val.GetNumber());
        }

        [TestMethod]
        public void GetObjectTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetObject();
            });
        }

        [TestMethod]
        public void GetVectorIntTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorInt();
            });
        }

        [TestMethod]
        public void GetVectorUIntTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorUInt();
            });
        }

        [TestMethod]
        public void GetVectorDoubleTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorDouble();
            });
        }

        [TestMethod]
        public void GetXmlTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetXml();
            });
        }

        [TestMethod]
        public void GetVectorObjectTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorObject();
            });
        }

        [DataTestMethod]
        [DataRow(5, "5")]
        public void ToStringTest(double input, string expected)
        {
            var val = AmfValue.CreteNumberValue(input);

            Assert.AreEqual(expected.ToString(), val.ToString());

        }

        [DataTestMethod]
        [DataRow("0x4,0x0", 0)]
        public void Amf3ParseTest(string input, double expected)
        {
            var actualArray = TestHelper.CreateByteArray(input);


            var actual = AmfValue.Parse(actualArray.AsBuffer(), AmfEncodingType.Amf3);

            Assert.AreEqual(expected, actual.GetNumber());

        }

        [DataTestMethod]
        [DataRow(0, "0x4,0x0")]
        public void Amf3SequencifyTest(double input, string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = AmfValue.CreteNumberValue(input);


            var buffer = actual.Sequencify(AmfEncodingType.Amf3);

            CollectionAssert.AreEqual(expectArray, buffer.ToArray());

        }


        private AmfValue CreateGeneralAmfValue()
        {
            return AmfValue.CreteNumberValue(5.5);
        }
    }
}