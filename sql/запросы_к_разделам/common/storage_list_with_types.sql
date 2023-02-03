USE ISWildberries;

SELECT s.id,
       s.country,
	   s.federal_subject,
	   s.locality,
	   s.street,
	   s.house_number,
	   ss.title
  FROM storages s
  JOIN storage_types ss ON ss.id = s.storage_type