USE ISWildberries;

SELECT s.id,
       st.title,
	   s.country,
	   s.federal_subject,
	   s.locality,
	   s.street,
	   s.house_number
  FROM storages s
 INNER JOIN storage_types st ON st.id = s.storage_type;


