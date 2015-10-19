using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    class Label : DrawableElement
    {
        // Labels have the same properties as DrawableElements so there's really no need to do anything further here
        public Label(EDisplaySetting displaySetting, string label)
            : base(displaySetting, label) { }

        // Again, the same as DrawableElement
        public override int Width
        {
            get
            {
                return this.Label.Length;
            }
        }

        // Labels are not selectable
        public override bool IsSelectable
        {
            get { return false; }
        }
    }
}
