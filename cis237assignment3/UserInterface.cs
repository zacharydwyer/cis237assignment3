using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleUI;
using DroidPack;

namespace cis237assignment3
{

    // Class that handles all dealings with the console UI.
    static class UserInterface
    {
        #region Class Variables & Constants

        // Console information
        public static ConsoleColor defaultBackColor = ConsoleColor.Black;
        public static ConsoleColor defaultForeColor = ConsoleColor.White;
        public static ConsoleColor highlightedBackColor = ConsoleColor.White;
        public static ConsoleColor highlightedForeColor = ConsoleColor.Black;

        private const int DEFAULT_STATUS_LINE_START = 22;           // Default line to start drawing the "status box"
        private const int WINDOW_HEIGHT = 26;                       // Height to make console window
        private const int WINDOW_WIDTH = 100;                       // Width to make console window
        private const int MINIMUM_SPACES_BETWEEN_ELEMENT = 3;       // Minimum amount of spaces between each DrawableElement

        #endregion

        #region Utility Methods

        /* RETURN "PRESS ANY KEY TO CONTINUE" PHRASE (EASIER TO TYPE THAN THE WHOLE THING; ENFORCES CONSISTENCY) */
        public static string PressAnyPhrase()
        {
            return "Press any key to continue...";
        }

        /* RETURN APPENDED "PRESS ANY KEY TO CONTINUE" PHRASE ON A MESSAGE */
        public static string PressAnyPhrase(string message)
        {
            return (message + " (Press any key to continue...)");
        }

        /* INITIALIZE CONSOLE WINDOW */
        public static void InitializeConsoleWindow(string windowTitle)
        {
            Console.Title = windowTitle;                                        // Set title
            Console.BackgroundColor = defaultBackColor;                          // Set back and foreground colors of console window
            Console.ForegroundColor = defaultForeColor;
            Console.CursorVisible = false;                                      // Do not show the cursor (this is a press-a-key kinda UI not a type-it-in)
            Console.Clear();                                                    // Resets console window which in turn applies the new specified colors
            Console.WindowHeight = WINDOW_HEIGHT;
            Console.WindowWidth = WINDOW_WIDTH;
        }

        // Columns 21 - 24 are reserved for the "status" which helps inform the user on what key to press/what to do next/what happened

        /* SET PROGRAM STATUS */
        public static void SetStatus(string status)
        {
            // Get the console position that the console is currently at right now
            int cursorPosLeft = Console.CursorLeft;
            int cursorPosTop = Console.CursorTop;

            Console.SetCursorPosition(0, DEFAULT_STATUS_LINE_START);            // Set position of cursor
            Console.WriteLine(" ----------------------");
            ClearCurrentConsoleLine();                                          // Clear the existing status
            Console.WriteLine(" " + status);                                    // Write the new status
            Console.WriteLine(" ----------------------");

            Console.SetCursorPosition(cursorPosLeft, cursorPosTop);             // Put the cursor back where it was
        }

        /* SET PROGRAM STATUS & STARTING ROW OF STATUS */
        public static void SetStatus(string status, int startingRow)
        {
            // Get the console position that the console is currently at right now
            int cursorPosLeft = Console.CursorLeft;
            int cursorPosTop = Console.CursorTop;

            Console.SetCursorPosition(0, startingRow);                          // Set position of cursor
            Console.WriteLine(" ----------------------");
            ClearCurrentConsoleLine();                                          // Clears the existing status
            Console.WriteLine(" " + status);                                    // Writes a new status
            Console.WriteLine(" ----------------------");

            Console.SetCursorPosition(cursorPosLeft, cursorPosTop);             // Put the cursor back where it was
        }

        /* CLEAR THE ENTIRE LINE THAT THE CURSOR IS CURRENTLY AT */
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;              // 
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /* CLEARS SCREEN & RESETS CURSOR*/
        public static void ClearScreen()
        {
            Console.Clear();
            Console.SetCursorPosition(1, 1);
        }

        /* HIGHLIGHTS MESSAGE THEN PUTS IT BACK TO NORMAL */
        public static void HighlighterPrint(string text)
        {
            Console.BackgroundColor = highlightedBackColor;
            Console.ForegroundColor = highlightedForeColor;
            Console.Write(text);
            Console.BackgroundColor = defaultBackColor;
            Console.ForegroundColor = defaultForeColor;
        }

        public static string PadBoth(string phrase, int totalSpacesLength)
        {
            int spaces = totalSpacesLength - phrase.Length;
            int padLeftAmount = spaces / 2 + phrase.Length;
            return phrase.PadLeft(padLeftAmount).PadRight(totalSpacesLength);
        }

        #endregion


        #region Traditional Menu Methods

