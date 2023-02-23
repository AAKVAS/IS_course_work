USE ISWildberries;


DELETE FROM receipt_of_products_to_storages 
 WHERE storage_id  = @storage_id
   AND product_id  = @product_id
   AND received_at = @received_at