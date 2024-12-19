using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Calculations;
using ThreadCalc.Types;

namespace ThreadCalc.Core;

/// <summary>
/// Produces specification objects from the UnifiedThreadCalculator. 
/// </summary>
internal class UnifiedThreadSpecificationProvider
{
    private decimal BasicSize { get; }
    private decimal BasicPitch { get; }
    private ThreadOrientations Orientation { get; }
    private UnifiedClassOfFits ClassOfFit { get; }
    private decimal? LengthOfEngagement { get; }
    private bool IsUnr { get; }

    public UnifiedThreadCalculationProvider(decimal basicSize, decimal basicPitch, ThreadOrientations orientation,
                                         UnifiedClassOfFits classOfFit, decimal? lengthOfEngagement, bool isUnr)
    {
        BasicSize = basicSize;
        BasicPitch = basicPitch;
        Orientation = orientation;
        ClassOfFit= classOfFit;
        LengthOfEngagement= lengthOfEngagement;
        IsUnr = isUnr;

    }



   



}
