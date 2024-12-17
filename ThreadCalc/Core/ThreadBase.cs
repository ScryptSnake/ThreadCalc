using ThreadCalc.Types;

namespace ThreadCalc.Core;


/// <summary>
/// A base class from which all threads inherit.
/// </summary>
public record ThreadBase(
    ThreadStandards Standard,
    decimal BasicSize,
    decimal BasicPitch,
    ThreadOrientations Orientation,
    bool IsCustom
)
{

};