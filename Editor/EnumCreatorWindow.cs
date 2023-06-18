using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public class EnumCreatorWindow : EditorWindow
    {
        private EnumFoldOut _enumArea;
        private SaveDataEnum _saveDataEnum;

        private EnumCollections _collections;

        private Vector2 _scrollPos; 
        // private ConstFoldOut _constArea ;

        [MenuItem("Window/EnumCreator")] 
        private static void ShowWindow()
        {
            var window = GetWindow<EnumCreatorWindow>();
            window.titleContent = new GUIContent("Enum Creator");
            window.Show();
        }

        private void OnEnable()
        {
            _saveDataEnum ??= new SaveDataEnum("Enum Save Options");
            _enumArea ??= new EnumFoldOut("Enums");
            _collections = _saveDataEnum.LoadCollection();
            _enumArea.SetCollections(_collections);
            _saveDataEnum.SetCollection(_collections);
            _enumArea.Init();
            
        }

        private void OnGUI()
        {
            if (!isReady()) return;
            
            var rect = EditorGUILayout.BeginVertical();
            {
                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                DrawTitle();
                _enumArea.Draw();
                _saveDataEnum.Draw();
                // _constArea.Draw();
                // DebugData();
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawTitle()
        {
            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(0,0,10,10)
            });
            {
                GUIStyle titleStyle = new GUIStyle("Label")
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 20,
                    fontStyle = FontStyle.Bold
                };
                EditorGUILayout.LabelField("Enum Creator", titleStyle );
            }
            EditorGUILayout.EndVertical();
        }

        private bool isReady()
        {
            return _enumArea != null && _collections != null;
        }
        
        //Not delete this line of code for debugging in the future
        /*
        private void DebugData()
        {
            EditorGUILayout.BeginVertical();
            {
                foreach (EnumData collectionsEnumData in _collections.EnumDatas)
                {
                    EditorGUILayout.LabelField($"Enum name : {collectionsEnumData.Name}");
                    foreach (EnumList enumList in collectionsEnumData.Enums)
                    {
                        EditorGUILayout.LabelField($"   - {enumList.Name}");
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
        */
    }
}