USE ISWildberries;

SELECT o.id as order_id,
       u.id as user_id,
       u.firstname,
       u.lastname,
       u.patronymic,
       p.id as product_id,
       p.title,
	   o.product_count,
       o.price,
	   o.pick_up_point_id,
	   s.country,
	   s.federal_subject,
	   s.locality,
	   s.street,
	   s.house_number,	
       o.created_at,
	   o.estimated_delivery_at
  FROM users u
  JOIN orders o   ON o.user_id          = u.id
  JOIN products p ON o.product_id       = p.id
  JOIN storages s ON o.pick_up_point_id = s.id


;