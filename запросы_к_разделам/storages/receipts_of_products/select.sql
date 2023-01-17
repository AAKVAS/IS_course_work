USE ISWildberries;

SELECT  
	   s.id as storage_id,
       s.title as storage_name,
       s.country, 
       s.federal_subject, 
       s.locality,
       s.street, 
       s.house_number, 
	   p.id as product_id,
	   p.title as product_name,
	   rps.amount,
       rps.received_at
	   
  FROM 
       receipt_of_products_to_storages rps
  JOIN products p ON p.id = rps.product_id
  JOIN storages s ON s.id = rps.storage_id
;