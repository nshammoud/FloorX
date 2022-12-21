using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KQF.Floor.Web.Data.Migrations
{
    public partial class JFJobHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" 
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetJobHistory]
	@JobNo varchar(64) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
SELECT
 we.[Source No_] AS JobNo
, we.[Item No_] AS ItemNo
, we.[Lot No_] AS LotNo
, CAST(we.Quantity AS DECIMAL(16,0)) AS Qty
, we.[Container No_] AS ContainerNo
, we.[Creation DateTime] AS CreateDateTime
, we.[User ID] AS [User]
, CASE WHEN we.[Source Subtype] = '4' THEN 'Consumption'
       WHEN we.[Source Subtype] = '5' THEN 'Output'
       END AS SrcSubType
FROM JustFood.dbo.[1_KQF_LIVE$Warehouse Entry] AS we WITH (NOLOCK)
INNER JOIN (
       SELECT i.No_ AS ItemNo, i.[Item Category Code] AS ItemCat FROM JustFood.dbo.[1_KQF_LIVE$Item] AS i WITH (NOLOCK)
       ) AS i ON i.[ItemNo] = we.[Item No_]
INNER JOIN (SELECT DISTINCT weicc.[Item Category] AS ItemCat FROM JustFood.dbo.[1_KQF_LIVE$Whse_ Employee IC Cons_] AS weicc WITH (NOLOCK)) AS ic ON ic.ItemCat = i.ItemCat
WHERE we.[Source No_] = @JobNo
and we.[Source Type] = '83' 
ORDER BY we.[Creation DateTime]
END
GO

");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[sp_GetJobHistory]");
        }
    }
}
