USE ISWildberries;

SELECT
       w.id,
	   w.lastname,
	   w.firstname,
	   w.patronymic,
	   w.date_of_birthday,
	   IIF(w.is_male = 1, 'мужчина', 'женщина') as sex,
	   w.phone_number,
	   p.title as post
  FROM
       workers w
  JOIN posts p ON w.post_id = p.id

;