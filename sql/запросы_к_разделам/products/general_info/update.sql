USE ISWildberries;

UPDATE products 
   SET 
       title = @title,
	   price = @price,
	   supplier_id = @supplier_id,
	   category_id = @category_id,
	   description = @description,
	   supplier_percent = @supplier_percent
 WHERE id = @id
;
	   
