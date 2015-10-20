using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    static class DroidCollection
    {
        // The list of droids.
        public static List<Droid> DroidList = new List<Droid>();

        // Add a droid to the list
        public static void Add(Droid droid)
        {
            DroidList.Add(droid);
        }
    }
}
