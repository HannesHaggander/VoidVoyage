using Persistence;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine.SceneManagement;

namespace Scenes.Menu
{
    public class MainMenuInterface : MonoBehaviour
    {
        public TMP_InputField SaveFileNameInput;
        public GameObject NewSaveFilePanel;

        public void Continue()
        {
            if (string.IsNullOrEmpty(PersistenceOperations.GetMetaData().LastSaveFile))
            {
                print("No previous save file");
                return;
            }

            SceneManager.LoadScene("SampleScene");
        }

        public void CreateSaveFile()
        {
            var text = SaveFileNameInput.text;
            if (string.IsNullOrEmpty(text)) { return; }

            print($"creating new file: {text}");
            var metaData = PersistenceOperations.GetMetaData(out var path);
            metaData.LastSaveFile = text;
            PersistenceOperations.SaveToFile(path, metaData);
            PersistenceOperations.CreateNewSaveDirectory(text);
            SceneManager.LoadScene("SampleScene");
        }

        public void TogglePanelVisibility()
        {
            NewSaveFilePanel.SetActive(!NewSaveFilePanel.activeSelf);
        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}
