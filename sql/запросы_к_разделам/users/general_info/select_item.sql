USE ISWildberries;

SELECT 
	   u.id,
	   u.lastname,
	   u.firstname,
	   u.patronymic,
	   u.phone_number,
	   u.birthday,
	   u.email,
	   u.is_male,
	   u.country_id
  FROM users u
  JOIN countries c ON c.id = u.country_id
  WHERE u.id = @id;