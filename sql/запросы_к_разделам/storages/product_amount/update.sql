USE ISWildberries;

UPDATE products_on_storages 
   SET 
	   storage_id     = @storage_id_new,
	   product_id     = @product_id_new,
	   product_amount = @product_amount

 WHERE storage_id = @storage_id_old
   AND product_id = @product_id_old


