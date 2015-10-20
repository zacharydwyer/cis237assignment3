using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// My namespaces
using ConsoleUI;
using DroidPack;

namespace cis237assignment3
{
    class Program
    {
        public static string[] MenuSelections = { "Add new droid", "List droids", "Exit program" };                     // Holds first menu available selections

        // All of these menus are populated in methods called by main
        public static UIMenu GeneralDroidMenu = new UIMenu();                                                          // General droid UIMenu
        public static UIMenu ProtocolDroidMenu = new UIMenu();                                                         // Protocol droid menu
        public static UIMenu UtilityDroidMenu = new UIMenu();                                                          // Utility droid menu
        public static UIMenu JanitorDroidMenu = new UIMenu();                                                           // Janitor Droid Menu
        public static UIMenu AstromechDroidMenu = new UIMenu();                                                        // Astromech Droid Menu

        static void Main(string[] args)
        {
            // Populate menus
            populateMenus();

            // Initialize the console window
            UserInterface.InitializeConsoleWindow("Droid Creator");

            // Main program loop
            do
            {
                // Print the main menu and get an answer from the user
                int menuChoice = UserInterface.GetMainMenuSelection(MenuSelections, "Droid Making Program");

                // Make a choice depending on the menu selection
                switch (menuChoice)
                {
                    // Add a droid
                    case 1:
                        // General pattern here is 1) draw the menu 2) declare value variables 3) get the values from the menu elements and assign them to the value variables

                        // Variable to hold the new droid
                        Droid assembledDroid = null;

                        #region Droid Variables
                        // Variables to hold values that will be used to create a new droid
                        Droid.DroidMaterial material = Droid.DroidMaterial.AGRINIUM;
                        Droid.DroidColor color = Droid.DroidColor.BLACK;
                        Droid.DroidModel model = Droid.DroidModel.ASTROMECH;

                        int numberOfLanguages = 0;

                        bool hasToolbox = false;
                        bool hasComputerConnection = false;
                        bool hasArm = false;

                        bool hasTrashCompactor = false;
                        bool hasVacuum = false;

                        bool hasFireExtinguisher = false;
                        int numberOfShips = 0;
                        #endregion

                        // Start menu
                        GeneralDroidMenu.Start();

                        #region General Droid Handling
                        // Get handle to UI elements
                        SelectionBox materialBox = (SelectionBox) GeneralDroidMenu.GetElementByTitle("material:");
                        SelectionBox colorBox = (SelectionBox)GeneralDroidMenu.GetElementByTitle("color:");
                        SelectionBox modelBox = (SelectionBox)GeneralDroidMenu.GetElementByTitle("model:");

                        // Extract and assign data from elements
                        Enum.TryParse<Droid.DroidMaterial>(materialBox.SelectedText, out material);
                        Enum.TryParse<Droid.DroidColor>(colorBox.SelectedText, out color);
                        Enum.TryParse<Droid.DroidModel>(modelBox.SelectedText, out model);
                        #endregion

                        #region Protocol and Utility Droid Handling
                        // Decide which menu to show next based on what they entered for "model"
                        switch (model)
                        {
                            case Droid.DroidModel.PROTOCOL:         // For the protocol droid...

                                // Start the menu
                                ProtocolDroidMenu.Start();

                                // Get handle to UI element
                                SelectionBox numLanguagesBox = (SelectionBox)ProtocolDroidMenu.GetElementByTitle("number of languages:");

                                // Extract and assign data from element
                                int.TryParse(numLanguagesBox.SelectedText, out numberOfLanguages);

                                break;
                            case Droid.DroidModel.UTILITY:          // For the utility, janitor, and astromech droids...
                            case Droid.DroidModel.JANITOR:
                            case Droid.DroidModel.ASTROMECH:

                                // Start the menu
                                UtilityDroidMenu.Start();

                                // Get handle to UI elements
                                SelectionBox hasToolboxBox = (SelectionBox)UtilityDroidMenu.GetElementByTitle("toolbox:");
                                SelectionBox hasComputerConnectionBox = (SelectionBox)UtilityDroidMenu.GetElementByTitle("computer connection:");
                                SelectionBox hasArmBox = (SelectionBox)UtilityDroidMenu.GetElementByTitle("arm:");

                                // Extract and assign data from elements
                                bool.TryParse(hasToolboxBox.SelectedText, out hasToolbox);
                                bool.TryParse(hasComputerConnectionBox.SelectedText, out hasComputerConnection);
                                bool.TryParse(hasArmBox.SelectedText, out hasArm);

                                #region Janitor and Astromech Droid Handling

                                // Take care of the janitor and astromech droids
                                switch (model)
                                {
                                    case Droid.DroidModel.JANITOR:          // For the janitor droid...

                                        // Start the menu
                                        JanitorDroidMenu.Start();

                                        // Get handle to UI elements
                                        SelectionBox hasTrashCompactorBox = (SelectionBox)JanitorDroidMenu.GetElementByTitle("trash compactor:");
                                        SelectionBox hasVacuumBox = (SelectionBox)JanitorDroidMenu.GetElementByTitle("vacuum:");

                                        // Extract and assign data from elements
                                        bool.TryParse(hasTrashCompactorBox.SelectedText, out hasTrashCompactor);
                                        bool.TryParse(hasVacuumBox.SelectedText, out hasVacuum);

                                        break;
                                    case Droid.DroidModel.ASTROMECH:        // For the astromech droid...

                                        // Start the menu
                                        AstromechDroidMenu.Start();

                                        // Get handle to UI elements
                                        SelectionBox hasFireExtinguisherBox = (SelectionBox)AstromechDroidMenu.GetElementByTitle("fire extinguisher:");
                                        SelectionBox numberOfShipsBox = (SelectionBox)AstromechDroidMenu.GetElementByTitle("number of ships:");

                                        // Extract and assign data from elements
                                        bool.TryParse(hasFireExtinguisherBox.SelectedText, out hasFireExtinguisher);
                                        int.TryParse(numberOfShipsBox.SelectedText, out numberOfShips);

                                        break;
                                }
                                #endregion

                                break;
                        }
                        #endregion

                        
                        // Based on what the user entered, create a new droid from that data
                        switch (model)
                        {
                            case Droid.DroidModel.PROTOCOL:
                                assembledDroid = new ProtocolDroid(material, model, color, numberOfLanguages);
                                break;
                            case Droid.DroidModel.UTILITY:
                                assembledDroid = new UtilityDroid(material, model, color, hasToolbox, hasComputerConnection, hasArm);
                                break;
                            case Droid.DroidModel.JANITOR:
                                assembledDroid = new JanitorDroid(material, model, color, hasToolbox, hasComputerConnection, hasArm, hasTrashCompactor, hasVacuum);
                                break;
                            case Droid.DroidModel.ASTROMECH:

                                break;
                        }

                        // FINALLY, add the assembled droid to the list
                        DroidCollection.Add(assembledDroid);

                        // Clear screen
                        UserInterface.ClearScreen();

                        // Draw status
                        UserInterface.SetStatus(UserInterface.PressAnyPhrase(assembledDroid.Model + " droid added to droid list!"));

                        // Wait for user to press a key
                        Console.ReadKey(true);

                        break;

                    // List the droids
                    case 2:
                        UserInterface.ListDroids();
                        break;

                    // Exit program
                    case 3:
                        System.Environment.Exit(0);
                        break;
                }

            } while (true);
        }

