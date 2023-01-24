USE ISWildberries;

UPDATE users 
   SET
	   lastname = @lastname,
	   firstname = @firstname,
	   patronymic = @patronymic,
	   phone_number = @phone_number,
	   birthday = @birthday,
	   email = @email,
	   sex = @sex,
	   country = @country 
WHERE  id = @id
;