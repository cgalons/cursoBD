CREATE PROCEDURE GET_COCHE_POR_MARCA_MATRICULA_PLAZAS
AS
BEGIN
	SELECT 
	M.denominacion AS Marca
	,C.matricula
	,C.nPlazas
	FROM Marcas M
		INNER JOIN Coches C ON M.id = C.idMarca
	GROUP BY
	M.denominacion
	,C.matricula
	,C.nPlazas
	ORDER BY nPlazas
END

--CREATE PROCEDURE GET_COCHE_POR_MARCA_MATRICULA_PLAZAS_2

ALTER PROCEDURE GET_COCHE_POR_MARCA_MATRICULA_PLAZAS_2
-- ANTES DE LA PALABRA "AS" SE LE PASAN LOS PARÁMETROS A UN PROCEDIMIENTO ALMACENADO
-- DELANTE LLEVA UNA ARROBA Y DETRÁS DEL NOMBRE DEL DATO SE LE PONE EL TIPO
	  @marca nvarchar (50) = NULL
	, @nPlazas smallint = NULL
AS
BEGIN
	SELECT 
	M.denominacion AS Marca
	,C.matricula
	,C.nPlazas
	FROM Marcas M
		INNER JOIN Coches C ON M.id = C.idMarca
	WHERE 
		 M.denominacion LIKE '%' + @marca + '%' OR @marca is null
		AND (C.nPlazas >= @nPlazas OR @nPlazas is null) 
	--GROUP BY
	--M.denominacion
	--,C.matricula
	--,C.nPlazas
	ORDER BY nPlazas
END