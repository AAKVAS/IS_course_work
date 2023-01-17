USE ISWildberries;

SELECT
       w.id,
	   w.lastname,
	   w.firstname,
	   w.patronymic,
	   w.date_of_birthday,
	   w.sex,
	   w.phone_number,
	   p.title as post

  FROM
       workers w
  JOIN posts p ON w.post = p.id

;