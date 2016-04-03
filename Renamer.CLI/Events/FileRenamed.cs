using System;

namespace Kenbo.Renamer.CLI.Events
{
    internal class FileRenamed : EventArgs
    {
        public string FilePath { get; private set; }
        public string OldName { get; private set; }
        public string NewName { get; private set; }

        private FileRenamed(string filePath, string oldName, string newName)
        {
            FilePath = filePath;
            OldName = oldName;
            NewName = newName;
        }

        public override string ToString()
        {
            return $"Renamed {OldName} to {NewName}, found in {FilePath}";
        }

        public static FileRenamed Create(string filePath, string oldName, string newName)
        {
            return new FileRenamed(filePath, oldName, newName);
        }
    }
}