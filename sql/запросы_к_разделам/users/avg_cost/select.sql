USE ISWildberries;

SELECT u.id, 
       u.firstname, 
	   u.lastname, 
	   u.patronymic, 
	   avg(o.price) as avg_cost
  FROM users u
	   JOIN orders o ON o.user_id = u.id
 GROUP BY 
       u.id, 
	   u.firstname, 
       u.lastname, 
       u.patronymic
;