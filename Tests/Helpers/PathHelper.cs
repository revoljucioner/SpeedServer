using System;
using System.IO;

namespace Tests.Helpers
{
    public static class PathHelper
    {
        public static string GetProjectPath()
        {
            return Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.Length - 1)).FullName).FullName).FullName;
        }
    }
}
