using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PartialActionScript.Data.Amf.ResourceTest
{
    [TestClass]
    public class ExceptionHelperTest
    {
        [TestMethod]
        public void CreateInvalidTypeExceptionTest()
        {
            var exception = ExceptionHelper.CreateInvalidTypeException(AmfValueType.Boolean);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("Boolean型のAmfデータではこの操作を行えません", exception.Message);
            
            
        }

        
        [DataTestMethod]
        [DataRow(UInt29.MaxRemainingValue + 1)]
        [DataRow(UInt29.MaxRemainingValue + 2)]
        public void CreateInvalidRemainingValueExceptionTest(UInt32 input)
        {
            var exception = ExceptionHelper.CreateInvalidRemainingValueException(input);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual(string.Format("{0}は無効な参照UInt29型の値です", input), exception.Message);
        }

        [TestMethod]
        public void CreateInvalidOperationStringValueTooLongTest()
        {
            var exception = ExceptionHelper.CreateInvalidOperationStringValueTooLong("test");
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("文字列の長さ4は長すぎます", exception.Message);
        }

        [TestMethod]
        public void CreateOutOfUInt29ExceptionTest()
        {
            var exception = ExceptionHelper.CreateOutOfUInt29Exception(UInt29.MaxValue + 1);
            Assert.IsInstanceOfType(exception, typeof(OverflowException));
            Assert.AreEqual(UInt29.MaxValue + 1 + "はUInt29型を超える値です", exception.Message);
        }

        [TestMethod]
        public void CreateOutOfStringRemainIndexExceptionTest()
        {
            var exception = ExceptionHelper.CreateOutOfStringRemainLengthException();
            Assert.IsInstanceOfType(exception, typeof(IndexOutOfRangeException));
            Assert.AreEqual("String参照テーブルの最大長を超えています", exception.Message);
        }
    }
}
