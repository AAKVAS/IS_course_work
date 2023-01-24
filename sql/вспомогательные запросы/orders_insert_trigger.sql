ALTER TRIGGER insert_order
ON orders
INSTEAD OF INSERT
AS
BEGIN;

DECLARE @non_pick_up_point_storage_count int;

SELECT 
       @non_pick_up_point_storage_count = COUNT(*)
  FROM 
       inserted i
       INNER JOIN storages s       ON i.pick_up_point_id = s.id 
	   INNER JOIN storage_types st ON s.storage_type = st.id AND st.title != 'пункт выдачи'
;

IF @non_pick_up_point_storage_count != 0
BEGIN;
    THROW 51000, 'В pick_up_point_id должен быть склад с типом "пункт выдачи"', 1;
END;

INSERT INTO orders (
       user_id, 
	   product_id,
	   product_count,
	   pick_up_point_id, 
	   price, 
	   created_at, 
	   estimated_delivery_time
)
SELECT 
       i.user_id, 
	   i.product_id, 
	   i.product_count, 
	   i.pick_up_point_id, 
	   p.price * i.product_count, 
	   i.created_at, 
	   i.estimated_delivery_time
  FROM 
       inserted i
       INNER JOIN storages s       ON i.pick_up_point_id = s.id 
	   INNER JOIN storage_types st ON s.storage_type = st.id AND st.title = 'пункт выдачи'
	   INNER JOIN products p       ON i.product_id = p.id
;
END;