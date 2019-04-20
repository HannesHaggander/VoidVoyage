using System.IO;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;

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

        public static MetaDataFile GetMetaData()
        {
            return GetMetaData(out _);
        }

        public static MetaDataFile GetMetaData(out string filePath)
        {
            filePath = Path.Combine(Application.persistentDataPath, nameof(MetaDataFile));
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllText(filePath, JsonUtility.ToJson(new MetaDataFile()));
            }

            return JsonUtility.FromJson<MetaDataFile>(File.ReadAllText(filePath));
        }

        public static void CreateNewSaveDirectory(string safeFileName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, safeFileName);
            if (Directory.Exists(dirPath)) { return; }
            Directory.CreateDirectory(dirPath);
        }
    }
}
