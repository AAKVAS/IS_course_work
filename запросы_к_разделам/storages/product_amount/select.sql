USE ISWildberries;

SELECT 
	   s.id as storage_id,
	   s.title,
	   s.country,
	   s.federal_subject, 
	   s.locality,
	   s.street,
	   s.house_number,
	   p.id as product_id,
	   p.title,
	   ps.product_amount

  FROM products p
	   JOIN products_on_storages ps ON p.id = ps.product_id
	   JOIN storages s              ON s.id = ps.storage_id
;