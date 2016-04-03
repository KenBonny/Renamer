using Kenbo.Renamer.CLI.Helpers;
using Shouldly;
using Xunit;

namespace Kenbo.Renamer.UnitTests
{
    public class IdentifyTests
    {
        [Theory]
        [InlineData("Show.S01E02.LOL", 1)]
        [InlineData("Show.S12E22.LOL", 12)]
        [InlineData("Show.201.LOL", 2)]
        [InlineData("Show.1218.LOL", 12)]
        public void Get_season_from_filename(string fileName, int season)
        {
            Identify.Season(fileName).ShouldBe(season);
        }

        [Theory]
        [InlineData("Show.S01E02.LOL", 2)]
        [InlineData("Show.S12E22.LOL", 22)]
        [InlineData("Show.201.LOL", 1)]
        [InlineData("Show.1218.LOL", 18)]
        public void Get_episode_from_filename(string fileName, int episode)
        {
            Identify.Episode(fileName).ShouldBe(episode);
        }
    }
}
