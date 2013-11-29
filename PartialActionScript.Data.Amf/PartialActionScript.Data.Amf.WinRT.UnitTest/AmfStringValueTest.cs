using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PartialActionScript.Data.Amf.UnitTest
{
   
    public class AmfStringValueTest
    {
        [Fact]
        public void CreateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.Equal(AmfValueType.String, val.ValueType);

            
        }

        [Theory]
        [InlineData("test")]
        public void GetStringTest(string expected)
        {
            var val = AmfValue.CreteStringValue(expected);
            var actual = val.GetString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBooleanTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetBoolean();
            });
            
        }

        [Fact]
        public void GetArrayTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetArray();
            });
            
        }

        [Fact]
        public void GetDateTest()
        {
            var val = CreateGeneralAmfValue();
            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetDate();
            });
            
        }

        [Fact]
        public void GetByteArrayTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws < InvalidOperationException>(() =>
            {
                val.GetByteArray();
            });
            
        }

        [Fact]
        public void GetNumberTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetNumber();
            });
        }

        [Fact]
        public void GetObjectTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetObject();
            });
        }

        [Fact]
        public void GetVectorIntTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetVectorInt();
            });
        }

        [Fact]
        public void GetVectorUIntTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetVectorUInt();
            });
        }

        [Fact]
        public void GetVectorDoubleTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetVectorDouble();
            });
        }

        [Fact]
        public void GetVectorObjectTest()
        {
            var val = CreateGeneralAmfValue();

            Assert.Throws<InvalidOperationException>(() =>
            {
                val.GetVectorObject();
            });
        }

        [Theory]
        [InlineData("test","test")]
        public void ToStringTest(string input,string expected)
        {
            var val = AmfValue.CreteStringValue(input);

            Assert.Equal(input, val.ToString());
            
        }

        [Theory]
        [InlineData("test", "0x06,0x09,0x74,0x65,0x73,0x74")]
        [InlineData("c", "0x06,0x03,0x63")]
        public void Amf3SequencifyTest(string input,string expect)
        {
            var expectArray = TestHelper.CreateByteArray(expect);
            var actual = AmfValue.CreteStringValue(input);
            var writer = new DataWriter();
            actual.WriteTo(writer,AmfEncodingType.Amf3);
            var buffer = writer.DetachBuffer();

            Assert.Equal(expectArray, buffer.ToArray());

        }


        private AmfValue CreateGeneralAmfValue()
        {
            return AmfValue.CreteStringValue("");
        }
    }
}
