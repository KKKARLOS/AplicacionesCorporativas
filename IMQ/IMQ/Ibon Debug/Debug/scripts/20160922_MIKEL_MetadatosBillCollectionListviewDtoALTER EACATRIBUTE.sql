BEGIN


	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (681, 'FiltroFechaFacturaDesde', 'Si esta vacio, toma el primero del mes actual',4, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')


	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (681, 'FiltrofechaFacturaHasta', 'true=hasta hoy
	false=hasta ayer',5, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', 'true', '', 2, GetDate(), 'Administrador')

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (681, 'FiltroFechaCobroDesde', 'Si esta vacio, toma el primero del mes actual',4, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', '', '', 2, GetDate(), 'Administrador')

	INSERT INTO [EACAttribute] ([EACElementID], [Name], [Description], [Type], [TypeName], [ComponentType], [DesignRequired],
	[Design], [HasOptions], [HasCodeGenerator], [Required], [Visible], [Enabled], [Length], [Index],
	[ValidationMask], [DisplayMask], [InputMask], [DefaultValue], [CodeGenerator], [Status], [LastUpdated], [ModifiedBy])
	VALUES (681, 'FiltroFechaCobroHasta', 'true=hoy
	false=ayer',5, '', 0, 'false', 'true', 'false', 'false', 'false', 'true', 'true', 0, -1, '', '', '', 'true', '', 2, GetDate(), 'Administrador')
	
END
