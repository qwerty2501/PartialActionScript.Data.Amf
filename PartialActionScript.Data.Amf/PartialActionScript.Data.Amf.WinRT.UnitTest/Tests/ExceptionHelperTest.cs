using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace PartialActionScript.Data.Amf.UnitTest.Tests
{
    [TestClass]
    public class ExceptionHelperTest
    {
        [TestMethod]
        public void CreateInvalidTypeExceptionTest()
        {
            var exception = ExceptionHelper.CreateInvalidTypeException();
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("Invalid Amf type.", exception.Message);
            
            
        }

        
        [DataTestMethod]
        [DataRow(UInt29.MaxRemainingValue + 1)]
        [DataRow(UInt29.MaxRemainingValue + 2)]
        public void CreateInvalidRemainingValueExceptionTest(UInt32 input)
        {
            var exception = ExceptionHelper.CreateInvalidRemainingValueException(input);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual(string.Format("{0} is invalid remaining UInt29 type value.", input), exception.Message);
        }

        [TestMethod]
        public void CreateInvalidOperationStringValueTooLongTest()
        {
            var exception = ExceptionHelper.CreateInvalidOperationStringValueTooLong("test");
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
            Assert.AreEqual("String length is too long.", exception.Message);
        }

        [TestMethod]
        public void CreateOutOfUInt29ExceptionTest()
        {
            var exception = ExceptionHelper.CreateOutOfUInt29Exception(UInt29.MaxValue + 1);
            Assert.IsInstanceOfType(exception, typeof(OverflowException));
            Assert.AreEqual(UInt29.MaxValue + 1 + " is out of UInt29 type value.", exception.Message);
        }

        [TestMethod]
        public void CreateOutOfStringRemainIndexExceptionTest()
        {
            var exception = ExceptionHelper.CreateOutOfStringRemainLengthException();
            Assert.IsInstanceOfType(exception, typeof(IndexOutOfRangeException));
            Assert.AreEqual("Out of String remaining table length.", exception.Message);
        }
    }
}
