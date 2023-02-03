USE ISWildberries;

INSERT INTO users (
	   lastname,
	   firstname,
	   patronymic,
	   phone_number,
	   birthday,
	   email,
	   is_male,
	   country_id)
VALUES (
	   @lastname,
	   @firstname,
	   @patronymic,
	   @phone_number,
	   @birthday,
	   @email,
	   @is_male,
	   @country_id
)
;