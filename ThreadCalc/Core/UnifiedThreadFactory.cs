using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Calculations;
using ThreadCalc.Types;


namespace ThreadCalc.Core;

// Provides methods for generating a UnifiedThread object from calculations.
public static class UnifiedThreadFactory
{
    public static UnifiedThread CreateInternal(decimal basicSize, decimal basicPitch,
                                                UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {
        // Establish thread orientation for this method.
        const ThreadOrientations orient = ThreadOrientations.Internal;

        // Perform calculations needed.
        var allowance = 0; // Not a feature of Internal threads.
        var majorNominal = UnifiedThreadCalculator.MajorDiameterNominal(basicSize, basicPitch, orient, classOfFit, lengthOfEngagement);
        var majorTolerance = UnifiedThreadCalculator.MajorDiameterTolerance(basicSize,basicPitch,orient,classOfFit, lengthOfEngagement);
        

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
