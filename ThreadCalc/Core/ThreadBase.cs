using ThreadCalc.Types;

namespace ThreadCalc.Core;


/// <summary>
/// A base class from which all threads inherit.
/// </summary>
public record ThreadBase
{
    public ThreadStandards Standard { get; init; }
    public decimal BasicSize { get; init; }
    public decimal Pitch { get; init; }
    public ThreadOrientations Orientation { get; init; }
    public bool IsCustom { get; init; }
}