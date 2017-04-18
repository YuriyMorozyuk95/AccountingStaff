using System.IO;

namespace Rfid.Helpers
{
    public static class СryptographerHelper
    {
        static readonly string[] Arr = { ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}",
            ".{21EC2020-3AEA-1069-A2DD-08002B30309D}",
            ".{2559a1f4-21d7-11d4-bdaf-00c04f60b9f0}",
             ".{645FF040-5081-101B-9F08-00AA002F954E}",
             ".{2559a1f1-21d7-11d4-bdaf-00c04f60b9f0}",
           ".{7007ACC7-3202-11D1-AAD2-00805FC1270E}"
        };

        public static string status = ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
        private static string Getstatus(string stat)
        {
            for (var i = 0; i < 6; i++)
            {
                if (stat.LastIndexOf(Arr[i]) != -1)
                {
                    stat = stat.Substring(stat.LastIndexOf("."));
                }
            }

            return stat;
        }

        public static void Decrypt(string path)
        {
            path = path + ".{2559a1f2-21d7-11d4-bdaf-00c04f60b9f0}";
            var d = new DirectoryInfo(path);
            status = Getstatus(status);
            d.MoveTo(path.Substring(0, path.LastIndexOf(".")));
        }
        public static void Encrypt(string path)
        {
            var d = new DirectoryInfo(path);
            if (path.LastIndexOf(".{") == -1)
            {
                if (!d.Root.Equals(d.Parent.FullName))
                {
                    d.MoveTo(d.Parent.FullName + "\\" + d.Name + status);
                }
                else
                {
                    d.MoveTo(d.Parent.FullName + d.Name + status);
                }
            }
        }

        
    }
}
