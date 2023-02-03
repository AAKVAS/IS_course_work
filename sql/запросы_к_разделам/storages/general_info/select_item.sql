USE ISWildberries;

SELECT s.id,
	   s.country,
	   s.federal_subject,
	   s.locality,
	   s.street,
	   s.house_number,
	   s.storage_type
  FROM storages s
 WHERE s.id = 1;
