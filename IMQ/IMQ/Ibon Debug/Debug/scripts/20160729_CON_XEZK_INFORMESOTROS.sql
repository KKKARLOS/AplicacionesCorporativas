declare @id int

insert into DocumentTemplate (DocumentTemplateTypeID,	DocumentContentName,	TransformContentName,	Description,	IsTemporalDocument,	TemplateNumberMask,	Status,	LastUpdated,	ModifiedBy)
values (6,	'ObservationTemplate.docx',	'',	'INFORME OTROS',	1,	'DocumentoDeObservacionesPDF',	1,	getdate(),	'Administrador')


insert into Observation (ObservationTypeID,	AssignedCode,	Name,	Description,	IsCodified,	
		PhysicalUnitID,	KindOf,	BasicType,	BasicTypeLength,	BasicTypeDecimalLength,	GraphicColor,	ValidateCriteria,
		RegistrationDateTime,	AncestorID,	InUse,	InUseByCustomer,	IsCalculated,	ValidateOnDemand,	Status,
		LastUpdated, ModifiedBy)
values (2,	'NOC_IDG513',	'Descripción Tipo Informe',	'Descripción Tipo Informe',		0,		
		0,	1,	8,	100,	0,	16777215,	0,	
		getdate(),	0,	1,	1,	0,	0,	3, GETDATE(), 'xezkerra.ibe')

select @id = SCOPE_IDENTITY()
select @id

INSERT INTO [ObservationTemplate]
           ([Name], [Description], [TemplateTitle], [ExportDocumentName], [TemplateEditionPresentation], [TemplateLayout]
           , [TemplateLayoutItems], [TemplateViewResults], [TemplateViewResultItems], [TemplateCopy], [DocumentTemplateID]
           , [RegistrationDateTime], [InUse], [InUseByCustomer], [Status], [ModifiedBy], [LastUpdated])
     VALUES
           ('INFORME OTROS (INDIGO)',	'INFORME OTROS (INDIGO)',	'INFORME OTROS',	'',
		   4,	2,	0,	0,	0,	0,	10,	getdate(),	1,	1,	3,	'Administrador',	getdate())

INSERT INTO [ObservationTemplateRel]
           ([ObservationTemplateID], [ElementType], [ObservationBlockID], [ObservationID], [ElementTitle]
           ,[ElementTitlePosition], [VisibleLabel], [Order], [Required], [Status], [ModifiedBy], [LastUpdated])
     VALUES
           (25, 2,	0,	119,	'Informe',	0,	1,	2,	0,	1, 'Administrador', getdate())
		   
INSERT INTO [ObservationTemplateRel]
           ([ObservationTemplateID], [ElementType], [ObservationBlockID], [ObservationID], [ElementTitle]
           ,[ElementTitlePosition], [VisibleLabel], [Order], [Required], [Status], [ModifiedBy], [LastUpdated])
     VALUES
           (25, 2,	0,	@id,	'DescTipoInforme',	0,	1,	2,	0,	1, 'Administrador', getdate())

INSERT INTO [ObservationTemplateRel]
           ([ObservationTemplateID], [ElementType], [ObservationBlockID], [ObservationID], [ElementTitle]
           ,[ElementTitlePosition], [VisibleLabel], [Order], [Required], [Status], [ModifiedBy], [LastUpdated])
     VALUES
           (25, 1,	21,	0,	'',	0,	1,	1,	0,	1, 'Administrador', getdate())


insert into StepPreprint (ProcessChartID,	StepConfig,	StepConfigID,	SourceType,	DirectionType,	ReferenceID,	Reference,	Type,
					Label,	Copies,	AppointmentElement,	EntityTypeID,	EntityID,	LastUpdated,	ModifiedBy)
values (2,	32768,	2,	16,	2,	25,	'INFORME OTROS (INDIGO)',	16,	'INFORME OTROS',
		1,	0,	0,	0,	getdate(),	'Administrador')

insert into StepPreprint (ProcessChartID,	StepConfig,	StepConfigID,	SourceType,	DirectionType,	ReferenceID,	Reference,	Type,
					Label,	Copies,	AppointmentElement,	EntityTypeID,	EntityID,	LastUpdated,	ModifiedBy)
values (1,	32768,	1,	16,	2,	25,	'INFORME OTROS (INDIGO)',	16,	'INFORME OTROS',
		1,	0,	0,	0,	getdate(),	'Administrador')

		
insert into StepPreprint (ProcessChartID,	StepConfig,	StepConfigID,	SourceType,	DirectionType,	ReferenceID,	Reference,	Type,
					Label,	Copies,	AppointmentElement,	EntityTypeID,	EntityID,	LastUpdated,	ModifiedBy)
values (10,	32768,	9,	16,	2,	25,	'INFORME OTROS (INDIGO)',	16,	'INFORME OTROS',
		1,	0,	0,	0,	getdate(),	'Administrador')

		
insert into StepPreprint (ProcessChartID,	StepConfig,	StepConfigID,	SourceType,	DirectionType,	ReferenceID,	Reference,	Type,
					Label,	Copies,	AppointmentElement,	EntityTypeID,	EntityID,	LastUpdated,	ModifiedBy)
values (18,	32768,	14,	16,	2,	25,	'INFORME OTROS (INDIGO)',	16,	'INFORME OTROS',
		1,	0,	0,	0,	getdate(),	'Administrador')



INSERT INTO [EACAttribute]
			([EACElementID],[Name],
			[Description],
			[Type],[TypeName],[ComponentType],[DesignRequired],
			[Design],[HasOptions],[HasCodeGenerator],[Required],[Visible],[Enabled],[Length],[Index],
			[ValidationMask],[DisplayMask],[InputMask],[DefaultValue],[CodeGenerator],[Status],[LastUpdated],[ModifiedBy])        
			VALUES(588,'InformeOtros',
				'En las opciones si indican los mapeos de observaciones. Descripción Nombre atributo Indigo, Value código de la observacion en HCDIS. En valor predeterminado nombre plantilla.',
				3,'',0,0,
				1,1,0,0,1,1,0,-1,
				'','','','INFORME OTROS (INDIGO)','',2,GETDATE(),'Administrador')
/*
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])  
VALUES (15888, 'IDCENTRODEST', 'NOC_IDG208', '2015-29-06 18:47:29.580', 'Administrador', 2)
*/

INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])  
VALUES (15888,	'TIPOINFORME', 'NOC_IDG500', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'IDPROTOCOLO', 'NOC_IDG502', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'IDTIPOPROTOCOLO', 'NOC_IDG501', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'VERSION', 'NOC_IDG503', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'FECHAMODIFICACION', 'NOC_IDG506', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'NOMBREUSUARIOMODIFICACION', 'NOC_IDG507', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'FECHAINFORME', 'NOC_IDG504', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'NOMBRECODIGOUSUARIO', 'NOC_IDG505', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'FECHAFIRMA', 'NOC_IDG508', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'NOMBREUSUARIOFIRMA', 'NOC_IDG509', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'IDINFORME', 'NOC_IDG510', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'OBSERVACIONES', 'NOC_IDG511', getdate(),	'Administrador', 2)
INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])
VALUES (15888,	'ESTADO', 'NOC_IDG512', getdate(),	'Administrador', 2)

INSERT INTO [EACAttributeOption]  ([EACAttributeID]  ,[Description]  ,[Value]  ,[LastUpdated]  ,[ModifiedBy] ,[Status])  
VALUES (15888,	'DESCTIPOINFORME', 'NOC_IDG513', getdate(),	'Administrador', 2)


