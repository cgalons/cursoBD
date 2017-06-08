--CREAMOS UN PROCEDIMIENTO ALMACENADO

ALTER PROCEDURE GET_COCHES_POR_MARCAS
AS
BEGIN

SELECT 
	Marcas.denominacion AS DenominacionMarca
	, matricula, nPlazas FROM Coches
	INNER JOIN Marcas ON Marcas.id = Coches.idMarca
--GROUP BY
--	Marcas.denominacion 
--	,Coches.matricula
--	,Coches.nPlazas
ORDER BY Coches.nPlazas

END 

--EXEC GET_COCHES_POR_MARCAS

--ALTER PROCEDURE GET_COCHES_POR_MARCAS
--AS
--BEGIN 

--	SELECT COUNT (*) FROM Coches
--	SELECT * FROM Coches
---- INSERCIÓN MASIVAS DUPLICA EL CONTENIDO DE LA TABLA
--	INSERT INTO Coches (matricula, idMarca, idTipoCombustible, color, cilindrada
--		,nPlazas, fechaMatriculacion)
--	SELECT matricula, idMarca, idTipoCombustible, color, cilindrada
--		,nPlazas, fechaMatriculacion 
--FROM Coches
  --SELECT * FROM Coches
	--PRINT 'MI PRIMER PROCEDIMIENTO ALMACENADO'

	--SELECT 
--      Marcas.denominacion as denominacionMarca
--    , TiposCombustible.denominacion as denominacionTipoCombustible
--    , Coches.idMarca
--    , Coches.idTipoCombustible
--    , Coches.id, Coches.matricula, Coches.color, Coches.nPlazas
--    , Coches.fechaMatriculacion, Coches.cilindrada
--FROM Marcas
--    INNER JOIN Coches on Marcas.id = Coches.idMarca
--    INNER JOIN TiposCombustible on Coches.idTipoCombustible = TiposCombustible.id
--GROUP BY 
--      Marcas.denominacion
--    , TiposCombustible.denominacion
--    , Coches.idMarca
--    , Coches.idTipoCombustible
--    , Coches.id, Coches.matricula, Coches.color, Coches.nPlazas
--    , Coches.fechaMatriculacion, Coches.cilindrada
--ORDER BY Marcas.denominacion


--END