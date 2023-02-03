USE ISWildberries;

SELECT 
       ph.product_id,
       ph.price, 
       ph.price_date
  FROM price_history ph
 WHERE ph.product_id = @product_id
   AND ph.price_date = @price_date
	   
;