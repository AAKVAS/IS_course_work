USE ISWildberries;

SELECT 
       *
  FROM 
       rights r
  JOIN section_rights sr  ON sr.right_id   = r.id
  JOIN sections s         ON sr.section_id = s.id
  JOIN posts p            ON sr.post_id    = p.id
  JOIN workers w          ON w.post_id     = p.id
  WHERE 
        w.id = 6 AND section_key = 'users_general_info'
;