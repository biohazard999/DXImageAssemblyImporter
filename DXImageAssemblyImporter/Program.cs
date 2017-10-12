using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXImageAssemblyImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var targetFolder = @"C:\tmp\Images";
            var sourceFolder = @"C:\Program Files (x86)\DevExpress 17.1\Components\Sources\DevExpress.Images";

            if (args.Length > 0)
                sourceFolder = args[0];
            if (args.Length > 1)
                targetFolder = args[1];

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            foreach (var icon in Directory.EnumerateFiles(sourceFolder, "*.png", SearchOption.AllDirectories))
            {
                var type = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(icon));
                var style = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(Path.GetDirectoryName(icon)));
                var iconName = Path.GetFileNameWithoutExtension(icon);

                if (iconName.ToLowerInvariant().EndsWith("_16x16"))
                {
                    iconName = iconName.TrimEnd("_16x16".ToCharArray());
                }

                iconName = style + "_" + type + "_" + iconName + ".png";

                iconName = iconName.Replace(" ", "").Replace("%", "Percent").Replace("&", "And");

                var targetPath = Path.Combine(targetFolder, iconName);

                File.Copy(icon, targetPath, true);

            }
        }
    }
}
