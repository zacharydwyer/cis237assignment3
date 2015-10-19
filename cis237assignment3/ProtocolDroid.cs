using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    class ProtocolDroid : Droid
    {
        // Each known language adds 150 credits to the cost
        public const int COST_PER_LANGUAGE = 150;

        // Number of languages this droid knows.
        public int NumberOfLanguages { get; set; }

        // Constructor; calls parent class Droid first, then assigns numberOfLanguages to itself
        public ProtocolDroid(Material material, DroidModel model, DroidColor color, int numberOfLanguages)
            : base(material, model, color)
        {
            this.NumberOfLanguages = numberOfLanguages;
            CalculateTotalCost();
        }

        // Overridden CalculateTotalCost method, which now takes into consideration the number of languages
        public override void CalculateTotalCost()
        {
            base.CalculateTotalCost();                                  // Call Droid's CalculateTotalCost (which sets the TotalCost to the BaseCost of the Droid)

            // Add the number of languages * cost per language value to the total cost
            TotalCost += (this.NumberOfLanguages * COST_PER_LANGUAGE);
        }


    }
}
