CREATE FUNCTION [dbo].[fnCalculateJaro](@str1 VARCHAR(MAX), @str2 VARCHAR(MAX)) 
RETURNS FLOAT AS 
BEGIN
	DECLARE	@Common1				VARCHAR(MAX)
	DECLARE	@Common2				VARCHAR(MAX)
	DECLARE @Common1_Len			INT
	DECLARE	@Common2_Len			INT
	DECLARE @s1_len					INT  
	DECLARE @s2_len					INT 
	DECLARE	@transpose_cnt			INT
	DECLARE @match_window			INT
	DECLARE @jaro_distance			FLOAT
	SET		@transpose_cnt			= 0
	SET		@match_window			= 0
	SET		@jaro_distance			= 0
	Set @s1_len = LEN(@str1)
	Set @s2_len = LEN(@str2)
	SET	@match_window = dbo.fnCalculateMatchWindow(@s1_len, @s2_len)
	SET	@Common1 = dbo.fnGetCommonCharacters(@str1, @str2, @match_window)
	SET @Common1_Len = LEN(@Common1)
	IF @Common1_Len = 0 OR @Common1 IS NULL
	BEGIN
		RETURN 0		
	END
	SET @Common2 = dbo.fnGetCommonCharacters(@str2, @str1, @match_window)
	SET @Common2_Len = LEN(@Common2)
	IF @Common1_Len <> @Common2_Len OR @Common2 IS NULL
	BEGIN
		RETURN 0
	END

	SET	@transpose_cnt = dbo.[fnCalculateTranspositions](@Common1_Len, @Common1, @Common2)
	SET	@jaro_distance =	@Common1_Len / (3.0 * @s1_len) + 
							@Common1_Len / (3.0 * @s2_len) +
							(@Common1_Len - @transpose_cnt) / (3.0 * @Common1_Len);

	RETURN @jaro_distance
END