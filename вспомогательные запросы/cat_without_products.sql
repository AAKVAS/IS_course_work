USE ISWildberries;
  
 WITH p_c(id, p_id) AS (
	SELECT c.id, c.parent_category_id
	  FROM categories c
     INNER JOIN products p ON p.category_id = c.id

	 UNION ALL

	SELECT c.id, c.parent_category_id
	  FROM categories c
	 INNER JOIN p_c ON p_c.p_id = c.id
 )

 SELECT k.id, 
        k.title, 
		k.parent_category_id 
 FROM categories k
 WHERE k.id NOT IN 
 (
	 SELECT DISTINCT 
			c.id
	   FROM categories c
	  INNER JOIN p_c ON p_c.id = c.id
  );


  SELECT * FROM suppliers;

SELECT * FROM suppliers s
LEFT JOIN products p ON p.supplier_id = s.id 
WHERE p.id IS NULL;
  

