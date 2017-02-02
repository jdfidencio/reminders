using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminders.Service
{
    public interface IFileHelperService
    {
        string GetLocalFilePath(string filename);
    }
}
