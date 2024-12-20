using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Calculations;
using ThreadCalc.Types;


namespace ThreadCalc.Core;

/// <summary>
/// Provides methods for generating a UnifiedThread object with data derived from ASME B1.1 calculations. 
/// </summary>
public static class UnifiedThreadFactory
{
    /// <summary>
    /// Creates a UTS thread with an internal orientation.
    /// </summary>
    /// <returns></returns>
    /// 
    public static UnifiedThread CreateInternal(decimal basicSize, decimal basicPitch,
                                                UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement, bool isUnr)
    {
        // Declare the orientation for this method.
        var orient = ThreadOrientations.Internal;

        // Perform calculations needed, create specifications from calculations
        // Allowance - not a feature of internal threads:
        var allowance = new SimpleSpecification("Allowance", "ES", 0);
        // Major Diameter:
        var majorDiameterMinimum = UnifiedThreadCalculator.MajorDiameterMinimum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var majorDiameterMaximum = 0; // Note: for internal, established by a GO gage in practice. See ASME 8.3.2 (a).
        var majorDiameter = new Specification("Major Diameter", "D", majorDiameterMaximum, majorDiameterMinimum);
        // Minor Diameter:
        var minorDiameterMinimum = UnifiedThreadCalculator.MinorDiameterMinimum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var minorDiameterMaximum = UnifiedThreadCalculator.MinorDiameterMaximum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement,isUnr);
        var minorDiameter = new Specification("Minor Diameter","D1",minorDiameterMaximum, minorDiameterMinimum);
        // Pitch Diameter:
        var pitchDiameterMinimum = UnifiedThreadCalculator.PitchDiameterMinimum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var pitchDiameterMaximum= UnifiedThreadCalculator.PitchDiameterMaximum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var pitchDiameter = new Specification("Pitch Diameter", "D2", pitchDiameterMaximum, pitchDiameterMinimum);



        // Build the thread.
        var utsThread = new UnifiedThread
        {
            BasicSize = basicSize,
            BasicPitch = basicPitch,
            Orientation = orientation,
            ClassOfFit = classOfFit,
            LengthOfEngagement = lengthOfEngagement,
            Allowance = new SimpleSpecification("Allowance", UnifiedThreadInternalNotations.ALLOWANCE, allowance),
            m






        };


        
        
        
        
      




    }




}
