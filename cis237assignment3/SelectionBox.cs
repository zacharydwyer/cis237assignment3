using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237assignment3
{
    class SelectionBox : DrawableElement
    {
        // A selection box will be displayed like so: "Label: Selected Choice". Every time the SelectionBox is focused in on, it will highlight the SelectedChoice portion
        //  of the element. When the user pressed the up and down keys, as handled by other parts of the program, it will increment/decrement the 
        //  index and redraw the element. When left and right are pressed, it will unhighlight/leave the selected element and highlight the next/previous element in the collection.

        // Each selection box has a big thing of strings that the user can choose from
        public string[] Choices { get; set; }

        // The selected choice in the list - is applied in UserInterface.DrawElements
        public int SelectedChoiceIndex { get; set; }

        // Constructor
        public SelectionBox(EDisplaySetting displaySetting, string label, string[] choices)
            : base (displaySetting, label)
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

        // The width of an element should be equal to the biggest content that it will display
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

                return longestStringLength;
            }
        }

        // Selection boxes are selectable
        public override bool IsSelectable
        {
            get { return true; }
        }
    }
}
