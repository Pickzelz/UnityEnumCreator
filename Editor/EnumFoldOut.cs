using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EnumCreator.Editor
{
    public class EnumFoldOut : FoldOutBasic
    {
        private readonly List<EnumListFoldout> _items;
        private int _index = 0;
        private EnumCollections _collections;
        public EnumFoldOut(string name) : base(name)
        {
            isShow = true;
            _items = new List<EnumListFoldout>();
        }

        public void Init()
        {
            foreach (EnumData data in _collections.EnumDatas)
            {
                EnumListFoldout listFoldout = AddNewEnumFoldout(data);
                listFoldout.Init();
            }
        }
        
        protected override void Display()
        {
            EditorGUILayout.BeginVertical();
            {
                DrawList();
                DrawAddButton();
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawList()
        {
            EditorGUILayout.BeginVertical();
            {
                foreach (var item in _items.ToList())
                {
                    item.Draw();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawAddButton()
        {
            EditorGUILayout.BeginVertical();
            {
                GUIStyle style = new GUIStyle("Button")
                {
                    alignment = TextAnchor.MiddleCenter
                };

                if (GUILayout.Button("Add", style))
                {
                    EnumData newData = CreateNewDataAndSaveToCollection();
                    AddNewEnumFoldout(newData);
                }
            }
            EditorGUILayout.EndVertical();
        }

        private EnumData CreateNewDataAndSaveToCollection()
        {
            EnumData newData = new EnumData();
            _collections.EnumDatas.Add(newData);

            return newData;
        }

        private EnumListFoldout AddNewEnumFoldout(EnumData data)
        {
            EnumListFoldout listFoldout = new EnumListFoldout($"E{data.Name}", data);
            listFoldout.onClose.AddListener(OnClose);
            _items.Add(listFoldout);

            return listFoldout;
        }

        private void OnClose(EnumListFoldout enumList)
        {
            _items.Remove(enumList);
            _collections.EnumDatas.Remove(enumList.Data);
        }

        public void SetCollections(EnumCollections collections)
        {
            _collections = collections;
        }
    }
}