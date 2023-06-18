using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{

    public enum ETableFieldType
    {
        Label, TextField, DeleteRowButton
    }
    public class Table
    {
        private readonly string[] _headers;
        private List<TableRow> _rows;
        private int _headerHeight = 25;

        public Table(string[] headers)
        {
            _headers = headers;
            _rows = new List<TableRow>();
        }
        
        public void Draw()
        {
            Rect body = EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(0,0,0,0),
                padding = new RectOffset(0,0,0,0)
            });
            {
                DrawBorder(body, Color.gray, 0,1,0,0);
                DrawEnumTableBody();
            }
            EditorGUILayout.EndVertical();
        }

        public void AddField(TableRow row)
        {
            _rows.Add(row);
        }

        private void DrawDeleteButton(TableField field)
        {
            if (GUILayout.Button(field.value))
            {
                field.OnDeleteButtonClick.Invoke(field.Row.List);
                DeleteRow(field.Row);
            }
        }
        
        private void DeleteRow(TableRow row)
        {
            _rows.Remove(row);
        }
        
        private void DrawHeaderField(string value)
        {
            Rect body = EditorGUILayout.BeginVertical(new GUIStyle()
            {
                padding = new RectOffset(0,0,0,0),
                margin = new RectOffset(1,0,0,1),
                fixedHeight = _headerHeight
            });
            {
                DrawBorder(body, Color.gray, 1,0,0,1);
                DrawQuad(body, new Color(0.7f, 0.7f, 0.7f));
                GUIStyle headerLabelStyle = new GUIStyle("Label");
                headerLabelStyle.fontStyle = FontStyle.Bold;
                headerLabelStyle.alignment = TextAnchor.MiddleCenter;
                GUILayout.Label(value, headerLabelStyle);
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawEnumTableBody()
        {
            EditorGUILayout.BeginHorizontal();
            {
                for(int i = 0; i < _headers.Length; i++)
                {
                    DrawTableFields(_headers[i], i);   
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTableFields(string header, int index)
        {
            EditorGUILayout.BeginVertical( );
            {
                DrawHeaderField(header);
                DrawTableRow(index);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawTableRow(int index)
        {
            foreach (TableRow tableRow in _rows.ToList())
            {
                EditorGUILayout.BeginHorizontal();
                {
                    DrawField(tableRow.Fields[index]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        
        private void DrawField(TableField field)
        {
            Rect body = EditorGUILayout.BeginVertical(new GUIStyle()
            {
                padding = new RectOffset(5,5,5,5),
                margin = new RectOffset(1,0,0,1),
                fixedHeight = field.Row.RowHeight
            });
            {
                DrawBorder(body, Color.gray, 1,0,0,1);
                DrawQuad(body, new Color(0.9f, 0.9f, 0.9f));
                
                FillFieldByType(field);
            }
            EditorGUILayout.EndVertical();
        }
        
        private void FillFieldByType(TableField field)
        {
            switch (field.TableType)
            {
                case ETableFieldType.TextField:
                    field.Row.List.Name = EditorGUILayout.TextField(field.Row.List.Name);
                    break;
                case ETableFieldType.Label:
                    EditorGUILayout.LabelField(field.value);
                    break;
                case ETableFieldType.DeleteRowButton:
                    DrawDeleteButton(field);
                    break;
            }
        }
        private void DrawBorder(Rect body, Color color, int top, int right, int bottom, int left)
        {
            GUIStyle borderStyle = GUI.skin.box;
            borderStyle.normal.background = GetColorTexture(color);
            body.x -= left;
            body.y -= top;
            body.width += (left + right);
            body.height += (top + bottom);
            
            GUI.Box(body, GUIContent.none, borderStyle);
        }
        
        private Texture2D GetColorTexture(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0,0,color);
            texture.Apply();

            return texture;
        }
        
        private void DrawQuad(Rect position, Color color) {
            
            GUI.skin.box.normal.background = GetColorTexture(color);
            GUI.Box(position, GUIContent.none);
        }
    }
}