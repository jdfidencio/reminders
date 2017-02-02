using Reminders.Droid.Service;
using Reminders.Service;
using System;
using System.IO;
using Xamarin.Forms;


[assembly: Dependency(typeof(FileHelperService))]
namespace Reminders.Droid.Service
{
    public class FileHelperService : IFileHelperService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}