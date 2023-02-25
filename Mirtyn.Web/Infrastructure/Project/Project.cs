using System.IO;

namespace Mirtyn.Web
{
    public static class Project
    {
        public static string MapPath(string path)
        {
            if(path.StartsWith("~"))
            {
                path = path.Substring(1);
            }
            if (path.StartsWith("\\"))
            {
                path = path.Substring(1);
            }
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }

            // see https://stackoverflow.com/a/64435230/527843
            return Path.Combine(
                (string)System.AppDomain.CurrentDomain.GetData("WebRootPath"),
                path);
        }
    }
}
