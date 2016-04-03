using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Kenbo.Renamer.CLI.Renamers;
using Shouldly;
using Xunit;

namespace Kenbo.Renamer.UnitTests
{
    public class ShowSeasonRenamerTests : IDisposable
    {
        private readonly string _directory;

        public ShowSeasonRenamerTests()
        {
            _directory = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Files");
        }

        [Fact]
        public void Rename_files_in_folder_according_to_format()
        {
            var sut = new ShowSeasonRenamer("Show S%sE%e");
            sut.Rename(_directory);
            Directory.GetFiles(_directory).Any(x => x.Contains("Show S01E01")).ShouldBe(true);
        }

        public void Dispose()
        {
            new ShowSeasonRenamer("show.S%sE%e.release.team").Rename(_directory);
        }
    }
}