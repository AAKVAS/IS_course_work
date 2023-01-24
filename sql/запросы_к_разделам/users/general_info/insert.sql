USE ISWildberries;

INSERT INTO users (
	   lastname,
	   firstname,
	   patronymic,
	   phone_number,
	   birthday,
	   email,
	   sex,
	   country)
VALUES (
	   @lastname,
	   @firstname,
	   @patronymic,
	   @phone_number,
	   @birthday,
	   @email,
	   @sex,
	   @country
)
;