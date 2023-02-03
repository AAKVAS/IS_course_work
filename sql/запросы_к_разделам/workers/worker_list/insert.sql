USE ISWildberries;

INSERT INTO workers (
	   lastname,
	   firstname,
	   patronymic,
	   date_of_birthday,
	   is_male,
	   phone_number,
	   post_id
)
VALUES (
       @lastname,
	   @firstname,
	   @patronymic,
	   @date_of_birthday,
	   @is_male,
	   @phone_number,
	   @post_id
)
;