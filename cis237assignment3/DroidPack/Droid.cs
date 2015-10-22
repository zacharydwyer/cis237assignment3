using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroidPack
{
    /// <summary>
    /// Abstract class representing a generic, base droid.
    /// </summary>
    abstract class Droid : IDroid
    {
        /* ENUMS */
        /// <summary>
        /// Models of droid that can be selected from.
        /// </summary>
        public enum DroidModel {PROTOCOL, UTILITY, JANITOR, ASTROMECH}

        /// <summary>
        /// Droid colors that can be selected from.
        /// </summary>
        public enum DroidColor {WHITE, GRAY, SILVER, RED, GREEN, BLUE, SLATE, BLACK}

        /// <summary>
        /// Represents a Material and its associated cost
        /// </summary>
        public enum DroidMaterial : int
        {
            AGRINIUM = 1000,
            BONDITE = 6000,
            CRODIUM = 1200,
            DURALIUM = 3000,
            DURAPLAST = 2500,
            GENTENTHIUM = 400,
            IRON = 800,
            OSMIUM = 1500,
            PLATINUM = 1970000,
            PYRONIUM = 900000,
            QUADANIUM = 1700,
            RHODIUM = 1300,
            TITANIUM = 1800,
            TRICOPPER = 700,
            ULTRACHROME = 2400
        }
        
        /* PROPERTIES */
        /// <summary>
        /// Type of Material this droid is made out of. Material also has an associated cost.
        /// </summary>
        public DroidMaterial Material { get; set; }

        /// <summary>
        /// Type of model this droid is. This information could also be obtained through type-checking.
        /// </summary>
        public DroidModel Model { get; set; }

        /// <summary>
        /// The color of this droid.
        /// </summary>
        public DroidColor Color { get; set; }

        /// <summary>
        /// The base cost of this droid.
        /// </summary>
        public decimal BaseCost
        {
            get
            {
                return GetBaseCost();
            }
        }

        /// <summary>
        /// The total cost of this droid. Is set when this program runs.
        /// </summary>
        public decimal TotalCost { get; set; }

        /// <summary>
        /// The basic cost of building this droid, associated with the type of droid (protocol, utility, etc.)
        /// </summary>
        /// Each droid must specify what their DroidCost is.
        protected abstract decimal DroidCost { get; }

        /* CONSTRUCTOR */
        public Droid(DroidMaterial material, DroidModel model, DroidColor color)
        {
            this.Material = material;
            this.Model = model;
            this.Color = color;
            CalculateTotalCost();
        }

        /* METHODS */
        /// <summary>
        /// Returns the base cost of this droid, which is the cost of the material to build it. 
        /// Should only be called internally (must be made public due to IDroid constraints).
        /// </summary>
        /// <returns>Base cost of droid. Please don't use this.</returns>
        public virtual decimal GetBaseCost()
        {
            return (int) this.Material;
        }

        /// <summary>
        /// Calculates the total cost of this droid. 
        /// Should only be called internally (must be made public due to IDroid constraints).
        /// </summary>
        public virtual void CalculateTotalCost()
        {
            this.TotalCost = this.BaseCost;
        }

        /// <summary>
        /// Returns information about this Droid.
        /// </summary>
        public override string ToString()
        {
            return "MTRL[" + this.Material+ "] " +
                "MODL[" + this.Model.ToString() + "] " +
                "COLR[" + this.Color.ToString() + "] " +
                "BASE[" + this.BaseCost + "] ";
        }
    }
}
