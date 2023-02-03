
USE ISWildberries;

UPDATE reviews
   SET    
	   review_text = @review_text,
	   stars       = @stars     
 WHERE order_id = @order_id
;
	   