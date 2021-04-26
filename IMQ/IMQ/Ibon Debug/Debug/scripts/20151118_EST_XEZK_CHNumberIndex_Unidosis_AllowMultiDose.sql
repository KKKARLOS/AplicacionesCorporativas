GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

GO
PRINT N'Quitando [dbo].[CustomerRelatedCHNumber].[IX_CustomerRelatedCHNumber_CareCenterID]...';

GO
DROP INDEX [IX_CustomerRelatedCHNumber_CareCenterID]
    ON [dbo].[CustomerRelatedCHNumber];

GO
PRINT N'Quitando [dbo].[CustomerRelatedCHNumber].[IX_CustomerRelatedCHNumber_CustomerID_CareCenterID]...';
GO
DROP INDEX [IX_CustomerRelatedCHNumber_CustomerID_CareCenterID]
    ON [dbo].[CustomerRelatedCHNumber];
GO


PRINT N'Modificando [dbo].[UnidosisInfo]...';
GO
ALTER TABLE [dbo].[UnidosisInfo]
    ADD [AllowMultiDose] BIT DEFAULT ((0)) NOT NULL;

GO
PRINT N'Creando [dbo].[CustomerRelatedCHNumber].[II_CHNumber]...';
GO
CREATE NONCLUSTERED INDEX [II_CHNumber]
    ON [dbo].[CustomerRelatedCHNumber]([CHNumber] ASC);

GO
PRINT N'Actualización completada.';


GO
