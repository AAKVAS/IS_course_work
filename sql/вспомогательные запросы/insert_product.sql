USE ISWildberries


INSERT INTO products (
       title,
       price,
       supplier_id, 
       category_id, 
       description, 
       supplier_percent)
VALUES (
       'Баскетбольные кроссовки', 
	   ROUND((RAND(CHECKSUM(NEWID())) * (1600 - 6000 + 1) + 6000 ), 2), 
	   10,
	   19, 
	   'Баскетбольные кроссовки имеют прочную резиновую подошву, обеспечивающее хорошее сцепление с любой поверхностью. Идеальны для занятий спортом.',
	   ROUND((RAND(CHECKSUM(NEWID())) * (99 - 75 + 1) + 75 ), 2)
)

;



