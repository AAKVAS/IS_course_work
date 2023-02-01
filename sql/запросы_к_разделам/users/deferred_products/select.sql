USE ISWildberries;

SELECT dp.user_id,
       u.firstname, 
       u.lastname, 
       u.patronymic,
	   dp.product_id,
       p.title, 
       p.price
  FROM deferred_products dp
	   JOIN users u    ON u.id = dp.user_id
	   JOIN products p ON p.id = dp.product_id
;