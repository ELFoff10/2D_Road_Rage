using System;

public static class SetScoreExt
{
    public const string Symbols = "123456789";

    public static string ConvertIntToStringValue(double value, int precision = 0)
    {
        if (value < 1000)
        {
            return value.ToString();
        }

        if (value is >= 1000 and < 1000000)
        {
            value /= 1000;
            return $"{value.ToString($"F{precision}")}K";
        }

        return GetPrettyNumber(value, precision);
    }

    public static string GetPrettyNumber(double number, int precision)
    {
        int n = (int)Math.Log10(Math.Abs(number));

        number /= Math.Pow(10.0, n - (n % 3));

        return $"{number.ToString($"F{precision}")}{BigNumbers[n / 3 - 2][0]}";
    }

    private static readonly string[] BigNumbers = new string[]
    {
        "Million",
        "Billion",
        "Trillion",
        "Quadrillion",
        "Quintillion",
        "Sextillion",
        "Septillion",
        "Octillion",
        "Nonillion",
        "Decillion",
        "Undecillion",
        "Duodecillion",
        "Tredecillion",
        "Quattuordecillion",
        "Quindecillion",
        "Sedecillion",
        "Septendecillion",
        "Octodecillion",
        "Novendecillion",
        "Vigintillion",
        "Unvigintillion",
        "Duovigintillion",
        "Tresvigintillion",
        "Quattuorvigintillion",
        "Quinvigintillion",
        "Sesvigintillion",
        "Septemvigintillion",
        "Octovigintillion",
        "Novemvigintillion",
        "Trigintillion",
        "Untrigintillion",
        "Duotrigintillion",
        "Trestrigintillion",
        "Quattuortrigintillion",
        "Quintrigintillion",
        "Sestrigintillion",
        "Septentrigintillion",
        "Octotrigintillion",
        "Noventrigintillion",
        "Quadragintillion",
        "Unquadragintillion",
        "Duoquadragintillion",
        "Tresquadragintillion",
        "Quattuorquadragintillion",
        "Quinquadragintillion",
        "Sesquadragintillion",
        "Septenquadragintillion",
        "Octoquadragintillion",
        "Novenquadragintillion",
        "Quinquagintillion",
        "Unquinquagintillion",
        "Duoquinquagintillion",
        "Tresquinquagintillion",
        "Quattuorquinquagintillion",
        "Quinquinquagintillion",
        "Sesquinquagintillion",
        "Septenquinquagintillion",
        "Octoquinquagintillion",
        "Novenquinquagintillion",
        "Sexagintillion",
        "Unsexagintillion",
        "Duosexagintillion",
        "Tresexagintillion",
        "Quattuorsexagintillion",
        "Quinsexagintillion",
        "Sesexagintillion",
        "Septensexagintillion",
        "Octosexagintillion",
        "Novensexagintillion",
        "Septuagintillion",
        "Unseptuagintillion",
        "Duoseptuagintillion",
        "Treseptuagintillion",
        "Quattuorseptuagintillion",
        "Quinseptuagintillion",
        "Seseptuagintillion",
        "Septenseptuagintillion",
        "Octoseptuagintillion",
        "Novenseptuagintillion",
        "Octogintillion",
        "Unoctogintillion",
        "Duooctogintillion",
        "Treoctogintillion",
        "Quattuoroctogintillion",
        "Quinoctogintillion",
        "Sexoctogintillion",
        "Septemoctogintillion",
        "Octooctogintillion",
        "Novemoctogintillion",
        "Nonagintillion",
        "Unnonagintillion",
        "Duononagintillion",
        "Trenonagintillion",
        "Quattuornonagintillion",
        "Quinnonagintillion",
        "Senonagintillion",
        "Septenonagintillion",
        "Octononagintillion",
        "Novenonagintillion",
        "Centillion",
        "Uncentillion"
    };
}