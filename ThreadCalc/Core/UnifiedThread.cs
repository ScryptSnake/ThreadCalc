
using ThreadCalc.Types;


namespace ThreadCalc.Core;
/// <summary>
/// Defines a UTS thread.
/// </summary>
public record UnifiedThread : IThread
{
    public ThreadStandards Standard { get; } = ThreadStandards.UTS;
    public required decimal BasicSize { get; init; }
    public required decimal BasicPitch {get;init; }
    public required ThreadOrientations Orientation { get; init; }
    public required bool IsCustom { get; init; }
    public required decimal ThreadsPerInch { get; init; }
    public required UnifiedClassOfFits ClassOfFit { get; init; }
    public required SimpleSpecification Allowance { get; init; }
    public required Specification MajorDiameter { get; init; }
    public required Specification MinorDiameter { get; init; }
    public required Specification PitchDiameter { get; init; }
    public required Specification RootWidth { get; init; }
    public required Specification CrestWidth { get; init; }
    public required Specification MeasurementOverWires { get; init; }
    public required Specification Pitch { get; init; }
    public required bool isUnr { get; init; }
    public required SimpleSpecification LengthOfEngagement { get; init; }    

}
