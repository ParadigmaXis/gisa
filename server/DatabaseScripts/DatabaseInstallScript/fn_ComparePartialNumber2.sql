/****** Object:  UserDefinedFunction [dbo].[fn_ComparePartialNumber2]    Script Date: 04/27/2009 15:48:21 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/****************************************
 * Compara dois numeros (potencialmente) parciais. Um numero parcial pode ser vazio (string vazia ou NULL) e pode 
 * conter pontos de interrrogacao em representacao de caracteres desconhecidos. 
 * Exemplos de numeros comparaveis com esta funcao: '1?', '??', '14', '', NULL
 * Retorno:
 *  0: numeros iguais
 * -1: primeiro valor é menor que o segundo
 *  1: primeiro valor é maior que o segundo
 * NULL: sao ambos igualmente incertos
 * -2: o primeiro valor é o mais incerto
 *  2: o segundo valor é o mais incerto
 */

CREATE FUNCTION [dbo].[fn_ComparePartialNumber2] (@NumA varchar(100), @NumB varchar(100)) RETURNS Integer AS  
BEGIN

	DECLARE @maxLen INTEGER
	DECLARE @paddingChar NCHAR
	SET @maxLen = CASE WHEN LEN(@NumA) > LEN(@NumB) THEN LEN(@NumA) ELSE LEN(@NumB) END
	
	IF LEN(@NumA) = 0 SET @paddingChar = '?'
	ELSE SET @paddingChar = '0' 
	
	WHILE (LEN(@NumA)<@maxLen)
	BEGIN
	SET @NumA = @paddingChar + @NumA
	END
	
	IF LEN(@NumB) = 0 SET @paddingChar = '?'
	ELSE SET @paddingChar = '0'
	
	WHILE (LEN(@NumB)<@maxLen)
	BEGIN
	SET @NumB = @paddingChar + @NumB
	END
	
	DECLARE @A varchar(100), @B varchar(100)
	SET @A = @NumA
	SET @B = @NumB

	DECLARE @clip INTEGER
	SET @clip = 0

	DECLARE @isNumA INTEGER
	DECLARE @isNumB INTEGER
	SET @isNumA = ISNUMERIC(@A)
	SET @isNumB = ISNUMERIC(@B)

	DECLARE @lenA INTEGER
	DECLARE @lenB INTEGER
	SET @lenA = LEN(@A)
	SET @lenB = LEN(@B)

	-- existem caracteres em ambos os valores mas pelo um deles ainda contem caracteres nao numericos
	WHILE (@lenA>0 AND @lenB>0 AND (@isNumA=0 OR @isNumB=0))
	BEGIN
		-- O ultimo a deixar de ser numerico é o mais incompleto
		IF @isNumA = 0 AND @isNumB = 0
			SET @clip = NULL
		ELSE IF @isNumA = 0 AND @isNumB = 1
			SET @clip = -1
		ELSE IF @isNumA = 1 AND @isNumB = 0
			SET @clip = 1
		
		SET @A = LEFT(@A, @lenA - 1)
		SET @B = LEFT(@B, @lenB - 1)
		SET @isNumA = ISNUMERIC(@A)
		SET @isNumB = ISNUMERIC(@B)
		SET @lenA = LEN(@A)
		SET @lenB = LEN(@B)
	END

	-- existem caracteres mas ja ambos sao valores numericos
	IF (@lenA>0 AND @lenB>0)
	BEGIN
		DECLARE @Ai INTEGER
		DECLARE @Bi INTEGER
		SET @Ai = CONVERT(INTEGER, @A)
		SET @Bi = CONVERT(INTEGER, @B)

		-- @clip é 0 quando ambos os valores forem completos à partida e é NULL quando forem completos após remover os caracteres do fim
		IF @clip IS NULL OR @clip = 0
		BEGIN
			IF (@Ai < @Bi) RETURN -1
			IF (@Ai > @Bi) RETURN 1
			RETURN @clip
		END
		ELSE IF ABS(@clip) = 1 -- datas incompletas
		BEGIN
			IF (@Ai < @Bi) RETURN -1 -- consegue-se determinar que Ai é menor que Bi, apesar de se tratarem de datas incompletas 
			IF (@Ai > @Bi) RETURN 1 -- consegue-se determinar que Ai é maior que Bi, apesar de se tratarem de datas incompletas 
			RETURN 2 * @clip -- se @Ai = @Bi, tratam-se de datas equivalentes mas incompletas. devolve-se como menor a data mais incompleta
		END
	END
	ELSE IF @lenA=0 OR @lenB=0 -- foram esgotados os caracteres
		BEGIN
			-- se num dos valores sobrou um caracter numerico, é esse o menos vago
			IF ISNUMERIC(LEFT(@A, 1)) = 1 RETURN 2
			IF ISNUMERIC(LEFT(@B, 1)) = 1 RETURN -2
		END

	RETURN NULL
END

GO

