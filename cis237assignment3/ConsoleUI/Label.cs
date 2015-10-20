using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Label : UIElement
    {
        // Labels are the most basic type of UI element and share the most basic properties with its parent class, so there's really no need to do anything further here
        public Label(EDisplaySetting displaySetting, string label)
            : base(displaySetting, label) { }

        // A Label's width is determined by it's title.
        public override int Width
        {
            get
            {
                return this.Title.Length;
            }
        }

        // Labels are not selectable - they're just plain ol' text. They don't need to be interacted with.
        // Note: if you want to interact with a label, make a button instead.
        public override bool IsSelectable
        {
            get { return false; }
        }
    }
}
