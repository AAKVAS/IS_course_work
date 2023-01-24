BEGIN
DECLARE @DATE datetime;
DECLARE @PRODUCT int;
SET @DATE = DATEADD(HOUR, RAND(CHECKSUM(NEWID())) * (24),
				DATEADD(MINUTE, RAND(CHECKSUM(NEWID())) * (60),
					DATEADD(SECOND, RAND(CHECKSUM(NEWID())) * (60),
						DATEADD(MONTH, RAND(CHECKSUM(NEWID())) * (3), Cast('6/6/2022' as datetime))
					)
				)
			);
SET @PRODUCT = 11;


INSERT INTO orders (user_id, product_id, product_count, pick_up_point_id, created_at)
SELECT
       (SELECT TOP 1 u.id FROM users u LEFT JOIN orders o ON o.user_id = u.id WHERE o.id IS NULL),
	   @PRODUCT,
	   1,
	   8,
	   @DATE
END;