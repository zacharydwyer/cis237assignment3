using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    /// <summary>
    /// Class representing a protocol droid, which knows languages.
    /// </summary>
    class ProtocolDroid : Droid
    {
        /// <summary>
        /// How much it costs for a droid to know a new language.
        /// </summary>
        public const int COST_PER_LANGUAGE = 150;

        /// <summary>
        /// Number of languages this droid knows.
        /// </summary>
        public int NumberOfLanguages { get; set; }

        // Basic cost of this droid, factored into basecost.
        protected override decimal DroidCost
        {
            get
            {
                return 15000;
            }
        }

        // Constructor
        public ProtocolDroid(DroidMaterial material, DroidModel model, DroidColor color, int numberOfLanguages) : base(material, model, color)
        {
            this.NumberOfLanguages = numberOfLanguages;
            CalculateTotalCost();
        }

        // Overridden CalculateTotalCost method
        public override void CalculateTotalCost()
        {
            // Have basic parts accounted for in total cost
            base.CalculateTotalCost();

            // Add extra costs depending on how this droid was configured.
            this.TotalCost += this.DroidCost + (this.NumberOfLanguages * COST_PER_LANGUAGE);
        }

        // Overriden ToString method
        public override string ToString()
        {
            return base.ToString() +
                "LANG[" + this.NumberOfLanguages + "] ";
        }
    }
}
