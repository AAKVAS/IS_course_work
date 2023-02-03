USE ISWildberries;

WITH child_categories AS (
	SELECT c.id,
		   c.parent_category_id
	  FROM categories c
	 WHERE c.id = @id OR @id IS NULL

	 UNION ALL

	SELECT c.id,
		   c.parent_category_id
	  FROM categories c
	  JOIN child_categories cc ON cc.id = c.parent_category_id

)

SELECT c.id,
       c.title,
       c.parent_category_id
  FROM categories c
  JOIN child_categories cc ON cc.id = c.id
;