using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F28x_Project.Interfaces
{
    internal interface ISettings
    {
        string? Port { get; }
        string? Language { get; }
        void UpdatePort(string? port);
        void UpdateLanguage(string? language);
    }
}
