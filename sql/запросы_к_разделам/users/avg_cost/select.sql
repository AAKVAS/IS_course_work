USE ISWildberries;

SELECT u.id, 
       u.firstname, 
	   u.lastname, 
	   u.patronymic, 
	   ISNULL(AVG(o.price), 0) as avg_cost
  FROM users u
	   LEFT JOIN orders o ON o.user_id = u.id
 GROUP BY 
       u.id, 
	   u.firstname, 
       u.lastname, 
       u.patronymic
;