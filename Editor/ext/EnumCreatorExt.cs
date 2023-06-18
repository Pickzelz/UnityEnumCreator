using UnityEngine;

namespace EnumCreator.Editor.ext
{
    public class EnumCreatorExt
    {
        public static void DrawBorder(Rect body, Color color, int top, int right, int bottom, int left)
        {
            GUIStyle borderStyle = GUI.skin.box;
            borderStyle.normal.background = GetColorTexture(color);
            body.x -= left;
            body.y -= top;
            body.width += (left + right);
            body.height += (top + bottom);
            
            GUI.Box(body, GUIContent.none, borderStyle);
        }
        
        private static Texture2D GetColorTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0,color);
            texture.Apply();

            return texture;
        }
        
        public static void DrawQuad(Rect position, Color color)
        {
            GUIStyle boxStyle = new GUIStyle("Box");
            boxStyle.normal.background = GetColorTexture(color);
            GUI.Box(position, GUIContent.none, boxStyle);
        }
    }
}