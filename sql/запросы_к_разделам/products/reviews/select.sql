USE ISWildberries;

SELECT r.order_id,
	   p.id as product_id,
       p.title as product_name, 
	   u.id as user_id,
	   u.firstname, 
	   u.lastname,
	   u.patronymic,
	   r.review_text,
	   r.stars
  FROM 
       reviews r
  JOIN orders o   ON o.id = r.order_id
  JOIN products p ON o.product_id = p.id
  JOIN users u    ON u.id = o.user_id
;