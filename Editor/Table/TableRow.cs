using System.Collections.Generic;

namespace EnumCreator.Editor
{
    public class TableRow
    {
        private List<TableField> _fields;
        public List<TableField> Fields => _fields;
        public int RowHeight { get; set; } = 30;

        private EnumList _list;
        public EnumList List => _list;

        public TableRow(TableField[] fields, EnumList dataList)
        {
            _fields = new List<TableField>();
            if (fields != null)
            {
                foreach (TableField tableField in fields)
                {
                    AddNewRow(tableField);
                }
            }

            _list = dataList;
        }
        
        public void AddNewRow(TableField field)
        {
            field.Row = this;
            _fields.Add(field);
        }
    }
}