USE ISWildberries;

DELETE FROM storage_worker_shifts 
 WHERE 
       storage_id       = @storage_id
   AND started_shift_at = @started_shift_at
   AND worker_id        = @worker_id