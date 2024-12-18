using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Types;

namespace ThreadCalc.Core
{

    /// <summary>
    /// Describes properties for which all threads should implement.
    /// </summary>
    internal interface IThread
    {
        ThreadStandards Standard { get; }
        decimal BasicSize { get; }
        decimal BasicPitch { get; }
        ThreadOrientations Orientation { get; }
        bool IsCustom { get; }

    }
}
