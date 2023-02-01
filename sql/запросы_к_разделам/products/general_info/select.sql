USE ISWildberries;

SELECT 
       p.id,
       p.title,
	   p.description,
       p.price,
	   p.category_id,
	   c.title as category,
	   p.supplier_id,
	   s.title as supplier,
	   p.supplier_percent

  FROM 
       products AS p
  JOIN suppliers s       ON s.id = p.supplier_id
  LEFT JOIN categories c ON c.id = p.category_id
;