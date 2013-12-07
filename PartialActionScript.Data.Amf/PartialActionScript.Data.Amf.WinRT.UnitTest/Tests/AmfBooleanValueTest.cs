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
    public class AmfBooleanValueTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.AreEqual(AmfValueType.Boolean, val.ValueType);


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

            Assert.AreEqual(false, val.GetBoolean());

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
        public void GetXmlTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetXml();
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

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetNumber();
            });
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
        public void GetVectorObjectTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetVectorObject();
            });
        }

        [DataTestMethod]
        [DataRow(false, "False")]
        public void ToStringTest(bool input, string expected)
        {
            var val = AmfValue.CreateBooleanValue(input);

            Assert.AreEqual(expected.ToString(), val.ToString());

        }

        [DataTestMethod]
        [DataRow("0x02", false)]
        [DataRow("0x03",true)]
        public void Amf3ParseTest(string input, bool expected)
        {
            var actualArray = TestHelper.CreateByteArray(input);


            var actual = AmfValue.Parse(actualArray.AsBuffer(), AmfEncodingType.Amf3);

            Assert.AreEqual(expected, actual.GetBoolean());

        }

        [DataTestMethod]
        [DataRow(false, "0x02")]
        [DataRow(true,"0x03")]
        public void Amf3SequencifyTest(bool input, string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = AmfValue.CreateBooleanValue(input);


            var buffer = actual.Sequencify(AmfEncodingType.Amf3);

            CollectionAssert.AreEqual(expectArray, buffer.ToArray());

        }


        private AmfValue CreateGeneralAmfValue()
        {
            return AmfValue.CreateBooleanValue(false);
        }
    }
}