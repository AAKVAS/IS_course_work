USE ISWildberries;

SELECT 
       p.id,
       p.title,
	   p.description,
       p.price,
	   p.category_id,
	   p.supplier_id,
	   p.supplier_percent

  FROM 
       products AS p
 WHERE p.id = 1
;