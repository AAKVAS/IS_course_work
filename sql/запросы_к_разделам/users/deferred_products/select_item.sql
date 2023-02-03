USE ISWildberries;

SELECT dp.id,
       dp.user_id,
	   dp.product_id
  FROM deferred_products dp
 WHERE dp.id = @id
;