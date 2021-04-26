IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerProcedure]') AND name = N'IX_CustomerProcedure_AssistancePlanID')
CREATE NONCLUSTERED INDEX [IX_CustomerProcedure_AssistancePlanID]
ON [dbo].[CustomerProcedure] 
(
	[AssistancePlanID]
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerLeave]') AND name = N'IX_CustomerLeave_EpisodeID')
CREATE NONCLUSTERED INDEX [IX_CustomerLeave_EpisodeID] ON [dbo].[CustomerLeave] 
(
	[EpisodeID]
)
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAccountCharge]') AND name = N'IX_CustomerAccountCharge_TargetEpisodeID')
CREATE NONCLUSTERED INDEX [IX_CustomerAccountCharge_TargetEpisodeID] ON [dbo].[CustomerAccountCharge] 
(
	[TargetEpisodeID]
)
GO