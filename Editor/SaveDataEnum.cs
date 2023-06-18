
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public class SaveDataEnum
    {
        private readonly SaveDataFoldout _saveDataView;
        private readonly EnumClassCreator _classCreator;
        private string _jsonSaveFolder = ".EnumSaveData";
        private string _jsonFileName = "enums.json";
        
        public SaveDataEnum(string labelName)
        {
            _saveDataView = new SaveDataFoldout(labelName);
            _classCreator = new EnumClassCreator();
        }

        public void SetCollection(EnumCollections collections)
        {
            _saveDataView.SetCollection(collections);
            _classCreator.SetCollection(collections);
        }

        public void Draw()
        {
            _saveDataView.Draw();
            DrawSaveButton();
        }
        
        private void DrawSaveButton()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save and Update Enum"))
                {
                    SaveEnumFile();
                    SaveJsonFile();
                }
                if (GUILayout.Button("Save"))
                {
                    SaveJsonFile();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void SaveJsonFile()
        {
            string enumJson = JsonUtility.ToJson(_saveDataView.Collections);
            string fullPathDirectory = GetJsonDirectoryPath();
            if (!Directory.Exists(fullPathDirectory))
            {
                Directory.CreateDirectory(fullPathDirectory);
            }
            File.WriteAllText(GetJsonFilePath(), enumJson);
        }

        private void SaveEnumFile()
        {
            _classCreator.SaveEnumFile();
        }

        public EnumCollections LoadCollection()
        {
            if (File.Exists(GetJsonFilePath()))
            {
                string enumJson = File.ReadAllText(GetJsonFilePath());
                if (enumJson.Length > 0)
                {
                    EnumCollections data = JsonUtility.FromJson<EnumCollections>(enumJson);
                    if (data != null)
                    {
                        if (data.EnumPath.Length == 0)
                        {
                            data.SetDefaultPath();
                        }
                        return data;   
                    }
                }
            }

            return new EnumCollections();
        }

        private string GetJsonDirectoryPath()
        {
            return $"{Application.dataPath}/{_jsonSaveFolder}/";
        }

        private string GetJsonFilePath()
        {
            return $"{GetJsonDirectoryPath()}/{_jsonFileName}";
        }
    }
}