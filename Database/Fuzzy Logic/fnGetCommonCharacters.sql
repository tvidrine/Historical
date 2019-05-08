CREATE FUNCTION [dbo].fnGetCommonCharacters(@firstWord VARCHAR(MAX), @secondWord VARCHAR(MAX), @matchWindow INT)
RETURNS VARCHAR(MAX) AS
BEGIN
	DECLARE @CommonChars VARCHAR(MAX)
	DECLARE @copy VARCHAR(MAX)
	DECLARE @char CHAR(1)
	DECLARE @foundIT BIT

	DECLARE @f1_len INT
	DECLARE @f2_len INT
	DECLARE @i INT
	DECLARE @j INT
	DECLARE @j_Max INT

	SET	@CommonChars = ''
    IF @firstWord IS NOT NULL AND @secondWord IS NOT NULL 
    BEGIN
		SET @f1_len = LEN(@firstWord)
		SET @f2_len = LEN(@secondWord)
		SET @copy = @secondWord

		SET @i = 1
		WHILE @i < (@f1_len + 1)
		BEGIN
			SET	@char = SUBSTRING(@firstWord, @i, 1)
			SET @foundIT = 0

			-- Set J starting value
			IF @i - @matchWindow > 1
			BEGIN
				SET @j = @i - @matchWindow
			END
			ELSE
			BEGIN
				SET @j = 1
			END
			-- Set J stopping value
			IF @i + @matchWindow <= @f2_len
			BEGIN
				SET @j_Max = @i + @matchWindow
			END
			ELSE
			IF @f2_len < @i + @matchWindow
			BEGIN
				SET @j_Max = @f2_len
			END

			WHILE @j < (@j_Max + 1) AND @foundIT = 0
			BEGIN
				IF SUBSTRING(@copy, @j, 1) = @char COLLATE SQL_Latin1_General_Cp1_CS_AS
				BEGIN
					SET	@foundIT = 1
					SET	@CommonChars = @CommonChars + @char
					SET @copy = STUFF(@copy, @j, 1, '#')
				END
				SET @j = @j + 1
			END	
			SET @i = @i + 1
		END
    END

	RETURN @CommonChars
END