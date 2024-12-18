using ThreadCalc.Types;

namespace ThreadCalc.Calculations;

using System;

public static class UnifiedThreadCalculator
{


    public static void ValidateClassOfFit(ThreadOrientations orientation, UnifiedClassOfFits classOfFit)
    {
        // Throws an exception if an invalid classOfFit / Orientation combination are provided.
        UnifiedClassOfFits[] internalFits = 
            [UnifiedClassOfFits._1B,UnifiedClassOfFits._2B,UnifiedClassOfFits._3B];
        UnifiedClassOfFits[] externalFits =
            [UnifiedClassOfFits._1A, UnifiedClassOfFits._2A, UnifiedClassOfFits._3A];

        if (orientation == ThreadOrientations.External && externalFits.Contains(classOfFit))
        {
            return;
        }
        if (orientation == ThreadOrientations.Internal && internalFits.Contains(classOfFit))
        {
            return;
        }
        throw new ArgumentException("Invalid class of fit for given thread orientation.");
    }



    public static decimal MajorDiameterNominal(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                               UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {
        var diameterTolerance = MajorDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
        var allowance = Allowance(basicSize, pitch, classOfFit, lengthOfEngagement);

        var result = 0m;

        if (orientation == ThreadOrientations.External)
        {
            result = basicSize - allowance - (0.5m * diameterTolerance);
        }
        else
        {
            result = basicSize - (0.5m * diameterTolerance);
        }
        return result;
    }


    public static decimal FundamentalHeight(decimal pitch)
    {
        return 0.8660254m * pitch;
    }

    public static decimal Height(decimal pitch)
    {
        return 0.54126588m * pitch;
    }

    public static decimal WidthAtPitchLine(decimal pitch)
    {
        var result = 0.5m * pitch;
        return Math.Round(result, _rounding);
    }

    public static decimal MinorDiameterTolerance(decimal basicSize, decimal pitch,
                                                 ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit,
                                                 decimal lengthOfEngagement = 0.0m,
                                                 bool isUNR = false)
    {
        if (orientation == ThreadOrientations.External)
        {
            var pitchDiameterTolerance = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);

            if (!isUNR)
            {
                if (classOfFit == UnifiedClassOfFits._1A || classOfFit == UnifiedClassOfFits._2A || classOfFit == UnifiedClassOfFits._3A)
                {
                    const decimal multiplier = 0.21650635m;
                    var result = pitchDiameterTolerance + (multiplier * pitch);
                    return Math.Round(result, _rounding);
                }
                else
                {
                    throw new Exception("Incorrect input. Class of Fit invalid.");
                }
            }
            else
            {
                const decimal multiplier2 = 0.10825318m;
                var result = pitchDiameterTolerance + (multiplier2 * pitch);
                return Math.Round(result, _rounding);
            }
        }
        else
        {
            var pitchDiameterTolerance = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, 0.0m);

            if (classOfFit == UnifiedClassOfFits._1B || classOfFit == UnifiedClassOfFits._2B)
            {
                var result = InternalMinorDiameterTolerance(basicSize, pitch);
                return Math.Round(result, _rounding);
            }
            else if (classOfFit == UnifiedClassOfFits._3B)
            {
                var result = InternalMinorDiameterTolerance(basicSize, pitch, true);
                return Math.Round(result, _rounding);
            }
            else
            {
                throw new Exception("Incorrect input parameters.");
            }
        }
    }

    private static decimal InternalMinorDiameterTolerance(decimal basicSize, decimal pitch, bool is3B = false)
    {
        var result = (0.05m * (decimal)Math.Pow((double)pitch, 2.0 / 3.0))
                   + (0.03m * (pitch / basicSize)) - 0.002m;

        if (is3B)
        {
            result = Math.Min(result, 0.12m * pitch);
        }

        return result;
    }

    public static decimal PitchDiameterTolerance(decimal basicSize, decimal pitch,
                                                 ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit,
                                                 decimal lengthOfEngagement = 0.0m)
    {
        if (basicSize == 0) throw new Exception("Basic cannot be zero.");
        if (pitch == 0) throw new Exception("Pitch cannot be zero.");

        var le = lengthOfEngagement;

        if (lengthOfEngagement == 0)
        {
            if (pitch <= 0.08333m)
            {
                le = 9 * pitch;
            }
            else
            {
                le = basicSize;
            }
        }

        var ptol2A = (0.0015m * (decimal)Math.Sqrt((double)le)) +
                     (0.015m * (decimal)Math.Pow((double)pitch, 2.0 / 3.0)) +
                     (0.0015m * (decimal)Math.Pow((double)basicSize, 1.0 / 3.0));

        decimal result;

        if (orientation == ThreadOrientations.External)
        {
            if (classOfFit == UnifiedClassOfFits._1A)
                result = 1.5m * ptol2A;
            else if (classOfFit == UnifiedClassOfFits._2A)
                result = ptol2A;
            else if (classOfFit == UnifiedClassOfFits._3A)
                result = 0.75m * ptol2A;
            else
                throw new Exception("Invalid ClassOfFit for External thread.");
        }
        else
        {
            if (classOfFit == UnifiedClassOfFits._1B)
                result = 1.95m * ptol2A;
            else if (classOfFit == UnifiedClassOfFits._2B)
                result = 1.3m * ptol2A;
            else if (classOfFit == UnifiedClassOfFits._3B)
                result = 0.975m * ptol2A;
            else
                throw new Exception("Invalid ClassOfFit for Internal thread.");
        }

        return Math.Round(result, _rounding);
    }

    public static decimal MajorDiameterTolerance(decimal basicSize, decimal pitch,
                                                 ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit,
                                                 decimal lengthOfEngagement = 0.0m)
    {
        decimal result;

        if (orientation == ThreadOrientations.External)
        {
            if (classOfFit == UnifiedClassOfFits._1A)
                result = 0.09m * (decimal)Math.Pow(Math.Sqrt((double)pitch), 1.0 / 3.0);
            else if (classOfFit == UnifiedClassOfFits._2A || classOfFit == UnifiedClassOfFits._3A)
                result = 0.06m * (decimal)Math.Pow(Math.Sqrt((double)pitch), 1.0 / 3.0);
            else
                throw new Exception("Invalid ClassOfFit for External thread.");
        }
        else
        {
            result = (0.14433757m * pitch) + PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
        }

        return Math.Round(result, _rounding);
    }

    public static decimal Allowance(decimal basicSize, decimal pitch,
                                    UnifiedClassOfFits classOfFit,
                                    decimal lengthOfEngagement = 0.0m)
    {
        if (classOfFit == UnifiedClassOfFits._3A) return 0;

        var pitchTolerance = PitchDiameterTolerance(basicSize, pitch, ThreadOrientations.External, classOfFit, lengthOfEngagement);

        if (classOfFit == UnifiedClassOfFits._1A || classOfFit == UnifiedClassOfFits._2A)
        {
            return Math.Round(0.3m * pitchTolerance, _rounding);
        }

        throw new Exception("Allowance for internal thread invalid. ClassOfFit must be external.");
    }
}
