USE ISWildberries;

SELECT u.id,
       u.firstname,
	   u.lastname,
	   u.patronymic,
	   p.id,
	   p.title,
	   o.price,
	   o.created_at
  FROM users u
	   INNER JOIN orders o   ON o.user_id = u.id
	   INNER JOIN products p ON o.product_id = p.id
;