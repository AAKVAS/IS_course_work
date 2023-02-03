USE ISWildberries;


SELECT c.id,
       c.title,
       c.parent_category_id
  FROM categories c
  WHERE c.id = @id
;