        // Populate droid menu
        private static void populateMenus() {

            // Populate droid menu
            GeneralDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "DROID CONFIGURATION MENU"));
            GeneralDroidMenu.Add(new Spacer());
            GeneralDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "Left and Right arrow keys: navigate through menu"));
            GeneralDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "Up and Down arrow keys: select from choices"));
            GeneralDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "Enter key: activates button"));
            GeneralDroidMenu.Add(new Spacer());
            GeneralDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "General Droid Configuration"));
            GeneralDroidMenu.Add(new Spacer());
            GeneralDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Material:", Enum.GetNames(typeof(Droid.DroidMaterial))));
            GeneralDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Color:", Enum.GetNames(typeof(Droid.DroidColor))));
            GeneralDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Model:", Enum.GetNames(typeof(Droid.DroidModel))));
            GeneralDroidMenu.Add(new Spacer(1));
            GeneralDroidMenu.Add(new Button(UIElement.EDisplaySetting.BLOCK, "Submit"));

            // Populate protocol droid menu
            ProtocolDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "PROTOCOL DROID CONFIGURATION"));
            ProtocolDroidMenu.Add(new Spacer());
            ProtocolDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Number of languages:", getNumberArray(1, 20)));
            ProtocolDroidMenu.Add(new Spacer(1));
            ProtocolDroidMenu.Add(new Button(UIElement.EDisplaySetting.BLOCK, "Submit"));


            // Populate utility droid menu
            UtilityDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "UTILITY DROID CONFIGURATION"));
            UtilityDroidMenu.Add(new Spacer());
            UtilityDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Toolbox:", getTrueFalseChoices()));
            UtilityDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Computer Connection:", getTrueFalseChoices()));
            UtilityDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Arm:", getTrueFalseChoices()));
            UtilityDroidMenu.Add(new Spacer());
            UtilityDroidMenu.Add(new Button(UIElement.EDisplaySetting.BLOCK, "Submit"));

            // Populate Janitor Droid Menu
            JanitorDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "JANITOR UTILITY DROID CONFIGURATION"));
            JanitorDroidMenu.Add(new Spacer());
            JanitorDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Trash Compactor:", getTrueFalseChoices()));
            JanitorDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Vacuum:", getTrueFalseChoices()));
            JanitorDroidMenu.Add(new Spacer(1));
            JanitorDroidMenu.Add(new Button(UIElement.EDisplaySetting.BLOCK, "Submit"));

            // Populate Astromech Droid Menu
            AstromechDroidMenu.Add(new Label(UIElement.EDisplaySetting.BLOCK, "ASTROMECH UTILITY DROID CONFIGURATION"));
            AstromechDroidMenu.Add(new Spacer());
            AstromechDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Fire Extinguisher:", getTrueFalseChoices()));
            AstromechDroidMenu.Add(new SelectionBox(UIElement.EDisplaySetting.INLINE, "Number of ships:", getNumberArray(0, 20)));
            AstromechDroidMenu.Add(new Spacer(1));
            AstromechDroidMenu.Add(new Button(UIElement.EDisplaySetting.BLOCK, "Submit"));
            
        }

        // Returns an array for use in a true/false selection box
        private static string[] getTrueFalseChoices()
        {
            return new string[2] { "true", "false" };
        }

        // Returns an array filled with numbers (in the form of strings) between lowest and highest number
        public static string[] getNumberArray(int lowestNum, int highestNum)
        {
            // Initialize array

            // Add 1 to highest num - seems to be a little wonky without it
            highestNum++;

            // Get amount to alter the array
            int lowestNumAmountToAdd = lowestNum * (-lowestNum);

            // IF we're given -5 and 45, this will make an array sized at 50.
            string[] numberArray = new string[lowestNumAmountToAdd + highestNum];

            // Add these numbers. Start with the lowest number.
            int counter = lowestNum;

            for (int i = 0; i <= numberArray.GetUpperBound(0); i++)
            {
                numberArray[i] = counter.ToString();
                counter++;
            }

            return numberArray;
        } 
    }
}
