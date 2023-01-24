DECLARE @DATE datetime;
DECLARE @ORDER_ID int;
DECLARE @CURRENT_STORAGE_ID int;
DECLARE @FINISH_POINT int;
SET @DATE = DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
				DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
					DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(MONTH, RAND(CHECKSUM(NEWID())) * (3), Cast('7/7/2022' as datetime))
					)
				)
			);
SET @ORDER_ID = 14;
SET @CURRENT_STORAGE_ID = 3;
SET @FINISH_POINT = 13;

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 1, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
					DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
				);

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 2, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
				DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
					DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
				)
			);

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 3, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(DAY, RAND(CHECKSUM(NEWID())) * (3),
				DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
					DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
					)
				)
			);
			/*
SET @CURRENT_STORAGE_ID = 2;

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 7, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (4),
					DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
					)
			);

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 8, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (2),
					DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
					)
			);

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 9, @CURRENT_STORAGE_ID;


SET @DATE = DATEADD(DAY, RAND(CHECKSUM(NEWID())) * (3),
				DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
					DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
					)
				)
			);
			*/
SET @CURRENT_STORAGE_ID = @FINISH_POINT;

INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 10, @CURRENT_STORAGE_ID;

SET @DATE = DATEADD(DAY, RAND(CHECKSUM(NEWID())) * (1),
				DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
					DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60), @DATE)
					)
				)
			);


INSERT INTO order_history (order_id, status_changed_at, status_id, current_storage_id)
SELECT @ORDER_ID, @DATE, 11, @CURRENT_STORAGE_ID;



SELECT * FROM order_statuses;

SELECT * FROM storages s
JOIN storage_types st ON st.id = s.storage_type;

SELECT * FROM orders o
LEFT JOIN order_history oh ON oh.order_id = o.id
JOIN products_on_storages ps ON o.product_id = ps.product_id
WHERE oh.order_id IS NULL;
