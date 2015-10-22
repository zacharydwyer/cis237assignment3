using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    class JanitorDroid : UtilityDroid
    {
        public const decimal TRASH_COMPACTOR_COST = 550;
        public const decimal VACUUM_COST = 250;
        public bool HasTrashCompactor {get; set;}
        public bool HasVacuum {get; set;}
        protected override decimal DroidCost
        {
            get
            {
                return 30000;
            }
        }

        public JanitorDroid(DroidMaterial material, DroidModel model, DroidColor color, bool hasToolbox, bool hasComputerConnection, bool hasArm, bool hasTrashCompactor, bool hasVacuum)
        : base (material, model, color, hasToolbox, hasComputerConnection, hasArm)
        {
            this.HasTrashCompactor = hasTrashCompactor;
            this.HasVacuum = hasVacuum;
            CalculateTotalCost();
        }

        public override void CalculateTotalCost()
        {
            // Have basic parts (as well as general Utility Droid parts) accounted for in total cost
            base.CalculateTotalCost();

            // Add extra costs depending on how this droid was configured.
            if (this.HasTrashCompactor) { this.TotalCost += TRASH_COMPACTOR_COST; }
            if (this.HasVacuum) { this.TotalCost += VACUUM_COST; }
        }

        public override string ToString()
        {
            return base.ToString() +
                "TRSH[" + this.HasTrashCompactor + "] " +
                "VACC[" + this.HasVacuum + "] ";
        }
    }
}
