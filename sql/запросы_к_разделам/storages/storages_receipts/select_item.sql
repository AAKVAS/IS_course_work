USE ISWildberries;

SELECT  
	   rps.storage_id, 
	   rps.product_id,
	   rps.amount,
       rps.received_at  
  FROM 
       receipt_of_products_to_storages rps
 WHERE rps.storage_id = @id