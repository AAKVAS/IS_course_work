USE ISWildberries;

SELECT 
       s.id as storage_id,
       s.country, 
	   s.federal_subject,
	   s.locality, 
	   s.street, 
	   s.house_number,
	   sw.started_shift_at, 
	   sw.finished_shift_at,
	   w.id as worker_id,
	   w.firstname,
	   w.lastname,
	   w.patronymic,
	   p.title as post

  FROM storage_worker_shifts sw
	   JOIN storages s ON s.id   = sw.storage_id
	   JOIN workers w  ON w.id   = sw.worker_id
	   JOIN posts p    ON w.post = p.id
;