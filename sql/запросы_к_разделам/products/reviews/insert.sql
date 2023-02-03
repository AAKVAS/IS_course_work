USE ISWildberries;

INSERT INTO reviews (
       order_id,   
	   review_text,
	   stars
)
VALUES (
       @order_id,   
	   @review_text,
	   @stars
)
;