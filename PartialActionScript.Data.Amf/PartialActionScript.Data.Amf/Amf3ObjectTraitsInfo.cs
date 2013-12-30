using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal struct Amf3ObjectTraitsInfo :IEquatable<Amf3ObjectTraitsInfo>
    {
        

        internal Amf3ObjectTraitsInfo(string typeName, IEnumerable<string> traits,bool isDynamic):this()
        {
            this.TypeName = typeName;
            this.Traits = traits;
            this.IsDynamic = isDynamic;
        }

        internal string TypeName { get; private set; }

        internal IEnumerable<string> Traits { get; private set; }

        internal bool IsDynamic { get; private set; }



        public bool Equals(Amf3ObjectTraitsInfo other)
        {
            return this.TypeName.Equals(other.TypeName) && this.Traits.SequenceEqual(other.Traits);
        }
    }
}
