USE ISWildberries;

INSERT INTO price_history (
       product_id,
       price, 
       price_date
)
VALUES (
       @product_id,
       @price, 
       @price_date
)
;