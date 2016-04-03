using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kenbo.Core.Events;
using Kenbo.Renamer.CLI.Events;
using Kenbo.Renamer.CLI.Helpers;

namespace Kenbo.Renamer.CLI.Renamers
{
    public class ShowSeasonRenamer : IRenamer
    {
        private readonly string _format;

        public ShowSeasonRenamer(string format)
        {
            _format = format;
        }

        public void Rename(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException(directoryPath);
            }

            var filesToRename = Directory.GetFiles(directoryPath);
            Parallel.ForEach(filesToRename, x => RenameFile(x, _format));
        }

        private static void RenameFile(string filePath, string format)
        {
            const string numberFormat = "D2";

            var file = new FileInfo(filePath);
            var season = Identify.Season(file.Name).ToString(numberFormat);
            var episode = Identify.Episode(file.Name).ToString(numberFormat);
            var name = format.Replace("%s", season).Replace("%e", episode);
            name = $"{name}{file.Extension}";
            var path = Path.Combine(file.DirectoryName ?? string.Empty, name);
            var oldName = file.Name;

            file.MoveTo(path);

            Event.Raise(FileRenamed.Create(filePath, oldName, name));
        }
    }
}