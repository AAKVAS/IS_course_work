USE ISWildberries;

UPDATE deferred_products 
   SET 
       user_id    = @user_id,
	   product_id = @product_id
 WHERE id = @id;