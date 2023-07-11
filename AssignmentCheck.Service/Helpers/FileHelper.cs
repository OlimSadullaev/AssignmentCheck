using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Helpers
{
    public class FileHelper
    {
        public static bool Remove(string staticPath)
        {
            string fullPath = Path.Combine(EnvironmentHelper.WebRootPath, staticPath);
            if (!File.Exists(fullPath))
                return false;

            File.Delete(fullPath);

            return true;
        }
    }
}
