using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    class UtilityDroid : Droid
    {
        /* CLASS LEVEL CONSTANTS */
        public const decimal TOOLBOX_COST = 375;
        public const decimal COMPUTER_CONNECTION_COST = 400;
        public const decimal ARM_COST = 1000;

        /* PROPERTIES */
        /// <summary>
        /// Indicates whether this droid possesses a Toolbox or not.
        /// </summary>
        public bool HasToolbox { get; set; }

        /// <summary>
        /// Indicates whether this droid possesses a Computer Connection or not.
        /// </summary>
        public bool HasComputerConnection { get; set; }

        /// <summary>
        /// Indicates whether this droid possesses a Utility Arm or not.
        /// </summary>
        public bool HasArm { get; set; }

        /// <summary>
        /// Basic cost of this droid. Factored into base cost.
        /// </summary>
        protected override decimal DroidCost 
        {
            get
            {
                return 25000;
            }
        }

        /* CONSTRUCTOR */
        public UtilityDroid(DroidMaterial material, DroidModel model, DroidColor color, bool hasToolbox, bool hasComputerConnection, bool hasArm) : base (material, model, color)
        {
            this.HasToolbox = hasToolbox;
            this.HasComputerConnection = hasComputerConnection;
            this.HasArm = hasArm;
            CalculateTotalCost();
        }

        /* METHODS */
        public override void CalculateTotalCost()
        {
            // Have basic parts accounted for in total cost
            base.CalculateTotalCost();

            // Add extra costs depending on how this droid was configured.
            if (this.HasToolbox) { this.TotalCost += TOOLBOX_COST; }
            if (this.HasComputerConnection) { this.TotalCost += COMPUTER_CONNECTION_COST; }
            if (this.HasArm) { this.TotalCost += ARM_COST; }
        }

        // Overloaded ToString method
        public override string ToString()
        {
            return base.ToString() +
                "TOOL[" + this.HasToolbox + "] " +
                "COMP[" + this.HasComputerConnection + "] " +
                "ARM[" + this.HasArm + "] ";
        }
    }
}
