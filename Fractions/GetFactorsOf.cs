namespace MoonlightLib.Fractions;

/// <summary>
/// Manages a pair of functions for gathering factors 
/// </summary>
public static class Factors{
	/// <summary>
	/// Obtains the factors of a number
	/// </summary>
	/// <param name="X">Number whose factors are needed</param>
	/// <returns>Array of int factors</returns>
	public static int[] FactorsOf(decimal X){
		List<int> Factors = new List<int>();
		decimal Rounded = Math.Round(X);
		for (decimal i = 1; i <= Rounded; i++){
			if ((Rounded/i) % 1 == 0){
				Factors.Add((int) i);
			}
		}
		return Factors.ToArray();
	}

	/// <summary>
	/// Gets the factors shared by both numbers
	/// </summary>
	/// <param name="X">First number whose factors are needed</param>
	/// <param name="Y">Second number whose factors are needed</param>
	/// <returns>Shared factors of X and Y</returns>
	public static int[] SharedFactorsOf(decimal X, decimal Y){
		List<int> Factors = new List<int>();
		int[] XFact = FactorsOf(X);
		int[] YFact = FactorsOf(Y);
		foreach(int XF in XFact){
			foreach(int YF in YFact){
				if (XF == YF){
					Factors.Add(XF);
				}
			}
		}
		return Factors.ToArray();
	}
}