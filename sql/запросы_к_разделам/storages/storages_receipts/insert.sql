USE ISWildberries;

INSERT INTO receipt_of_products_to_storages (
	   storage_id, 
	   product_id,
	   amount,
       received_at  
)
VALUES (
	   @storage_id, 
	   @product_id,
	   @amount,
       @received_at
)