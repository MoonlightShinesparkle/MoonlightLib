using MoonlightLib.ConsoleHelpers;
using static System.Console;

namespace MoonlightLib.Menus;

/// <summary>
/// Manages a displayable set of options
/// </summary>
public class Menu{
	// Used in case user has a preference for background colors

	/// <summary>
	/// Default backgound used by menus
	/// </summary>
	public static ConsoleColor DefaultBackground = ConsoleColor.White;

	/// <summary>
	/// Default foreground used by menus
	/// </summary>
	public static ConsoleColor DefaultForeground = ConsoleColor.Black;

	// Title printed once menu is shown
	/// <summary>
	/// Menu's title to be displayed
	/// </summary>
	public string Title = "Menu";

	/// <summary>
	/// Text option to exit the menu
	/// </summary>
	public string ExitText = "End";
	
	// Per-menu colors
	/// <summary>
	/// Menu's current background color
	/// </summary>
	public ConsoleColor Background = DefaultBackground;

	/// <summary>
	/// Menu's current foreground color
	/// </summary>
	public ConsoleColor Foreground = DefaultForeground;

	// Defines if loop runs or stops
	/// <summary>
	/// Definues if the menu loop runs or stops
	/// </summary>
	protected bool Running = false;

	// List of options the user may modify
	/// <summary>
	/// List of options the menu holds
	/// </summary>
	/// 
	/// <remarks>
	/// <b>Data:</b>
		/// <list type="bullet">
			/// <item>
				/// <b>String:</b> Text to be displayed by the menu
			/// </item>
			/// <item>
				/// <b>Delegate:</b> Function to be ran by the option, can be a <b>lambda</b> or 
				/// any other runnable, including a <b>submenu's display function</b>
			/// </item>
		/// </list> 
	/// </remarks>
	public Dictionary<string,Delegate> Options = new Dictionary<string, Delegate>();
	// Internal list for option parsing
	/// <summary>
	/// Internal copy of Options, created when ran
	/// </summary>
	protected Dictionary<string,Delegate> AssembledOptions = new Dictionary<string, Delegate>();

	// Just some constructors
	/// <summary>
	/// Creates a menu with the default options
	/// </summary>
	public Menu(){}

	/// <summary>
	/// Creates a menu with a given title
	/// </summary>
	/// <param name="Title">Name to display</param>
	public Menu(string Title){
		this.Title = Title;
	}

	/// <summary>
	/// Creates a menu with a given title and a set of options
	/// </summary>
	/// <param name="Title">Name to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	public Menu(string Title, Dictionary<string,Delegate> Options){
		this.Title = Title;
		this.Options = Options;
	}

	// Runpoint for menu displaying
	/// <summary>
	/// Builds a menu's options and displays them
	/// </summary>
	/// <remarks>
		/// <b>A menu's options can't be changed at runtime</b>
	/// </remarks>
	public void DisplayMenu(){
		AssembledOptions.Clear();
		// Assemble list, adding "End" as an option
		foreach(KeyValuePair<string,Delegate> Func in Options){
			AssembledOptions.Add(Func.Key,Func.Value);
		}
		AssembledOptions.Add(ExitText,() => {
			Running = false;
		});
		// Run internal logic
		DisplayMenuInternal();
	}

	// Generic console pause that awaits user input
	/// <summary>
	/// Generic console pause that awaits an user's input
	/// </summary>
	public static void GenericConsolePause(){
		WriteLine("Press enter to continue...");
		Read();
	}

	// Overrideable function to add text to the menu
	/// <summary>
	/// Internal additive display text
	/// </summary>
	/// <param name="Current">Text to display within the menu</param>
	protected virtual void DisplayMenuInternalOverlay(int Current){
	}

	// Main function for showing and handling the menu
	/// <summary>
	/// Internal display manager
	/// </summary>
	protected void DisplayMenuInternal(){
		Clear();
		Running = true;
		// Important values to manage within the loop
		int Current = 0;
		int Count;
		// Main menu loop
		while (Running){
			// Reset count
			Count = 0;
			// Show title
			WriteLine($"\t{this.Title}");
			// Show all function names
			foreach(KeyValuePair<string,Delegate> Func in AssembledOptions){
				// If the selected index is the same as the current printed value, change colors
				if (Count == Current){
					BackgroundColor = Background;
					ForegroundColor = Foreground;
				}
				WriteLine($"{Count+1}- {Func.Key}");
				ResetColor();
				Count++;
			}
			// Show currently selected item at the bottom
			WriteLine($"--Selected {Current+1}/{Count}--");
			// Run overrideable function
			DisplayMenuInternalOverlay(Current);
			// Get user input
			switch(ReadKey().Key){
				// Navigate downwards
				case ConsoleKey.DownArrow: {
					Current++;
					if (Current > (AssembledOptions.Count-1)){
						Current = 0;
					}
					break;
				}
				// Navigate upwards
				case ConsoleKey.UpArrow: {
					Current--;
					if (Current < 0){
						Current = AssembledOptions.Count-1;
					}
					break;
				}
				// Choose option
				case ConsoleKey.Enter: {
					// Get entry in list and clear the menu to execute the desired function
					KeyValuePair<string,Delegate> Executing = AssembledOptions.ElementAt(Current);
					Clear();
					try {
						// Attempt execution of the given delegate
						Executing.Value.DynamicInvoke();
					} catch (Exception e){
						// Clear screen after failure, show the generated exception
						Clear();
						Printer.PrintColored(ConsoleColor.DarkRed,ConsoleColor.White,
							$"Exception when executing {Executing.Key}:");
						Printer.PrintLines(e.Message,$"Caused in {e.TargetSite}",e.StackTrace ?? "");
						if (e.InnerException != null){
							Printer.PrintColored(ConsoleColor.DarkRed,ConsoleColor.White,
								$"Caused through {e.InnerException.TargetSite}",e.InnerException.Message);
							WriteLine(e.InnerException.StackTrace);
						}
						GenericConsolePause();
					}
					break;
				}
			}
			// Clear list once its execution finishes
			Clear();
		}
	}
}
