using ThreadCalc.Types;

namespace ThreadCalc.Core;


/// <summary>
/// A base class (record) from which all threads inherit.
/// </summary>
public record Thread(
    
    ThreadStandards Standard, 
    decimal BasicSize,
    decimal Pitch,
    ThreadOrientations Orientation,
    bool IsCustom

    )
{

}