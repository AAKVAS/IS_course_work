USE ISWildberries;

SELECT 
       s.id as storage_id,
	   s.country,
	   s.federal_subject,
	   s.locality,
	   s.street,
	   s.house_number,
       o.id as order_id,
       u.id as user_id,
	   u.firstname,
       u.lastname,
       u.patronymic,
	   u.order_code,
	   p.id as produst_id,
	   p.title,
	   o.created_at,
       o.price
  FROM orders o
  JOIN users u          ON u.id  = o.user_id
  JOIN products p       ON p.id  = o.product_id
  JOIN storages s       ON s.id = o.pick_up_point_id
  JOIN order_history oh ON o.id = oh.order_id
 WHERE oh.is_last_status = 1 
   AND oh.status_id = 11

;