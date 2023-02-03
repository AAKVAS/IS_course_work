USE ISWildberries;

SELECT o.id as order_id,
       o.user_id,
       o.product_id,
	   o.product_count,
       o.price,
	   o.pick_up_point_id,
       o.created_at,
	   o.estimated_delivery_at
  FROM orders o
 WHERE o.id = @id
;

