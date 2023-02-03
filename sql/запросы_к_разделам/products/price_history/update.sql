USE ISWildberries;

UPDATE price_history
   SET 
       product_id = @product_id_new,
       price = @price, 
       price_date = @price_date_new

 WHERE product_id = @product_id_old
   AND price_date = @price_date_old
;