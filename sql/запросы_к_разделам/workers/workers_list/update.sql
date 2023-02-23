USE ISWildberries;

UPDATE workers
   SET 
       lastname         = @lastname,
	   firstname        = @firstname,
	   patronymic       = @patronymic,
	   date_of_birthday = @date_of_birthday,
	   is_male          = @is_male,
	   phone_number     = @phone_number,
	   post_id          = @post_id
 WHERE id = @id
;
	   