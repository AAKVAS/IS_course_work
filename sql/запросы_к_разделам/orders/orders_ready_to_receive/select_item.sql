USE ISWildberries;

SELECT 
       o.pick_up_point_id,
       o.id as order_id,
       o.user_id,
	   o.product_id,
	   o.created_at,
       o.price 
  FROM orders o
  JOIN order_history oh ON o.id = oh.order_id
 WHERE oh.is_last_status = 1 
   AND oh.status_id      = 11
   AND order_id          = @id

;