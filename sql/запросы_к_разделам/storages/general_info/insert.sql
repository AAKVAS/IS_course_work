USE ISWildberries;

INSERT INTO storages (
	   country,
	   federal_subject,
	   locality,
	   street,
	   house_number,
	   storage_type
)
VALUES (
	   @country,
	   @federal_subject,
	   @locality,
	   @street,
	   @house_number,
	   @storage_type
)
;