using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Calculations;
using ThreadCalc.Types;


namespace ThreadCalc.Core;

// Provides methods for generating a UnifiedThread object from calculations.
public static class UnifiedThreadFactory
{
    /// <summary>
    /// Creates a UTS thread with an internal orientation.
    /// </summary>
    /// <returns></returns>
    /// 
    public static UnifiedThread CreateInternal(decimal basicSize, decimal basicPitch,
                                                UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement)
    {
        // Establish thread orientation for this method.
        const ThreadOrientations orient = ThreadOrientations.Internal;

        // Perform calculations needed, load into memory.

        // Allowance:
        var allowance = 0; // Not a feature of Internal threads.
        // Major Diameter:
        var majorDiameterMinimum = UnifiedThreadCalculator.MajorDiameterMinimum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var majorDiameterMaximum = 0; // Note:  established by a GO gage in practice. See ASME 8.3.2 (a).
        // Minor Diameter:
        var minorDiameterMinimum = UnifiedThreadCalculator.MinorDiameterMinimum(basicSize,decimal)


        var arg = new CalcArgument(basicSize,basicPitch,blah,blah,blah)

        var majorDiameterMinimum = UnifiedThreadCalculator.MajorDiameterMinimum(arg);
        var majorDiameterMaximum = UnifiedThreadCalculator.MajorDiameterMaximum(arg);
        ...
        var majorDiameterMinimum = UnifiedThreadCalculator.MajorDiameterMinimum(arg); ;

        var majorDiameterMinimum = UnifiedThreadCalculator.MajorDiameterMinimum(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);

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
