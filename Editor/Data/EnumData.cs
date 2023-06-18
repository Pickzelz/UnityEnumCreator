using System.Collections.Generic;

namespace EnumCreator.Editor
{
    [System.Serializable]
    public class EnumData
    {
        public string Name;
        public List<EnumList> Enums = new List<EnumList>();
    }
}