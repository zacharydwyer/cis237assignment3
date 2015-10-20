using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    // A selection box will be displayed like so: "Title: Selected Choice". Every time the SelectionBox is focused in on, it will highlight the SelectedChoice portion
    //  of the element, NOT its title. When the user presses the up and down keys and the element is selected, it will increment/decrement the 
    //  index and redraw the element. All of this behavior is handled by the UIGroup's Start() method.
    class SelectionBox : UIElement
    {
        /* OBJECT VARIABLE */

        // Each selection box has a big thing of strings that the user can choose from.
        public string[] Choices { get; set; }

        /* PROPERTIES */

        // The selected choice in the list - is incremented and decremented using IncrementSelection/DecrementSelection
        // Protected Set used here since Increment/Decrement selection should only be used to manipulate this value.
        public int SelectedChoiceIndex { get; protected set; }

        // Returns selected text
        public string SelectedText
        {
            get
            {
                return this.Choices[this.SelectedChoiceIndex];
            }
        }

        // The width of an element should be equal to the LARGEST piece of text/string that it will be displaying
        public override int Width
        {
            get
            {
                int longestStringLength = 0;

                // Loop through each item in the choices array
                foreach (string choice in Choices)
                {
                    // If this choice's character amount is more than the longest char sequence we've found so far
                    if (choice.Length > longestStringLength)
                    {
                        // It is now the new longest string
                        longestStringLength = choice.Length;
                    }
                }

                // Add the length of the label to it, along with 1, to account for the space between the label and the select box next to it

                longestStringLength += 1 + this.Title.Length;

                return longestStringLength;
            }
        }

        // Selection boxes are selectable
        public override bool IsSelectable
        {
            get { return true; }
        }

        /* CONSTRUCTOR */
        public SelectionBox(EDisplaySetting displaySetting, string text, string[] choices)
            : base (displaySetting, text)
        {
            // Make sure choices wasn't 0
            if (choices.Length < 1)
            {
                throw new Exception("Number of choices cannot be less than 1.");
            }
            else
            {
                this.Choices = choices;
            }
        }

        /* INCREMENT/DECREMENT METHODS */

        // Increment the selected choice index
        public void IncrementSelection()
        {
            // Make sure that we're not going out of bounds, and if we are, loop back to index 0
            int newIndex = SelectedChoiceIndex + 1;

            // If the new proposed index is more than the last index in the Choices array
            if (newIndex > Choices.GetUpperBound(0)) {

                // Set the selected choice index to 0
                SelectedChoiceIndex = 0;

            }
            else
            {
                // The proposed index is not more than the last index in the choices array, set it to the propsed index
                SelectedChoiceIndex = newIndex;
            }
        }

        // Decrement the selected choice index
        public void DecrementSelection()
        {
            int newIndex = SelectedChoiceIndex - 1;

            // Make sure the decrement is not less than 0
            if (newIndex < 0)
            {
                // Make the index wrap back around to the array's maximum index
                SelectedChoiceIndex = Choices.GetUpperBound(0);
            }
            else
            {
                // The proposed index is not less than 0; decrement the index
                SelectedChoiceIndex = newIndex;
            }
        }

        
    }
}
