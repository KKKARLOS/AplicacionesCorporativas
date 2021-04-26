using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using SII.Framework.Common;
using SII.Framework.ExceptionHandling;
using SII.Framework.HDLA;
using SII.Framework.LLDA;
using SII.HCD.BackOffice.Entities;
using SII.HCD.Misc;
using CommonEntities = SII.HCD.Common.Entities;

namespace SII.HCD.BackOffice.DA
{
    public class PersonDA : DAServiceBase
    {
        #region Field length constants
        public const int FirstNameLength = 50;
        public const int LastNameLength = 50;
        public const int LastName2Length = 50;
        public const int EmailAddressLength = 250;
        public const int ModifiedByLength = 256;
        #endregion

        #region private methods
        private string GetPersonSQLSearch(PersonSpecification spec, int chNumberInCareCenterID)
        {
            bool showCHNumberInCareCenter = chNumberInCareCenterID > 0;
            bool hasFilters = false;
            string mainSQL =
                String.Concat(
                "SELECT DISTINCT TOP(@MaxRecords) P.[ID], P.FirstName, P.LastName, P.LastName2, P.EmailAddress, P.RegistrationDate, SD.DeathDateTime, ", Environment.NewLine,
                "P.RecordMerged, P.HasMergedRegisters, P.[Status], P.[DuplicateGroupID], ", Environment.NewLine);

            if (spec.DefaultIdentifierTypeID > 0)
                mainSQL = string.Concat(mainSQL, "(SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE PIR.PersonID=P.[ID] AND PIR.IdentifierTypeID=@DefaultIdentifierTypeID) DefaultIdentifier, ", Environment.NewLine);
            else
            {
                if (!string.IsNullOrWhiteSpace(spec.DefaultIdentifierTypeName))
                    mainSQL = string.Concat(mainSQL, "(SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) JOIN IdentifierType IT WITH(NOLOCK) ON IT.[ID]=PIR.IdentifierTypeID WHERE PIR.PersonID=P.[ID] AND IT.[Name]=@DefaultIdentifierTypeName) DefaultIdentifier, ", Environment.NewLine);
                else
                    mainSQL = string.Concat(mainSQL, "(SELECT TOP 1 PIR.IDNumber FROM PersonIdentifierRel PIR WITH(NOLOCK) WHERE PIR.PersonID=P.[ID]) DefaultIdentifier, ", Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(spec.DefaultTelephoneTypeName))
                mainSQL = string.Concat(mainSQL, "(SELECT TOP 1 Telephone FROM Telephone T WITH(NOLOCK) JOIN PersonTelephoneRel PTR WITH(NOLOCK) ON T.[ID]=PTR.TelephoneID ", Environment.NewLine,
                "WHERE PTR.PersonID=P.[ID] AND T.TelephoneType=@DefaultTelephoneTypeName) DefaultTelephone, ", Environment.NewLine);
            else
                mainSQL = string.Concat(mainSQL, "(SELECT TOP 1 Telephone FROM Telephone T WITH(NOLOCK) JOIN PersonTelephoneRel PTR WITH(NOLOCK) ON T.[ID]=PTR.TelephoneID ", Environment.NewLine,
                "WHERE PTR.PersonID=P.[ID]) DefaultTelephone, ", Environment.NewLine);

            if (showCHNumberInCareCenter)
                mainSQL = String.Concat(mainSQL, "CRCH.CHNumber, ");
            else
                mainSQL = String.Concat(mainSQL, "C.CHNumber, ");


            mainSQL = String.Concat(mainSQL,
                "CAST(P.DBTimeStamp as bigint) DBTimeStamp, AD.Address1 AS [Address], AD.ZipCode, AD.City, AD.State, AD.Province, AD.Country", Environment.NewLine,
                "FROM Person P WITH(NOLOCK)", Environment.NewLine,
                "LEFT JOIN [SensitiveData] SD WITH(NOLOCK) ON P.[ID]=SD.[PersonID] ", Environment.NewLine,
                "LEFT JOIN [Address] AD WITH(NOLOCK) ON P.AddressID=AD.[ID]"
                );

            //JOIN Section
            mainSQL = GetJOINPersonFind(spec, showCHNumberInCareCenter, mainSQL);

            //WHERE Section
            GetWHEREPersonFind(spec, chNumberInCareCenterID, ref hasFilters, ref mainSQL);

            //ORDER BY Section
            mainSQL = String.Concat(mainSQL, Environment.NewLine, "ORDER BY P.[ID]");

            return mainSQL;
        }

        private string GetPersonIDSQLSearch(PersonSpecification spec)
        {
            bool hasFilters = false;
            string mainSQL =
                String.Concat(
                "SELECT DISTINCT TOP(@MaxRecords) P.[ID]", Environment.NewLine,
                "FROM Person P WITH(NOLOCK)"
                );

            //JOIN Section
            mainSQL = GetJOINPersonFind(spec, false, mainSQL);

            //WHERE Section
            GetWHEREPersonFind(spec, 0, ref hasFilters, ref mainSQL);

            //ORDER BY Section
            mainSQL = String.Concat(mainSQL, Environment.NewLine, "ORDER BY P.[ID]");

            return mainSQL;
        }

        private string GetPersonCustomerProcessDataSQLSearch(PersonSpecification spec, string excludedProcessIDs)
        {
            string mainSQL =
                String.Concat(
                "SELECT C.PersonID, C.[ID] CustomerID, CE.ProcessChartID", Environment.NewLine,
                "FROM (", GetPersonIDSQLSearch(spec), ") T1", Environment.NewLine,
                "JOIN Customer C ON T1.[ID] = C.PersonID", Environment.NewLine,
                "JOIN CustomerEpisode CE ON CE.CustomerID=C.[ID]", Environment.NewLine,
                "WHERE (CE.[Status]=@CEStatus)");

            if (!string.IsNullOrWhiteSpace(excludedProcessIDs))
                mainSQL = String.Concat(
                    mainSQL, Environment.NewLine,
                    "AND (CE.ProcessChartID IN (",
                    excludedProcessIDs, "))"
                    );

            return mainSQL;
        }

        private string GetPersonCustomerCategoriesDataSQLSearch(PersonSpecification spec)
        {
            string mainSQL =
                String.Concat(
                    "SELECT DISTINCT PCR.PersonID, C.[ID], C.CategoryKey, C.[Name]", Environment.NewLine,
                    "FROM Category C", Environment.NewLine,
                    "JOIN PersonCatRel PCR ON PCR.CategoryID=C.[ID]", Environment.NewLine,
                    "JOIN (", GetPersonIDSQLSearch(spec), ") T1 ON T1.[ID]=PCR.PersonID");

            return mainSQL;
        }

        private string GetPersonCustomerCHNumberDataSQLSearch(int[] personIDs)
        {
            string mainSQL = String.Concat(
                    "SELECT DISTINCT C.[ID] CustomerID, C.PersonID, CRN.CHNumber, CRN.CareCenterID, Org.[Name] CareCenterName", Environment.NewLine,
                    "FROM CustomerRelatedCHNumber CRN", Environment.NewLine,
                    "JOIN Customer C ON CRN.CustomerID=C.[ID]", Environment.NewLine,
                    "LEFT JOIN CareCenter CC ON CRN.CareCenterID=CC.[ID]", Environment.NewLine,
                    "LEFT JOIN Organization Org ON CC.OrganizationID=Org.[ID]", Environment.NewLine,
                    "WHERE C.PersonID IN (", StringUtils.BuildIDString(personIDs), ")");
            return mainSQL;
        }

        private static string GetJOINPersonFind(PersonSpecification spec, bool showCHNumberInCareCenter, string mainSQL)
        {
            if ((spec.IsFilteredByAny(PersonSearchOptionEnum.Profile
                | PersonSearchOptionEnum.CHNumber | PersonSearchOptionEnum.CHNumberCareCenter | PersonSearchOptionEnum.CardNumber | PersonSearchOptionEnum.CardBand
                | PersonSearchOptionEnum.EpisodeNumber | PersonSearchOptionEnum.Insurer | PersonSearchOptionEnum.PoorlyIdentified)) ||
                (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter) && (spec.ProcessChartAndCareCenterSearchMode == SearchMode.Including)))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN Customer C WITH(NOLOCK) ON P.[ID]=C.PersonID");
            else
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "LEFT JOIN Customer C WITH(NOLOCK) ON P.[ID]=C.PersonID");

