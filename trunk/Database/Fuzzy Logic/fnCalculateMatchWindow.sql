CREATE FUNCTION [dbo].[fnCalculateMatchWindow](@s1_len INT, @s2_len INT) 
RETURNS INT AS 
BEGIN
	DECLARE @matchWindow INT
	SET	@matchWindow =	CASE	WHEN @s1_len >= @s2_len
								THEN (@s1_len / 2) - 1
								ELSE (@s2_len / 2) - 1
						END
	RETURN @matchWindow
END
GO
