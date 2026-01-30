namespace MoonlightLib.Menus;

/// <summary>
/// Menu made specifically to be displayed by another menu
/// </summary>
/// <remarks>
/// Shows a "Return to last menu" text instead of the generic End
/// </remarks>
public class Submenu : Menu{
	/// <summary>
	/// Creates a submenu with the default menu options
	/// </summary>
	public Submenu() : base(){
		ExitText = "Return to last menu";
	}

	/// <summary>
	/// Creates a submenu with a given title
	/// </summary>
	/// <param name="Title">Text to display</param>
	public Submenu(string Title) : base(Title){
		ExitText = "Return to last menu";
	}

	/// <summary>
	/// Creates a submenu with a given title and options
	/// </summary>
	/// <param name="Title">Name to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	public Submenu(string Title, Dictionary<string,Delegate> Options) : base(Title,Options){
		ExitText = "Return to last menu";
	}
}
