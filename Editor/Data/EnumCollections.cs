using System.Collections.Generic;
using UnityEngine;

namespace EnumCreator.Editor
{
    [System.Serializable]
    public class EnumCollections
    {
        public string EnumPath;
        public List<EnumData> EnumDatas = new List<EnumData>();

        public void SetDefaultPath()
        {
            EnumPath = $"{Application.dataPath}";
        }
    }
}