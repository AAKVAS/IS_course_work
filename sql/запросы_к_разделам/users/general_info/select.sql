USE ISWildberries;

SELECT 
	   u.id,
	   u.lastname,
	   u.firstname,
	   u.patronymic,
	   u.phone_number,
	   u.birthday,
	   u.email,
	   IIF(u.is_male = 1, 'мужчина', 'женщина') as sex,
	   c.title as country
  FROM users u
  JOIN countries c ON c.id = u.country_id
  
;