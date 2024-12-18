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
    public static UnifiedThread Create(decimal basicSize, decimal basicPitch, ThreadOrientations orientation,
                                        UnifiedClassOfFits classOfFit, decimal lengthOfEngagement = 0.0m)
    {

        // Perform calculations needed
        var allowance = UnifiedThreadCalculator.Allowance(basicSize, basicPitch, classOfFit, lengthOfEngagement);




        var utsThread = new UnifiedThread
        {
            BasicSize = basicSize,
            BasicPitch = basicPitch,
            Orientation = orientation,
            ClassOfFit = classOfFit,
            LengthOfEngagement = lengthOfEngagement,
            Allowance = new SimpleSpecification("Allowance",)





        };

        
        
        
        
      




    }




}