        /* PRINTS MAIN MENU; RETURNS NUMERIC ANSWER */
        public static int GetMainMenuSelection(string[] choices, string mainMenuTitle)
        {
            /* STEP 1: Make sure that the choices were no more than 9 (there are only 1 - 9 keys on the keyboard (not including a "choice 0")) */
            if (choices.Length > 9)
            {
                throw new System.Exception("Max size of 'choices' is 9.");                              // Crash program if given an amount of choices less than 9 (1 - 9 keys on keyboard)
            }

            // Reset the screen
            ClearScreen();

            // Print the given main menu title                                                                  
            Console.WriteLine(mainMenuTitle + Environment.NewLine);

            /* STEP 2: Print out every option given */
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + " - " + choices[i]);                                        // Print out " # - Choice goes here"
            }

            // Holds the key that the user will hit
            char hitKey;

            /* STEP 3: Loop until we get a valid answer out of the user */
            do
            {
                UserInterface.SetStatus("Press a number key corresponding to the menu option.");

                hitKey = Console.ReadKey(true).KeyChar;                                                 // Read the key the user pressed                               

                // If the key that was hit was a number, and was between 1 and the last menu item in the choices array 
                if (Char.IsNumber(hitKey) && (int.Parse(hitKey.ToString()) <= choices.Length) && (int.Parse(hitKey.ToString()) > 0))
                {
                    // Choice was valid.
                    break;
                }
                else
                {
                    // Error - key was not recognized
                    UserInterface.SetStatus("Key not recognized. (Press any key to continue)");
                    Console.ReadKey(true);
                }

            } while (true);

