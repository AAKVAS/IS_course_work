USE ISWildberries;

SELECT 
       w.id,
       w.firstname,
	   w.lastname,
	   w.patronymic,
	   p.title

  FROM workers w
  JOIN posts p   ON p.id = w.post_id
;