USE ISWildberries;

UPDATE orders 
   SET
       user_id               = @user_id,
       product_id            = @product_id,
	   product_count         = @product_count,
	   pick_up_point_id      = @pick_up_point_id,
       created_at            = @created_at,
	   estimated_delivery_at = @estimated_delivery_at
 WHERE id = @id
;