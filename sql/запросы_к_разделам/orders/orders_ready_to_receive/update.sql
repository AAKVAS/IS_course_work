USE ISWildberries;

UPDATE order_history
   SET status_id = @status_id
 WHERE order_id = @id

;