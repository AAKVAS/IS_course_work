USE ISWildberries;

SELECT 
	   ps.storage_id,
	   ps.product_id,
	   ps.product_amount

  FROM products_on_storages ps 
 WHERE ps.storage_id = @storage_id
   AND ps.product_id = @product_id

;