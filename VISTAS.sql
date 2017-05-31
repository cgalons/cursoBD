-- CREAMOS UNA VISTA PARA CONTAR EL Nº DE COCHES POR MARCAS
CREATE VIEW V_N_COCHES_POR_MARCA AS
SELECT M.denominacion as Marca, count (C.id) as nCoches
FROM Marcas M
	LEFT JOIN Coches C ON M.id = C.idMarca
GROUP BY M.denominacion
