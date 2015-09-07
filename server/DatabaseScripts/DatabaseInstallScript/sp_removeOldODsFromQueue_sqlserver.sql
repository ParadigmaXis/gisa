/****** Object:  StoredProcedure [dbo].[sp_removeOldODsFromQueue]    Script Date: 03/07/2014 13:18:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_removeOldODsFromQueue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_removeOldODsFromQueue]
GO

/****** Object:  StoredProcedure [dbo].[sp_removeOldODsFromQueue]    Script Date: 03/14/2014 17:28:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_removeOldODsFromQueue] (@pid nvarchar(20), @quality nvarchar(20), @state nvarchar(20)) AS
BEGIN

	SET NOCOUNT ON
	
	DELETE FROM ObjetoDigitalStatus WHERE LOWER(state) = 'processed' AND DATEDIFF(day,ObjetoDigitalStatus.date,CURRENT_TIMESTAMP) >= 1
END