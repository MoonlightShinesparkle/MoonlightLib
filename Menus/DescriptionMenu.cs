namespace MoonlightLib.Menus;

public class DescriptionMenu : Menu{
	/// <summary>
	/// A dictionary which includes descriptions for different displayable options
	/// </summary>
	/// <remarks>
		/// <b>Data:</b>
		/// <list type="bullet">
			/// <item>
				/// <b>First string:</b> Name of the option bound to the description
			/// </item>
			/// <item>
				/// <b>Second string:</b> Description of the option bound to itself
			/// </item>
		/// </list>
	/// </remarks>
	public Dictionary<string,string> Descriptions =  new Dictionary<string, string>();

	/// <summary>
	/// Creates a descriptive menu with the default options
	/// </summary>
	public DescriptionMenu() : base(){}

	/// <summary>
	/// Creates a descriptive menu with a given title
	/// </summary>
	/// <param name="Title">Text to display</param>
	public DescriptionMenu(string Title) : base(Title){}

	/// <summary>
	/// Creates a descriptive menu with a given title and options
	/// </summary>
	/// <param name="Title">Text to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	public DescriptionMenu(string Title, Dictionary<string,Delegate> Options) : base(Title,Options){}

	/// <summary>
	/// Creates a descriptive menu with a given title and options
	/// </summary>
	/// <param name="Title">Text to display</param>
	/// <param name="Options">List to load as options, check the menu's Options variable for more information</param>
	/// <param name="Descriptions">List to load as descriptions, check the description menu's Descriptions variable for more information</param>
	public DescriptionMenu(string Title, Dictionary<string,Delegate> Options, Dictionary<string,string> Descriptions)
	: base (Title,Options){
		this.Descriptions = Descriptions;
	}

	protected override void DisplayMenuInternalOverlay(int Current){
		try{
			KeyValuePair<string,Delegate> CurrentOption = AssembledOptions.ElementAt(Current);
			if (Descriptions.TryGetValue(CurrentOption.Key, out string? value)){
				Console.Write(value);
			}
		}catch{

		}
	}
}