USE ISWildberries;

UPDATE workers
   SET 
       worker_password = HASHBYTES('SHA_256', @password)
 WHERE id = @id
;
	   
	