using UnityEngine.Events;

namespace EnumCreator.Editor
{
    public class ButtonEvent : UnityEvent<EnumList>
    {
        
    }
    public class TableField
    {
        public ETableFieldType TableType { get; set; }
        public string value;
        public ButtonEvent OnDeleteButtonClick { get; set; }
        public TableRow Row;

        public TableField()
        {
            OnDeleteButtonClick = new ButtonEvent();
        }
        
        ~TableField()
        {
            OnDeleteButtonClick.RemoveAllListeners();
        }
    }
}