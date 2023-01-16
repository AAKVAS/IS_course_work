USE ISWildberries;

SELECT u.id,
	   u.firstname,
       u.lastname,
       u.patronymic,
	   u.order_code,
	   p.id,
	   p.title,
       o.price
  FROM orders o
	   INNER JOIN users          u  ON u.id  = o.user_id
	   INNER JOIN products       p  ON p.id  = o.product_id
	   INNER JOIN pick_up_points pup ON pup.id = o.pick_up_point_id
 WHERE u.firstname  = @firstname
   AND u.lastname   = @lastname
   AND u.patronymic = @patronymic
   AND u.order_code = @order_code
   AND pup.id       = @pup
   ;