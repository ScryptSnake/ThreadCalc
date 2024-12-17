
using ThreadCalc.Types;


namespace ThreadCalc.Core;
/// <summary>
/// Defines a UTS thread.
/// </summary>
public record UnifiedThread : ThreadBase
{
    public decimal Tpi { get; init; }
    public UnifiedClassOfFits? ClassOfFit { get; init; }
    public Specification? Allowance { get; init; }
    public Specification? MajorDiameter { get; init; }
    public Specification? MinorDiameter { get; init; }
    public Specification? PitchDiameter { get; init; }
    public Specification? Root { get; init; }
    public Specification? Crest { get; init; }
    public Specification? Pitch { get; init; }
    public Specification? Mow { get; init; }

}

