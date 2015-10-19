using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public delegate void ClickMethod();

namespace cis237assignment3
{
    // A button is essentially a selectable label.
    class Button : DrawableElement
    {
        // Buttons are selectable. 
        public override bool IsSelectable
        {
            get { return true; }
        }

        // Constructor
        public Button(EDisplaySetting displaySetting, string label) : base (displaySetting, label) { }

        // Overloaded constructor
        public Button(EDisplaySetting displaySetting, string label, ClickMethod onClickMethod) : this (displaySetting, label)
        {
            this.OnClick = onClickMethod;
        }

        // The width of a button is just the length of its label
        public override int Width
        {
            get { return this.Label.Length; }
        }

        // Get or set the method that is called when this button's PerformClick method is called
        public ClickMethod OnClick { get; set; }

        public void PerformClick() 
        {
            // If this method has a click performed on it, and nothing happens...
            if (OnClick == null)
            {
                // Tell debug about it
                Debug.WriteLine("Button" + this.ToString() + " was clicked, but no method was linked to it. Use the button's OnClick property to set a method to perform on click.");
            }
            else
            {
                OnClick();
            }
        }
    }
}
