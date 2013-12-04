using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.IO;
using Windows.Data.Xml.Dom;

namespace PartialActionScript.Data.Amf.UnitTest
{
    [TestClass]
    public class AmfXmlValueTest
    {
        [TestMethod]
        public void CreateTest()
        {
            var val = createGeneralAmfValue();
            Assert.AreEqual(AmfValueType.Xml, val.ValueType);


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
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                val.GetDate();
            });

        }

        [TestMethod]
        public void GetXmlTest()
        {
            var xml = new XmlDocument();
            xml.LoadXml("<test></test>");
            var val = AmfValue.AsAmfXmlValue(xml);

            Assert.AreEqual(xml, val.GetXml());
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

        [DataTestMethod]
        [DataRow("<root></root>", "Windows.Data.Xml.Dom.XmlDocument")]
        public void ToStringTest(string input, string expected)
        {
            var xml = convertXml(input);
            var val = AmfValue.AsAmfXmlValue(xml);

            Assert.AreEqual(expected.ToString(), val.ToString());

        }

        [DataTestMethod]
        [DataRow("3", "<root></root>")]
        public void Amf3ParseTest(string input, string expected)
        {
            var actualArray = TestHelper.CreateByteArray(input);


            var actual = AmfValue.Parse(actualArray.AsBuffer(), AmfEncodingType.Amf3);

            Assert.AreEqual(expected, actual.GetXml());

        }

        [DataTestMethod]
        [DataRow("<root></root>", "3")]
        public void Amf3SequencifyTest(string input, string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = AmfValue.AsAmfXmlValue(convertXml(input));


            var buffer = actual.Sequencify(AmfEncodingType.Amf3);

            CollectionAssert.AreEqual(expectArray, buffer.ToArray());

        }


        private AmfValue createGeneralAmfValue()
        {
            return AmfValue.AsAmfXmlValue(convertXml("<root></root>"));
        }

        private XmlDocument convertXml(string input)
        {
            var xml = new XmlDocument();
            xml.LoadXml(input);
            return xml;
        }
    }
}