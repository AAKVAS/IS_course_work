USE ISWildberries;

SELECT r.title,
       s.id,
	   s.title,
	   p.id,
	   p.title
  FROM rights r
  JOIN section_rights sr ON sr.right_id   = r.id
  JOIN sections s         ON sr.section_id = s.id
  JOIN posts p           ON sr.post_id    = p.id
  JOIN workers w         ON w.post_id        = p.id
  WHERE w.id = @id
;