using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Core
{
    public interface IPage
    {
        string PageUrl { get; }
        bool IsAt { get; }
        void GoTo();
    }
}
