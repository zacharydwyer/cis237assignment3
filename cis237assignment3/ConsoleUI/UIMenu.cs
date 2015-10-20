using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    // If any comments refer to a "UIGroup", they should instead refer to "UIMenu".
    class UIMenu
    {
        // Console information
        public ConsoleColor DefaultBackColor = ConsoleColor.Black;
        public ConsoleColor DefaultForeColor = ConsoleColor.White;
        public ConsoleColor HighlightedBackColor = ConsoleColor.White;
        public ConsoleColor HighlightedForeColor = ConsoleColor.Black;
        private const int WINDOW_HEIGHT = 26;                       // Height to make console window
        private const int WINDOW_WIDTH = 100;                       // Width to make console window

        // todo: not sure if i'll need this
        // private const int DEFAULT_STATUS_LINE_START = 22;           // Default line to start drawing the "status box"

        // Minimum amount of spaces between each UIElement
        public int MinimumNumberOfSpacesBetweenElement = 3;

        // Spaces per tab
        private static int tabMultiple = 5;

        // Padding for the menu
        public int MenuPaddingLeft = 1;
        public int MenuPaddingTop = 1;

        // Clear the screen when this is drawn?
        public bool ClearScreenOnStart = true;

        // UI elements of this menu
        private List<UIElement> UIElements = new List<UIElement>();

        // Coordinates for each parallel UI element in UIElements. Manipulated in setCoordinates.
        private List<int> ElementLeftCoordinates = new List<int>();
        private List<int> ElementTopCoordinates = new List<int>();

        // Index of the currently selected UIElement
        private int currentlySelectedIndex;

        /* CONSTRUCTOR */ 
        public UIMenu()
        {
            
        }

        /* PROPERTIES */

        // Add element(s) to the UIElements list
        public void Add(UIElement element)
        {
            UIElements.Add(element);
        }
        public void Add(List<UIElement> elements)
        {
            UIElements.AddRange(elements);
        }

        // Remove an element at a specified index (shouldn't this return a bool?)
        public void RemoveAt(int index)
        {
            UIElements.RemoveAt(index);
        }

        // Return the list (UIElements) - used to examine what changed.
        public List<UIElement> UIElementList
        {
            get
            {
                return this.UIElements;
            }
        }

        /* METHODS */

        // Starts drawing and allowing interaction with this UIMenu
        public void Start()
        {
            if (ClearScreenOnStart)
            {
                ClearScreen();
            }

            // Make the console's cursor not visible
            Console.CursorVisible = false;

            // Get the first selectable UI element in UI Elements
            currentlySelectedIndex = getIndexOfFirstSelectableElement();

            // Get the coordinates of the UI elements - these will be later on used to draw each element.
            setUICoordinates();

            // Draw the elements.
            drawElements();

            // Keeps loop going
            bool keepLooping = true;

            // Get a keystroke and behave accordingly
            while (keepLooping)
            {
                // Wait until the user hits a key
                ConsoleKey keyHitByUser = Console.ReadKey(true).Key;

                // Handle the navigation between elements

                // Was it the right arrow? (Go to next selectable element)
                if (keyHitByUser == ConsoleKey.RightArrow)
                {
                    // Start from the position where the current element sits
                    // todo: figure out why this line needs to be here but isn't there in the left arrow section. I was tired when I wrote this but it seems to work.
                    Console.SetCursorPosition(ElementLeftCoordinates[currentlySelectedIndex], ElementTopCoordinates[currentlySelectedIndex]);

                    // Get the NEXT valid selectable element, starting with the one that is currently selected
                    getNextValidElement();
                }

                // Was it the left arrow?
                if (keyHitByUser == ConsoleKey.LeftArrow)
                {
                    getLastValidElement();
                }

                // Handle the selection of different choices in a Selection Box

                // Is this a SelectionBox?
                if (UIElements[currentlySelectedIndex] is SelectionBox)
                {
                    #region Handle Up Arrow

                    if (keyHitByUser == ConsoleKey.UpArrow)
                    {
                        // Increment the selection in the selection box
                        ((SelectionBox)UIElements[currentlySelectedIndex]).IncrementSelection();

                        // (will be redrawn later)
                    }

                    #endregion

                    #region Handle Down Arrow

                    if (keyHitByUser == ConsoleKey.DownArrow)
                    {
                        // Decrement the selection in the selection box
                        ((SelectionBox)UIElements[currentlySelectedIndex]).DecrementSelection();

                        // (will be redrawn later)
                    }

                    #endregion
                }

                #region Handle Enter Button

                if (keyHitByUser == ConsoleKey.Enter)
                {
                    // Is this a button?
                    if (UIElements[currentlySelectedIndex] is Button)
                    {
                        // Activate it!
                        Button tempButton = (Button)UIElements[currentlySelectedIndex];
                        tempButton.PerformClick();

                        // Is it the Submit button?
                        if (tempButton.Title.ToLower() == "submit")
                        {
                            // Get out of here
                            keepLooping = false;
                        }
                    }
                }
                #endregion

                // Clear screen
                ClearScreen();

                // Redraw the screen
                drawElements();
            }
        }

        private void getNextValidElement()
        {
            // Temporary index
            int index;

            // If the new index will be more than the last index of UIElements
            if (currentlySelectedIndex + 1 > UIElements.Count - 1)
            {
                // Reset back to the first index
                index = 0;
            }
            else
            {
                // Increment the index
                index = currentlySelectedIndex + 1;
            }

            bool nextIndexFound = false;

            // While the index is still less than the last index of UIElements, and the next selectable index has yet to be found
            while (index <= UIElements.Count - 1 && nextIndexFound == false)
            {
                // Is this element selectable?
                if (UIElements[index].IsSelectable)
                {
                    // We found a selectable element
                    nextIndexFound = true;

                    // Set the currentlySelectedIndex to the newly found index
                    currentlySelectedIndex = index;
                }
                else /* ELEMENT NOT SELECTABLE - DETERMINE HOW WE WILL MOVE FORWARD */
                {
                    // Are we looking at the last element in the array?
                    if (index == UIElements.Count - 1)
                    {
                        // Loop back over to the beginning of the index
                        index = 0;
                    }
                    else
                    {
                        // We are not at the max index, look at the next element on the list in the next run-through of this loop.
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Return the element with the specified title. This is kind of buggy because it will only return the first element it finds with that title, and there's no way to
        /// make sure that two elements don't have the same title, and this thing should be finding elements by ID. Something else should be doling out ID's as well. Also,
        /// ignores case.
        /// </summary>
        /// <param name="title">Element's title property.</param>
        /// <param name="throwExceptionIfNotFound">Throw an exception if the element with the specified ID (text property) is not found.</param>
        /// <returns></returns>
        public UIElement GetElementByTitle(string title)
        {
            title = title.ToLower();

            bool elementFound = false;
            UIElement elementToBeReturned = null;       // Won't return null because an exception will be thrown first.

            // Look at every single UI element
            foreach (UIElement element in UIElementList)
            {
                string elementTitle = element.Title.ToLower();

                // If this element's text property matches the id given to this method
                if (elementTitle == title)
                {
                    // Make this element the element to be returned
                    elementToBeReturned = element;
                    elementFound = true;
                }
            }

            if (!elementFound)
            {
                throw new System.Exception("Element with title " + title + " not found!");
            }

            return elementToBeReturned;
        }

        // Get the PREVIOUS valid selectable element, starting with the one that is currently selected
        private void getLastValidElement()
        {
            // Start with refering to the element that comes before this one.
            int index = currentlySelectedIndex - 1;

            // Is this new index going to be less than 0? (below the beginning of the array)?
            if (index < 0)
            {
                // Set the index to the highest one we can (loop back up)
                index = UIElements.Count - 1;
            }

            bool prevIndexFound = false;

            while (index >= 0 && prevIndexFound == false)
            {

                // Is this element selectable?
                if (UIElements[index].IsSelectable)
                {
                    // We found a selectable element
                    prevIndexFound = true;

                    // Set the currentlySelectedIndex to the newly found index
                    currentlySelectedIndex = index;
                }
                else
                {
                    // The element wasn't selectable.

                    // Is the current index we just looked at equal to the lowest index of the drawable elements?
                    if (index == UIElements.Count - 1)
                    {
                        // Reset the index to the highest available index
                        index = UIElements.Count - 1;
                    }
                    else
                    {
                        // Wasn't the max index. Decrement by one. Remember we are looking backwards here.
                        index--;
                    }
                }
            }
        }

        // Set cursor printing coordinates of the menu elements in UIElements
        private void setUICoordinates()
        {
            // nextLeft and nextTop refer to where to draw the next element.
            // Set to start drawing elements at the top of the screen.
            int nextLeft = MenuPaddingLeft, nextTop = MenuPaddingTop;

            // Reset the previous coordinates.
            ElementLeftCoordinates = new List<int>(UIElements.Count);
            ElementTopCoordinates = new List<int>(UIElements.Count);

            // Get the max index for the UIElements list
            int maxIndexOfUIElementsList = UIElements.Count - 1;

            // Get the coordinate for every element in the collection - this will be used to draw it later on
            foreach (UIElement element in UIElements)
            {
                // Get the index of the element we're currently referencing
                int currentElementIndex = UIElements.IndexOf(element);

                // Assign element coordinates that we got from last time, respectively
                // ElementLeftCoordinates[currentElementIndex] = nextLeft;
                // ElementTopCoordinates[currentElementIndex] = nextTop;
                ElementLeftCoordinates.Add(nextLeft);
                ElementTopCoordinates.Add(nextTop);

                /* Handle different types of elements appropriately. */

                // Is this a block element?
                if (element.DisplaySetting == UIElement.EDisplaySetting.BLOCK) /* HANDLE BLOCK ELEMENT */
                {
                    // Increment line
                    nextLeft = MenuPaddingLeft;
                    nextTop++;

                    // Handle any extra lines a Spacer calls for
                    if (element is Spacer) 
                    {
                        // Temporarily cast
                        Spacer tempSpacer = (Spacer)element;

                        // Add a new line for every additional space called for
                        for (int spacesDone = 0; spacesDone < tempSpacer.AdditionalLines; spacesDone++)
                        {
                            // Add new line
                            nextTop++;
                        }
                    }
                }
                else /* HANDLE INLINE ELEMENT */
                {
                    // If we're not referencing the last element
                    if (currentElementIndex != maxIndexOfUIElementsList) 
                    {
                        // Take the position from last time, plus the width of this element, plus the min spaces between each element,
                        //   and round it up to the nearest tab multiple. This will make sure the element has a print coordinate that
                        //   neatly lands on a tab position to make things look more aligned.
                        int tempLeftPosition = roundUp(nextLeft + element.Width + MinimumNumberOfSpacesBetweenElement, tabMultiple);

                        // If the next proposed position, PLUS the width of the NEXT element, will go out of the window bounds...
                        // This one is hard to understand because it is making sure that the next element won't spill over past the window border.
                        if (tempLeftPosition + UIElements[currentElementIndex + 1].Width > WINDOW_WIDTH) 
                        {
                            // Put this element on the next line (it will spill out)
                            nextTop++;
                            nextLeft = MenuPaddingLeft;
                        }
                        else 
                        {
                            // The next element will fit - assign it the coordinate.
                            nextLeft = tempLeftPosition;
                        }
                    }
                }
            }
        }

        // Returns the index of the first selectable element within this.UIElements
        private int getIndexOfFirstSelectableElement()
        {
            int firstSelectableElement = 0;                     // What we will be returning

            // Search through the UIElements list
            foreach (UIElement element in this.UIElements)
            {
                if (element.IsSelectable)
                {
                    // We have found the UI element - capture the index of it.
                    firstSelectableElement = this.UIElements.IndexOf(element);
                    
                    // Exit loop
                    break;
                }
            }

            return firstSelectableElement;
        }

        // Draw the elements
        private void drawElements()
        {
            // Start drawing the UI elements

            // Indicates whether the element should be drawn highlighted (to indicate "selected")
            bool drawHighlighted;

            // For every UIElement in UIElements
            foreach (UIElement element in UIElements)
            {
                // Get index of current element
                int indexOfCurrentElement = UIElements.IndexOf(element);

                // Is this element the one that should be selected?
                if (indexOfCurrentElement == currentlySelectedIndex)
                {
                    drawHighlighted = true;
                }
                else
                {
                    drawHighlighted = false;
                }

                // Actually draw the element
                drawElement(element, ElementLeftCoordinates[indexOfCurrentElement], ElementTopCoordinates[indexOfCurrentElement], drawHighlighted);
            }
        }

        // Draws an element at the specified left and top positions
        private void drawElement(UIElement element, int leftPos, int topPos, bool drawHighlighted)
        {
            // Set the cursor position to this element's respective x and y coordinates
            Console.SetCursorPosition(leftPos, topPos);

            // Handle drawing each element differently

            // If it's a spacer
            if (element is Spacer)
            {
                // Don't do anything! Spacers have nothing to write
            }
            else if (element is Label)
            {
                // Temporarily unbox the label
                Label tempLabel = (Label)element;

                // Write the element
                if (drawHighlighted)
                {
                    HighlighterPrint(tempLabel.Title);
                }
                else
                {
                    Console.Write(tempLabel.Title);
                }
            }
            else if (element is SelectionBox)
            {
                // Temporarily unbox the selection box
                SelectionBox tempSelectionBox = (SelectionBox)element;

                // Draw the label/title of this selection box
                Console.Write(tempSelectionBox.Title + " ");

                // Write the element
                if (drawHighlighted)
                {
                    HighlighterPrint(tempSelectionBox.Choices[tempSelectionBox.SelectedChoiceIndex]);
                }
                else
                {
                    Console.Write(tempSelectionBox.Choices[tempSelectionBox.SelectedChoiceIndex]);
                }
            }
            else if (element is Button)
            {
                // Temporarily unbox the element
                Button tempButton = (Button)element;

                if (drawHighlighted)
                {
                    HighlighterPrint(tempButton.Title);
                }
                else
                {
                    Console.Write(tempButton.Title);
                }
            }
        }

        // Writes text on the console highlighted
        public void HighlighterPrint(string text)
        {
            Console.BackgroundColor = this.HighlightedBackColor;
            Console.ForegroundColor = this.HighlightedForeColor;
            Console.Write(text);
            Console.BackgroundColor = this.DefaultBackColor;
            Console.ForegroundColor = this.DefaultForeColor;
        }

        // Helpful method to help me round up to the nearest multiple of 5
        private static int roundUp(int numberToRound, int multiple)
        {
            if (multiple == 0)
            {
                return numberToRound;
            }

            int remainder = numberToRound % multiple;
            if (remainder == 0)
            {
                return numberToRound;
            }

            return numberToRound + multiple - remainder;
        }

        // CLEARS SCREEN & RESETS CURSOR
        public static void ClearScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.CursorVisible = false;
        }
    }
}
