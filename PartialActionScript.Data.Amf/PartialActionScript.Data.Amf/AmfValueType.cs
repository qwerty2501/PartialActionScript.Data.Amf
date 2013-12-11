using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    public enum AmfValueType
    {
        Undefined,
        Null,
        Boolean,
        Number,
        String,
        Date,
        Xml,
        ByteArray,
        VectorInt,
        VectorUInt,
        VectorDouble,
        VectorObject,
        Object,
        EcmaArray,
        StrictArray,
        Dictionary
    }
}
