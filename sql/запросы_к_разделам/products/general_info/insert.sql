USE ISWildberries;

INSERT INTO products (
       title,
	   price,
	   supplier_id,
	   category_id,
	   description,
	   supplier_percent
)
VALUES (
       @title,
	   @price,
	   @supplier_id,
	   @category_id,
	   @description,
	   @supplier_percent
)