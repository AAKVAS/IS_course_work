USE ISWildberries;

SELECT u.id,
       u.firstname, 
       u.lastname, 
       u.patronymic,
	   p.id,
       p.title, 
       p.price
  FROM deferred_products dp
	   JOIN users u    ON u.id = dp.user_id
	   JOIN products p ON p.id = dp.product_id
;