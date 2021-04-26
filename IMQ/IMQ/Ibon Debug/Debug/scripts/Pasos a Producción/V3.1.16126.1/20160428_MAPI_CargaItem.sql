DECLARE @TherapeuticGroup varchar(200);
DECLARE @itemid int
DECLARE ITem_TH CURSOR
FOR SELECT TherapeuticGroup,ItemID from DrugInfo where TherapeuticGroup!=''
FOR READ ONLY;
OPEN ITem_TH
FETCH NEXT FROM ITem_TH 
INTO @TherapeuticGroup, @itemid
WHILE @@FETCH_STATUS = 0
BEGIN
	--Medicamento
	Update Item set TherapeuticGroup=@TherapeuticGroup where ID = @itemid
	--Unidosis
	Update Item set TherapeuticGroup=@TherapeuticGroup where ID in ( select ChildItemID from DrugUnidosisRelationship d where d.ParentItemID=@itemid)
	FETCH NEXT FROM ITem_TH 
	INTO @TherapeuticGroup, @itemid
END
CLOSE ITem_TH;
DEALLOCATE ITem_TH;