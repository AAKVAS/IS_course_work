USE ISWildberries;

SELECT 
       o.id as order_id,
	   pr.id as product_id,
       pr.title as product_name,
       os.description as order_status,
	   s.id as storage_id,
       s.locality,
       s.street,
       s.house_number,
       oh.status_changed_at,
	   w.id as worker_id,
       w.firstname,
       w.lastname,
       w.patronymic,
       w.sex,
       p.title as post
  FROM 
       order_history oh 
  JOIN orders o                   ON oh.order_id = o.id
  JOIN products pr                ON pr.id = o.product_id
  JOIN order_statuses os          ON os.id = oh.status_id
  JOIN storages s                 ON s.id = oh.current_storage_id
  LEFT JOIN workers_in_orders wio ON oh.order_id = wio.order_id
	                             AND wio.status_changed_at = oh.status_changed_at
  LEFT JOIN workers w             ON w.id = wio.worker_id
  LEFT JOIN posts p               ON p.id = w.post

  
  ;
