using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    public  enum  Amf3Type
    {
         Undefined = 0x00,
         Null = 0x01,
         False = 0x02,
         True = 0x03,
         Integer = 0x04,
         Double = 0x05,
         String = 0x06,
         XmlDocument = 0x07,
         Date = 0x08,
         Array = 0x09,
         Object = 0x0a,
         Xml = 0x0b,
         ByteArray = 0x0c,
         VectorInt = 0x0d,
         VectorUInt = 0x0e,
         VectorDouble = 0x0f,
         VectorObject = 0x10,
         Dictionary = 0x11,
         UnInitialized = 256
    }
}
