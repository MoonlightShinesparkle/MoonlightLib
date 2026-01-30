namespace MoonlightLib.ConsoleHelpers{
	public class Printer{
		public static void PrintLines(params string[] Printables){
			foreach (string Printable in Printables){
				Console.WriteLine(Printable);
			}
		}
		public static void PrintColored(ConsoleColor? BackgroundColor,params string[] Printables){
			Console.BackgroundColor = BackgroundColor ?? Console.BackgroundColor;
			PrintLines(Printables);
			Console.ResetColor();
		}
		public static void PrintColored(ConsoleColor? BackgroundColor, ConsoleColor? ForegroundColor,params string[] Printables){
			Console.BackgroundColor = BackgroundColor ?? Console.BackgroundColor;
			Console.ForegroundColor = ForegroundColor ?? Console.ForegroundColor;
			PrintLines(Printables);
			Console.ResetColor();
		}
	}
}