using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    // Abstract class of Droid; implements IDroid
    // Represents a general droid
    abstract class Droid : IDroid
    {
        public enum DroidModel {PROTOCOL, UTILITY, JANITOR, ASTROMECH}
        public enum DroidColor {WHITE, GRAY, SILVER, RED, GREEN, BLUE, SLATE, BLACK}
        
        // Every droid is made of a material, has a model, has a color, and has a base cost (which is determined by the material's cost)
        public Material Material { get; set; }
        public DroidModel Model { get; set; }
        public DroidColor Color { get; set; }
        public decimal BaseCost {
            get
            {
                return GetBaseCost();
            }
        }

        // For a generic Droid, it's total cost is just the base cost
        public decimal TotalCost { get; set; }

        // Constructor - sets material, model and color
        public Droid(Material material, DroidModel model, DroidColor color)
        {
            this.Material = material;
            this.Model = model;
            this.Color = color;
            CalculateTotalCost();
        }

        // Called by BaseCost every time it is accessed, so BaseCost is really the same thing as Material.MaterialCost
        public decimal GetBaseCost()
        {
            return this.Material.MaterialCost;
        }

        // Called once in the constructor - adds (sets at least) BaseCost to TotalCost
        public virtual void CalculateTotalCost()
        {
            this.TotalCost = this.BaseCost;
        }

        // Prints out stats for this Droid
        public override string ToString()
        {
            return "MTRL: " + this.Material.MaterialName + "  " +
                "MODL: " + this.Model + "  " +
                "COLR: " + this.Color + "  " +
                "BASE: " + this.BaseCost + " CREDITS";
        }
    }
}
