USE ISWildberries;

UPDATE suppliers
   SET 
       title = @title
 WHERE id = @id
;