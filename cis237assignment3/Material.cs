using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    // Represents a material that has a name and a cost (with regards to the amount needed to build a droid)
    class Material
    {
        public string MaterialName { get; set; }
        public decimal MaterialCost { get; set; }

        public Material(string materialName, decimal materialCost)
        {
            this.MaterialName = materialName;
            this.MaterialCost = materialCost;
        }

        public override string ToString()
        {
            // Return name and cost in credits
            return this.MaterialName + "(" + this.MaterialCost + " C)";
        }
    }
    
}
