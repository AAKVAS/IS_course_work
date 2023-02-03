USE ISWildberries;

DELETE FROM products_on_storages 
 WHERE storage_id = @storage_id
   AND product_id = @product_id


