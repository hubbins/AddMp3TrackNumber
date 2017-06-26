using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddMp3TrackNumber
{
    class Program
    {
        static Regex regex = new Regex(@"^[0-9]{2}");   // matches two digits at beginning of string
        static int count = 0;

        static void Main(string[] args)
        {
            bool renameFiles = false;
            String folderName = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: AddMp3TrackNumber folder_name [rename]");
                Console.WriteLine("Add [rename] parameter to make changes, otherwise will just print the names to change.");
                return;
            }

            if (args.Length >= 1)
                folderName = args[0];

            if (args.Length == 2 && args[1] == "rename")
                renameFiles = true;

            RenameFiles(folderName, renameFiles);
            Console.WriteLine($"{count} files found");
        }

        static void RenameFiles(string folderName, bool renameFiles)
        {
            // recurse into folders, then rename files
            String[] folders = Directory.GetDirectories(folderName);
            foreach (var folder in folders)
            {
                RenameFiles(Path.Combine(folderName, folder), renameFiles);
            }

            // find all the mp3 files in the folder that do not start with two digits and rename them
            String[] files = Directory.GetFiles(folderName, "*.mp3");
            foreach (var file in files)
            {
                TagLib.File f = TagLib.File.Create(file);
                var track = f.Tag.Track;
                var mp3Name = Path.GetFileName(file);
                if (track > 0 && !regex.IsMatch(mp3Name))
                {
                    String newMp3Name = $"{track.ToString("00")} {mp3Name}";
                    Console.WriteLine($"{mp3Name} -> {newMp3Name}");
                    count++;
                    if (renameFiles)
                    {
                        File.Move(Path.Combine(folderName, mp3Name), Path.Combine(folderName, newMp3Name));
                    }
                }
            }
        }
    }
}
