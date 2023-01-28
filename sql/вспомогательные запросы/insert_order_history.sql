DECLARE @DATE datetime;
DECLARE @ORDER_ID int;
DECLARE @CURRENT_STORAGE_ID int;
DECLARE @FINISH_POINT int;

SET @ORDER_ID = 20;
SET @DATE = (SELECT created_at FROM orders WHERE id = @ORDER_ID);
SET @CURRENT_STORAGE_ID = 3;
SET @FINISH_POINT = (SELECT pick_up_point_id FROM orders WHERE id = @ORDER_ID);

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
			
SET @CURRENT_STORAGE_ID = 1;

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


