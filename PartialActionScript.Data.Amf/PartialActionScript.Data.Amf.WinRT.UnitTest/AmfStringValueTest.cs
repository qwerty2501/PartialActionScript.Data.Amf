using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.IO;

namespace PartialActionScript.Data.Amf.UnitTest
{
    [TestClass]
    public class AmfStringValueTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.AreEqual(AmfValueType.String, val.ValueType);

            
        }

        [DataTestMethod]
        [DataRow("test")]
        public void GetStringTest(string expected)
        {
            var val = AmfValue.CreteStringValue(expected);
            var actual = val.GetString();

            Assert.AreEqual(expected, actual);
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

            Assert.ThrowsException < InvalidOperationException>(() =>
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
        [DataRow("test","test")]
        public void ToStringTest(string input,string expected)
        {
            var val = AmfValue.CreteStringValue(input);

            Assert.AreEqual(input, val.ToString());
            
        }

        [DataTestMethod]
        [DataRow("0x06,0x09,0x74,0x65,0x73,0x74", "test")]
        public void Amf3LoadFromStreamAsyncTest(string input,string expected)
        {
            var actualArray = TestHelper.CreateByteArray(input);


            var stream = new MemoryStream();
            stream.Write(actualArray,0,actualArray.Length);
            
            stream.Flush();
            stream.Position = 0;
            var inputStream = stream.AsInputStream();

            var actual = AmfValue.LoadFromStreamAsync(inputStream, AmfEncodingType.Amf3).AsTask().GetAwaiter().GetResult();

            Assert.AreEqual(expected, actual.GetString());

        }

        [DataTestMethod]
        [DataRow("test", "0x06,0x09,0x74,0x65,0x73,0x74")]
        [DataRow("c", "0x06,0x03,0x63")]
        public void Amf3SaveToStreamAsyncTest(string input,string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = AmfValue.CreteStringValue(input);
            var stream = new MemoryStream();
            actual.SaveToStreamAsync(stream.AsOutputStream(), AmfEncodingType.Amf3).GetResults();
            var buffer = stream.ToArray().AsBuffer();

            CollectionAssert.AreEqual(expectArray, buffer.ToArray());

        }


        private AmfValue CreateGeneralAmfValue()
        {
            return AmfValue.CreteStringValue("");
        }
    }
}
