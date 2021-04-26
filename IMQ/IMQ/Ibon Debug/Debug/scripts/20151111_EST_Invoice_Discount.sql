ALTER TABLE [dbo].[Invoice]
    ADD [DiscountValue]       MONEY          NULL,
        [DiscountType]        SMALLINT       NULL,
        [DiscountDescription] NVARCHAR (256) NULL;