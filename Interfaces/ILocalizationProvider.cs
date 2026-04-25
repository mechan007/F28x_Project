using F28x_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace F28x_Project.Interfaces
{
    internal interface ILocalizationProvider
    {
        string Get(string key);
    }
}
