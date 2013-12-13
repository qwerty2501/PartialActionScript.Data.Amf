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
    public class AmfArrayTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = createGeneralAmfValue();
            Assert.AreEqual(AmfValueType.EcmaArray, val.ValueType);
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

            
            var array =  val.GetArray();

            Assert.AreEqual(array[3].GetBoolean(), false);
            Assert.AreEqual(array["t"].GetNumber(), 50);
        }

        [TestMethod]
        public void GetDateTest()
        {
            var val = createGeneralAmfValue();
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetDate();
            });

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
            var val = createGeneralAmfValue();

            Assert.AreEqual("False,50", val.ToString());

        }

        [DataTestMethod]
        [DataRow("0x9,0x1,0x3,0x33,0x2,0x3,0x74,0x4,0x32,0x1")]
        public void Amf3ParseTest(string input)
        {
            var actualArray = TestHelper.CreateByteArray(input);

            var expect =(AmfArray) this.createGeneralAmfValue();
            var value = AmfValue.Parse(actualArray.AsBuffer(), AmfEncodingType.Amf3);
            var actual = value.GetArray();

            Assert.AreEqual(expect[3].GetBoolean(), actual[3].GetBoolean());

            Assert.AreEqual(expect["t"].GetNumber(), actual["t"].GetNumber());
           

        }

         

        [DataTestMethod]
        [DataRow("0x9,0x1,0x3,0x33,0x2,0x3,0x74,0x4,0x32,0x1")]
        public void Amf3SequencifyTest( string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = createGeneralAmfValue();


            var buffer = actual.Sequencify(AmfEncodingType.Amf3).ToArray();

            CollectionAssert.AreEqual(expectArray, buffer);

        }

        [DataTestMethod]
        [DataRow("0x9,0x5,0x1,0x6,0x9,0x6f,0x6f,0x6f,0x6f,0x6,0x0")]
        public void Amf3SequencifyRemained(string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = new AmfArray();
            actual[0] = AmfValue.CreateStringValue("oooo");
            actual[1] = AmfValue.CreateStringValue("oooo");

            var buffer = actual.Sequencify(AmfEncodingType.Amf3);
            var actualArray = buffer.ToArray();
            CollectionAssert.AreEqual(expectArray, actualArray);
        }


        private IAmfValue createGeneralAmfValue()
        {
            var array = new AmfArray();
            array.Add(3, AmfValue.CreateBooleanValue(false));
            array.Add("t", AmfValue.CreateNumberValue(50));
            return array;
        }
    }
}