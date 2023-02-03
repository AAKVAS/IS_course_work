USE ISWildberries;

INSERT INTO categories(
       title,
       parent_category_id  
)
VALUES (
       @title,
       @parent_category_id         
)
;