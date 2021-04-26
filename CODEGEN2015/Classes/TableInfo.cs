using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator2005
{
    public class TableInfo
    {
        private string _Name;
        private string _DBName;
        private bool _IsChild;
        private bool _IsParent;
        ArrayList _arrColumns=new ArrayList();
        

        public string Name
        { get { return _Name; } set { _Name = value; } }

     
        public ArrayList arrColumns
        { get { return _arrColumns; } set { _arrColumns = value; } }


        public bool IsChild
        { get { return _IsChild; } set { _IsChild = value; } }

        public bool IsParent
        { get { return _IsParent; } set { _IsParent = value; } }

        public string DBName
        { get { return _DBName; } set { _DBName = value; } }

    }
}
