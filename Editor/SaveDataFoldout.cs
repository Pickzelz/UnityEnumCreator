using EnumCreator.Editor.ext;
using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public class SaveDataFoldout : FoldOutBasic
    {
        private EnumCollections _collections;
        public EnumCollections Collections => _collections;
        
        public SaveDataFoldout(string labelName) : base(labelName)
        {
        }

        protected override void Display()
        {
            DrawSaveOptions();
        }
        
        public void SetCollection(EnumCollections collections)
        {
            _collections = collections;
        }
        
        private void DrawSaveOptions()
        {
            Rect body = EditorGUILayout.BeginVertical(new GUIStyle()
            {
                padding = new RectOffset(5,5,5,5),
                margin = new RectOffset(5,5,5,5)
            });
            {
                EnumCreatorExt.DrawBorder(body, Color.gray, 1,1,1,1);
                EnumCreatorExt.DrawQuad(body, new Color(0.9f,0.9f,0.9f));
                
                GUILayout.Label("Enum Save Location", new GUIStyle
                {   
                    normal = new GUIStyleState() { textColor = Color.black },
                });
                DrawFilePathText();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawFilePathText()
        {
            EditorGUILayout.BeginHorizontal();
            {
                _collections.EnumPath = GUILayout.TextField(_collections.EnumPath);
                GUIStyle buttonStyle = new GUIStyle("Button");
                buttonStyle.fixedWidth = 100;
                if (GUILayout.Button("Browse", buttonStyle))
                {
                    string path = EditorUtility.OpenFolderPanel("Enum File Path", _collections.EnumPath, "");
                    if (path.Length > 0)
                    {
                        _collections.EnumPath = path;
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}