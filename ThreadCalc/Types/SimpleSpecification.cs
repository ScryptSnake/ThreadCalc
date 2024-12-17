using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadCalc.Types
{
    public record SimpleSpecification(
        string Name,
        string Notation,
        decimal Value
        )
    {
    }
}
