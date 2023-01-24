USE ISWildberries;

SELECT 
	   u.id,
	   u.lastname,
	   u.firstname,
	   u.patronymic,
	   u.phone_number,
	   u.birthday,
	   u.email,
	   u.sex,
	   u.country
  FROM users u;