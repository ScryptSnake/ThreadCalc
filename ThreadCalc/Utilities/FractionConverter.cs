using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadCalc.Utilities;



public static class FractionConverter
{
    public static string Dec2Frac(decimal input, bool throwOnInvalid=false, decimal tolerance = 0.0m)
    {
        // Allowed denominators.
        int[] denominators = { 2, 4, 8, 16 };

        // Find closest fraction.
        foreach (int denominator in denominators)
        {
            // Multiply the decimal by the denominator to find the numerator.
            int numerator = (int)Math.Round(input * denominator);

            // If the numerator matches after converting back, it's valid.
            decimal fractionValue = (decimal)numerator / denominator;

            if (Math.Abs(input - fractionValue) < tolerance) 
            {
                // Reduce the fraction
                int gcd = GCD(numerator, denominator);
                int reducedNumerator = numerator / gcd;
                int reducedDenominator = denominator / gcd;

                return $"{reducedNumerator}/{reducedDenominator}";
            }
        }

        if (throwOnInvalid == true)
            throw new ArgumentOutOfRangeException("Unable to divide decimal.");
        return input.ToString();
    }

    // Helper method to calculate the greatest common divisor
    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }
}