            if ((spec.IsFilteredByAny(PersonSearchOptionEnum.EpisodeNumber)) ||
                (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter) && (spec.ProcessChartAndCareCenterSearchMode == SearchMode.Including)))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN CustomerProcess CP WITH(NOLOCK) ON C.[ID]=CP.CustomerID");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.IdentifierType | PersonSearchOptionEnum.IdentifierNumber))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN PersonIdentifierRel PIR WITH(NOLOCK) ON PIR.PersonID=P.[ID]");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Category))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN PersonCatRel PCR WITH(NOLOCK) ON P.[ID]=PCR.PersonID");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CategoryKey))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN PersonCatRel PCR WITH(NOLOCK) ON P.[ID]=PCR.PersonID",
                                                 Environment.NewLine, "JOIN Category CAT WITH(NOLOCK) ON CAT.[ID]=PCR.CategoryID");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardBand | PersonSearchOptionEnum.CardNumber | PersonSearchOptionEnum.CardNumberInsurerID))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN CustomerCard CC WITH(NOLOCK) ON CC.CustomerID=C.[ID]");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.EpisodeNumber))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CE.[ID]=CP.CustomerEpisodeID");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Insurer))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN CustomerPolicy CPol WITH(NOLOCK) ON C.[ID]=CPol.CustomerID");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByNameParts | PersonSearchOptionEnum.PhoneticLookupByFullName))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN PersonPhoneticInfo PPI WITH(NOLOCK) ON PPI.PersonID=P.[ID]");

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter))
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID]");
            else
                if (showCHNumberInCareCenter)
                    mainSQL = String.Concat(mainSQL, Environment.NewLine, "LEFT JOIN CustomerRelatedCHNumber CRCH WITH(NOLOCK) ON CRCH.CustomerID=C.[ID] AND CRCH.CareCenterID=@CHNumberCareCenterID");

            return mainSQL;
        }

        private static void GetWHEREPersonFind(PersonSpecification spec, int chNumberInCareCenterID, ref bool hasFilters, ref string mainSQL)
        {
            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByNameParts))
            {
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(PPI.FirstName LIKE @FirstName) ");
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(PPI.LastName LIKE @LastName) ");
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(PPI.LastName2 LIKE @LastName2) ");
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.FirstName | PersonSearchOptionEnum.LastName | PersonSearchOptionEnum.LastName2))
                {
                    mainSQL = String.Concat(mainSQL, " AND (PPI.AddinName=@AddinName)");
                }
            }
            else if (spec.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByFullName))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(PPI.FullName LIKE @FullName) AND (PPI.AddinName=@AddinName)");
            }
            else
            {
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(P.FirstName LIKE @FirstName)");
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(P.LastName LIKE @LastName)");
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(P.LastName2 LIKE @LastName2)");
                }
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter) && (spec.ProcessChartAndCareCenterSearchMode == SearchMode.Including))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CP.ProcessChartID=@ProcessChartID) AND (CP.CareCenterID=@CareCenterID) AND (CP.Status=@Status)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter) && (spec.ProcessChartAndCareCenterSearchMode == SearchMode.Excluding))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL,
                    @"NOT(P.[ID] IN (SELECT CP.PersonID FROM CustomerProcess CP WITH(NOLOCK) 
				   LEFT JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.[ID]
				   WHERE (CP.ProcessChartID=@ProcessChartID) AND (CP.CareCenterID=@CareCenterID) AND
				   ((CE.Status=@Status) OR (CE.[ID] IS NULL)) AND (CP.Status=@Status)))");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.IdentifierType))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(PIR.IdentifierTypeID=@IdentifierTypeID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.IdentifierNumber))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                if (spec.MatchingMode == MatchingModeEnum.Like)
                    mainSQL = String.Concat(mainSQL, "(PIR.IDNumber LIKE @IdentifierNumber+'%')");
                else
                    mainSQL = String.Concat(mainSQL, "(PIR.IDNumber=@IdentifierNumber)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Category))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(PCR.CategoryID=@CategoryID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CategoryKey))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CAT.CategoryKey=@CategoryKey) AND (CAT.[Type]=@CategoryType)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ExcludedCategory))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(NOT EXISTS(SELECT [ID] FROM PersonCatRel WITH(NOLOCK) WHERE CategoryID=@ExcludedCategoryID AND PersonID=P.[ID]))");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ExcludedCategoryKey))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(NOT EXISTS(SELECT PersonCatRel.[ID] FROM PersonCatRel WITH(NOLOCK) JOIN Category WITH(NOLOCK) ON PersonCatRel.CategoryID=Category.[ID] ",
                    "WHERE (Category.CategoryKey=@ExcludedCategoryKey) AND (Category.[Type]=@CategoryType) AND (PersonCatRel.PersonID=P.[ID])))");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Profile))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(C.ProfileID=@ProfileID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumber))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(C.CHNumber=@CHNumber)");
            }
            else
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(CRCH.CHNumber=@CHNumber)");
                    if (chNumberInCareCenterID > 0)
                    {
                        CheckHasFilters(ref hasFilters, ref mainSQL);
                        mainSQL = String.Concat(mainSQL, "(CRCH.CareCenterID=@CHNumberCareCenterID)");
                    }
                }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardNumber))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CC.CardNumber=@CardNumber)");
            }
            else
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardNumberInsurerID))
                {
                    CheckHasFilters(ref hasFilters, ref mainSQL);
                    mainSQL = String.Concat(mainSQL, "(CC.CardNumber=@CardNumber) AND (CC.InsurerID=@CardInsurerID)");
                }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardBand))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CC.Track2=@CardBand)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.EpisodeNumber))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CE.EpisodeNumber=@EpisodeNumber)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Insurer))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(CPol.InsurerID=@InsurerID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PoorlyIdentified))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(C.PoorlyIdentified=@PoorlyIdentified)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PersonID))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(P.[ID]=@PersonID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.RecordMerged))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(P.RecordMerged=@RecordMergedPersonID)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.DuplicateGroupID))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(P.DuplicateGroupID=@DuplicateGroupID)");
            }

            if (!spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumber) && !spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter)
                && !spec.IsFilteredByAny(PersonSearchOptionEnum.PersonID) && !spec.IsFilteredByAny(PersonSearchOptionEnum.RecordMerged)
                && !spec.IsFilteredByAny(PersonSearchOptionEnum.DuplicateGroupID))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                mainSQL = String.Concat(mainSQL, "(P.[Status]=@PersonStatus)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Deceased))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                if (spec.IsDeceased)
                    mainSQL = String.Concat(mainSQL, "(SD.DeathDateTime IS NOT NULL)");
                else
                    mainSQL = String.Concat(mainSQL, "(SD.DeathDateTime IS NULL)");
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.HasDuplicates))
            {
                CheckHasFilters(ref hasFilters, ref mainSQL);
                if (spec.HasDuplicates)
                    mainSQL = String.Concat(mainSQL, "(P.DuplicateGroupID>0)");
                else
                    mainSQL = String.Concat(mainSQL, "(P.DuplicateGroupID=0)");
            }
        }

        private static void GetParametersPersonFind(PersonSpecification spec, int chNumberInCareCenterID, string phoneticAddinName, List<StoredProcParam> myParams)
        {
            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByNameParts))
            {
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
                {
                    myParams.Add(StoredProcInStringParam.Create("FirstName", string.Concat(spec.FirstName, "%"), 100));
                    if (!myParams.Exists(p => p.Name == "AddinName"))
                        myParams.Add(StoredProcInStringParam.Create("AddinName", phoneticAddinName, 256));
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName))
                {
                    myParams.Add(StoredProcInStringParam.Create("LastName", string.Concat(spec.LastName, "%"), 100));
                    if (!myParams.Exists(p => p.Name == "AddinName"))
                        myParams.Add(StoredProcInStringParam.Create("AddinName", phoneticAddinName, 256));
                }

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
                {
                    myParams.Add(StoredProcInStringParam.Create("LastName2", string.Concat(spec.LastName2, "%"), 100));
                    if (!myParams.Exists(p => p.Name == "AddinName"))
                        myParams.Add(StoredProcInStringParam.Create("AddinName", phoneticAddinName, 256));
                }
            }
            else if (spec.IsFilteredByAny(PersonSearchOptionEnum.PhoneticLookupByFullName))
            {
                myParams.Add(StoredProcInStringParam.Create("FullName", string.Concat(spec.PhoneticLookupFullName, "%"), 300));
                if (!myParams.Exists(p => p.Name == "AddinName"))
                    myParams.Add(StoredProcInStringParam.Create("AddinName", phoneticAddinName, 256));
            }
            else
            {
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.FirstName))
                    myParams.Add(StoredProcInStringParam.Create("FirstName", string.Concat(spec.FirstName, "%"), 100));

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName))
                    myParams.Add(StoredProcInStringParam.Create("LastName", string.Concat(spec.LastName, "%"), 100));

                if (spec.IsFilteredByAny(PersonSearchOptionEnum.LastName2))
                    myParams.Add(StoredProcInStringParam.Create("LastName2", string.Concat(spec.LastName2, "%"), 100));
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter))
            {
                myParams.Add(new StoredProcInParam("ProcessChartID", DbType.Int32, spec.ProcessChartID));
                myParams.Add(new StoredProcInParam("CareCenterID", DbType.Int32, spec.CareCenterID));
                myParams.Add(new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ProcessChartAndCareCenter) && (spec.ProcessChartAndCareCenterSearchMode == SearchMode.Excluding))
            {
                myParams.Add(new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.IdentifierType))
                myParams.Add(new StoredProcInParam("IdentifierTypeID", DbType.Int32, spec.IdentifierTypeID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.IdentifierNumber))
                myParams.Add(StoredProcInStringParam.Create("IdentifierNumber", spec.IdentifierNumber, 50));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Category))
                myParams.Add(new StoredProcInParam("CategoryID", DbType.Int32, spec.CategoryID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ExcludedCategory))
                myParams.Add(new StoredProcInParam("ExcludedCategoryID", DbType.Int32, spec.ExcludedCategoryID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CategoryKey))
            {
                myParams.Add(new StoredProcInParam("CategoryKey", DbType.Int32, (int)spec.CategoryKey));
                myParams.Add(new StoredProcInParam("CategoryType", DbType.Int32, (int)CategoryTypeEnum.Person));
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.ExcludedCategoryKey))
            {
                myParams.Add(new StoredProcInParam("ExcludedCategoryKey", DbType.Int32, (int)spec.ExcludedCategoryKey));
                myParams.Add(new StoredProcInParam("CategoryType", DbType.Int32, (int)CategoryTypeEnum.Person));
            }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Profile))
                myParams.Add(new StoredProcInParam("ProfileID", DbType.Int32, spec.ProfileID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumber))
                myParams.Add(StoredProcInStringParam.Create("CHNumber", spec.CHNumber, 50));
            else
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter))
                {
                    myParams.Add(StoredProcInStringParam.Create("CHNumber", spec.CHNumber, 50));
                    myParams.Add(new StoredProcInParam("CHNumberCareCenterID", DbType.Int32, spec.CHNumberCareCenterID));
                }

            if (chNumberInCareCenterID > 0)
                if (!myParams.Exists(p => p.Name == "CHNumberCareCenterID"))
                    myParams.Add(new StoredProcInParam("CHNumberCareCenterID", DbType.Int32, chNumberInCareCenterID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardNumber))
                myParams.Add(StoredProcInStringParam.Create("CardNumber", spec.CardNumber, 50));
            else
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardNumberInsurerID))
                {
                    myParams.Add(StoredProcInStringParam.Create("CardNumber", spec.CardNumber, 50));
                    myParams.Add(new StoredProcInParam("CardInsurerID", DbType.Int32, spec.CardInsurerID));
                }

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.CardBand))
                myParams.Add(StoredProcInStringParam.Create("CardBand", spec.CardBand, 256));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.EpisodeNumber))
                myParams.Add(StoredProcInStringParam.Create("EpisodeNumber", spec.EpisodeNumber, 50));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.Insurer))
                myParams.Add(new StoredProcInParam("InsurerID", DbType.Int32, spec.InsurerID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PoorlyIdentified))
                myParams.Add(new StoredProcInParam("PoorlyIdentified", DbType.Boolean, spec.PoorlyIdentified));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.PersonID))
                myParams.Add(new StoredProcInParam("PersonID", DbType.Int32, spec.PersonID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.RecordMerged))
                myParams.Add(new StoredProcInParam("RecordMergedPersonID", DbType.Int32, spec.RecordMergedPersonID));

            if (spec.IsFilteredByAny(PersonSearchOptionEnum.DuplicateGroupID))
                myParams.Add(new StoredProcInParam("DuplicateGroupID", DbType.Int32, spec.DuplicateGroupID));

            if (!spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumber) && !spec.IsFilteredByAny(PersonSearchOptionEnum.CHNumberCareCenter)
                && !spec.IsFilteredByAny(PersonSearchOptionEnum.PersonID) && !spec.IsFilteredByAny(PersonSearchOptionEnum.RecordMerged)
                && !spec.IsFilteredByAny(PersonSearchOptionEnum.DuplicateGroupID))
            {
                if (spec.IsFilteredByAny(PersonSearchOptionEnum.PersonStatus))
                    myParams.Add(new StoredProcInParam("PersonStatus", DbType.Int32, (int)spec.PersonStatus));
                else
                    myParams.Add(new StoredProcInParam("PersonStatus", DbType.Int32, (int)CommonEntities.StatusEnum.Active));
            }

            if (spec.DefaultIdentifierTypeID > 0)
                myParams.Add(new StoredProcInParam("DefaultIdentifierTypeID", DbType.Int32, spec.DefaultIdentifierTypeID));
            else
            {
                if (!string.IsNullOrWhiteSpace(spec.DefaultIdentifierTypeName))
                    myParams.Add(StoredProcInStringParam.Create("DefaultIdentifierTypeName", spec.DefaultIdentifierTypeName, 100));
            }

            if (!string.IsNullOrWhiteSpace(spec.DefaultTelephoneTypeName))
                myParams.Add(StoredProcInStringParam.Create("DefaultTelephoneTypeName", spec.DefaultTelephoneTypeName, 50));
        }

        private static void CheckHasFilters(ref bool hasFilters, ref string mainSQL)
        {
            if (!hasFilters)
            {
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "WHERE ");
                hasFilters = true;
            }
            else
                mainSQL = String.Concat(mainSQL, Environment.NewLine, "AND ");
        }

        #endregion

        #region public methods
        public PersonDA() : base(DAServiceBase.GetDatabaseName("HCDDB")) { }

        public PersonDA(Gateway gateway) : base(gateway) { }

        public int Insert(string firstName, string lastName, bool asUser, string lastName2,
            string emailAddress, int imageID, int duplicateGroupID, int recordMerged, bool hasMergedRegisters,
            int addressID, int addressID2, string modifiedBy, int status)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.InsertPersonCommand,
                    new StoredProcInParam("FirstName", DbType.String, SIIStrings.Left(firstName, FirstNameLength)),
                    new StoredProcInParam("LastName", DbType.String, SIIStrings.Left(lastName, LastNameLength)),
                    new StoredProcInParam("AsUser", DbType.Boolean, asUser),
                    new StoredProcInParam("LastName2", DbType.String, SIIStrings.Left(lastName2, LastName2Length)),
                    new StoredProcInParam("EmailAddress", DbType.String, SIIStrings.Left(emailAddress, EmailAddressLength)),
                    new StoredProcInParam("ImageID", DbType.Int32, imageID),
                    new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID),
                    new StoredProcInParam("RecordMerged", DbType.Int32, recordMerged),
                    new StoredProcInParam("HasMergedRegisters", DbType.Boolean, hasMergedRegisters),
                    new StoredProcInParam("AddressID", DbType.Int32, addressID),
                    new StoredProcInParam("Address2ID", DbType.Int32, addressID2),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    ))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, string firstName, string lastName, bool asUser, string lastName2,
            string emailAddress, int imageID, int duplicateGroupID, int recordMerged, bool hasMergedRegisters,
            int addressID, int addressID2, string modifiedBy, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePersonCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("FirstName", DbType.String, SIIStrings.Left(firstName, FirstNameLength)),
                    new StoredProcInParam("LastName", DbType.String, SIIStrings.Left(lastName, LastNameLength)),
                    new StoredProcInParam("AsUser", DbType.Boolean, asUser),
                    new StoredProcInParam("LastName2", DbType.String, SIIStrings.Left(lastName2, LastName2Length)),
                    new StoredProcInParam("EmailAddress", DbType.String, SIIStrings.Left(emailAddress, EmailAddressLength)),
                    new StoredProcInParam("ImageID", DbType.Int32, imageID),
                    new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID),
                    new StoredProcInParam("RecordMerged", DbType.Int32, recordMerged),
                    new StoredProcInParam("HasMergedRegisters", DbType.Boolean, hasMergedRegisters),
                    new StoredProcInParam("AddressID", DbType.Int32, addressID),
                    new StoredProcInParam("Address2ID", DbType.Int32, addressID2),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)),
                    new StoredProcInParam("Status", DbType.Int32, status)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePersonStampCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int id, int status, int recordMerged, bool hasMergedRegisters, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePersonStatusCommand,
                    new StoredProcInParam("ID", DbType.Int32, id),
                    new StoredProcInParam("Status", DbType.Int32, status),
                    new StoredProcInParam("RecordMerged", DbType.Int32, recordMerged),
                    new StoredProcInParam("HasMergedRegisters", DbType.Boolean, hasMergedRegisters),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public int Update(int[] ids, int recordMerged, string modifiedBy)
        {
            try
            {
                if (ids == null || ids.Length <= 0) return 0;

                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePersonRecordMergedCommand,
                    new StoredProcInTVPIntegerParam("TVPTable", ids),
                    new StoredProcInParam("RecordMerged", DbType.Int32, recordMerged),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength))
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }


        public int GetRecordMerged(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetRecordMergedCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["RecordMerged"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        //public void UpdateSecondDuplicate(int id, bool hasDuplicate, int recordMerged, string modifiedBy)
        //{
        //    try
        //    {
        //        this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedSecondDuplicateCommand,
        //            new StoredProcInParam("ID", DbType.Int32, id),
        //            new StoredProcInParam("HasDuplicate", DbType.Boolean, hasDuplicate),
        //            new StoredProcInParam("RecordMerged", DbType.Int32, recordMerged),
        //            new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
        //        return;
        //    }
        //}

        public DataSet GetPersonDuplicateGroups(int[] personIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonDuplicateGroupsCommand, TableNames.PersonTable,
                    new StoredProcInTVPIntegerParam("TVPTable", personIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsInDuplicateGroups(int[] duplicateGroupIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsInDuplicateGroupsCommand, TableNames.PersonTable,
                    new StoredProcInTVPIntegerParam("TVPTable", duplicateGroupIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }


        public int UpdatePersonDuplicateGroups(int[] personIDs, int duplicateGroupID, string modifiedBy)
        {
            try
            {
                return this.Gateway.ExecuteQueryNonQuery(SQLProvider.UpdatePersonDuplicateGroupsCommand,
                    new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID),
                    new StoredProcInTVPIntegerParam("TVPTable", personIDs),
                    new StoredProcInParam("ModifiedBy", DbType.String, SIIStrings.Left(modifiedBy, ModifiedByLength)));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public void MarkRelatedPhysiciansUpdated(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysiciansFromPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromAdmissionConfigMedEpProcessPhysicianRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromAdmissionConfigMedEpProcessPhysicianRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReceptionConfigMedEpProcessPhysicianRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReceptionConfigMedEpProcessPhysicianRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelHHRR(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelHHRRPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.HumanResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReceptionConfigReceptionResourceRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.PhysicianAsResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromCitationConfigMedEpProcessPhysicianRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromCitationConfigMedEpProcessPhysicianRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelHHRR(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelHHRRPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.HumanResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromCitationConfigCitationResourceRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.PhysicianAsResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromWaitingListConfigMedEpProcessPhysicianRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromWaitingListConfigMedEpProcessPhysicianRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelHHRR(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelHHRRPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.HumanResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromWaitingListConfigWLResourceRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceElement", DbType.Int16, (short)AppointmentResourceElementEnum.PhysicianAsResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromInterviewConfigMedEpProcessPhysicianRelPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromInterviewConfigMedEpProcessPhysicianRelPhysicianPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReservationConfigResourceReservedRel(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReservationConfigResourceReservedRelPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ResourceType", DbType.Int16, (short)ResourceTypeEnum.HumanResource),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedPhysician(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysicianFromPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedPhysicianFromOrganizationContactPerson(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedPhysicianFromOrganizationContactPersonPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedObservationsFromObservationNotificationCriterionNotificationToPerson(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdateObservationsFromObservationNotificationCriterionNotificationToPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedObservationTypesFromObservationObservationNotificationCriterionNotificationToPerson(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdateObservationTypesFromObservationObservationNotificationCriterionNotificationToPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedObservationBlocksFromObservationObservationNotificationCriterionNotificationToPerson(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdateObservationBlocksFromObservationObservationNotificationCriterionNotificationToPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedObservationTemplatesFromObservationObservationNotificationCriterionNotificationToPerson(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdateObservationTemplatesFromObservationObservationNotificationCriterionNotificationToPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkRelatedUpdatedObservationTemplatesFromObservationBlockObservationNotificationCriterionNotificationToPerson(int personID, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkRelatedUpdateObservationTemplatesFromObservationBlockObservationObservationNotificationCriterionNotificationToPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, personID),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromProcessChartCareCenterRelProcessChartHierarchyRelNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromProcessChartCareCenterRelProcessChartHierarchyRelNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromAdmissionConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromAdmissionConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromTransferConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromTransferConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromLeaveConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromLeaveConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromInterviewConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromInterviewConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromPreAssessmentConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromPreAssessmentConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReportConfigResultRejectNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReportConfigResultRejectNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReportConfigReportAbortCancelNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReportConfigReportAbortCancelNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromReportConfigReportSignedNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromReportConfigReportSignedNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromCoverConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromCoverConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromDeliveryNoteConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromDeliveryNoteConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public void MarkUpdatedRelatedProcessChartFromInvoiceConfigNotification(int id, string modifiedBy)
        {
            try
            {
                this.Gateway.ExecuteQueryNonQuery(SQLProvider.MarkUpdatedRelatedProcessChartFromInvoiceConfigNotificationPersonCommand,
                        new StoredProcInParam("PersonID", DbType.Int32, id),
                        new StoredProcInParam("ModifiedBy", DbType.String, modifiedBy));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return;
            }
        }

        public Int64 GetDBTimeStamp(int id)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonDBTimeStampCommand, TableNames.PersonTable,
                    new StoredProcInParam("ID", DbType.Int32, id));
                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger64(result.Tables[TableNames.PersonTable].Rows[0]["DBTimeStamp"].ToString());
                }
                else return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetPersons(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsCommand, TableNames.PersonListDTOTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(DateTime fromDate, DateTime toDate, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNCommand, TableNames.PersonListDTOTable,
                    new StoredProcInParam("FromDate", DbType.DateTime, (fromDate != DateTime.MinValue) ? (object)fromDate : (object)DBNull.Value),
                    new StoredProcInParam("ToDate", DbType.DateTime, (toDate != DateTime.MinValue) ? (object)toDate : (object)DBNull.Value),
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
/*
        public DataSet GetPerson(int personID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonCommand, TableNames.PersonTable,
                    new StoredProcInParam("ID", DbType.Int32, personID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
*/
        public DataSet GetPerson(int personID)
        {
            try
            {
                SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@PersonID", SqlDbType.Int, 4, personID)
					};
                DataSet ds = SqlHelper.ExecuteDataset("ObtenerPersonEntity", aParam);

                int i = 0;

                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonTelephoneTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.TelephoneTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.AddressTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.Address2Table;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonCategoryTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.CategoryTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.SensitiveDataTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.PersonIdentifierRelTable;
                ds.Tables[i++].TableName = BackOffice.Entities.TableNames.IdentifierTypeTable;
                return ds;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        public int obtenerPersonID_From_HumanResource(int humanResourceId)
        {
            int personId = 0;
            SqlParameter[] aParam = new SqlParameter[]{
						ParametroSql.add("@HumanResourceID", SqlDbType.Int, 4, humanResourceId)
					};

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("Obtener_PersonID_From_HumanResource", aParam);

            if (dr.Read()) personId=int.Parse(dr["PersonID"].ToString());
            dr.Close();
            dr.Dispose();
            return personId;
        }
        
        public int obtenerPersonID_From_CustomerContactPerson(int Id)
        {
            int personId = 0;
            SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@ID", SqlDbType.Int, 4, Id)
                    };

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("Obtener_PersonID_From_CustomerContactPerson", aParam);

            if (dr.Read()) personId = int.Parse(dr["PersonID"].ToString());
            dr.Close();
            dr.Dispose();
            return personId;
        }
        public int obtenerPersonID_From_NOK(int Id)
        {
            int personId = 0;
            SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@ID", SqlDbType.Int, 4, Id)
                    };

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("Obtener_PersonID_From_NOK", aParam);

            if (dr.Read()) personId = int.Parse(dr["PersonID"].ToString());
            dr.Close();
            dr.Dispose();
            return personId;
        }
        public int obtenerPersonID_From_Customer(int Id)
        {
            int personId = 0;
            SqlParameter[] aParam = new SqlParameter[]{
                        ParametroSql.add("@ID", SqlDbType.Int, 4, Id)
                    };

            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("Obtener_PersonID_From_Customer", aParam);

            if (dr.Read()) personId = int.Parse(dr["PersonID"].ToString());
            dr.Close();
            dr.Dispose();
            return personId;
        }
        public DataSet GetPersonsByRecordMerged(int recordMergedID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsByRecordMergedCommand, TableNames.PersonTable,
                    new StoredProcInParam("RecordMergedID", DbType.Int32, recordMergedID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetByPhysicianTimestamp(long timestamp)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                    SQLProvider.GetPersonByPhysicianTimestampCommand, TableNames.PersonTable,
                    new StoredProcInParam("DBTimestamp", DbType.Int64, timestamp));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsOthersWhitoutAdmision(string firstName, string lastName, int identifierTypeID, string idNumber,
            int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNOthersWithoutAdmissionCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsCustomerWhitoutAdmision(string firstName, string lastName, int identifierTypeID, string idNumber,
            int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNCustomerWithoutAdmissionCommand,
                    TableNames.PersonAddressListDTOTable, new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsHumanResourceWhitoutAdmision(string firstName, string lastName, int identifierTypeID, string idNumber,
            int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNHumanResourceWithoutAdmissionCommand,
                    TableNames.PersonAddressListDTOTable, new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeCustomerCustomerCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeCustomerCustomerCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeCustomerHHRRCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeCustomerHHRRCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeCustomer(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeCustomerCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeHHRRCustomerCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeHHRRCustomerCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeHHRRHHRRCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeHHRRHHRRCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeHHRR(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeHHRRCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludePhysicianCustomerCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludePhysicianCustomerCatCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludePhysicianHHRRCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludePhysicianHHRRCatCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludePhysician(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludePhysicianCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeNOK(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords, int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeNOKCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeCCP(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords, int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeCCPCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsExcludeOrg(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords, int organizationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNExcludeOrgCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("OrganizationID", DbType.Int32, organizationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsCustomerCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNCustomerCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsHHRRCat(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int profileID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNHHRRCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(string firstName, string lastName, int identifierTypeID, string idNumber, int categoryID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNAnyCatCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsAnyCategory(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNAnyCatAmpliedCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("LastName2", DbType.String, lastName2),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(int processChartID, int careCenterID, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsCustomerTopNByProcessChartIDCareCenterIDCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(int processChartID, int careCenterID, int status, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsCustomerTopNByStatusProcessChartInCareCenterCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsByStatus(int processChartID, int careCenterID, int customerProcessStatus, int customerEpisodeStatus, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsCustomerTopNByStatusProcessChartInCareCenterByStatusCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("CustomerProcessStatus", DbType.Int32, customerProcessStatus),
                    new StoredProcInParam("CustomerEpisodeStatus", DbType.Int32, customerEpisodeStatus));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(int processChartID, int careCenterID, int status, long step, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsCustomerTopNByNotStepProcessChartInCareCenterCommand,
                    TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Step", DbType.Int64, step),
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsByType(int customerID, int type)
        {
            try
            {
                if (type == 1) // contacto cliente
                {
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetCustomerContactPerson,
                        TableNames.PersonBaseTable,
                        new StoredProcInParam("customerID", DbType.Int32, customerID),
                        new StoredProcInParam("type", DbType.Int32, type));

                }
                else if (type == 2)//tipo NOK
                {
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetNOKPersonCommand,
                        TableNames.PersonBaseTable,
                        new StoredProcInParam("customerID", DbType.Int32, customerID),
                        new StoredProcInParam("type", DbType.Int32, type));
                }
                else if (type == 3)//tipo contact
                {
                    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetContactPersonCommand,
                        TableNames.PersonBaseTable,
                        new StoredProcInParam("customerID", DbType.Int32, customerID),
                        new StoredProcInParam("type", DbType.Int32, type));
                }
                //Pte cambio 

                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonBaseByID(int personID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonBaseByID,
                        TableNames.PersonBaseTable,
                        new StoredProcInParam("ID", DbType.Int32, personID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsBaseByIDs(int[] personIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsBaseByIDs,
                        TableNames.PersonBaseTable,
                        new StoredProcInTVPIntegerParam("TVPTable", personIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonBaseByCustomerID(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonBaseByCustomerID,
                        TableNames.PersonBaseTable,
                        new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsRRHH(int careCenterID, int processChartID, int processStep)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetRRHHPersons,
                    TableNames.PersonBaseTable,
                    new StoredProcInParam("CareCenter", DbType.Int32, careCenterID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("ProcessStep", DbType.Int32, processStep));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetPerson(PersonFindRequest person)
        {
            try
            {
                if (person == null) throw new ArgumentNullException("findperson");

                if (!person.MandatoryFirstName && !person.MandatoryLastName) return -1;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Person WHERE ";

                if (person.MandatoryFirstName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[FirstName]=@FirstName");
                }

                if (person.MandatoryLastName)
                {
                    if (person.MandatoryFirstName)
                    {
                        sqlCommand = String.Concat(sqlCommand, " AND [LastName]=@LastName");
                        result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, person.FirstName),
                            new StoredProcInParam("LastName", DbType.String, person.LastName));
                    }
                    else
                    {
                        sqlCommand = String.Concat(sqlCommand, "[LastName]=@LastName");
                        result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, person.LastName));
                    }
                }
                else
                {
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                        new StoredProcInParam("FirstName", DbType.String, person.FirstName));
                }

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.PersonTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int[] GetPersonIDs(PersonFindRequest person)
        {
            try
            {
                if (person == null) throw new ArgumentNullException("findperson");

                if (!person.MandatoryFirstName && !person.MandatoryLastName) return null;

                DataSet result = null;
                string sqlCommand = "SELECT [ID] FROM Person WHERE ";

                if (person.MandatoryFirstName)
                {
                    sqlCommand = String.Concat(sqlCommand, "[FirstName]=@FirstName");
                }

                if (person.MandatoryLastName)
                {
                    if (person.MandatoryFirstName)
                    {
                        sqlCommand = String.Concat(sqlCommand, " AND [LastName]=@LastName");
                        result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, person.FirstName),
                            new StoredProcInParam("LastName", DbType.String, person.LastName));
                    }
                    else
                    {
                        sqlCommand = String.Concat(sqlCommand, "[LastName]=@LastName");
                        result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, person.LastName));
                    }
                }
                else
                {
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                        new StoredProcInParam("FirstName", DbType.String, person.FirstName));
                }

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    List<int> ids = new List<int>();
                    foreach (DataRow row in result.Tables[TableNames.PersonTable].Rows)
                    {
                        ids.Add(SIIConvert.ToInteger(row["ID"].ToString()));
                    }
                    return ids.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int GetPerson(CustomerFindRequest customer)
        {
            try
            {
                if (customer == null) throw new ArgumentNullException("findperson");

                if (!customer.MandatoryFirstName && !customer.MandatoryLastName && !customer.MandatoryIdentifierType) return -1;

                DataSet result = null;
                int identifierTypeID = 0;
                string sqlCommand = "SELECT Person.[ID] FROM Person ";

                if (customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "LEFT JOIN PersonIdentifierRel ON Person.[ID]=PersonIdentifierRel.PersonID ");
                    IdentifierTypeDA identifierTypeDA = new IdentifierTypeDA();
                    identifierTypeID = identifierTypeDA.GetIdentifierTypeID(customer.MandatoryIdentifierTypeDefaultValue);
                }

                sqlCommand = String.Concat(sqlCommand, "WHERE ");

                if (customer.MandatoryFirstName && !customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                        new StoredProcInParam("FirstName", DbType.String, customer.FirstName));
                }

                if (customer.MandatoryFirstName && customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND Person.[LastName]=@LastName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("LastName", DbType.String, customer.LastName));
                }

                if (!customer.MandatoryFirstName && customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[LastName]=@LastName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, customer.LastName));
                }

                if (customer.MandatoryFirstName && !customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (customer.MandatoryFirstName && customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND Person.[LastName]=@LastName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("LastName", DbType.String, customer.LastName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (!customer.MandatoryFirstName && !customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (!customer.MandatoryFirstName && customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[LastName]=@LastName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, customer.LastName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return SIIConvert.ToInteger(result.Tables[TableNames.PersonTable].Rows[0]["ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return -1;
            }
        }

        public int[] GetPersonIDs(CustomerFindRequest customer)
        {
            try
            {
                if (customer == null) throw new ArgumentNullException("findperson");

                if (!customer.MandatoryFirstName && !customer.MandatoryLastName && !customer.MandatoryIdentifierType) return null;

                DataSet result = null;
                int identifierTypeID = 0;
                string sqlCommand = "SELECT Person.[ID] FROM Person ";

                if (customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "LEFT JOIN PersonIdentifierRel ON Person.[ID]=PersonIdentifierRel.PersonID ");
                    IdentifierTypeDA identifierTypeDA = new IdentifierTypeDA();
                    identifierTypeID = identifierTypeDA.GetIdentifierTypeID(customer.MandatoryIdentifierTypeDefaultValue);
                }

                sqlCommand = String.Concat(sqlCommand, "WHERE ");

                if (customer.MandatoryFirstName && !customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                        new StoredProcInParam("FirstName", DbType.String, customer.FirstName));
                }

                if (customer.MandatoryFirstName && customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND Person.[LastName]=@LastName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("LastName", DbType.String, customer.LastName));
                }

                if (!customer.MandatoryFirstName && customer.MandatoryLastName && !customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[LastName]=@LastName");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, customer.LastName));
                }

                if (customer.MandatoryFirstName && !customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (customer.MandatoryFirstName && customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[FirstName]=@FirstName AND Person.[LastName]=@LastName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, customer.FirstName),
                            new StoredProcInParam("LastName", DbType.String, customer.LastName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (!customer.MandatoryFirstName && !customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (!customer.MandatoryFirstName && customer.MandatoryLastName && customer.MandatoryIdentifierType)
                {
                    sqlCommand = String.Concat(sqlCommand, "Person.[LastName]=@LastName AND ((PersonIdentifierRel.IDNumber=@IDNumber AND PersonIdentifierRel.IdentifierTypeID=@IdentifierTypeID) OR (PersonIdentifierRel.[ID] IS NULL))");
                    result = this.Gateway.ExecuteQueryDataSet(sqlCommand, TableNames.PersonTable,
                            new StoredProcInParam("LastName", DbType.String, customer.LastName),
                            new StoredProcInParam("IDNumber", DbType.String, customer.IdentifierIDNumber),
                            new StoredProcInParam("IdentifierTypeID", DbType.String, identifierTypeID));
                }

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    List<int> ids = new List<int>();
                    foreach (DataRow row in result.Tables[TableNames.PersonTable].Rows)
                    {
                        ids.Add(SIIConvert.ToInteger(row["ID"].ToString()));
                    }
                    return ids.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int[] GetPersonIDs(string queryResult, StoredProcInParam[] storedProcInParam)
        {
            try
            {
                if (String.IsNullOrEmpty(queryResult)) throw new ArgumentNullException("queryResult");
                if (storedProcInParam == null) throw new ArgumentNullException("storedProcInParam");

                DataSet result = this.Gateway.ExecuteQueryDataSet(queryResult, TableNames.PersonTable, storedProcInParam);

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    List<int> ids = new List<int>();
                    foreach (DataRow row in result.Tables[TableNames.PersonTable].Rows)
                    {
                        ids.Add(SIIConvert.ToInteger(row["ID"].ToString()));
                    }
                    return ids.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public string GetUserNameFromPersonID(int personID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetUserNameFromPersonIDCommand,
                    new StoredProcInParam("PersonID", DbType.Int32, personID)))
                {
                    return (IsEmptyReader(reader)) ? string.Empty : reader["UserName"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return string.Empty;
            }
        }

        public int GetImageIDByPersonID(int id)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetPersonImageIDCommand,
                    new StoredProcInParam("ID", DbType.Int32, id)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ImageID"].ToString(), 0);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetImageIDsByPersonIDs(int[] personIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonImagesIDCommand,
                    TableNames.PersonBaseTable,
                    new StoredProcInTVPIntegerParam("PersonIDs", personIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetPersonsOfCoverAgreementByContractID(int contractID)
        {
            try
            {
                //JC_MOD_08_2010
                //return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsOfCoverAgreementByContractIDCommand, TableNames.InvoiceAgreementTable,
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsOfCoverAgreementByContractIDCommand, TableNames.PersonBaseTable,
                    new StoredProcInParam("CustomerContractID", DbType.Int32, contractID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetPersonByUserName(string userName)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsByUserNameCommand, TableNames.PersonBaseTable,
                    new StoredProcInParam("UserName", DbType.String, userName));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public int GetPersonIDByUserName(string userName)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetPersonsByUserNameCommand,
                    new StoredProcInParam("UserName", DbType.String, userName)))
                {
                    return (IsEmptyReader(reader)) ? 0 : SIIConvert.ToInteger(reader["ID"].ToString(), 0);
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetPersonsUserNames(int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsUserNamesCommand, SII.HCD.Common.Entities.TableNames.CodeDescriptionTable,
                    new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetPersonByCustomerReservation(int customerReservationID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonByCustomerReservationCommand,
                    TableNames.PersonBaseTable,
                    new StoredProcInParam("CustomerReservationID", DbType.Int32, customerReservationID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonAddressListDTOsByFilters(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID, int profileID,
            CommonEntities.StatusEnum status, int maxRecords)
        {
            try
            {
                string sqlQuery = string.Concat(SQLProvider.SelectPersonAddressListDTOTopNFromCommand, Environment.NewLine, "WHERE (P.[Status]=", ((int)status).ToString(), ") ");

                if (!string.IsNullOrEmpty(firstName))
                    sqlQuery += string.Concat("AND (P.FirstName like '", firstName, "' + '%') ");

                if (!string.IsNullOrEmpty(lastName))
                    sqlQuery += string.Concat("AND (P.LastName like '", lastName, "' + '%') ");

                if (!string.IsNullOrEmpty(lastName2))
                    sqlQuery += string.Concat("AND (P.LastName2 like '", lastName2, "' + '%') ");

                if (identifierTypeID > 0)
                    sqlQuery += string.Concat("AND (PIR.IdentifierTypeID=", identifierTypeID.ToString(), ") ");

                if (!string.IsNullOrEmpty(idNumber))
                    sqlQuery += string.Concat("AND (PIR.IDNumber like '", idNumber, "' + '%') ");

                if (profileID > 0)
                    sqlQuery += string.Concat("AND (C.ProfileID=", profileID.ToString(), ") ");

                if (categoryID > 0)
                    sqlQuery += string.Concat("AND ( ", categoryID.ToString(), " IN (SELECT PCR.CategoryID FROM PersonCatRel PCR WITH(NOLOCK) WHERE (P.[ID]=PCR.PersonID)))");

                return this.Gateway.ExecuteQueryDataSet(sqlQuery, TableNames.PersonAddressListDTOTable, new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        //TODO: Adaptaciones MT a las solicitudes de Luis.
        public DataSet GetPersonsCustomerNotInProcessChart(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID, int profileID,
            int processChartID, int careCenterID, CommonEntities.StatusEnum status, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNCustomerNotInProcessChartCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("LastName2", DbType.String, lastName2),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsHHRRNotInProcessChart(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID, int profileID,
            int processChartID, int careCenterID, CommonEntities.StatusEnum status, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNHHRRNotInProcessChartCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("LastName2", DbType.String, lastName2),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProfileID", DbType.Int32, profileID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, status));

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsNotInProcessChart(string firstName, string lastName, string lastName2, int identifierTypeID, string idNumber, int categoryID,
            int processChartID, int careCenterID, CommonEntities.StatusEnum status, int maxRecords)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsTopNAnyNotInProcessChartCommand, TableNames.PersonAddressListDTOTable,
                    new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords),
                    new StoredProcInParam("Name", DbType.String, firstName),
                    new StoredProcInParam("LastName", DbType.String, lastName),
                    new StoredProcInParam("LastName2", DbType.String, lastName2),
                    new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                    new StoredProcInParam("IdentifierNumber", DbType.String, idNumber),
                    new StoredProcInParam("CategoryID", DbType.Int32, categoryID),
                    new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Status", DbType.Int32, (int)status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionByCategory(string firstName, string lastName, string lastName2, int categoryID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionPersonByCategoryCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("FirstName", DbType.String, (!String.IsNullOrEmpty(firstName)) ? firstName : string.Empty),
                            new StoredProcInParam("LastName", DbType.String, (!String.IsNullOrEmpty(lastName)) ? lastName : string.Empty),
                            new StoredProcInParam("LastName2", DbType.String, (!String.IsNullOrEmpty(lastName2)) ? lastName2 : string.Empty),
                            new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionByCategoryWithLike(string firstName, string lastName, string lastName2, CategoryPersonKeyEnum categoryKey)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionPersonByCategoryWithLikeCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("FirstName", DbType.String, (!String.IsNullOrEmpty(firstName)) ? firstName : string.Empty),
                            new StoredProcInParam("LastName", DbType.String, (!String.IsNullOrEmpty(lastName)) ? lastName : string.Empty),
                            new StoredProcInParam("LastName2", DbType.String, (!String.IsNullOrEmpty(lastName2)) ? string.Concat("%", lastName2) : string.Empty),
                            new StoredProcInParam("CategoryKey", DbType.Int32, categoryKey));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int Exists(string firstName, string lastName, int categoryID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.ExistsPersonByNameAndCategoryCommand,
                            TableNames.PersonTable,
                            new StoredProcInParam("FirstName", DbType.String, (!String.IsNullOrEmpty(firstName)) ? firstName : string.Empty),
                            new StoredProcInParam("LastName", DbType.String, (!String.IsNullOrEmpty(lastName)) ? lastName : string.Empty),
                            new StoredProcInParam("CategoryID", DbType.Int32, categoryID));
                return (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                    ? SIIConvert.ToInteger(result.Tables[TableNames.PersonTable].Rows[0]["ID"].ToString()) : 0;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public int GetPersonIDByIDNumber(int identifierTypeID, string idNumber)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonIDByIDNumberCommand,
                            TableNames.PersonTable,
                            new StoredProcInParam("IdentifierTypeID", DbType.Int32, identifierTypeID),
                            new StoredProcInParam("IDNumber", DbType.String, idNumber));
                return (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                    ? SIIConvert.ToInteger(result.Tables[TableNames.PersonTable].Rows[0]["ID"].ToString()) : 0;

            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return 0;
            }
        }

        public DataSet GetPersonCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonCustomerCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonNOKsByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonNOKsByCustomerCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonContactPersonsByCustomer(int customerID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonContactPersonsByCustomerCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("CustomerID", DbType.Int32, customerID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetProcessChartStepProfilePersons(int processChartID, long processStep)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetProcessChartStepProfilePersonsCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("ProcessChartID", DbType.Int32, processChartID),
                            new StoredProcInParam("ProcessStep", DbType.Int64, processStep)
                            );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetProcessChartStepProfilePersons(int[] processChartIDs, long processStep)
        {
            try
            {
                String whereFilterByProcessChartIDs = String.Empty;
                if ((processChartIDs != null)
                    && (processChartIDs.Length > 0))
                {
                    whereFilterByProcessChartIDs = String.Join(",", Array.ConvertAll(processChartIDs, new Converter<int, string>(m => m.ToString())));
                }

                string finalQuery = String.Format(SQLProvider.GetProcessChartsStepProfilesPersonsCommand,
                                             whereFilterByProcessChartIDs);

                return this.Gateway.ExecuteQueryDataSet(finalQuery,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("ProcessStep", DbType.Int64, processStep)
                            );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsAsReportActorByRoutineActID(int routineActID, int participateAs, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsAsReportActorByRoutineActIDCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("RoutineActID", DbType.Int32, routineActID),
                            new StoredProcInParam("ParticipateAs", DbType.Int32, participateAs),
                            new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsAsReportActorByProcedureActID(int procedureActID, int participateAs, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsAsReportActorByProcedureActIDCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("ProcedureActID", DbType.Int32, procedureActID),
                            new StoredProcInParam("ParticipateAs", DbType.Int32, participateAs),
                            new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsAsReportActorByCustomerOrderRequestID(int customerOrderRequestID, int participateAs, int status)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonsAsReportActorByCustomerOrderRequestIDCommand,
                            SII.HCD.Common.Entities.TableNames.IDDescriptionTable,
                            new StoredProcInParam("CustomerOrderRequestID", DbType.Int32, customerOrderRequestID),
                            new StoredProcInParam("ParticipateAs", DbType.Int32, participateAs),
                            new StoredProcInParam("Status", DbType.Int32, status));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonByCategories(CategoryPersonKeyEnum[] categories)
        {
            try
            {
                String whereFilterByCategoryKeys = String.Empty;
                if ((categories != null)
                    && (categories.Length > 0))
                {
                    whereFilterByCategoryKeys = String.Join(",", Array.ConvertAll(categories, new Converter<CategoryPersonKeyEnum, string>(m => ((int)m).ToString())));
                }

                if (!String.IsNullOrEmpty(whereFilterByCategoryKeys))
                {
                    string finalQuery = String.Format(SQLProvider.GetPersonByCategoriesCommand, whereFilterByCategoryKeys);

                    return this.Gateway.ExecuteQueryDataSet(finalQuery, Common.Entities.TableNames.IDDescriptionTable);
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetIDDescriptionWithMasterID(int requestedPhysicianID)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetIDDescriptionWithMasterIDByPersonIDCommand,
                    "RefPhysician", new StoredProcInParam("PersonID", DbType.Int32, requestedPhysicianID));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsHHRRByCareCenter(int careCenterID)
        {
            try
            {
                string filterString = String.Concat("WHERE (PCC.CareCenterID=@CareCenterID) AND (PCC.StartAccessDate<@Date)",
                    "AND ((PCC.EndAccessDate is null) OR (PCC.EndAccessDate>=@Date)) AND (P.[Status]=@Status)");
                string query = String.Concat(SQLProvider.GetPersonsHHRRCommand, filterString);

                return this.Gateway.ExecuteQueryDataSet(query, TableNames.PersonBaseTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Date", DbType.DateTime, DateTime.Now),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonsHHRRByProfilesCareCenter(int[] profileIDs, int careCenterID)
        {
            try
            {
                string filterString = String.Concat("WHERE ",
                    profileIDs == null || profileIDs.Length == 0 ? String.Empty
                    : String.Concat("(HPR.ProfileID in (", SII.HCD.Misc.StringUtils.BuildIDString(profileIDs), ")) AND "),
                    "(PCC.CareCenterID=@CareCenterID) AND (PCC.StartAccessDate<@Date)",
                    "AND ((PCC.EndAccessDate is null) OR (PCC.EndAccessDate>=@Date)) AND (P.[Status]=@Status)");
                string query = String.Concat(SQLProvider.GetPersonsHHRRCommand, filterString);

                return this.Gateway.ExecuteQueryDataSet(query, TableNames.PersonBaseTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Date", DbType.DateTime, DateTime.Now),
                    new StoredProcInParam("Status", DbType.Int32, (int)CommonEntities.StatusEnum.Active)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersons(PersonSpecification spec, int chNumberInCareCenterID, int maxRecords, string phoneticAddinName = "")
        {
            try
            {
                string sql = GetPersonSQLSearch(spec, chNumberInCareCenterID);

                List<StoredProcParam> myParams = new List<StoredProcParam>();
                myParams.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));

                GetParametersPersonFind(spec, chNumberInCareCenterID, phoneticAddinName, myParams);

                return this.Gateway.ExecuteQueryDataSet(sql, TableNames.PersonLookupDTOTable, 999,
                    myParams.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonCustomerProcessData(PersonSpecification spec, int maxRecords, CommonEntities.StatusEnum ceStatus, int[] excludedProcessIDs, string phoneticAddinName = "")
        {
            try
            {
                string excludedProcessIDString = string.Empty;
                if ((excludedProcessIDs != null) && (excludedProcessIDs.Length > 0))
                {
                    excludedProcessIDString = StringUtils.BuildIDString(excludedProcessIDs);
                    string sql = GetPersonCustomerProcessDataSQLSearch(spec, excludedProcessIDString);

                    List<StoredProcParam> myParams = new List<StoredProcParam>();
                    myParams.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
                    myParams.Add(new StoredProcInParam("CEStatus", DbType.Int32, (int)ceStatus));
                    GetParametersPersonFind(spec, 0, phoneticAddinName, myParams);

                    return this.Gateway.ExecuteQueryDataSet(sql, TableNames.PersonCustomerProcessInfoDTOTable,
                        myParams.ToArray());
                }
                return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonCustomerCategoriesData(PersonSpecification spec, int maxRecords, string phoneticAddinName = "")
        {
            try
            {
                string sql = GetPersonCustomerCategoriesDataSQLSearch(spec);

                List<StoredProcParam> myParams = new List<StoredProcParam>();
                myParams.Add(new StoredProcInParam("MaxRecords", DbType.Int32, maxRecords));
                GetParametersPersonFind(spec, 0, phoneticAddinName, myParams);

                return this.Gateway.ExecuteQueryDataSet(sql, BackOffice.Entities.TableNames.CategoryTable,
                    myParams.ToArray());
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonCustomerCHNumberData(int[] personIDs)
        {
            try
            {
                string sql = GetPersonCustomerCHNumberDataSQLSearch(personIDs);
                return this.Gateway.ExecuteQueryDataSet(sql, Administrative.Entities.TableNames.CustomerRelatedCHNumberTable);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public DataSet GetPersonByCustomerProcess(int[] processChartIDs,
            BackOffice.Entities.BasicProcessStepsEnum step, CommonEntities.StatusEnum status,
            int[] locations, int[] careCenterIDs, int assistanceServiceID,
            DateTime? startDateTime, DateTime? endDateTime)
        {
            try
            {
                string whereProcessChartIDs = string.Empty;
                string whereLocationIDs = string.Empty;
                if (processChartIDs != null && processChartIDs.Length > 0)
                {
                    whereProcessChartIDs = StringUtils.BuildIDString(processChartIDs);
                }
                if (locations != null && locations.Length > 0)
                {
                    whereLocationIDs = StringUtils.BuildIDString(locations);
                }


                string finalQuery = SQLProvider.GetPersonByCustomerProcessCommand;

                string includes = string.Empty;
                string wheres = string.Empty;
                if ((processChartIDs != null && processChartIDs.Length > 0) ||
                    (locations != null && locations.Length > 0) ||
                    step != BasicProcessStepsEnum.None ||
                    status != CommonEntities.StatusEnum.None ||
                    (careCenterIDs != null && careCenterIDs.Length > 0) || assistanceServiceID > 0 ||
                    startDateTime != null || endDateTime != null)
                {
                    bool andPossible = false;
                    wheres = string.Concat(wheres, Environment.NewLine, "WHERE ");
                    if (step != BasicProcessStepsEnum.None)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerProcessStepsRel CPSR WITH(NOLOCK) ON CP.[ID] = CPSR.CustomerProcessID ");

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            "CPSR.Step = ", ((long)step).ToString(), " AND CPSR.CurrentStepID > 0 ");
                        andPossible = true;
                        if (status != CommonEntities.StatusEnum.None)
                            wheres = string.Concat(wheres, " AND CPSR.StepStatus = ", ((int)status).ToString(), " ");
                    }
                    if (processChartIDs != null && processChartIDs.Length > 0)
                    {
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " PC.[ID] IN (", whereProcessChartIDs, ") ");
                        andPossible = true;
                    }
                    if (locations != null && locations.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerAdmission CA WITH(NOLOCK) ON CE.CustomerAdmissionID = CA.ID ");
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CA.CurrentLocationID IN (", whereLocationIDs, ") ");
                        andPossible = true;
                    }
                    if (assistanceServiceID > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        if (locations == null || locations.Length <= 0)
                            includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                        includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisodeServiceRel CESR WITH(NOLOCK) ON CE.ID = CESR.CustomerEpisodeID ");
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CESR.AssistanceServiceID = ", assistanceServiceID.ToString(), " ");
                        andPossible = true;
                    }
                    if (careCenterIDs != null && careCenterIDs.Length > 0)
                    {
                        /// primero analizo si tengo que poner mas joins
                        includes = string.Concat(includes, Environment.NewLine,
                                    "JOIN CareCenter CC WITH(NOLOCK) ON CP.CareCenterID = CC.[ID] ", Environment.NewLine,
                                    "JOIN Organization OCC WITH(NOLOCK) ON CC.OrganizationID = OCC.[ID] ");

                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CP.CareCenterID IN (", StringUtils.BuildIDString(careCenterIDs), ") ");
                        andPossible = true;
                    }
                    if (startDateTime != null)
                    {
                        if (step == BasicProcessStepsEnum.None)
                        {
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.CloseDateTime IS NULL OR CP.CloseDateTime >= @StartDateTime) ");
                            andPossible = true;
                        }
                        else
                        {
                            if (step == BasicProcessStepsEnum.Admission || step == BasicProcessStepsEnum.Reception)
                            {
                                if ((locations == null || locations.Length <= 0) && assistanceServiceID <= 0)
                                    includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CE.EndDateTime IS NULL OR CE.EndDateTime >= @StartDateTime) ");
                            }
                            else
                            {
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime >= @StartDateTime) ");
                            }
                            andPossible = true;
                        }
                    }
                    if (endDateTime != null)
                    {
                        if (step == BasicProcessStepsEnum.None)
                        {
                            wheres = string.Concat(wheres, Environment.NewLine,
                                (andPossible) ? " AND " : string.Empty,
                                " (CP.RegistrationDateTime <= @EndDateTime) ");
                            andPossible = true;
                        }
                        else
                        {
                            if (step == BasicProcessStepsEnum.Admission || step == BasicProcessStepsEnum.Reception)
                            {
                                if ((locations == null || locations.Length <= 0) && assistanceServiceID <= 0 && startDateTime == null)
                                    includes = string.Concat(includes, Environment.NewLine, "JOIN CustomerEpisode CE WITH(NOLOCK) ON CP.CustomerEpisodeID = CE.ID ");
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CE.StartDateTime IS NULL OR CE.StartDateTime <= @EndDateTime) ");
                            }
                            else
                            {
                                wheres = string.Concat(wheres, Environment.NewLine,
                                    (andPossible) ? " AND " : string.Empty,
                                    " (CPSR.StepDateTime IS NULL OR CPSR.StepDateTime <= @EndDateTime) ");
                            }
                            andPossible = true;
                        }
                    }
                    if (status != CommonEntities.StatusEnum.None && step == BasicProcessStepsEnum.None)
                    {
                        wheres = string.Concat(wheres, Environment.NewLine,
                            (andPossible) ? " AND " : string.Empty,
                            " CP.Status = ", ((int)status).ToString(), " ");
                        andPossible = true;
                    }
                }
                finalQuery = string.Concat(finalQuery, includes, wheres);
                return this.Gateway.ExecuteQueryDataSet(finalQuery, SII.HCD.BackOffice.Entities.TableNames.PersonBaseTable,
                    new StoredProcInParam("StartDateTime", DbType.DateTime, startDateTime),
                    new StoredProcInParam("EndDateTime", DbType.DateTime, endDateTime)
                    );
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        public int GetDuplicateGroupIDByPersonID(int personID)
        {
            try
            {
                if (personID <= 0)
                {
                    return 0;
                }

                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetDuplicateGroupIDByPersonIDCommand, TableNames.PersonTable,
                            new StoredProcInParam("PersonID", DbType.Int32, personID));

                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return result.Tables[TableNames.PersonTable].AsEnumerable()
                        .Select(row => (row["DuplicateGroupID"] as int? ?? 0))
                        .FirstOrDefault();
                }
                return 0;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        public DataSet GetPersonsByHHRR(IEnumerable<int> hhrrIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(
                                SQLProvider.GetPersonsByHHRRCommand,
                                BackOffice.Entities.TableNames.PersonTable,
                                new StoredProcInTVPIntegerParam("TVPTable", hhrrIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }
        #endregion

        #region public temporal methods
        //// estos métodos deberán desaparecer cuando se implemente correctamente la cache de cliente

        public void NotifyAllUsersTypeModified(MasterDataTypeEnum typeModified)
        {
            //try
            //{
            //    return this.Gateway.ExecuteQueryNonQuery(SQLProvider.NotifyAllUsersTypeModifiedCommand,
            //        new StoredProcInParam("TypeModified", DbType.Int32, (int)typeModified)
            //        );
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            //}
        }


        public DataSet GetAllUsers()
        {
            //try
            //{
            //    return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllUsersCommand, TableNames.PersonListDTOTable);
            //}
            //catch (Exception ex)
            //{
            //    if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
            //    else 
            return null;
            //}
        }

        #endregion

        public DataSet GetPersonsByCareCenter(int careCenterID, int[] profileIDs)
        {
            if (careCenterID <= 0) return null;

            try
            {
                string whereProfileIDs = string.Empty;
                if (profileIDs != null && profileIDs.Length > 0)
                {
                    whereProfileIDs = StringUtils.BuildIDString(profileIDs);
                }

                string selectSQLQuery = SQLProvider.CommonSELECTHHRRPersonsCommand;
                string fromSQLQuery = SQLProvider.CommonFROMHHRRPersonsCommand;
                string whereSQLQuery = SQLProvider.CommonWHEREHHRRPersonsCommand;

                if (careCenterID > 0)
                {
                    fromSQLQuery += string.Concat(Environment.NewLine, "JOIN PersonCareCenterAccess PCC ON PER.[ID]=PCC.PersonID");

                    whereSQLQuery += string.Concat(Environment.NewLine, "AND (PCC.CareCenterID=@CareCenterID) AND (PCC.StartAccessDate<@Date)",
                       Environment.NewLine, "AND ((PCC.EndAccessDate is null) OR (PCC.EndAccessDate>=@Date))");
                }
                if ((profileIDs != null)
                    && (profileIDs.Length > 0))
                {
                    fromSQLQuery += string.Concat(Environment.NewLine, "LEFT JOIN HHRRProfileRel HPR ON HPR.HumanResourceID = HR.[ID]",
                                                    Environment.NewLine, "LEFT JOIN [Profile] PF ON HPR.ProfileID = PF.[ID]");

                    whereSQLQuery += string.Concat(Environment.NewLine, "AND (PF.[ID] IN (", StringUtils.BuildIDString(profileIDs), "))");
                }

                string finalSQLQuery = string.Concat(selectSQLQuery,
                    Environment.NewLine, fromSQLQuery,
                    Environment.NewLine, whereSQLQuery);


                return this.Gateway.ExecuteQueryDataSet(finalSQLQuery, TableNames.PersonTable,
                    new StoredProcInParam("CareCenterID", DbType.Int32, careCenterID),
                    new StoredProcInParam("Date", DbType.DateTime, DateTime.Now));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                else return null;
            }
        }

        ////  esto tiene que ir al DA de DuplicateGroupDA
        public int[] GetDuplicatePersonIDsByDuplicateGroup(int duplicateGroupID)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetDuplicatePersonIDsByDuplicateGroupCommand, TableNames.PersonTable,
                           new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID));
                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return result.Tables[TableNames.PersonTable].AsEnumerable()
                        .Select(row => (row["ID"] as int? ?? 0))
                        .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        ////  esto tiene que ir al DA de DuplicateGroupDA
        public int GetResultPersonIDByDuplicateGroup(int duplicateGroupID)
        {
            try
            {
                using (IDataReader reader = this.Gateway.ExecuteQueryReader(SQLProvider.GetResultPersonIDByDuplicateGroupCommand,
                    new StoredProcInParam("DuplicateGroupID", DbType.Int32, duplicateGroupID)))
                {
                    return (IsEmptyReader(reader)) ? 0 : reader["ResultPersonID"] as int? ?? 0;
                }
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return 0;
            }
        }

        ////  esto tiene que ir al DA de DuplicateGroupDA
        public int[] GetAllPersonIDsToUpdate(int[] personIDs)
        {
            try
            {
                DataSet result = this.Gateway.ExecuteQueryDataSet(SQLProvider.GetAllPersonIDsToUpdateCommand, TableNames.PersonTable,
                           new StoredProcInTVPIntegerParam("TVPTable", personIDs));
                if (result.Tables[TableNames.PersonTable].Rows.Count > 0)
                {
                    return result.Tables[TableNames.PersonTable].AsEnumerable()
                        .Select(row => (row["ID"] as int? ?? 0))
                        .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }




        public Tuple<int, int>[] GetFromDuplicatesPersonByTableName(int[] oldpersonIDs, string tableName)
        {
            try
            {
                if (oldpersonIDs == null || oldpersonIDs.Length <= 0 || string.IsNullOrEmpty(tableName)) return null;
                string sql = string.Concat(
                    "SELECT DISTINCT TVP.[ID], R.[ID] ResultID ", Environment.NewLine,
                    "FROM ", tableName, " R WITH(NOLOCK)", Environment.NewLine,
                    "JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]");

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.BackOffice.Entities.TableNames.PersonTable,
                                        new StoredProcInTVPIntegerParam("TVPTable", oldpersonIDs));
                if (result.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].AsEnumerable()
                            .Select(r => new Tuple<int, int>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public Tuple<int, int, string, string>[] GetFromDuplicatesPersonByReplacementTables(int[] oldpersonIDs, int aaCoverElementID)
        {
            try
            {
                if (oldpersonIDs == null || oldpersonIDs.Length <= 0) return null;
                string sql = string.Concat(

                    "SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'AppointmentContactPersonRel' TableName, 'ContactPersonID' ColumnName FROM AppointmentContactPersonRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.ContactPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ContractCoverAgreement' TableName, 'PersonID' ColumnName FROM ContractCoverAgreement R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerBudget' TableName, 'PersonID' ColumnName  FROM CustomerBudget R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerContact' TableName, 'ContactPersonID' ColumnName  FROM CustomerContact R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.ContactPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerContact' TableName, 'PersonID' ColumnName  FROM CustomerContact R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerContactPerson' TableName, 'PersonID' ColumnName  FROM CustomerContactPerson R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerContract' TableName, 'PersonID' ColumnName  FROM CustomerContract R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerDeliveryResults' TableName, 'PersonID' ColumnName  FROM CustomerDeliveryResults R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerEpisodeGuarantor' TableName, 'PersonID' ColumnName  FROM CustomerEpisodeGuarantor R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerEpisodeNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerEpisodeNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerEpisodeReferencedPhysicianRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerEpisodeReferencedPhysicianRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerMedEpisodeActNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerMedEpisodeActNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerObservationNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerObservationNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerOrderRealizationNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerOrderRealizationNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerOrderRealizationObsNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerOrderRealizationObsNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerOrderRequest' TableName, 'RequestedPersonID' ColumnName  FROM CustomerOrderRequest R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.RequestedPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerOrderRequestNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM CustomerOrderRequestNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerPreAssessment' TableName, 'PersonID' ColumnName  FROM CustomerPreAssessment R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerProcess' TableName, 'PersonID' ColumnName  FROM CustomerProcess R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerTemporalLeave' TableName, 'PersonID' ColumnName  FROM CustomerTemporalLeave R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'CustomerVisit' TableName, 'PersonID' ColumnName  FROM CustomerVisit R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'IncidenceActorRel' TableName, 'PersonID' ColumnName  FROM IncidenceActorRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'IncidenceNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM IncidenceNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'MedicalEpisodeNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM MedicalEpisodeNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'Notification' TableName, 'NotifyToPersonID' ColumnName  FROM Notification R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'NotificationAct' TableName, 'NotifyToPersonID' ColumnName  FROM NotificationAct R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'OrderRequestHumanResourceRel' TableName, 'PersonID' ColumnName  FROM OrderRequestHumanResourceRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PersonAvailability' TableName, 'PersonID' ColumnName  FROM PersonAvailability R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PersonAvailPattern' TableName, 'PersonID' ColumnName  FROM PersonAvailPattern R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PersonCareCenterAccess' TableName, 'PersonID' ColumnName  FROM PersonCareCenterAccess  R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PersonSecurityRel' TableName, 'PersonID' ColumnName  FROM PersonSecurityRel  R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PersonSummaryAvailability' TableName, 'PersonID' ColumnName  FROM PersonSummaryAvailability  R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.PersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'PrescriptionRequest' TableName, 'RequestedPersonID' ColumnName  FROM PrescriptionRequest  R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.RequestedPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ProcedureActNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM ProcedureActNotificationRel  R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ProcedureActObsNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM ProcedureActObsNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ProtocolActNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM ProtocolActNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ProtocolActObsNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM ProtocolActObsNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'RoutineActNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM RoutineActNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'RoutineActObsNotificationRel' TableName, 'NotifyToPersonID' ColumnName  FROM RoutineActObsNotificationRel R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.NotifyToPersonID=TVP.[ID]", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'DeliveryNote' TableName, 'GuarantorID' ColumnName  FROM DeliveryNote R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.GuarantorID=TVP.[ID] AND R.CoverElementID = @AACoverElementID", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'Invoice' TableName, 'GuarantorID' ColumnName  FROM Invoice R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.GuarantorID=TVP.[ID] AND R.GuarantorType  IN (3,4)", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'ManualDeliveryNoteAvail' TableName, 'GuarantorID' ColumnName  FROM ManualDeliveryNoteAvail R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.GuarantorID=TVP.[ID] AND R.GuarantorType  IN (3,4)", Environment.NewLine,
                    "UNION SELECT DISTINCT TVP.[ID],R.[ID] ResultID, 'RemittanceContent' TableName, 'GuarantorID' ColumnName  FROM RemittanceContent R  WITH(NOLOCK) JOIN @TVPTable TVP ON R.GuarantorID=TVP.[ID] AND R.GuarantorType  IN (3,4)"
                    );

                DataSet result = this.Gateway.ExecuteQueryDataSet(sql, SII.HCD.BackOffice.Entities.TableNames.PersonTable,
                                        new StoredProcInParam("AACoverElementID", DbType.Int32, aaCoverElementID),
                                        new StoredProcInTVPIntegerParam("TVPTable", oldpersonIDs));
                if (result.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].Rows.Count > 0)
                {
                    return result.Tables[SII.HCD.BackOffice.Entities.TableNames.PersonTable].AsEnumerable()
                            .Select(r => new Tuple<int, int, string, string>(r["ID"] as int? ?? 0, r["ResultID"] as int? ?? 0, r["TableName"] as string, r["ColumnName"] as string))
                            .ToArray();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }
        }

        public DataSet GetPersonByCustomerIDs(int[] customerIDs)
        {
            try
            {
                return this.Gateway.ExecuteQueryDataSet(SQLProvider.GetPersonByCustomerIDsCommand, TableNames.PersonTable,
                    new StoredProcInTVPIntegerParam("TVPTable", customerIDs));
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, ExceptionPolicies.DataAccess)) throw;
                return null;
            }

        }
    }
}
