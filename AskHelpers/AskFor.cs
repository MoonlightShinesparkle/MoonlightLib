using static System.Console;

namespace MoonlightLib.AskHelpers;

/// <summary>
/// Contains functions which ease obtaining a value from console which require the user to correctly supply
/// through the use of loops and verification
/// </summary>
public static class AskFor{
	/// <summary>
	/// Asks for an int
	/// </summary>
	/// <returns>int given by the user</returns>
	public static int AskForInt(){
		return AskForInt("Write a number");
	}
	/// <summary>
	/// Asks for an int
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>int given by the user</returns>
	public static int AskForInt(string Prompt){
		int? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (int.TryParse(ReadLine(),out int Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a long
	/// </summary>
	/// <returns>long given by the user</returns>
	public static long AskForLong(){
		return AskForLong("Write a number");
	}
	/// <summary>
	/// Asks for a long
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>long given by the user</returns>
	public static long AskForLong(string Prompt){
		long? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (long.TryParse(ReadLine(),out long Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a float
	/// </summary>
	/// <returns>float given by the user</returns>
	public static float AskForFloat(){
		return AskForFloat("Write a number");
	}
	/// <summary>
	/// Asks for a float
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>float given by the user</returns>
	public static float AskForFloat(string Prompt){
		float? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (float.TryParse(ReadLine(),out float Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a double
	/// </summary>
	/// <returns>double given by the user</returns>
	public static double AskForDouble(){
		return AskForDouble("Write a number");
	}
	/// <summary>
	/// Asks for a double
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>double given by the user</returns>
	public static double AskForDouble(string Prompt){
		double? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (double.TryParse(ReadLine(),out double Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for an UInt
	/// </summary>
	/// <returns>UInt given by the user</returns>
	public static uint AskForUInt(){
		return AskForUInt("Write a number");
	}
	/// <summary>
	/// Asks for an UInt
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>UInt given by the user</returns>
	public static uint AskForUInt(string Prompt){
		uint? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (uint.TryParse(ReadLine(),out uint Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for an ULong
	/// </summary>
	/// <returns>ULong given by the user</returns>
	public static ulong AskForULong(){
		return AskForULong("Write a number");
	}
	/// <summary>
	/// Asks for an ULong
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>ULong given by the user</returns>
	public static ulong AskForULong(string Prompt){
		ulong? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (ulong.TryParse(ReadLine(),out ulong Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a short
	/// </summary>
	/// <returns>short given by the user</returns>
	public static short AskForShort(){
		return AskForShort("Write a number");
	}
	/// <summary>
	/// Asks for a short
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>short given by the user</returns>
	public static short AskForShort(string Prompt){
		short? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (short.TryParse(ReadLine(),out short Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for an UShort
	/// </summary>
	/// <returns>UShort given by the user</returns>
	public static ushort AskForUShort(){
		return AskForUShort("Write a number");
	}
	/// <summary>
	/// Asks for an UShort
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>UShort given by the user</returns>
	public static ushort AskForUShort(string Prompt){
		ushort? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (ushort.TryParse(ReadLine(),out ushort Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a byte
	/// </summary>
	/// <returns>byte given by the user</returns>
	public static byte AskForByte(){
		return AskForByte("Write a number");
	}
	/// <summary>
	/// Asks for a byte
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>byte given by the user</returns>
	public static byte AskForByte(string Prompt){
		byte? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (byte.TryParse(ReadLine(),out byte Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for an SByte
	/// </summary>
	/// <returns>SByte given by the user</returns>
	public static sbyte AskForSByte(){
		return AskForSByte("Write a number");
	}
	/// <summary>
	/// Asks for an SByte
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>SByte given by the user</returns>
	public static sbyte AskForSByte(string Prompt){
		sbyte? Returnable = null;
		while(Returnable is null){
			Write($"{Prompt}: ");
			if (sbyte.TryParse(ReadLine(),out sbyte Pawsible)){
				Returnable = Pawsible;
			}
		}
		return Returnable ?? 0;
	}

	/// <summary>
	/// Asks for a text to be given
	/// </summary>
	/// <returns>string given by the user</returns>
	public static string AskForString() {
		return AskForString("Write a text");
	}
	/// <summary>
	/// Asks for a text to be given
	/// </summary>
	/// <param name="Prompt">Text to display as a question</param>
	/// <remarks>
	/// Prompt includes a ":" at the end
	/// </remarks>
	/// <returns>string given by the user</returns>
	public static string AskForString(string Prompt) {
		Write($"{Prompt}: ");
		return ReadLine() ?? "";
	}
}