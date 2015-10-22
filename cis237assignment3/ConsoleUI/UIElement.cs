using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Zachary Dwyer
 * Description: ConsoleUI is a primitive, basic, work-in-progress, kind of haphazardly-written solution to making console windows behave more like
 *              a graphical UI. UIGroup does the brunt of the work - it draws and handles the navigating and changing of elements. 
 *              
 * How to use:  Items like Button, Label, and SelectionBox are a type of UIElement. Create a UIGroup, use its .Add() function to add UIElements
 *              to it, and then call its Start() method to create an interactive menu.
 *              
 * Issues:      There is no implementation for a text entry element yet, so the only interactive elements it knows how to draw are Buttons
 *              (which can execute a method when the Enter key is pressed while they are selected) and SelectionBoxes (which has its "Selected
 *              Index" property whenever the Up and Down keys are pressed).
 *              
 *              
 */

namespace ConsoleUI
{
    /// <summary>
    /// Abstract class UI elements are based on. Used by a UIGroup object to draw elements.
    /// </summary>
    /// <example>
    /// Give a UIGroup object multiple UIElements using the .Add() method. Then, use the DrawGroup() method to draw the elements on the screen.
    /// </example>
    abstract class UIElement
    {
        /// <summary>
        /// Dictates whether a UIElement will always be drawn on its own line (BLOCK) or, if possible, alongside other elements (INLINE).
        /// </summary>
        public enum EDisplaySetting { BLOCK, INLINE }

        /// <summary>
        /// Whether this element is selectable or not.
        /// </summary>
        public abstract bool IsSelectable
        {
            get;
        }

        /// <summary>
        /// Whether this element will always be drawn on its own line (BLOCK) or, if possible, alongside other elements (INLINE).
        /// </summary>
        public EDisplaySetting DisplaySetting { get; set; }

        /// <summary>
        /// Main text/title of this UIElement.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The amount of Width that should be accomdated to draw this UIElement
        /// </summary>
        public abstract int Width
        {
            get;
        }

        // Constructor
        public UIElement(EDisplaySetting displaySetting, string title)
        {
            this.DisplaySetting = displaySetting;
            this.Title = title;
        }
    }
}
