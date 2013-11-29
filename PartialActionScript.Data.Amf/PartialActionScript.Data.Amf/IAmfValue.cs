using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage.Streams;

namespace PartialActionScript.Data.Amf
{
    public interface IAmfValue
    {
        AmfValueType ValueType { get; }

        bool GetBoolean();

        AmfArray GetArray();

        string GetString();

        AmfObject GetObject();

        DateTimeOffset GetDate();

        IReadOnlyList<byte> GetByteArray();

        IReadOnlyList<int> GetVectorInt();

        IReadOnlyList<uint> GetVectorUInt();

        IReadOnlyList<double> GetVectorDouble();

        IReadOnlyList<IAmfValue> GetVectorObject();

        double GetNumber();

        void WriteTo(IDataWriter writer, AmfEncodingType encodingType);

        
    }
}
