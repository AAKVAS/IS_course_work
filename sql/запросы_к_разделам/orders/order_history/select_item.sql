USE ISWildberries;

--Изменение заказа
SELECT 
       o.id as order_id,
	   o.product_id,
       oh.status_id,
	   oh.current_storage_id,
       oh.status_changed_at
  FROM 
       order_history oh 
  JOIN orders o                   ON oh.order_id = o.id
  LEFT JOIN workers_in_orders wio ON oh.order_id = wio.order_id
	                             AND wio.status_changed_at = oh.status_changed_at
  WHERE o.id = @id
    AND oh.status_changed_at = @status_changed_at
;


--Сотрудники, причастные к изменению заказа
SELECT 
       wio.worker_id,
	   w.firstname,
	   w.lastname,w.patronymic,
	   p.title
  FROM order_history oh 
  JOIN workers_in_orders wio ON oh.order_id = wio.order_id
                            AND wio.status_changed_at = oh.status_changed_at
  JOIN workers w             ON w.id = wio.worker_id
  JOIN posts   p             ON p.id = w.post_id
 WHERE oh.order_id = @id
   AND oh.status_changed_at = @status_changed_at

