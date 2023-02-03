USE ISWildberries;


--@status_changes_at - значение даты изменения статуса до обновления
--@worker_id         - значение id сотрудника до обновления
--удаление из табличной части

DELETE FROM workers_in_orders
  WHERE order_id          = @order_id
    AND status_changed_at = @status_changed_at 
	AND worker_id         = @worker_id
;

--удаление записи
DELETE FROM order_history
 WHERE order_id            = @order_id
   AND status_changed_at   = @status_changed_at
;
