USE ISWildberries;

UPDATE categories
   SET 
       title              = @title,
       parent_category_id = @parent_category_id
 WHERE id = @id
;