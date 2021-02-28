using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.IntegrationTests.Persistence
{
    public static class CopyDataBase
    {
        public static string Run()
        {
            int i = new Random().Next(0, int.MaxValue);
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            var pathtodatabasefile = dirPath + "\\DataBaseExample" + "\\GeekLemonTestDataBase.db";
            var folder = dirPath + "\\DataBaseExample" + $"\\Temp\\";

            System.IO.DirectoryInfo di = new DirectoryInfo(folder);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.CreateDirectory(folder);

            var dest = dirPath + "\\DataBaseExample" + $"\\Temp\\TEMPCOPY{i}.db";
            File.Copy(pathtodatabasefile, dest);

            return dest;
        }
    }
}
