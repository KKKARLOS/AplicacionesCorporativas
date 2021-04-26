using System.Collections;
namespace CodeGenerator2005
{
    public class ColumnInfo
    {
        private string _TableName;
        private string _Name;
        private string _DBType;
        private long _Length;
        private bool _IsAllowNull;
        private bool _IsPrimaryKey;
        private bool _IsForeignKey;
        private string _DefaultValue;
        private string _DefaultValueIfNull;
        private string _ParentColumnName;
        private string _ParentTableName;
        private bool _IsChild;
        private bool _IsParent;
        private bool _IsIdentity;
        private int _Scale;
        private int _Ordinal;
        private int _Precision;
        ArrayList _arrChildTables=new ArrayList();
        TableInfo  _ParentTable;


        public int Scale
        { get { return _Scale; } set { _Scale = value; } }

        public int Precision
            { get { return _Precision; } set { _Precision = value; } }


        public int Ordinal
        { get { return _Ordinal; } set { _Ordinal = value; } }


        public string TableName
        { get { return _TableName; } set { _TableName = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }


        public string DBType
        { get { return _DBType; } set { _DBType = value; } }


        public long Length 
        {get{return _Length;}set{_Length=value;}}

        public bool IsAllowNull 
        {get{return _IsAllowNull;}set{_IsAllowNull=value;}}

    
        public bool IsIdentity
        { get { return _IsIdentity; } set { _IsIdentity = value; } }


        public bool IsPrimaryKey 
        {get{return _IsPrimaryKey;}set{_IsPrimaryKey=value;}}

        public bool IsForeignKey 
        {get{return _IsForeignKey;}set{_IsForeignKey=value;}}

        public string DefaultValue 
        {get{return _DefaultValue;}set{_DefaultValue=value;}}

        public string ParentColumnName
        { get { return _ParentColumnName; } set { _ParentColumnName = value; } }

        public string ParentTableName
        { get { return _ParentTableName; } set { _ParentTableName = value; } }

        public string DefaultValueIfNull
        { get { return _DefaultValueIfNull; } set { _DefaultValueIfNull = value; } }

        public bool IsChild
        { get { return _IsChild; } set { _IsChild = value; } }

        public bool IsParent
        { get { return _IsParent; } set { _IsParent = value; } }

        
        public TableInfo ParentTable
        { get { return _ParentTable; } set { _ParentTable = value; } }


        public ArrayList arrChildTables
        { get { return _arrChildTables; } set { _arrChildTables = value; } }


    }
}