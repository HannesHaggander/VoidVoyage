using System.IO;
using UnityEngine;

namespace Persistence
{
    public class PersistenceOperations
    {
        public static void SaveToFile(string fileName, object content)
        {
            var metaData = GetMetaData();
            metaData.LastSaveFile = "TestDirectory";
            var dirFilePath = Path.Combine(Application.persistentDataPath, metaData.LastSaveFile);
            if (!Directory.Exists(dirFilePath)){ Directory.CreateDirectory(dirFilePath); }
            File.WriteAllText(Path.Combine(dirFilePath, fileName), JsonUtility.ToJson(content, true));
        }

        public static string GetFileContent(string filename)
        {
            var metaData = GetMetaData();
            metaData.LastSaveFile = "TestDirectory";
            var path = Path.Combine(Application.persistentDataPath, metaData.LastSaveFile, filename);
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }

        private static MetaDataFile GetMetaData()
        {
            var filePath = Path.Combine(Application.persistentDataPath, nameof(MetaDataFile));
            return File.Exists(filePath) ? JsonUtility.FromJson<MetaDataFile>(filePath) : new MetaDataFile();
        }
    }
}
