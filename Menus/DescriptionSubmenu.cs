namespace MoonlightLib.Menus;

/// <summary>
/// A special kind of submenu which includes the option to add a set of descriptions to each option
/// </summary>
public class DescriptionSubmenu : DescriptionMenu{
	/// <summary>
	/// Creates a submenu with the default menu options
	/// </summary>
	public DescriptionSubmenu() : base(){
		ExitText = "Return to last menu";
	}

	/// <summary>
	/// Creates a submenu with a given title
	/// </summary>
	/// <param name="Title">Text to display</param>
	public DescriptionSubmenu(string Title) : base(Title){
		ExitText = "Return to last menu";
	}

	/// <summary>
	/// Creates a submenu with a given title and options
	/// </summary>
	/// <param name="Title">Name to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	public DescriptionSubmenu(string Title, Dictionary<string,Delegate> Options) : base(Title,Options){
		ExitText = "Return to last menu";
	}

	/// <summary>
	/// Creates a submenu with a given title, options and descriptions
	/// </summary>
	/// <param name="Title">Name to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	/// <param name="Descriptions">List to load as descriptions, check the description menu's Descriptions variable for more information</param>
	public DescriptionSubmenu(string Title, Dictionary<string,Delegate> Options, Dictionary<string,string> Descriptions)
	: base (Title,Options,Descriptions){
		ExitText = "Return to last menu";
	}
}
