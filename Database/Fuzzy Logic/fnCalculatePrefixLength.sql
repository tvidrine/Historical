CREATE FUNCTION [dbo].[fnCalculatePrefixLength](@firstWord VARCHAR(MAX), @secondWord VARCHAR(MAX))
RETURNS INT As 
BEGIN
	DECLARE @f1_len INT
	DECLARE @f2_len INT
    DECLARE	@minPrefixTestLength INT
	DECLARE @i INT
	DECLARE @n INT
	DECLARE @foundIT BIT

	SET	@minPrefixTestLength = 4
    IF @firstWord IS NOT NULL AND @secondWord IS NOT NULL 
    BEGIN
		SET @f1_len = LEN(@firstWord)
		SET @f2_len = LEN(@secondWord)
		SET @i = 0
		SET	@foundIT = 0
		SET @n =	CASE	WHEN	@minPrefixTestLength < @f1_len 
									AND @minPrefixTestLength < @f2_len 
							THEN	@minPrefixTestLength
							WHEN	@f1_len < @f2_len 
									AND @f1_len < @minPrefixTestLength 
							THEN	@f1_len
							ELSE	@f2_len
					END
		WHILE @i < @n AND @foundIT = 0
		BEGIN
			IF SUBSTRING(@firstWord, @i+1, 1) <> SUBSTRING(@secondWord, @i+1, 1)
			BEGIN
				SET @minPrefixTestLength = @i
				SET @foundIT = 1
			END
			SET @i = @i + 1
		END
	END
    RETURN @minPrefixTestLength
END