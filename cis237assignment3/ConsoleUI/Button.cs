using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public delegate void ClickMethod();

namespace ConsoleUI
{
    // A button is essentially a selectable label. When you press Enter while you've highlighted it, it will execute whatever method OnClick refers to.
    class Button : UIElement
    {
        /* PROPERTIES */

        // Buttons are selectable.
        /// <summary>
        /// Whether this button is selectable or not (buttons are always selectable - used in polymorphic situations).
        /// </summary>
        public override bool IsSelectable
        {
            get { return true; }
        }

        // The width of a button is the length of its text, like a label.
        /// <summary>
        /// The width of the button.
        /// </summary>
        public override int Width
        {
            get { return this.Title.Length; }
        }

        /// <summary>
        /// Method to be called when this button is clicked.
        /// </summary>
        public ClickMethod OnClick { get; set; }

        /* CONSTRUCTORS */
        public Button(EDisplaySetting displaySetting, string title) : base (displaySetting, title) { }
        
        // You're allowed to create a button that doesn't do anything.
        public Button(EDisplaySetting displaySetting, string title, ClickMethod onClickMethod) : this (displaySetting, title)
        {
            this.OnClick = onClickMethod;
        }
        
        /* METHODS */

        /// <summary>
        /// Executes the method referenced by OnClick.
        /// </summary>
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
