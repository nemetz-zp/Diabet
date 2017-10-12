using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Helpers
{
    public class PathFinder
    {
        public static string GetImgPath()
        {
            return System.IO.Path.Combine(GetProgramPath(), "IMG");
        }

        public static string GetProgramPath()
        {
            string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return System.IO.Path.GetDirectoryName(path);
        }
    }
}
