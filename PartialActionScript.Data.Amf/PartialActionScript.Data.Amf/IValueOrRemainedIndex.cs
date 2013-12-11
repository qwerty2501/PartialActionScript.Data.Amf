using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartialActionScript.Data.Amf
{
    internal interface IValueOrRemainedIndex<T>
    {
        bool IsRemained { get; }

        T Value { get; }

        int RemainedIndex { get; }

    }
}
