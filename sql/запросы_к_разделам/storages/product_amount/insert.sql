USE ISWildberries;

INSERT INTO products_on_storages (
	   storage_id,
	   product_id,
	   product_amount
)
VALUES (
       @storage_id,
	   @product_id,
	   @product_amount
)


