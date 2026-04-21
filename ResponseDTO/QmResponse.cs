using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F28x_Project.ResponseDTO
{
    internal sealed record QmResponse(
        double ReadingValue,
        string Unit,
        string State,
        string Attribute);
}
