using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    // A spacer represents a blank line (or more, with AdditionalSpaces) in a layout of drawable elements.
    class Spacer : DrawableElement
    {
        public int AdditionalLines { get; set; }

        // Constructor (empty - all spacers are BLOCK and have no content)
        public Spacer() : base(EDisplaySetting.BLOCK, "") { }

        // Constructor - additional space
        public Spacer(int additionalLines) :
            base(EDisplaySetting.BLOCK, "")
        {
            this.AdditionalLines = additionalLines;
        }

        // Returns width of the spacer
        public override int Width
        {
            get
            {
                // The width of a spacer is essentially the entire window width. -2 in case it moves the scrollbar a little over by accident. 
                // Width isn't even accounted for when this element is drawn since its a BLOCK element anyway
                return Console.WindowWidth - 2;
            }
        }

        // Spacers are not selectable
        public override bool IsSelectable
        {
            get { return false; }
        }
    }
}
