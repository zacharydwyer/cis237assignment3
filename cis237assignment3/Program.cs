using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Media;

namespace cis237assignment3
{
    class Program
    {

        public static List<Material> MaterialsList = new List<Material>();      // List that holds all of the valid materials available to build a droid with
        public static List<Droid> DroidList = new List<Droid>();                // Holds the list of droids
        public static string[] MenuSelections = { "Add new droid", "List droids", "Exit program" };

        // Elements representing a general droid submenu
        public static DrawableElement[] GeneralDroidMenu = {
                                                               new Label(DrawableElement.EDisplaySetting.BLOCK, "DROID CONFIGURATION MENU"),
                                                               new Label(DrawableElement.EDisplaySetting.BLOCK, "Use the left and right keys to navigate, and the up and down keys to choose"),
                                                               new Spacer(),
                                                               new Label(DrawableElement.EDisplaySetting.BLOCK, "General Droid Configuration"),
                                                               new Spacer(),
                                                               new SelectionBox(DrawableElement.EDisplaySetting.INLINE, "Material:", convertMaterialsListToStringArray(MaterialsList)),
                                                               new SelectionBox(DrawableElement.EDisplaySetting.INLINE, "Color:", Enum.GetNames(typeof(Droid.DroidColor))),
                                                               new SelectionBox(DrawableElement.EDisplaySetting.INLINE, "Model:", Enum.GetNames(typeof(Droid.DroidModel))),
                                                               new Spacer(1)
                                                           };

        // Elements representing a utility droid submenu
        public static DrawableElement[] UtilityDroidMenu = {
                                                               new Label(DrawableElement.EDisplaySetting.BLOCK, "Utility Droid Configuration"),
                                                               new SelectionBox(DrawableElement.EDisplaySetting.INLINE, "Toolbox", getTrueFalseChoices()),


                                                           };

        // Returns an array for use in a true/false selection box
        private static string[] getTrueFalseChoices()
        {
            return new string[2] {"true", "false"};      
        }


        static void Main(string[] args)
        {

            Console.Beep(100, 200);
            Console.Beep(400, 200);

            // Populate materials list
            populateMaterialsList();

            // Initialize the console window
            UserInterface.InitializeConsoleWindow("Droid Maker v.0.1", ConsoleColor.Black, ConsoleColor.DarkYellow);

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
                        // todo: reference "add droid" method
                        break;

                    // List the droids
                    case 2:
                        // todo: reference "list droids" method
                        break;

                    // Exit program
                    case 3:
                        System.Environment.Exit(0);
                        break;
                }

            } while (true);

        }

        private static string[] convertMaterialsListToStringArray(List<Material> materialsList) 
        {
            // List to hold strings (using list so it's more dynamic)
            List<String> stringList = new List<String>();

            // For each material in the list
            foreach(Material material in materialsList) 
            {
                // Add it to the list
                stringList.Add(material.ToString());
            }
            
            // Return the list to a string array
            return stringList.ToArray<String>();
        }

        /* POPULATE LIST OF MATERIALS WITH NEW MATERIALS */
        private static void populateMaterialsList()
        {
            MaterialsList.Add(new Material("agrinium", 1000));
            MaterialsList.Add(new Material("bondite", 6000));
            MaterialsList.Add(new Material("crodium", 1200));
            MaterialsList.Add(new Material("duralium", 3000));
            MaterialsList.Add(new Material("duraplast", 2500));     
            MaterialsList.Add(new Material("gententhium", 400));
            MaterialsList.Add(new Material("iron", 800));
            MaterialsList.Add(new Material("osmium", 1500));
            MaterialsList.Add(new Material("platinum", 1970000));
            MaterialsList.Add(new Material("pyronium", 900000));
            MaterialsList.Add(new Material("quadanium", 1700));
            MaterialsList.Add(new Material("rhodium", 1300));
            MaterialsList.Add(new Material("songsteel", 750000));
            MaterialsList.Add(new Material("titanium", 1800));
            MaterialsList.Add(new Material("tricopper", 700));
            MaterialsList.Add(new Material("tungsten", 200));
            MaterialsList.Add(new Material("ultrachrome", 2400));
        }
    }
}
