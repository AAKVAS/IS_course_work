USE ISWildberries;

SELECT o.id as order_id,
       u.id as user_id,
       u.firstname,
       u.lastname,
       u.patronymic,
       p.id as product_id,
       p.title,
       o.price,
       o.created_at
  FROM users u
	   INNER JOIN orders o   ON o.user_id = u.id
	   INNER JOIN products p ON o.product_id = p.id
;