using System;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

namespace StaticObjects
{
    [Serializable]
    public class DataContainer : MonoBehaviour
    {
        [IgnoreDataMember]
        public DataContainer Instance { get; private set; }
        [IgnoreDataMember]
        private const string GameFileName = "game.savefile";

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void RequestSave()
        {
            Save();
        }

        public void RequestLoad()
        {
            Load();
        }

        private void Save()
        {
            var json = JsonUtility.ToJson(this);
            var filePath = Path.Combine(Application.persistentDataPath, GameFileName);
            if (!File.Exists(filePath)) { File.Create(filePath); }
            File.WriteAllText(filePath, json);
        }

        private void Load()
        {
            var filePath = Path.Combine(Application.persistentDataPath, GameFileName);
            if (!File.Exists(filePath)) { return; }
            var text = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(text, this);
        }
    }
}
