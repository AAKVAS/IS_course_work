USE ISWildberries;

UPDATE storage_worker_shifts 
   SET
       storage_id        = @storage_id_new,
	   started_shift_at  = @started_shift_at_new,
	   finished_shift_at = @finished_shift_at,
	   worker_id         = @worker_id_new
 WHERE 
       storage_id       = @storage_id_old
   AND started_shift_at = @started_shift_at_old
   AND worker_id        = @worker_id_old