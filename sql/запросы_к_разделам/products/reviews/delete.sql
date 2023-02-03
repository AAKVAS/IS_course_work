USE ISWildberries;

DELETE FROM reviews
 WHERE order_id = @order_id

;