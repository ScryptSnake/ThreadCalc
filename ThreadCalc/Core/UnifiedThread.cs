
using ThreadCalc.Types;


namespace ThreadCalc.Core;
/// <summary>
/// Defines a UTS thread.
/// </summary>
public record UnifiedThread(
    decimal ThreadsPerInch,
    UnifiedClassOfFits ClassOfFit,
    Specification Allowance,
    Specification MajorDiameter,
    Specification MinorDiameter,
    Specification PitchDiameter,
    Specification RootWidth,
    Specification CrestWidth,
    Specification MeasurementOverWires,
    Specification Pitch
) : ThreadBase(ThreadStandards.UTS,)

}
