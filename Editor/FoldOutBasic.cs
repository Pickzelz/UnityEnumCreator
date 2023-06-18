using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public abstract class FoldOutBasic
    {
        protected bool isShow = false;
        protected string name = "";
        protected Rect foldoutRect;

        protected FoldOutBasic(string labelName)
        {
            name = labelName;
        }

        protected abstract void Display();
        protected virtual void DrawFoldoutAfter()
        {
            
        }

        public void Draw()
        {
            Rect body = EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(5,5,5,5)
            });
            {
                DrawBorder(body, Color.gray, 1);
                DrawQuad(body, new Color(0.9f, 0.9f, 0.9f));
                
                DrawFoldout();
                if (isShow)
                {
                    DrawLine(Color.gray, 1);
                    Display();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawFoldout()
        {
            EditorGUILayout.BeginHorizontal(new GUIStyle()
            {
                padding = new RectOffset(10, 5, 5, 5)
            });
            {
                DrawButtonFoldout();
                DrawFoldoutAfter();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawButtonFoldout()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleLeft;
                style.padding = new RectOffset(0, 5, 3, 3);
                if (GUILayout.Button(name, style))
                {
                    isShow = !isShow;
                }
            }
            EditorGUILayout.EndHorizontal();
            
        }
        
        private void DrawQuad(Rect position, Color color) {
            
            GUI.skin.box.normal.background = GetColorTexture(color);
            GUI.Box(position, GUIContent.none);
        }

        private void DrawBorder(Rect body, Color color, int borderWidth)
        {
            GUIStyle borderStyle = GUI.skin.box;
            borderStyle.border = new RectOffset(borderWidth, borderWidth, borderWidth, borderWidth);
            borderStyle.normal.background = GetColorTexture(color);
            body.x -= borderWidth;
            body.y -= borderWidth;
            body.width += (borderWidth * 2);
            body.height += (borderWidth * 2);
            
            GUI.Box(body, GUIContent.none, borderStyle);
        }
        
        private void DrawLine(Color color, int lineHeight)
        {
            GUIStyle lineStyle = new GUIStyle();
            lineStyle.fixedHeight = lineHeight;
            lineStyle.normal.background = GetColorTexture(color);

            EditorGUILayout.BeginVertical();
            {
                GUILayout.Box(GUIContent.none,lineStyle);
            }
            EditorGUILayout.EndVertical();
        }

        private Texture2D GetColorTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0,color);
            texture.Apply();

            return texture;
        }
    }
}