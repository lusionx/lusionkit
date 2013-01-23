using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Alx.ORM.Core
{
    public interface IConstruction
    {
        string Name { get; }
        string Des { get; }
    }

    public class DbConstruction : IConstruction
    {
        public string Name { get; set; }
        public string ConnStr { get; set; }
        public string Des { get; set; }
        public List<TableConstruction> Tables { get; set; }
    }

    public class TableConstruction : IConstruction
    {
        public string Name { get; set; }
        public string Des { get; set; }
        public List<ColumnConstruction> Columns { get; set; }
    }

    public class ColumnConstruction : IConstruction
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public DbType DbType { get; set; }
        public string Des { get; set; }
        public int Length { get; set; }
        public bool IsPk { get; set; }
        public bool Nullable { get; set; }
        public IDefVal DefVal { get; set; }
    }
}
