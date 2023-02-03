USE ISWildberries;

SELECT 
       r.order_id,
	   o.product_id, 
	   o.user_id,
	   r.review_text,
	   r.stars
  FROM 
       reviews r
  JOIN orders o   ON o.id = r.order_id
  WHERE o.id = @id
;