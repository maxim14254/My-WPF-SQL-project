using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace НФП
{
   static class NumberExtensions
    {
        public static bool Between(this int value, int startInclusive, int endExclusive)
        {
            return value >= startInclusive && value <= endExclusive;
        }
        public static bool Between1(this double value, double startInclusive, double endExclusive)
        {
            return value >= startInclusive && value <= endExclusive;
        }
    }
}
