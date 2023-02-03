USE ISWildberries;

INSERT INTO orders (
       user_id,
       product_id,
	   product_count,
	   pick_up_point_id,
       created_at,
	   estimated_delivery_at
)
VALUES (
       @user_id,
       @product_id,
	   @product_count,
	   @pick_up_point_id,
       @created_at,
	   @estimated_delivery_at
)
;