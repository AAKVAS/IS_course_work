USE ISWildberries;

SELECT s.id,
       s.title
  FROM suppliers s
  WHERE s.id = @id;