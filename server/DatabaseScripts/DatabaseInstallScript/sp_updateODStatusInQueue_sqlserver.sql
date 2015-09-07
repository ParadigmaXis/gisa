/****** Object:  StoredProcedure [dbo].[sp_updateODStatusInQueue]    Script Date: 03/07/2014 13:18:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_updateODStatusInQueue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_updateODStatusInQueue]
GO

/****** Object:  StoredProcedure [dbo].[sp_updateODStatusInQueue]    Script Date: 03/14/2014 17:28:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_updateODStatusInQueue] (@pid nvarchar(20), @quality nvarchar(20), @state nvarchar(20), @stateDescription nvarchar(max)) AS
BEGIN

	SET NOCOUNT ON

	UPDATE ObjetoDigitalStatus SET state = @state, stateDescription = @stateDescription, date = CURRENT_TIMESTAMP WHERE pid = @pid AND quality = @quality
	
	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO ObjetoDigitalStatus VALUES (@pid, @quality, @state, @stateDescription, CURRENT_TIMESTAMP)
	END	
END