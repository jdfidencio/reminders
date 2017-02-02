using Reminders.iOS.Service;
using Reminders.Service;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelperService))]
namespace Reminders.iOS.Service
{
    public class FileHelperService : IFileHelperService
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
