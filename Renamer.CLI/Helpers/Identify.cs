using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kenbo.Renamer.CLI.Helpers
{
    internal static class Identify
    {
        private static readonly IEnumerable<Regex> SeasonRegexes = new[]
        {
            new Regex(@"(?<=S)\d{2}(?=E)"),
            new Regex(@"\d{1,2}(?=\d{2})")
        };

        private static readonly IEnumerable<Regex> EpisodeRegexes = new[]
        {
            new Regex(@"(?<=E)\d{1,2}"),
            new Regex(@"(?<=\d{2})\d{2}"),
            new Regex(@"(?<=\d)\d{2}")
        };

        private static readonly Regex SeasonSRegex = new Regex(@"(?<=S)\d{2}(?=E)");

        private static readonly Regex SeasonNumberRegex = new Regex(@"\d{1,2}(?=\d{2})");

        private static readonly Regex EpisodeERegex = new Regex(@"(?<=E)\d{1,2}");

        private static readonly Regex EpisodeTwoLeadingNumbersRegex = new Regex(@"(?<=\d{2})\d{2}");

        private static readonly Regex EpisodeLeadingNumbersRegex = new Regex(@"(?<=\d)\d{2}");

        public static int Season(string fileName)
        {
            var season = SeasonRegexes.Select(x => x.Match(fileName)).FirstOrDefault(x => x.Success);
            return season != null ? int.Parse(season.Value) : 0;
        }

        public static int Episode(string fileName)
        {
            var episode = EpisodeRegexes.Select(x => x.Match(fileName)).FirstOrDefault(x => x.Success);
            return episode != null ? int.Parse(episode.Value) : 0;

            //var episode = EpisodeERegex.Match(fileName);
            //if (!episode.Success)
            //{
            //    episode = EpisodeTwoLeadingNumbersRegex.Match(fileName);
            //}
            //if (!episode.Success)
            //{
            //    episode = EpisodeLeadingNumbersRegex.Match(fileName);
            //}

            //return episode.Success ? int.Parse(episode.Value) : 0;
        }
    }
}