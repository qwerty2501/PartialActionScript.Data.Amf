using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace PartialActionScript.Data.Amf
{
    internal struct XmlContextValue
    {
        internal XmlContextValue(XmlDocument document, bool newer)
        {
            this.Document = document;
            this.Newer = newer;
        }

        internal XmlDocument Document { get; private set; }

        internal bool Newer { get; private set; }
    }
}
