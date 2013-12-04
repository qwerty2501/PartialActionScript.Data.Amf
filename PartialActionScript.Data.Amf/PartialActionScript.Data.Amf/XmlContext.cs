using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace PartialActionScript.Data.Amf
{
    internal struct XmlContext
    {
        internal XmlContext(XmlDocument document, bool newer):this()
        {
            this.Document = document;
            this.Newer = newer;
        }

        internal XmlDocument Document { get; private set; }

        internal bool Newer { get; private set; }
    }
}
