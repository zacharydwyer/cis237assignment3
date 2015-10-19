using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    abstract class DrawableElement
    {
        // enum saying that this element should be on its own line (block) or it doesn't matter (inline)
        public enum EDisplaySetting { BLOCK, INLINE }

        // Every drawable element must state whether it is selectable or not. 
        public abstract bool IsSelectable
        {
            get;
        }

        // Whether this element wants to be displayed on its own line (block) or near others if possible (inline)
        public EDisplaySetting DisplaySetting { get; set; }

        // Main text of this element
        public string Label { get; set; }

        // Returns the width of the element - must be implemented in child classes
        public abstract int Width
        {
            get;
        }

        // Constructor
        public DrawableElement(EDisplaySetting displaySetting, string label)
        {
            this.DisplaySetting = displaySetting;
            this.Label = label;
        }
    }
}
