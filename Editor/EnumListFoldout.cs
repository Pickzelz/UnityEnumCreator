using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace EnumCreator.Editor
{
    [Serializable]
    public class CloseEvent : UnityEvent<EnumListFoldout>
    {
    }
    public class EnumListFoldout : FoldOutBasic
    {
        private string _enumName;
        public readonly CloseEvent onClose;
        public readonly Table enumTable;

        private EnumData _data;
        public EnumData Data => _data;
        public EnumListFoldout(string name, EnumData data) : base(name)
        {
            onClose = new CloseEvent();
            enumTable = new Table(new string[]{"Name", "Action"});
            _data = data;
        }

        public void Init()
        {
            foreach (EnumList list in _data.Enums)
            {
                AddEnum(list);
            }
        }

        protected override void Display()
        {
            DrawEnumTable();
            name = $"E{_data.Name}";
        }

        protected override void DrawFoldoutAfter()
        {
            base.DrawFoldoutAfter();
            EditorGUILayout.BeginHorizontal(new GUIStyle()
            {
                fixedWidth = 50
            });
            {
                if (GUILayout.Button("X"))
                {
                    onClose.Invoke(this);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawEnumTable()
        {
            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(5,5,5,5),
                padding = new RectOffset(5,5,5,5)
            });
            {
                DrawEnumNameTextBox();
                DrawTable();
                DrawAddButton();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawTable()
        {
            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(0,0,10,0)
            });
            {
                enumTable.Draw();
            }
            EditorGUILayout.EndVertical();
        }
        private void DrawEnumNameTextBox()
        {
            EditorGUILayout.BeginHorizontal();
            {
                DrawLabelName();
                _data.Name = EditorGUILayout.TextField(_data.Name);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawLabelName()
        {
            EditorGUILayout.BeginHorizontal(new GUIStyle()
            {
                fixedWidth = 100
            });
            {
                EditorGUILayout.LabelField("Name : ", new GUIStyle
                {
                    normal = new GUIStyleState
                    {
                        textColor = Color.black
                    }
                });
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawAddButton()
        {
            EditorGUILayout.BeginVertical(new GUIStyle()
            {
                margin = new RectOffset(0,0,0,0),
                padding = new RectOffset(0,0,0,0)
            });
            {
                GUIStyle buttonStyle = new GUIStyle("Button");
                buttonStyle.margin = new RectOffset(0, 0, 0, 0);
                buttonStyle.padding = new RectOffset(5,5,5,5);
                if (GUILayout.Button("Add",buttonStyle))
                {
                    AddEnum();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void AddEnum(EnumList list = null)
        {
            EnumList newData = list;
            newData ??= CreateAndAddEnumData();
            TableField actionField = new TableField()
            {
                TableType = ETableFieldType.DeleteRowButton,
                value = "Delete",
            };
            actionField.OnDeleteButtonClick.AddListener(OnRowDelete);
            enumTable.AddField(new TableRow(new TableField[]
                {
                    new TableField()
                    {
                        TableType = ETableFieldType.TextField
                    },
                    actionField
                }
                , newData)
            );
        }

        private void OnRowDelete(EnumList deletedEnumList)
        {
            _data.Enums.Remove(deletedEnumList);
        }

        private EnumList CreateAndAddEnumData()
        {
            EnumList data = new EnumList();
            _data.Enums.Add(data);   
            return data;
        }
    }
}