            // Return the number that corresponds to the key that was hit
            return int.Parse(hitKey.ToString());
        }

        /* ASKS USER A QUESTION AND RETURNS THE ANSWER */
        public static string GetAnswerToQuestion(string query, string captionStatus, bool allowBlankAnswer)
        {

            string answer;                      // Holds the answer
            bool validAnswer = false;           // Holds status of answer validity

            ClearScreen();                      // Reset the screen (console pos 1,1)
            Console.WriteLine(query);           // Print the question
            Console.CursorVisible = true;       // Make cursor visible

            do
            {
                SetStatus(captionStatus);           // Set the status to the caption (usually extra information like "the search term cannot be more than 100 characters")
                Console.SetCursorPosition(1, 3);    // Set cursor to typing area
                ClearCurrentConsoleLine();          // Clear line of any previous questions
                Console.SetCursorPosition(1, 3);    // Set up the cursor position for the user to type.

                answer = Console.ReadLine();        // Get the answer from the user

                if (allowBlankAnswer == false)      // Did they allow the answer to be blank?
                {
                    if (String.IsNullOrEmpty(answer))                                           // Check if it was blank
                    {
                        SetStatus("Answer must not be blank. (Press any key to continue).");    // The answer was blank. 
                        Console.ReadKey(true);
                    }
                    else
                    {
                        validAnswer = true;
                    }
                }
                else
                {
                    validAnswer = true;
                }

            } while (!validAnswer);                 // Do this until the answer is valid

            Console.CursorVisible = false;          // After all of this, hide the cursor again
            return answer;                          // Finally, give them the answer that the user entered.                     
        }

        /* ASKS USER A QUESTION AND RETURNS THE ANSWER; SIGNATURE FOR SPECIFYING A NUMERIC ANSWER */
        public static string PrintQuestion(string query, string captionStatus, bool allowBlankAnswer, bool requireNumericAnswer, int lowestAllowed, int highestAllowed)
        {
            string answer;                      // Holds the answer
            bool validAnswer = false;           // Holds status of answer validity

            ClearScreen();                      // Reset the screen (console pos 1,1)
            Console.WriteLine(query);           // Print the question
            Console.CursorVisible = true;       // Make cursor visible

            do
            {
                SetStatus(captionStatus);           // Set the status to the caption (usually extra information like "the search term cannot be more than 100 characters")
                Console.SetCursorPosition(1, 3);    // Set cursor to typing area
                ClearCurrentConsoleLine();          // Clear line of any previous questions
                Console.SetCursorPosition(1, 3);    // Set up the cursor position for the user to type.
                answer = Console.ReadLine();        // Get the answer from the user

                if (allowBlankAnswer == false)      // Do they not allow blank answers?
                {
                    if (String.IsNullOrEmpty(answer))                                           // Answer should not be blank. Is it blank?
                    {
                        SetStatus(PressAnyPhrase("Answer cannot be blank."));        // The answer was blank. 
                        Console.ReadKey(true);
                    }
                    else
                    {
                        if (requireNumericAnswer)                                               // Answer was not blank. Do they require a numeric answer?
                        {
                            int number;
                            if (!int.TryParse(answer, out number) || number < lowestAllowed || number > highestAllowed) // Answer was not numeric, or between the lowest and highest allowed number.
                            {
                                SetStatus(PressAnyPhrase("Answer must be a whole number between " + lowestAllowed + " and " + highestAllowed + "."));
                                Console.ReadKey(true);
                            }
                            else
                            {
                                validAnswer = true;                                             // Answer was numeric
                            }
                        }
                        else
                        {
                            validAnswer = true;                                                 // They did not require a numeric answer, but the answer was not blank
                        }
                    }
                }
                else
                {
                    validAnswer = true;                                                         // Answer was allowed blank
                }

            } while (!validAnswer);

            Console.CursorVisible = false;      // After all of this, hide the cursor again
            return answer;                      // Finally, give them the answer that the user entered.
        }

        #endregion

        #region Droid Assignment Specific Methods

        // Lists droids from DroidCollection
        public static void ListDroids()
        {
            ClearScreen();

            // If no droids are in the list
            if (DroidCollection.DroidList.Count < 1)
            {
                Console.WriteLine("No droids in the list!");
            }

            // For every droid in the droid list
            foreach (Droid droid in DroidCollection.DroidList)
            {
                // Write out its .ToString() and it's total cost
                Console.WriteLine(droid.ToString() + "TOTL[" + droid.TotalCost + "] ");
            }
            SetStatus(PressAnyPhrase(), Console.CursorTop + 2);
            Console.ReadKey(true);
        }

        #endregion

        #region Wine Item Assignment Specific Methods (Commented Out)

        //public static void PrintWineItems(int maxLinesPerPage)
        //{
        //    UserInterface.ClearScreen();



        //    if (Globals.wineListLoaded == false)                // Was the wine list already loaded?
        //    {
        //        PrintWindow("Wine list must be loaded before printing!");
        //        Console.ReadKey(true);
        //    }
        //    else
        //    {
        //        int wineItemsLeftToPrint = Globals.wineItemList.GetList().Length;               // How many records are left to print
        //        int linesPrinted = 0;                                                           // How many have we printed so far

        //        // Print out all of the wineItems
        //        foreach (WineItem wineItem in Globals.wineItemList.GetList())
        //        {
        //            if (linesPrinted >= maxLinesPerPage)
        //            {
        //                SetStatus(PressAnyPhrase(), (Console.CursorTop + 1));
        //                Console.ReadKey(true);
        //                UserInterface.ClearScreen();
        //                linesPrinted = 0;
        //            }

        //            Console.WriteLine(" " + wineItem.ID + ", " + wineItem.Description + ", " + wineItem.Pack);
        //            linesPrinted++;
        //        }

        //        SetStatus(GeneratePressAnyKeyPhrase("Done printing."), (Console.CursorTop + 1));
        //        Console.ReadKey(true);
        //    }
        //}

        ///* SEARCH THROUGH WINE ITEM LIST */
        //public static void SearchWineItemList()
        //{
        //    if (Globals.wineListLoaded == true)
        //    {
        //        // Ask them from what they want to search through, and what term they want to search with.
        //        int numberSelection = int.Parse(PrintQuestion("Query from (1) IDs, (2) Descriptions, (3) Packs or (4) all?", "Enter a number between 1 and 4.", false, true, 1, 4));
        //        string query = GetAnswerToQuestion("Enter your query: ", "Enter your search term.", false);
        //        bool resultsFound = false;

        //        ClearScreen();

        //        foreach (WineItem currentWineItem in Globals.wineItemList.GetList())
        //        {          // Query the whole list
        //            if (numberSelection == 1 || numberSelection == 4)
        //            {
        //                if (currentWineItem.ID.ToLower().Contains(query.ToLower()))
        //                {
        //                    // Highlight the ID
        //                    HighlighterPrint(currentWineItem.ID);
        //                    Console.WriteLine(", " + currentWineItem.Description + ", " + currentWineItem.Pack);
        //                    resultsFound = true;
        //                }
        //            }
        //            if (numberSelection == 2 || numberSelection == 4)
        //            {
        //                if (currentWineItem.Description.ToLower().Contains(query.ToLower()))
        //                {
        //                    Console.Write(currentWineItem.ID + ", ");
        //                    HighlighterPrint(currentWineItem.Description);
        //                    Console.WriteLine(", " + currentWineItem.Pack);
        //                    resultsFound = true;
        //                }
        //            }
        //            if (numberSelection == 3 || numberSelection == 4)
        //            {
        //                if (currentWineItem.Pack.ToLower().Contains(query.ToLower()))
        //                {
        //                    Console.Write(currentWineItem.ID + ", " + currentWineItem.Description + ", ");
        //                    HighlighterPrint(currentWineItem.Pack);
        //                    Console.WriteLine();
        //                    resultsFound = true;
        //                }
        //            }
        //        }

        //        if (!resultsFound)
        //        {
        //            Console.WriteLine("No results found.");
        //        }

        //        SetStatus(PressAnyPhrase(), (Console.CursorTop + 1));
        //        Console.ReadKey(true);
        //    }
        //    else
        //    {
        //        PrintWindow("The wine list must be loaded before searching through it!");
        //        Console.ReadKey(true);
        //    }
        //}

        #endregion
    }
}
