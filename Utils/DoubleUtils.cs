Double Utils

TESTED REVIT API: 2024

Author: DTDucas aka DUONG TRAN | https://github.com/DTDucas

Shared on www.revitapidocs.com
For more information visit http://github.com/gtalarico/revitapidocs
License: http://github.com/gtalarico/revitapidocs/blob/master/LICENSE.md



public static class DoubleUtils
{
    public static double FeetToMet(this double feet) => feet * 0.304800;
    public static double FeetToMet(this int feet) => feet * 0.304800;
    public static double MetToFeet(this double met) => met * 3.280840;
    public static double MetToFeet(this int met) => met * 3.280840;
    public static double MmToFeet(this double mm) => mm * 3.280840 / 1000.0;
    public static double MmToFeet(this int mm) => mm * 3.280840 / 1000.0;
    public static double FeetToMm(this double feet) => feet * 0.304800 * 1000;
    public static double FeetToMm(this int feet) => feet * 0.304800 * 1000;

    public static double InchesToMm(this double inches) => inches * 25.4;
    public static double InchesToMm(this int inches) => inches * 25.4;
    public static double MmToInches(this double mm) => mm / 25.4;
    public static double MmToInches(this int mm) => mm / 25.4;

    public static double CmToInches(this double cm) => cm / 2.54;
    public static double CmToInches(this int cm) => cm / 2.54;
    public static double InchesToCm(this double inches) => inches * 2.54;
    public static double InchesToCm(this int inches) => inches * 2.54;

    public static double MetToCm(this double met) => met * 100;
    public static double MetToCm(this int met) => met * 100;
    public static double CmToMet(this double cm) => cm / 100;
    public static double CmToMet(this int cm) => cm / 100;

    public static double SquareMetToSquareFeet(this double squareMet) => squareMet * 10.7639;
    public static double SquareFeetToSquareMet(this double squareFeet) => squareFeet / 10.7639;

    public static double CubicMetToCubicFeet(this double cubicMet) => cubicMet * 35.3147;
    public static double CubicFeetToCubicMet(this double cubicFeet) => cubicFeet / 35.3147;

    public static double RadiansToDegrees(this double radians) => radians * (180 / Math.PI);
    public static double DegreesToRadians(this double degrees) => degrees * (Math.PI / 180);

    public static double RoundToNearest(this double value, double nearest) => Math.Round(value / nearest) * nearest;
    public static int RoundToNearest(this int value, int nearest) => (int)Math.Round((double)value / nearest) * nearest;

    public static bool IsAlmostEqualTo(this double value1, double value2, double tolerance = 1e-9)
        => Math.Abs(value1 - value2) < tolerance;

    public static double Clamp(this double value, double min, double max)
        => Math.Max(min, Math.Min(max, value));
}