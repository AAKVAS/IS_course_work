USE ISWildberries;


SELECT o.id,
       o.created_at,
	   oh.status_changed_at,
	   DATEDIFF(SECOND, msd.min_status_changed_at, o.created_at) as delta_second,
	   DATEADD(SECOND, DATEDIFF(SECOND, msd.min_status_changed_at, o.created_at), oh.status_changed_at) 
  FROM orders o
  JOIN order_history oh ON o.id = oh.order_id
  JOIN (SELECT order_id, MIN(status_changed_at) as min_status_changed_at FROM order_history GROUP BY order_id) msd ON o.id = msd.order_id

 


SELECT o.id,
       o.created_at,
	   oh.status_changed_at 
  FROM orders o
  JOIN order_history oh ON o.id = oh.order_id
  JOIN (SELECT order_id, MIN(status_changed_at) as min_status_changed_at FROM order_history GROUP BY order_id) msd ON o.id = msd.order_id
  WHERE  DATEDIFF(DAY, o.created_at, oh.status_changed_at) > 14
  ;

  SELECT o.id,
       o.product_id,
	   created_at,
	   st.id,
	   st.locality as to_locality,
	   st_1.id,
	   st_1.locality as from_locality
  FROM orders o
  JOIN storages st             ON o.pick_up_point_id = st.id
  JOIN products_on_storages ps ON ps.product_id      = o.product_id
  JOIN storages st_1           on ps.storage_id      = st_1.id
  WHERE o.id NOT IN (SELECT order_id FROM order_history oh GROUP BY order_id)
  ;