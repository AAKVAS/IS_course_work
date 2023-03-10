USE ISWildberries;

SELECT w.id as worker_id,
       r.title as right_title,
       s.id as section_id,
	   s.title as section_title,
	   s.section_key,
	   p.id as post_id,
	   p.title as post
  FROM rights r
  JOIN section_rights sr  ON sr.right_id   = r.id
  JOIN sections s         ON sr.section_id = s.id
  JOIN posts p            ON sr.post_id    = p.id
  JOIN workers w          ON w.post_id     = p.id
  WHERE w.id = @id
;