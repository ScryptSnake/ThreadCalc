using ThreadCalc.Types;

namespace ThreadCalc.Calculations;

using System;

/// <summary>
/// Provides methods for computing various specifications about a Unified Thread.
/// This code references ASME B1.1-2019 Ch 8.3 'Formulas for Limits of Size'
/// </summary>
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

    /// <summary>
    /// Tests the size given is greater than zero. 
    /// </summary>
    public static bool ValidateBasicSize(decimal basicSize)
    {
        if (basicSize <= 0) return false;
        return true;
    }

    /// <summary>
    /// Tests the pitch provided is greater than zero.
    /// </summary>
    public static bool ValidatePitch(decimal pitch)
    {
        if (pitch <= 0) return false;
        return true;
    }

    /// <summary>
    /// Tests whether the class of fit provided is valid for the provided orientation. 
    /// </summary>
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

    /// <summary>
    /// The fundamental height is the height of the thread with an infinitely sharp crest and root.
    /// </summary>
    public static decimal FundamentalHeight(decimal pitch)
    {
        if (ValidatePitch(pitch) == false) 
            throw new ArgumentException("Invalid pitch provided.");
        return 0.8660254m * pitch;
    }

    /// <summary>
    /// Computes the height (Basic height) of the thread with a flat root and crest.
    /// Warning:  Does not include UNR external threads.
    /// </summary>
    public static decimal Height(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.54126588m * pitch;
    }

    /// <summary>
    /// Computes the height (Hs) of an external UNR thread.
    /// </summary>
    public static decimal HeightUnrExternal(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.59539247m * pitch;
    }


    /// <summary>
    /// Computes the width of the thread at the pitch line. 
    /// </summary>
    public static decimal WidthAtPitchLine(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.5m * pitch;
    }

    /// <summary>
    /// Allowance provides clearance for two threads to properly mate. It is applied to external thread features only.
    /// </summary>
    public static decimal Allowance(decimal basicSize, decimal pitch, UnifiedClassOfFits classOfFit,
                                decimal? lengthOfEngagement)
    {
        if (!(ValidateBasicSize(basicSize) || ValidatePitch(pitch)))
            throw new ArgumentException("Invalid sizes provided for calculation.");

        if (classOfFit == UnifiedClassOfFits._3A) return 0;

        var pitchTolerance = PitchDiameterTolerance(basicSize, pitch, ThreadOrientations.External, classOfFit, lengthOfEngagement);

        if (classOfFit == UnifiedClassOfFits._1A || classOfFit == UnifiedClassOfFits._2A)
            return 0.3m * pitchTolerance;
        // Allowance is only computed for external threads.
        throw new Exception("Allowance for internal thread invalid. ClassOfFit must be external.");
    }


    /// <summary>
    /// Calculates the maximum major diameter for an external thread. Internal threads maximum
    /// major diameter is established by a GO gage. It is at least greater than the basic size. 
    /// Per ASME B1.1 ch8.3.2 (a).
    /// </summary>
    public static decimal MajorDiameterMaximum_ExternalThread(decimal basicSize, decimal pitch,
                                               UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        var orientation = ThreadOrientations.External;
        Validate(basicSize, pitch, orientation, classOfFit);
        if (classOfFit == UnifiedClassOfFits._3B)
            return basicSize;
        else
            return basicSize - Allowance(basicSize, pitch, classOfFit, lengthOfEngagement);

    }

    /// <summary>
    /// Minimum major diameter. For external threads, uses the tolerance range and maximum. For internal threads, equals the basic size.
    /// </summary>
    public static decimal MajorDiameterMinimum(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                               UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        Validate(basicSize, pitch, orientation, classOfFit);
        if (orientation == ThreadOrientations.External)
        {
            var max = MajorDiameterMaximum_ExternalThread(basicSize, pitch, classOfFit, lengthOfEngagement);
            var tol = MajorDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
            return max - tol;

        }
        else
        {
            return basicSize;
        }
    }

    /// <summary>
    /// The target size for the thread's Major Diameter. Computed from the tolerance and allowance.
    /// </summary>
    public static decimal MajorDiameterNominal(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                               UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
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

    /// <summary>
    /// The total tolerance of the major diameter.
    /// Note: See ASME B1.1 Ch 5.4 (pg 79)
    /// </summary>
    public static decimal MajorDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                             UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
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

    /// <summary>
    /// The basic minor diameter which accounts for a crest flat and root flat.
    /// </summary>
    public static decimal MinorDiameterBasic(decimal basicSize, decimal pitch)

    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        if (ValidateBasicSize(basicSize) == false)
            throw new ArgumentException("Invalid size provided.");

        return basicSize - (2*Height(pitch));
    }


    /// <summary>
    /// Computes the minimum minor diameter of a thread. References ASME B1.1 ch 8.3.1 + ch 8.3.2 - section e.
    /// Does not support UNJ threads. Note: See ASME B1.1 Ch 5.4 (pg 79)
    /// </summary>
    public static decimal MinorDiameterMinimum(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                        UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        if (orientation == ThreadOrientations.External)
        {
            var minimumPitchDia = PitchDiameterMinimum(basicSize, pitch, orientation,classOfFit,lengthOfEngagement);
            return minimumPitchDia - (0.64951905m * pitch);
        }
        else
        {
            return basicSize - (1.08253175m * pitch);
        }
    }

    /// <summary>
    /// Computes the maximum minor diameter of a thread. References ASME B1.1 ch 8.3.1 + ch 8.3.2 - section e.
    /// Does not support UNJ threads.
    /// Note: this method does not adhere to ASME B1.1 Ch 8.3.2 section f-1
    /// </summary>
    public static decimal MinorDiameterMaximum(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                        UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement, bool isUnr)
    {
        Validate(basicSize, pitch, orientation, classOfFit);
        decimal result;
        if(orientation == ThreadOrientations.External)
        {
            if (classOfFit == UnifiedClassOfFits._3A)
                return MinorDiameterBasic(basicSize, pitch);
            else
                result = MinorDiameterBasic(basicSize, pitch) - Allowance(basicSize, pitch, classOfFit, 0);
                return result;
        }
        else
        {
            // Internal.
            var minimumMinorDia = MinorDiameterMinimum(basicSize,pitch,orientation, classOfFit, lengthOfEngagement);
            var minorDiameterTol = MinorDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement, isUnr);
            result = minimumMinorDia + minorDiameterTol;
            return result;
        }
    }

    /// <summary>
    /// The total tolerance of the minor diameter.
    /// Note: See ASME B1.1 Ch 5.4 (pg 79)
    /// </summary>
    public static decimal MinorDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement,
                                                 bool isUnr = false)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        if (orientation == ThreadOrientations.External)
        {
            var pitchDiameterTolerance = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
            decimal multipler;
            if (!isUnr)
            {
                const decimal MULTIPLIER = 0.21650635m;
                var result = pitchDiameterTolerance + (MULTIPLIER * pitch);
                return result;
            }
            else
            {
                const decimal UNR_MULTIPLIER = 0.10825318m;
                var result = pitchDiameterTolerance + (UNR_MULTIPLIER * pitch);
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

    public static decimal PitchDiameterBasic(decimal basicSize, decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        if (ValidateBasicSize(basicSize) == false)
            throw new ArgumentException("Invalid size provided.");
        return basicSize - (2 * 0.32475953m * pitch);
    }

    /// <summary>
    /// Minimum limit for the pitch diameter of the thread.
    /// </summary>

    public static decimal PitchDiameterMinimum(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                         UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        Validate(basicSize, pitch, orientation, classOfFit);
        if (orientation == ThreadOrientations.External)
        {
            var maximumPitchDia = PitchDiameterMaximum(basicSize, pitch, orientation, classOfFit,lengthOfEngagement);
            var pitchDiameterTol = PitchDiameterTolerance(basicSize,pitch,orientation,classOfFit,lengthOfEngagement);
            return maximumPitchDia - pitchDiameterTol;
        }
        else
        {
            return PitchDiameterBasic(basicSize, pitch);
        }


    }
    /// <summary>
    /// Maximum limit of the pitch diameter for the thread.
    /// </summary>
    public static decimal PitchDiameterMaximum(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                             UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        Validate(basicSize, pitch, orientation, classOfFit);
        if(orientation == ThreadOrientations.External)
        {
            if (classOfFit == UnifiedClassOfFits._3A)
                // Equals basic PD of internal thread (D2bsc) according to std. basic PD is irrespective of orientation anyway?) 
                return PitchDiameterBasic(basicSize, pitch);
            else
                // 2A or 1A
                return PitchDiameterBasic(basicSize, pitch) - Allowance(basicSize,pitch,classOfFit, lengthOfEngagement);
        }
        else
        {
            var minimumPitchDia = PitchDiameterMinimum(basicSize, pitch,orientation,classOfFit,lengthOfEngagement);
            var pitchDiameterTol = PitchDiameterTolerance(basicSize, pitch, orientation, classOfFit, lengthOfEngagement);
            return minimumPitchDia + pitchDiameterTol;
        }
    }

    /// <summary>
    /// Total tolerance of the pitch diameter for a thread, given an optional length of engagement. 
    /// </summary>
    public static decimal PitchDiameterTolerance(decimal basicSize, decimal pitch, ThreadOrientations orientation,
                                                 UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        Validate(basicSize, pitch, orientation, classOfFit);

        var le = lengthOfEngagement;

        if (lengthOfEngagement == null)
        {
            if (pitch <= 0.08333m)
                le = 9 * pitch;
            else
                le = basicSize;
        }
        // All tolerance calculations are based upon 2A class of fit - See switch.
        var pitchDiaTol2A = (0.0015m * (decimal)Math.Sqrt((double)le)) +
                     (0.015m * (decimal)Math.Pow((double)pitch, 2.0 / 3.0)) +
                     (0.0015m * (decimal)Math.Pow((double)basicSize, 1.0 / 3.0));


        decimal result = classOfFit switch
        {
            // External
            UnifiedClassOfFits._2A => pitchDiaTol2A,
            UnifiedClassOfFits._1A => 1.50m * pitchDiaTol2A,
            UnifiedClassOfFits._3A => 0.75m * pitchDiaTol2A,
            // Internal
            UnifiedClassOfFits._1B => 1.95m * pitchDiaTol2A,
            UnifiedClassOfFits._2B => 1.30m * pitchDiaTol2A,
            UnifiedClassOfFits._3B => 0.975m * pitchDiaTol2A,
            _ => throw new ArgumentException("Unknown class of fit.")
        };

        return result;
    }


    /// <summary>
    /// Computes the nominal crest width of an external thread.
    /// Reference:  ASME B1.10291 ch10. pg 142.
    /// </summary>
    public static decimal CrestWidthExternal(decimal pitch, bool isUnr, bool isTruncated)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        if (isTruncated)
            if (isUnr)
                return 0.16237976m * pitch;
            else
                return 1.0825318m * pitch;
        else
            return 0.125m * pitch;
    }

    /// <summary>
    /// Computes the nominal crest width of an internal thread.
    /// Reference:  ASME B1.10291 ch10. pg 142.
    /// </summary>
    public static decimal CrestWidthInternal(decimal pitch, bool isTruncated)
    {
        if (isTruncated)
            return 0.21650635m * pitch;
        else
            return 0.250m * pitch;
    }

    /// <summary>
    /// Computes the nominal root width on an external thread. 
    /// Reference:  ASME B1.10291 ch10. pg 142.
    /// </summary>
    public static decimal RootWidthExternal(decimal pitch)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        return 0.250m * pitch;

    }

    /// <summary>
    /// Computes the nominal root width on an internal thread.
    /// Reference:  ASME B1.10291 ch10. pg 142.
    /// </summary>
    public static decimal RootWidthInternal(decimal pitch, bool isTruncated)
    {
        if (ValidatePitch(pitch) == false)
            throw new ArgumentException("Invalid pitch provided.");
        if (isTruncated)
            return 1.0825318m * pitch;
        else
            return 0.125m * pitch;
    }
}
