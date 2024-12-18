using ThreadCalc.Types;

namespace ThreadCalc.Calculations;

using System;

public static class UnifiedThreadCalculator
{

    private static void Validate(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                 UnifiedClassOfFits classOfFit)
    {
        if (ValidateBasicSize(basicSize) == false)
            throw new ArgumentException("Size provided for calculation is invalid.");
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Pitch provided for calculation is invalid.");
        if (ValidateClassOfFit(orientation, classOfFit) == false)
            throw new ArgumentException("Invalid orientation-class of fit combination.");
    }

    public static bool ValidateBasicSize(decimal basicSize)
    {
        if (basicSize <= 0) return false;
        return true;
    }

    public static bool ValidatePitch(decimal pitch)
    {
        if (pitch <= 0) return false;
        return true;
    }

    public static bool ValidateClassOfFit(ThreadOrientations orientation, UnifiedClassOfFits classOfFit)
    {
        // Throws an exception if an invalid classOfFit / Orientation combination are provided.
        UnifiedClassOfFits[] internalFits = 
            [UnifiedClassOfFits._1B,UnifiedClassOfFits._2B,UnifiedClassOfFits._3B];
        UnifiedClassOfFits[] externalFits =
            [UnifiedClassOfFits._1A, UnifiedClassOfFits._2A, UnifiedClassOfFits._3A];

        if (orientation == ThreadOrientations.External && externalFits.Contains(classOfFit))
            return true;
        if (orientation == ThreadOrientations.Internal && internalFits.Contains(classOfFit))
            return true;
        return false;
    }



    public static decimal MajorDiameterNominal(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                               UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        var diameterTolerance = MajorDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
        var allowance = Allowance(basicSize, pitch, classOfFit, lengthOfEngagement);

        var result = 0m;

        if (orientation == ThreadOrientations.External)
            result = basicSize - allowance - (0.5m * diameterTolerance);
        else
            result = basicSize - (0.5m * diameterTolerance);
        return result;
    }


    public static decimal FundamentalHeight(decimal pitch)
    {
        if (ValidatePitch(pitch) == false) 
            throw new ArgumentException("Invalid pitch provided.");
        return 0.8660254m * pitch;
    }

    public static decimal Height(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.54126588m * pitch;
    }

    public static decimal WidthAtPitchLine(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.5m * pitch;
    }

    public static decimal MinorDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m,
                                                 bool isUNR = false)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        if (orientation == ThreadOrientations.External)
        {
            var pitchDiameterTolerance = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);

            if (!isUNR)
            {
                const decimal multiplier = 0.21650635m;
                var result = pitchDiameterTolerance + (multiplier * pitch);
                return result;
            }
            else
            {
                const decimal multiplier2 = 0.10825318m;
                var result = pitchDiameterTolerance + (multiplier2 * pitch);
                return result;
            }
        }
        else // Internal Thread.
        {
            // TODO: Check no length of engagement on PitchDiameterTolerance on internal thread? 
            var pitchDiameterTolerance = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, 0.0m);
            var result = (0.05m * (decimal)Math.Pow((double)pitch, 2.0 / 3.0))
                          + (0.03m * (pitch / basicSize)) - 0.002m;

            if (classOfFit == UnifiedClassOfFits._3B) return Math.Min(result, 0.12m * pitch);
            return result;
        }
    }

    public static decimal PitchDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        var le = lengthOfEngagement;

        if (lengthOfEngagement == 0)
        {
            if (pitch <= 0.08333m)
                le = 9 * pitch;
            else
                le = basicSize;
        }
        // All tolerance calculations are based upon 2A class of fit.
        var pitchDiaTol2A = (0.0015m * (decimal)Math.Sqrt((double)le)) +
                     (0.015m * (decimal)Math.Pow((double)pitch, 2.0 / 3.0)) +
                     (0.0015m * (decimal)Math.Pow((double)basicSize, 1.0 / 3.0));


        decimal result = classOfFit switch
        {
            // External
            UnifiedClassOfFits._2A => pitchDiaTol2A,
            UnifiedClassOfFits._1A => 1.5m * pitchDiaTol2A,
            UnifiedClassOfFits._3A => 0.75m * pitchDiaTol2A,

            UnifiedClassOfFits._1B => 1.95m * pitchDiaTol2A,
            UnifiedClassOfFits._2B => 1.3m * pitchDiaTol2A,
            UnifiedClassOfFits._3B => 0.975m * pitchDiaTol2A,
            _ => throw new ArgumentException("Unknown class of fit.")
        };

        return result;
    }

    public static decimal MajorDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {
        Validate(basicSize, pitch, orientation, classOfFit);
        decimal result;
        if (orientation == ThreadOrientations.External)
        {
            result = (decimal)Math.Pow(Math.Sqrt((double)pitch), 1.0 / 3.0);
            if (classOfFit == UnifiedClassOfFits._1A)
                result = 0.09m * result;
            else
                result = 0.06m * result;
        }
        else
        {
            result = (0.14433757m * pitch) + 
                PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
        }

        return result;
    }

    public static decimal Allowance(decimal basicSize, decimal pitch, UnifiedClassOfFits classOfFit,
                                    decimal lengthOfEngagement = 0.0m)
    {
        if (!(ValidateBasicSize(basicSize) || ValidatePitch(pitch)))
            throw new ArgumentException("Invalid sizes provided for calculation.");

        if (classOfFit == UnifiedClassOfFits._3A) return 0;

        var pitchTolerance = PitchDiameterTolerance(basicSize, pitch, ThreadOrientations.External, classOfFit, lengthOfEngagement);

        if (classOfFit == UnifiedClassOfFits._1A || classOfFit == UnifiedClassOfFits._2A)
        {
            return 0.3m * pitchTolerance;
        }
        // Allowance is only computed for external threads.
        throw new Exception("Allowance for internal thread invalid. ClassOfFit must be external.");
    }
}
