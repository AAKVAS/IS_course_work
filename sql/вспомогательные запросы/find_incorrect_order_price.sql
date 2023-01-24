
SELECT 
       o.product_id,
	   o.product_count,
	   o.price,
	   o.created_at,
	   (SELECT TOP 1 
			   ph.price
		  FROM price_history ph
		  WHERE o.product_id = ph.product_id
		    AND o.created_at > ph.price_date
		  ORDER BY price_date DESC)
  FROM orders o
;

SELECT o.price,
       (SELECT 
	   o.product_count *
	   (SELECT TOP 1 
			   ph.price
		  FROM price_history ph
		  WHERE o.product_id = ph.product_id
		    AND o.created_at > ph.price_date
		  ORDER BY price_date DESC))
  FROM orders o
  WHERE o.price != 
  (SELECT 
	   o.product_count *
	   (SELECT TOP 1 
			   ph.price
		  FROM price_history ph
		  WHERE o.product_id = ph.product_id
		    AND o.created_at > ph.price_date
		  ORDER BY price_date DESC))
;