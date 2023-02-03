USE ISWildberries;

UPDATE receipt_of_products_to_storages 
   SET storage_id  = @storage_id_new,
       product_id  = @product_id_new,
       received_at = @received_at_new,
	   amount      = @amount
       
 WHERE storage_id  = @storage_id_old
   AND product_id  = @product_id_old
   AND received_at = @received_at_old