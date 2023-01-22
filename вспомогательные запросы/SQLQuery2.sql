USE ISWildberries;

BEGIN
WHILE 1 IN (SELECT 1
	         FROM products p
	         LEFT JOIN price_history ph ON ph.product_id = p.id
	         WHERE ph.price IS NULL)
	BEGIN

	DECLARE @COUNTER int;
	DECLARE @PRODUCT_ID int;
	DECLARE @PRICE float;
	DECLARE @PRICE_DATE date;
	DECLARE @TEMP_PRICE float;

	SET @COUNTER = 0;

	SELECT TOP 1
			@PRODUCT_ID = p.id, 
			@PRICE = ROUND(p.price * (RAND(CHECKSUM(NEWID())) * (110 - 90 + 1) + 90 ) / 100, 2),
			@PRICE_DATE = DATEADD(day, RAND(CHECKSUM(NEWID())) * (10 - 1 + 1) + 1 + p.id, Cast('2/5/2022' as datetime))
		
	  FROM products p
	  LEFT JOIN price_history ph ON ph.product_id = p.id
	  WHERE ph.price IS NULL
	  ;

	  WHILE @COUNTER < (RAND() * (10 - 4 + 1) + 1)
	  BEGIN
		INSERT INTO price_history(product_id, price, price_date)
		VALUES (@PRODUCT_ID, @PRICE, @PRICE_DATE);
		SET @TEMP_PRICE = ROUND(@PRICE * (RAND(CHECKSUM(NEWID())) * (110 - 90 + 1) + 90 ) / 100, 2);
		IF @TEMP_PRICE = @PRICE 
			SET @PRICE = @TEMP_PRICE + 1
		ELSE 
			SET @PRICE = @TEMP_PRICE;
		SET @PRICE_DATE = DATEADD(month, 1, @PRICE_DATE);
		SET @COUNTER = @COUNTER + 1;
	  END;

	END;
END;
  