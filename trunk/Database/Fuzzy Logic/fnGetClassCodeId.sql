CREATE FUNCTION [dbo].[fnGetClassCodeId]
(
	  @auditTypeId int
	, @state varchar(2)
	, @classCodeNumber varchar(10)
	, @classCodeDescription varchar(max)
)
RETURNS INT
AS
BEGIN
	DECLARE @classCodeId int;
	DECLARE @lookupState varchar(2);

	SELECT @lookupState = [St] FROM dbo.[States] WHERE [St] = @state AND HasClassCodes = 1;

	SELECT TOP 1 @classCodeId = ClassCodeLookUpId
	FROM dbo.ClassCodeLookup
	WHERE ClassCodeNumber = @classCodeNumber
		AND AuditTypeID = @auditTypeId  
		AND ((@lookupState IS NULL AND ClassCodeState IS NULL) OR ClassCodeState = @lookupState)
		AND [dbo].fnCalculateJaroWinkler(@classCodeDescription, ClassCodeDescription) > .8
	ORDER BY CreatedDate;

	RETURN @classCodeId;
END
