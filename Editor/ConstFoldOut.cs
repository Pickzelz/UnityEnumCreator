using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public class ConstFoldOut : FoldOutBasic
    {
        public ConstFoldOut(string name) : base(name)
        {
        }

        protected override void Display()
        {
            EditorGUILayout.BeginVertical();
            {
                GUILayout.Label("This is Enum Area");
            }
            EditorGUILayout.EndVertical();
        }
    }
}