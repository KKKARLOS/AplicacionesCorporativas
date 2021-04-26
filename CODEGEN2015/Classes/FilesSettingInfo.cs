using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace CodeGenerator2005
{
    public enum CodeLanguage
    {
        CS,
        VB
    }

    public enum DBLanguage
    {
        SQL
    }

   public class FilesSettingInfo
    {
        #region Private Variables
        private string _folderPath;
        private ArrayList _arrTablesInfo;
        private CodeLanguage _CodeLanguage;
        private DBLanguage _DBLanguage;
        private bool _UseDataObject;
        private bool _OriginalTemplates;
        private string _TableName;
        private string _AppName; //se usa como prefijo de los SP
        private string _SPOwner;
        private string _SPTemplate;

       private string _SPBeforeSelectAll;
       private string _SPBeforeSelectByPK;
       private string _SPBeforeInsert;
       private string _SPBeforeUpdateByPK;
       private string _SPBeforeDeleteAll;
       private string _SPBeforeDeleteByPK;

       private string _SPAfterSelectAll;
       private string _SPAfterSelectByPK;
       private string _SPAfterInsert;
       private string _SPAfterUpdateByPK;
       private string _SPAfterDeleteAll;
       private string _SPAfterDeleteByPK;


        private string _DONameSpace;
        private string _DOParentInterface;
        private string _DOParentClass;
        private string _DOAfterName;
        private string _DOBeforeName;
        private string _DOTemplate;
        private bool _DOWCFService;

        private string _DANameSpace;
        private string  _DAParentInterface;
        private string  _DAParentClass;
        private string  _DAAfterName;
        private string _DABeforeName;
        private string _DATemplate;
        private string _DAProviderType;
        private string _DACommandType;
        private string _DADBFactoryClass;
        private string _DADBFactoryTemplate;

        private string _WSATemplate;

        private string _BusNameSpace;
        private string _BusParentInterface;
        private string _BusParentClass;
        private string _BusAfterName;
        private string _BusBeforeName;
        private string _BusTemplate;


       private string _WebSearchAfterName;
       private string _WebSearchBeforeName;
       private string _WebSearchTemplate;
       private string _WebSearchTemplateHtml;

       private string _WebDetailsAfterName;
       private string _WebDetailsBeforeName;
       private string _WebDetailsTemplate;
       private string _WebDetailsTemplateHtml;

       private string _WebMasterName;
       private string _WebMasterTitle;
       private string _WebMasterTemplate;
       private string _WebMasterTemplateHtml;
      
       private string _WebLoginSessionName;
       private string _WebHomePageName;


       private string _WebBaseWebFormTemplate;
       private string _WebBaseWebFormTemplateHtml;

       private string _WebLoginTemplate;
       private string _WebLoginTemplateHtml;
       
        #endregion

        #region Public Properties

        #region General

        public string FolderPath
        {
            get
            {
                return _folderPath;

            }
            set
            {
                _folderPath = value;
            }
        }
        public ArrayList TablesInfo
        {
            get
            {
                return _arrTablesInfo;

            }
            set
            {
                _arrTablesInfo = value;
            }
        }

       public CodeLanguage CodeLanguage
       {
           get
           {
               return _CodeLanguage;

           }
           set
           {
               _CodeLanguage = value;
           }
       }
       public DBLanguage DBLanguage
       {
           get
           {
               return _DBLanguage;

           }
           set
           {
               _DBLanguage = value;
           }
       }
       public bool UseDataObject
       {
           get
           {
               return _UseDataObject;

           }
           set
           {
               _UseDataObject = value;
           }
       }

        #endregion

        #region Stored Procedure Settings
       public string AppName
       {
           get
           {
               return _AppName;

           }
           set
           {
               _AppName = value;
           }
       }

       
       public string SPOwner
       {
           get
           {
               return _SPOwner;

           }
           set
           {
               _SPOwner = value;
           }
       }

        public string SPTemplate
       {
           get
           {
               return _SPTemplate;

           }
           set
           {
               _SPTemplate = value;
           }
       }



       public string SPBeforeSelectAll
       {
           get
           {
               return _SPBeforeSelectAll;

           }
           set
           {
               _SPBeforeSelectAll = value;
           }
       }
       public string SPBeforeSelectByPK
       {
           get
           {
               return _SPBeforeSelectByPK;

           }
           set
           {
               _SPBeforeSelectByPK = value;
           }
       }
       public string SPBeforeInsert
       {
           get
           {
               return _SPBeforeInsert;

           }
           set
           {
               _SPBeforeInsert = value;
           }
       }
       public string SPBeforeUpdateByPK
       {
           get
           {
               return _SPBeforeUpdateByPK;

           }
           set
           {
               _SPBeforeUpdateByPK = value;
           }
       }
       public string SPBeforeDeleteAll
       {
           get
           {
               return _SPBeforeDeleteAll;

           }
           set
           {
               _SPBeforeDeleteAll = value;
           }
       }
       public string SPBeforeDeleteByPK
       {
           get
           {
               return _SPBeforeDeleteByPK;

           }
           set
           {
               _SPBeforeDeleteByPK = value;
           }
       }

       public string SPAfterSelectAll
       {
           get
           {
               return _SPAfterSelectAll;

           }
           set
           {
               _SPAfterSelectAll = value;
           }
       }
       public string SPAfterSelectByPK
       {
           get
           {
               return _SPAfterSelectByPK;

           }
           set
           {
               _SPAfterSelectByPK = value;
           }
       }
       public string SPAfterInsert
       {
           get
           {
               return _SPAfterInsert;

           }
           set
           {
               _SPAfterInsert = value;
           }
       }
       public string SPAfterUpdateByPK
       {
           get
           {
               return _SPAfterUpdateByPK;

           }
           set
           {
               _SPAfterUpdateByPK = value;
           }
       }
       public string SPAfterDeleteAll
       {
           get
           {
               return _SPAfterDeleteAll;

           }
           set
           {
               _SPAfterDeleteAll = value;
           }
       }
       public string SPAfterDeleteByPK
       {
           get
           {
               return _SPAfterDeleteByPK;

           }
           set
           {
               _SPAfterDeleteByPK = value;
           }
       }
        #endregion
     
        #region Data Object Settings
       public string DOParentInterface
       {
           get
           {
               return _DOParentInterface;

           }
           set
           {
               _DOParentInterface = value;
           }
       }

       public string DONameSpace
       {
           get
           {
               return _DONameSpace;

           }
           set
           {
               _DONameSpace = value;
           }
       }

       public string DOParentClass
       {
           get
           {
               return _DOParentClass;

           }
           set
           {
               _DOParentClass = value;
           }
       }

       public string DOAfterName
       {
           get
           {
               return _DOAfterName;

           }
           set
           {
               _DOAfterName = value;
           }
       }

       public string DOBeforeName
       {
           get
           {
               return _DOBeforeName;

           }
           set
           {
               _DOBeforeName = value;
           }
       }

       public string DOTemplate
       {
           get
           {
               return _DOTemplate;

           }
           set
           {
               _DOTemplate = value;
           }
       }

       public bool DOWCFService
       {
           get
           {
               return _DOWCFService;

           }
           set
           {
               _DOWCFService = value;
           }
       }

       public bool OriginalTemplates
       {
           get
           {
               return _OriginalTemplates;

           }
           set
           {
               _OriginalTemplates = value;
           }
       }

       public string TableName {
           get
           {
               return _TableName;

           }
           set
           {
               _TableName = value;
           }
       }

       #endregion

        #region Data Access Settings
       public string DAParentInterface
       {
           get
           {
               return _DAParentInterface;

           }
           set
           {
               _DAParentInterface = value;
           }
       }

       public string DANameSpace
       {
           get
           {
               return _DANameSpace;

           }
           set
           {
               _DANameSpace = value;
           }
       }

       public string DAParentClass
       {
           get
           {
               return _DAParentClass;

           }
           set
           {
               _DAParentClass = value;
           }
       }
       public string DAAfterName
       {
           get
           {
               return _DAAfterName;

           }
           set
           {
               _DAAfterName = value;
           }
       }

       public string DABeforeName
       {
           get
           {
               return _DABeforeName;

           }
           set
           {
               _DABeforeName = value;
           }
       }

       public string DATemplate
       {
           get
           {
               return _DATemplate;

           }
           set
           {
               _DATemplate = value;
           }
       }

       public string WSATemplate
       {
           get
           {
               return _WSATemplate;

           }
           set
           {
               _WSATemplate = value;
           }
       }

       public string DAProviderType
       {
           get
           {
               return _DAProviderType;

           }
           set
           {
               _DAProviderType = value;
           }
       }


       public string DACommandType
       {
           get
           {
               return _DACommandType;

           }
           set
           {
               _DACommandType = value;
           }
       }

       public string DADBFactoryClass
       {
           get
           {
               return _DADBFactoryClass;

           }
           set
           {
               _DADBFactoryClass = value;
           }
       }

       public string DADBFactoryTemplate
       {
           get
           {
               return _DADBFactoryTemplate;

           }
           set
           {
               _DADBFactoryTemplate = value;
           }
       }

       #endregion

        #region Business Settings
       public string BusParentInterface
       {
           get
           {
               return _BusParentInterface;

           }
           set
           {
               _BusParentInterface = value;
           }
       }

       public string BusNameSpace
       {
           get
           {
               return _BusNameSpace;

           }
           set
           {
               _BusNameSpace = value;
           }
       }

       public string BusParentClass
       {
           get
           {
               return _BusParentClass;

           }
           set
           {
               _BusParentClass = value;
           }
       }
       public string BusAfterName
       {
           get
           {
               return _BusAfterName;

           }
           set
           {
               _BusAfterName = value;
           }
       }

       public string BusBeforeName
       {
           get
           {
               return _BusBeforeName;

           }
           set
           {
               _BusBeforeName = value;
           }
       }

       public string BusTemplate
       {
           get
           {
               return _BusTemplate;

           }
           set
           {
               _BusTemplate = value;
           }
       }
       #endregion

       #region Web Settings

       public string WebSearchAfterName { get { return _WebSearchAfterName; } set { _WebSearchAfterName = value; } }
       public string WebSearchBeforeName { get { return _WebSearchBeforeName; } set { _WebSearchBeforeName = value; } }
       public string WebSearchTemplate { get { return _WebSearchTemplate; } set { _WebSearchTemplate = value; } }
       public string WebSearchTemplateHtml { get { return _WebSearchTemplateHtml; } set { _WebSearchTemplateHtml = value; } }

       public string WebDetailsAfterName { get { return _WebDetailsAfterName; } set { _WebDetailsAfterName = value; } }
       public string WebDetailsBeforeName { get { return _WebDetailsBeforeName; } set { _WebDetailsBeforeName = value; } }
       public string WebDetailsTemplate { get { return _WebDetailsTemplate; } set { _WebDetailsTemplate = value; } }
       public string WebDetailsTemplateHtml { get { return _WebDetailsTemplateHtml; } set { _WebDetailsTemplateHtml = value; } }

       public string WebMasterName { get { return _WebMasterName; } set { _WebMasterName = value; } }
       public string WebMasterTitle { get { return _WebMasterTitle; } set { _WebMasterTitle = value; } }
       public string WebMasterTemplate { get { return _WebMasterTemplate; } set { _WebMasterTemplate = value; } }
       public string WebMasterTemplateHtml { get { return _WebMasterTemplateHtml ; } set { _WebMasterTemplateHtml = value; } }

       public string WebLoginSessionName { get { return _WebLoginSessionName; } set { _WebLoginSessionName = value; } }
       public string WebHomePageName { get { return _WebHomePageName; } set { _WebHomePageName = value; } }

       public string WebBaseWebFormTemplate { get { return _WebBaseWebFormTemplate; } set { _WebBaseWebFormTemplate = value; } }
       public string WebBaseWebFormTemplateHtml { get { return _WebBaseWebFormTemplateHtml; } set { _WebBaseWebFormTemplateHtml = value; } }


       public string WebLoginTemplate { get { return _WebLoginTemplate; } set { _WebLoginTemplate = value; } }
       public string WebLoginTemplateHtml { get { return _WebLoginTemplateHtml; } set { _WebLoginTemplateHtml = value; } }

       #endregion

        #endregion
   }
}
