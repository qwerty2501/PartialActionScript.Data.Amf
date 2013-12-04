using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
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

        XmlDocument GetXml();

        IReadOnlyList<byte> GetByteArray();

        IReadOnlyList<int> GetVectorInt();

        IReadOnlyList<uint> GetVectorUInt();

        IReadOnlyList<double> GetVectorDouble();

        IReadOnlyList<IAmfValue> GetVectorObject();

        double GetNumber();

        IBuffer Sequencify(AmfEncodingType encodingType);

        
    }
}
