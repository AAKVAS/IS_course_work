USE ISWildberries;

UPDATE storages 
   SET 
	   country = @country,
	   federal_subject =  @federal_subject,
	   locality = @locality,
	   street = @street,
	   house_number = @house_number,
	   storage_type = @storage_type
 WHERE id = @id
;