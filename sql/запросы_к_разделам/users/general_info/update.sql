USE ISWildberries;

UPDATE users 
   SET
	   lastname = @lastname,
	   firstname = @firstname,
	   patronymic = @patronymic,
	   phone_number = @phone_number,
	   birthday = @birthday,
	   email = @email,
	   is_male = @is_male,
	   country_id = @country_id 
 WHERE id = @id
;