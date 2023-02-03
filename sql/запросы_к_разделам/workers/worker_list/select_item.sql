USE ISWildberries;

SELECT
       w.id,
	   w.lastname,
	   w.firstname,
	   w.patronymic,
	   w.date_of_birthday,
	   w.is_male,
	   w.phone_number,
	   w.post_id
  FROM
       workers w
 WHERE w.id = 1

;