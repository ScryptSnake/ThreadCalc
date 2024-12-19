using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadCalc.Types;

public record Specification(
    string Name,
    string Notation,
    decimal UpperLimit,
    decimal LowerLimit,
    decimal Nominal,
    decimal Tolerance
    )
{
}

