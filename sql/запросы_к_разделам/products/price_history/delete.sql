USE ISWildberries;

DELETE FROM price_history
 WHERE product_id = @product_id
   AND price_date = @price_date

;
