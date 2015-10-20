using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    class AstromechDroid : UtilityDroid
    {
        /* CONSTANTS ASSOCIATED WITH COSTS */
        public const decimal FIRE_EXTINGUISHER_COST = 75;
        public const decimal COST_PER_SHIP = 1000;             // The number of ships this astromech will work on? Owned ships?

        /* PROPERTIES */
        public bool HasFireExtinguisher { get; set; }
        public int NumberOfShips { get; set; }

        /* CONSTRUCTOR */
        public AstromechDroid(DroidMaterial material, DroidModel model, DroidColor color, bool hasToolbox, bool hasComputerConnection, bool hasArm, bool hasFireExtinguisher, int numberOfShips)
            : base (material, model, color, hasToolbox, hasComputerConnection, hasArm)
        {
            this.NumberOfShips = numberOfShips;
            this.HasFireExtinguisher = hasFireExtinguisher;
        }

        /* METHODS */
        public override void CalculateTotalCost()
        {
            // Have basic parts of a Utility Droid accounted for in total cost
            base.CalculateTotalCost();

            // Add extra costs depending on how this droid was configured.
            if (this.HasFireExtinguisher) { this.TotalCost += FIRE_EXTINGUISHER_COST; }
            this.TotalCost += (this.NumberOfShips * COST_PER_SHIP);
        }

        // Overloaded ToString method
        public override string ToString()
        {
            return base.ToString() +
                "FIRE[" + this.HasFireExtinguisher + "] " +
                "SHIP[" + this.NumberOfShips + "] ";
        }
    }
}
