using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace EnumCreator.Editor
{
    public class EnumClassCreator
    {
        private EnumCollections _collections = null;
        private string _enumFileName = "EnumCreatorCollection.cs";

        private string _template = "//This is auto generated file, please don't change anything here \n" +
                                   "namespace GlobalEnum { \n" +
                                   "\n" +
                                   ":LIST_ENUM: \n" +
                                   "\n" +
                                   "}\n";

        private string _listEnumTemplate = "public enum :ENUM_NAME: {\n" +
                                       ":ENUM_BODY:\n" +
                                       "}" +
                                       "";

        public void SetCollection(EnumCollections collections)
        {
            _collections = collections;
        }

        public void SaveEnumFile()
        {
            string filePath = $"{_collections.EnumPath}/{_enumFileName}";
            string enumFileContent = CreateEnum();
            if (!Directory.Exists(_collections.EnumPath))
            {
                Directory.CreateDirectory(_collections.EnumPath);
            }
            if (Directory.Exists(_collections.EnumPath))
            {
                ReadAndReplaceContent(filePath, enumFileContent);
            }
            AssetDatabase.Refresh();
        }

        // private void CreateEnumFile(string filePath)
        // {
        //     File.Create(filePath);
        // }

        private void ReadAndReplaceContent(string filePath, string newContent)
        {
            if (IsEnumPathChangedAndFileExists())
            {
                File.Delete(Path.Combine(_collections.CurrentEnumPath, _enumFileName));
                _collections.CurrentEnumPath = _collections.EnumPath;
            }
            File.WriteAllText(filePath, newContent);
            
        }

        private bool IsEnumPathChangedAndFileExists()
        {
            return _collections.EnumPath != _collections.CurrentEnumPath && File.Exists($"{_collections.CurrentEnumPath}/{_enumFileName}");
        }

        private string CreateEnum()
        {
            List<string> enums = new List<string>();
            foreach (EnumData collectionsEnumData in _collections.EnumDatas)
            {
                if (collectionsEnumData.Enums.Count > 0)
                {
                    string enumText = _listEnumTemplate.Replace(":ENUM_NAME:", $"E{collectionsEnumData.Name}");
                    enumText = enumText.Replace(":ENUM_BODY:", SetEnumListValue(collectionsEnumData));
                    enums.Add(enumText);
                }
            }

            return PlaceAllEnums(string.Join("\n", enums));
        }

        private string PlaceAllEnums(string listEnumText)
        {
            return _template.Replace(":LIST_ENUM:", listEnumText);
        }

        private string SetEnumListValue(EnumData data)
        {
            List<string> enumList = new List<string>();
            foreach (EnumList dataEnum in data.Enums)
            {
                string enumBody = $"    {dataEnum.Name}";
                enumList.Add(enumBody);
            }

            return string.Join(",\n", enumList.ToArray());
        }
    }
}