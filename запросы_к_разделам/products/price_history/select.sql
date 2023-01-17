USE ISWildberries;

SELECT p.id,
       p.title,
       ph.price, 
       ph.price_date
  FROM price_history ph
	   JOIN products p ON p.id = ph.product_id
;