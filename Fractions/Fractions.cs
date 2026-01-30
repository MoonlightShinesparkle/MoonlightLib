using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace MoonlightLib.Fractions;

/// <summary>
/// Represents a division through 2 decimals, generally more precise than pure divisions
/// </summary>
public class Fraction : IComparable, IConvertible, ISignedNumber<Fraction>, IEquatable<Fraction>, IMinMaxValue<Fraction>, IComparisonOperators<Fraction,Fraction,bool>
, IDivisionOperators<Fraction,Fraction,Fraction>, IDecrementOperators<Fraction>, IEqualityOperators<Fraction,Fraction,bool>, IIncrementOperators<Fraction>
, IModulusOperators<Fraction,Fraction,decimal>, INumberBase<Fraction>{

	#region General data

	/// <summary>
	/// The uppermost half of the fraction; the numerator
	/// </summary>
	public decimal Upper = 0;

	/// <summary>
	/// The lowermost half of the fraction; the denominator
	/// </summary>
	public decimal Lower = 1;

	/// <summary>
	/// Generate a new fraction
	/// </summary>
	/// <param name="Numerator">The uppermost half of the fraction</param>
	/// <param name="Denominator">The lowermost half of the fraction</param>
	public Fraction(decimal Numerator, decimal Denominator){
		Upper = Numerator;
		Lower = Denominator;
	}

	/// <summary>
	/// Generate a fraction from a single value (x/1)
	/// </summary>
	/// <param name="Numerator">The number to turn into a fraction</param>
	public Fraction(decimal Numerator){
		Upper = Numerator;
	}

	public Fraction(){}

	/// <summary>
	/// Decimal equality of the fraction
	/// </summary>
	public decimal Value{get{return Upper/Lower;}}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}

	#endregion General data

	#region Numeric values

	/// <summary>
	/// Returns -1/1
	/// </summary>
	public static Fraction NegativeOne => new Fraction(-1);

	/// <summary>
	/// Returns 1/1
	/// </summary>
	public static Fraction One => new Fraction(1);

	/// <summary>
	/// The radix of fractions
	/// </summary>
	public static int Radix => 10;

	/// <summary>
	/// Returns 0/1
	/// </summary>
	public static Fraction Zero => new Fraction();

	/// <summary>
	/// Returns Zero
	/// </summary>
	public static Fraction AdditiveIdentity => Zero;


	/// <summary>
	/// Returns One
	/// </summary>
	public static Fraction MultiplicativeIdentity => One;

	/// <summary>
	/// A NaN fraction
	/// </summary>
	public static Fraction NaN => new Fraction(0,0);

	/// <summary>
	/// Alias for MaxValue
	/// </summary>
	static Fraction IMinMaxValue<Fraction>.MaxValue => MaxValue;

	/// <summary>
	/// Alias for MinValue
	/// </summary>
	static Fraction IMinMaxValue<Fraction>.MinValue => MinValue;

	/// <summary>
	/// Max value capable of being held by a fraction; decimal.MaxValue/1
	/// </summary>
	public static Fraction MaxValue = new Fraction(decimal.MaxValue);

	/// <summary>
	/// Min value capable of being held by a fraction; decimal.MinValue/1
	/// </summary>
	public static Fraction MinValue = new Fraction(decimal.MinValue);

	#endregion Numeric values

	#region Processing and comparisons

	/// <summary>
	/// Processes the signs of the fraction in such manner that the sign remains in Upper and double signs nullify
	/// each other
	/// </summary>
	protected void ProcessSign(){
		if (Upper < 0 && Lower < 0){
			Upper = Math.Abs(Upper);
			Lower = Math.Abs(Lower);
		} else if (Lower < 0){
			Upper = -Upper;
			Lower = Math.Abs(Lower);
		}
	}

	/// <summary>
	/// Searches for a viable equal fraction, only works with wholes
	/// </summary>
	public void Simplify(){
		ProcessSign();
		if(Upper % 1 == 0 && Lower % 1 == 0){
			while (true){
				int[] Fact = Factors.SharedFactorsOf(Upper,Lower);
				if (Fact.Length > 1){
					int Biggest = Fact[Fact.Length-1];
					Upper /= Biggest;
					Lower /= Biggest;
				} else {
					break;
				}
			}
		}
	}

	/// <summary>
	/// Allows for a simplification without storing in a variable beforehand or running simplification after storing
	/// </summary>
	/// <returns>The simplified fraction</returns>
	public Fraction GetSimplified(){
		Simplify();
		return this;
	}

	/// <summary>
	/// Manages comparisons between values and Fractions
	/// </summary>
	/// <param name="obj">The value to be compared against</param>
	/// <returns>An int representing the comparison's result between -1 and 1</returns>
	/// <exception cref="ArgumentException">When object can't be turned into a decimal</exception>
	public int CompareTo(object? obj)
	{
		if (obj == null)
		{
			return 1;
		}
		if (obj is decimal Comparable)
		{
			if (Comparable < Value) return -1;
			if (Comparable > Value) return 1;
			return 0;
		}
		throw new ArgumentException($"Can't compare \"{obj.GetType().Name}\" with a fraction");
	}

	/// <summary>
	/// Manages comparisons between fractions
	/// </summary>
	/// <param name="obj">The fraction to compare against</param>
	/// <returns>An int representing the comparison's result between -1 and 1</returns>
	public int CompareTo(Fraction? obj){
		if (obj == null){
			return 1;
		}
		decimal ThisComparable = Upper;
		decimal ThatComparable = obj.Upper;
		// Convert into comparable fractions
		if (obj.Lower != Lower){
			ThisComparable *= obj.Lower;
			ThatComparable *= Lower;
		}
		if (ThatComparable < ThisComparable){
			return -1;
		} else if (ThatComparable > ThisComparable){
			return 1;
		} else {
			return 0;
		}
	}

	/// <summary>
	/// Quick comparison between integrers and fractions
	/// </summary>
	/// <param name="obj">An integrer to compare against</param>
	/// <returns>An int representing the comparison's result between -1 and 1</returns>
	public int CompareTo(int obj){
		decimal Value = obj*Lower;
		if (Value < Upper){
			return -1;
		} else if (Value > Upper){
			return 1;
		} else {
			return 0;
		}
	}

	#endregion Processing and comparisons

	public TypeCode GetTypeCode()
	{
		return TypeCode.Decimal;
	}

	#region Conversions

	[DoesNotReturn]
	public bool ToBoolean(IFormatProvider? provider)
	{
		throw new InvalidCastException("Can't turn a fraction into a boolean");
	}

	public byte ToByte(IFormatProvider? provider)
	{
		return (byte) Value;
	}

	public char ToChar(IFormatProvider? provider)
	{
		return (char) Value;
	}

	[DoesNotReturn]
	public DateTime ToDateTime(IFormatProvider? provider)
	{
		throw new InvalidCastException("Can't turn a fraction into a date");
	}

	public decimal ToDecimal(IFormatProvider? provider)
	{
		return Value;
	}

	public double ToDouble(IFormatProvider? provider)
	{
		return (double) Value;
	}

	public short ToInt16(IFormatProvider? provider)
	{
		return (short) Value;
	}

	public int ToInt32(IFormatProvider? provider)
	{
		return (int) Value;
	}

	public long ToInt64(IFormatProvider? provider)
	{
		return (long) Value;
	}

	public sbyte ToSByte(IFormatProvider? provider)
	{
		return (sbyte) Value;
	}

	public float ToSingle(IFormatProvider? provider)
	{
		return (float) Value;
	}

	private string StrAddLength(string Forced, int NewLength){
		if (NewLength - Forced.Length > 0){
			Forced = new string(' ',NewLength-Forced.Length) + Forced;
		}
		return Forced;
	}

	[DoesNotReturn]
	public object ToType(Type conversionType, IFormatProvider? provider)
	{
		throw new InvalidCastException("Can't generate a cast through this form");
	}

	public ushort ToUInt16(IFormatProvider? provider)
	{
		return (ushort) Value;
	}

	public uint ToUInt32(IFormatProvider? provider)
	{
		return (uint) Value;
	}

	public ulong ToUInt64(IFormatProvider? provider)
	{
		return (ulong) Value;
	}

	#endregion Conversions

	#region Properties and math

	public static Fraction Abs(Fraction value)
	{
		return new Fraction(Math.Abs(value.Upper),Math.Abs(value.Lower));
	}

	public static bool IsCanonical(Fraction value)
	{
		return true;
	}

	public static bool IsComplexNumber(Fraction value)
	{
		return false;
	}

	public static bool IsEvenInteger(Fraction value)
	{
		return decimal.IsEvenInteger(value.Value);
	}

	public static bool IsFinite(Fraction value)
	{
		return true;
	}

	public static bool IsImaginaryNumber(Fraction value)
	{
		return false;
	}

	public static bool IsInfinity(Fraction value)
	{
		return false;
	}

	public static bool IsInteger(Fraction value)
	{
		return decimal.IsInteger(value.Value);
	}

	public static bool IsNaN(Fraction value)
	{
		return value.Lower == 0;
	}

	public static bool IsNegative(Fraction value)
	{
		value.ProcessSign();
		return value.Upper < 0;
	}

	public static bool IsNegativeInfinity(Fraction value)
	{
		return false;
	}

	public static bool IsNormal(Fraction value)
	{
		return value.Upper != 0 && value.Lower > 0 && decimal.IsInteger(value.Upper) && decimal.IsInteger(value.Lower);
	}

	public static bool IsOddInteger(Fraction value)
	{
		return decimal.IsOddInteger(value.Value);
	}

	public static bool IsPositive(Fraction value)
	{
		return !IsNegative(value);
	}

	public static bool IsPositiveInfinity(Fraction value)
	{
		return false;
	}

	public static bool IsRealNumber(Fraction value)
	{
		return !IsNaN(value);
	}

	public static bool IsSubnormal(Fraction value)
	{
		return !IsNormal(value);
	}

	public static bool IsZero(Fraction value)
	{
		return !IsNaN(value) && value.Upper == 0;
	}

	public static Fraction MaxMagnitude(Fraction x, Fraction y)
	{
		return x.Value >= y.Value ? x : y;
	}

	public static Fraction MaxMagnitudeNumber(Fraction x, Fraction y)
	{
		return IsNaN(x) ? y 
			: IsNaN(y) ? x 
			: MaxMagnitude(x,y);
	}

	public static Fraction MinMagnitude(Fraction x, Fraction y)
	{
		return MaxMagnitude(x,y) == x? y : x;
	}

	public static Fraction MinMagnitudeNumber(Fraction x, Fraction y)
	{
		return IsNaN(x) ? y 
		: IsNaN(y) ? x 
		: MinMagnitude(x,y);
	}

	#endregion Properties and math

	#region Parsing

	public static Fraction Parse(ReadOnlySpan<char> s){
		return Parse(s,NumberStyles.None,null);
	}

	public static Fraction Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
	{
		return Parse(s,NumberStyles.None,provider);
	}

	public static Fraction Parse(ReadOnlySpan<char> s,NumberStyles style){
		return Parse(s,style,null);
	}

	public static Fraction Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
	{
		decimal Useable = decimal.Parse(s,style,provider);
		return new Fraction(Useable);
	}

	public static Fraction Parse(string s){
		return Parse(s,NumberStyles.None,null);
	}

	public static Fraction Parse(string s, NumberStyles style){
		return Parse(s,style,null);
	}

	public static Fraction Parse(string s, IFormatProvider? provider)
	{
		return Parse(s,NumberStyles.None,provider);
	}

	public static Fraction Parse(string s, NumberStyles style, IFormatProvider? provider)
	{
		ArgumentNullException.ThrowIfNull(s);
		ArgumentException.ThrowIfNullOrWhiteSpace(s);
		string[] Halves = s.Split("/");
		if (!s.Contains('/') || Halves.Length == 1){
			if (decimal.TryParse(s,style,provider,out decimal Res)){
				return new Fraction(Res);
			}
			throw new ArgumentException("Can't turn string into a decimal");
		}
		if (Halves.Length == 2){
			if (decimal.TryParse(Halves[0],style,provider,out decimal Left) && decimal.TryParse(Halves[1],style,provider,out decimal Right)){
				Fraction Returnable = new Fraction(Left,Right);
				Returnable.Simplify();
				return Returnable;
			}
			throw new ArgumentException("Malformed number located in a side of the fraction");
		}
		throw new ArgumentException("Malformed fraction representative");
	}

	public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		bool Returnable = false;
		try{
			result = Parse(s,style,provider);
			Returnable = true;
		} catch{
			result = Zero;
		}
		return Returnable;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		bool Returnable = false;
		try{
			result = Parse(s ?? "",style,provider);
			Returnable = true;
		} catch{
			result = Zero;
		}
		return Returnable;
	}

	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		bool Returnable = false;
		try{
			result = Parse(s,provider);
			Returnable = true;
		} catch{
			result = Zero;
		}
		return Returnable;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Fraction result)
	{
		bool Returnable = false;
		try{
			result = Parse(s ?? "",provider);
			Returnable = true;
		} catch{
			result = Zero;
		}
		return Returnable;
	}

	public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Fraction result)
	{
		bool Returnable = false;
		result = Zero;
		try{
			result = Parse(s ?? "");
			Returnable = true;
		} catch {}
		return Returnable;
	}

	public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out Fraction result) where TOther : INumberBase<TOther>
	{
		if (value is Fraction FracVal){
			result = FracVal;
			return true;
		} else if (value is int IntVal){
			result = new Fraction((decimal) IntVal);
			return true;
		} else if (value is long LongVal){
			result = new Fraction((decimal) LongVal);
			return true;
		} else if (value is sbyte SByteVal){
			result = new Fraction((decimal) SByteVal);
			return true;
		} else if (value is byte ByteVal){
			result = new Fraction((decimal) ByteVal);
			return true;
		} else if (value is float FloatVal){
			result = new Fraction((decimal) FloatVal);
			return true;
		} else if (value is double DoubleVal){
			result = new Fraction((decimal) DoubleVal);
			return true;
		} else if (value is short ShortVal){
			result = new Fraction((decimal) ShortVal);
			return true;
		} else if (value is uint UIntVal){
			result = new Fraction((decimal) UIntVal);
			return true;
		} else if (value is ulong ULongVal){
			result = new Fraction((decimal) ULongVal);
			return true;
		} else if (value is Half HalfVal) {
			result = new Fraction((decimal) HalfVal);
			return true;
		} else if (value is Int128 HugeVal){
			result = new Fraction((decimal) HugeVal);
			return true;
		} else if (value is nint NIntVal){
			result = new Fraction((decimal) NIntVal);
			return true;
		} else {
			result = Zero;
			return false;
		}
	}

	public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out Fraction result) where TOther : INumberBase<TOther>
	{
		if (value is Fraction FracVal){
			result = FracVal;
			return true;
		} else if (value is int IntVal){
			result = IntVal <= decimal.MinValue? MinValue 
			: IntVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) IntVal);
			return true;
		} else if (value is long LongVal){
			result = LongVal <= decimal.MinValue? MinValue 
			: LongVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) LongVal);
			return true;
		} else if (value is sbyte SByteVal){
			result = SByteVal <= decimal.MinValue? MinValue 
			: SByteVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) SByteVal);
			return true;
		} else if (value is byte ByteVal){
			result = ByteVal <= decimal.MinValue? MinValue 
			: ByteVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) ByteVal);
			return true;
		} else if (value is float FloatVal){
			decimal Comparable = (decimal) FloatVal;
			result = Comparable <= decimal.MinValue? MinValue 
			: Comparable >= decimal.MaxValue? MaxValue 
			: new Fraction(Comparable);
			return true;
		} else if (value is double DoubleVal){
			decimal Comparable = (decimal) DoubleVal;
			result = Comparable <= decimal.MinValue? MinValue 
			: Comparable >= decimal.MaxValue? MaxValue 
			: new Fraction(Comparable);
			return true;
		} else if (value is short ShortVal){
			result = ShortVal <= decimal.MinValue? MinValue 
			: ShortVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) ShortVal);
			return true;
		} else if (value is uint UIntVal){
			result = UIntVal <= decimal.MinValue? MinValue 
			: UIntVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) UIntVal);
			return true;
		} else if (value is ulong ULongVal){
			result = ULongVal <= decimal.MinValue? MinValue 
			: ULongVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) ULongVal);
			return true;
		} else if (value is Half HalfVal) {
			decimal Comparable = (decimal) HalfVal;
			result = Comparable <= decimal.MinValue? MinValue 
			: Comparable >= decimal.MaxValue? MaxValue 
			: new Fraction(Comparable);
			return true;
		} else if (value is Int128 HugeVal){
			decimal Comparable = (decimal) HugeVal;
			result = Comparable <= decimal.MinValue? MinValue 
			: Comparable >= decimal.MaxValue? MaxValue 
			: new Fraction(Comparable);
			return true;
		} else if (value is nint NIntVal){
			result = NIntVal <= decimal.MinValue? MinValue 
			: NIntVal >= decimal.MaxValue? MaxValue 
			: new Fraction((decimal) NIntVal);
			return true;
		} else {
			result = Zero;
			return false;
		}
	}

	public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out Fraction result) where TOther : INumberBase<TOther>
	{
		return TryConvertFromSaturating<TOther>(value,out result);
	}

	public static bool TryConvertToChecked<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
	{
		decimal Convertible = value.Value;
		if (typeof(TOther) == typeof(Fraction)){
			result = (TOther)(object)value;
			return true;
		} else if (typeof(TOther) == typeof(int) || typeof(TOther) == typeof(long) || typeof(TOther) == typeof(sbyte) ||
		typeof(TOther) == typeof(byte) || typeof(TOther) == typeof(float) || typeof(TOther) == typeof(double)
		|| typeof(TOther) == typeof(short) || typeof(TOther) == typeof(uint) || typeof(TOther) == typeof(ulong) ||
		typeof(TOther) == typeof(Half) || typeof(TOther) == typeof(Int128) || typeof(TOther) == typeof(nint)){
			result = (TOther)(object)Convertible;
			return true;
		} else {
			result = default;
			return false;
		}
	}

	public static bool TryConvertToSaturating<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
	{
		decimal Convertible = value.Value;
		if (typeof(TOther) == typeof(Fraction)){
			result = (TOther)(object)value;
			return true;
		} else if (typeof(TOther) == typeof(int)){
			int Actual = Convertible <= int.MinValue ? int.MinValue
			: Convertible >= int.MaxValue? int.MaxValue
			: (int) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(long)){
			long Actual = Convertible <= long.MinValue ? long.MinValue
			: Convertible >= long.MaxValue? long.MaxValue
			: (long) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(sbyte)){
			sbyte Actual = Convertible <= sbyte.MinValue ? sbyte.MinValue
			: Convertible >= sbyte.MaxValue? sbyte.MaxValue
			: (sbyte) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(byte)){
			byte Actual = Convertible <= byte.MinValue ? byte.MinValue
			: Convertible >= byte.MaxValue? byte.MaxValue
			: (byte) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(float)){
			float Alias = (float) Convertible;
			float Actual = Alias <= float.MinValue ? float.MinValue
			: Alias >= float.MaxValue? float.MaxValue
			: (float) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(double)){
			double Alias = (double) Convertible;
			double Actual = Alias <= double.MinValue ? double.MinValue
			: Alias >= double.MaxValue? double.MaxValue
			: (double) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(short)){
			short Actual = Convertible <= short.MinValue ? short.MinValue
			: Convertible >= short.MaxValue? short.MaxValue
			: (short) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(uint)){
			uint Actual = Convertible <= uint.MinValue ? uint.MinValue
			: Convertible >= uint.MaxValue? uint.MaxValue
			: (uint) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(ulong)){
			ulong Actual = Convertible <= ulong.MinValue ? ulong.MinValue
			: Convertible >= ulong.MaxValue? ulong.MaxValue
			: (ulong) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(Half)){
			Half Alias = (Half) Convertible;
			Half Actual = Alias <= Half.MinValue ? Half.MinValue
			: Alias >= Half.MaxValue? Half.MaxValue
			: (Half) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(Int128)){
			Int128 Alias = (Int128) Convertible;
			Int128 Actual = Alias <= Int128.MinValue ? Int128.MinValue
			: Alias >= Int128.MaxValue? Int128.MaxValue
			: (Int128) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else if (typeof(TOther) == typeof(nint)){
			nint Actual = Convertible <= nint.MinValue ? nint.MinValue
			: Convertible >= nint.MaxValue? nint.MaxValue
			: (nint) Convertible;
			result = (TOther)(object)Actual;
			return true;
		} else {
			result = default;
			return false;
		}
	}

	public static bool TryConvertToTruncating<TOther>(Fraction value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
	{
		return TryConvertToSaturating<TOther>(value,out result);
	}

	#endregion Parsing

	#region Formatting and equality

	public bool Equals(Fraction? other)
	{
		if (other is null){
			return this is null;
		}
		// Make more precise
		if (Lower == other.Lower){
			// Both denominators are equal
			return Upper == other.Upper;
		} else{
			// Both denominators are unequal, change upper values to make equal
			decimal ThisUpper = Upper * other.Lower;
			decimal ThatUpper = other.Upper * Lower;
			return ThisUpper == ThatUpper;
		}
	}

	public override bool Equals(object? obj)
	{
		if (obj is null){
			return this is null;
		} else if (obj is Fraction Converted){
			return Equals(Converted);
		} else if (TryParse(obj.ToString() ?? "", out Fraction? New)){
			return Equals(New);
		} else {
			return false;
		}
	}

	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
	{
		return Value.TryFormat(destination,out charsWritten,format,provider);
	}

	#endregion Formatting and equality

	#region ToString functions

	public string ToString(string? format, IFormatProvider? provider)
	{
		if (Lower == 1){
			return Upper.ToString();
		} else if (Lower == 0){
			return "NaN";
		}
		string UpperStr = decimal.Abs(Upper).ToString(provider);
		string LowerStr = Lower.ToString(provider);
		int MaxLength = Math.Max(UpperStr.Length, LowerStr.Length);
		bool IsNeg = Upper < 0;
		string Padding = IsNeg? "  " : "";
		return $"{Padding}{StrAddLength(UpperStr,MaxLength)}\n{(IsNeg? "- ":"")}{new string('-',MaxLength)} [{Value.ToString(provider)}]\n{Padding}{StrAddLength(LowerStr,MaxLength)}";
	}

	public string ToString(string? format)
	{
		return ToString(format, null);
	}

	public string ToString(IFormatProvider? provider)
	{
		return ToString(null,provider);
	}

	public override string ToString()
	{
		return ToString(false,null);
	}

	public string ToString(bool OnlyValue,IFormatProvider? provider)
	{
		if (OnlyValue){
			return Value.ToString();
		} else {
			return ToString(provider);
		}
	}

	#endregion ToString functions

	#region Operators

	public static Fraction operator +(Fraction left, Fraction right)
	{
		Fraction Returnable;
		if (left.Lower == right.Lower){
			Returnable = new Fraction(left.Upper + right.Upper,left.Lower);
		} else {
			Returnable = new Fraction((left.Upper * right.Lower) + (right.Upper * left.Lower),left.Lower * right.Lower);
		}
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator +(int left, Fraction right)
	{
		return new Fraction(right.Upper+right.Lower*left,right.Lower);
	}

	public static Fraction operator +(Fraction left, int right)
	{
		left.Upper += left.Lower*right;
		return new Fraction(left.Upper + left.Lower*right,left.Lower);
	}

	public static Fraction operator --(Fraction value)
	{
		return new Fraction(value.Upper-value.Lower,value.Lower);
	}

	public static Fraction operator /(Fraction left, Fraction right)
	{
		Fraction Returnable = new Fraction(left.Upper * right.Lower,left.Lower * right.Upper);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator /(Fraction left, int right)
	{
		Fraction Returnable = new Fraction(left.Upper,left.Lower*right);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator /(int left, Fraction right)
	{
		Fraction Returnable = new Fraction(left)/right;
		Returnable.Simplify();
		return Returnable;
	}

	public static bool operator ==(Fraction? left, Fraction? right)
	{
		if (left is null || right is null){
			return left is null && right is null;
		} else if (left.Lower == right.Lower){
			return left.Upper == right.Upper;
		} else {
			return (left.Upper * right.Lower) == (right.Upper * left.Lower);
		}
	}

	public static bool operator !=(Fraction? left, Fraction? right)
	{
		return !(left == right);
	}

	public static Fraction operator ++(Fraction value)
	{
		return new Fraction(value.Upper + value.Lower,value.Lower);
	}

	public static Fraction operator *(Fraction left, Fraction right)
	{
		Fraction Returnable = new Fraction(left.Upper*right.Upper,left.Lower*right.Lower);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator *(Fraction left, int right)
	{
		Fraction Returnable = new Fraction(left.Upper*right,left.Lower);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator *(int left, Fraction right)
	{
		Fraction Returnable = new Fraction(right.Upper*left,right.Lower);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator -(Fraction left, Fraction right)
	{
		Fraction Returnable;
		if (left.Lower == right.Lower){
			Returnable = new Fraction(left.Upper - right.Upper,left.Lower);
		} else {
			Returnable = new Fraction((left.Upper * right.Lower) - (right.Upper * left.Lower),left.Lower * right.Lower);
		}
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator -(Fraction left, int right)
	{
		Fraction Returnable = new Fraction(left.Upper-right*left.Lower,left.Lower);
		Returnable.Simplify();
		return Returnable;
	}

	public static Fraction operator -(int left, Fraction right)
	{
		decimal Mul = (decimal) left * right.Lower;
		return new Fraction(Mul - right.Upper,right.Lower);
	}

	public static Fraction operator -(Fraction value)
	{
		return new Fraction(value.Upper * -1,value.Lower);
	}

	public static Fraction operator +(Fraction value)
	{
		return value;
	}

	public static bool operator <(Fraction left, Fraction right)
	{
		if (left.Lower == right.Lower){
			return left.Upper < right.Upper;
		} else {
			return (left.Upper*right.Lower) < (right.Upper*left.Lower);
		}
	}

	public static bool operator <=(Fraction left, Fraction right)
	{
		if (left.Lower == right.Lower){
			return left.Upper <= right.Upper;
		} else {
			return (left.Upper*right.Lower) <= (right.Upper*left.Lower);
		}
	}

	public static bool operator >(Fraction left, Fraction right)
	{
		if (left.Lower == right.Lower){
			return left.Upper > right.Upper;
		} else {
			return (left.Upper*right.Lower) > (right.Upper*left.Lower);
		}
	}

	public static bool operator >=(Fraction left, Fraction right)
	{
		if (left.Lower == right.Lower){
			return left.Upper >= right.Upper;
		} else {
			return (left.Upper*right.Lower) >= (right.Upper*left.Lower);
		}
	}

	public static decimal operator %(Fraction left, Fraction right){
		return left.Value % right.Value;
	}

	#endregion Operators
}