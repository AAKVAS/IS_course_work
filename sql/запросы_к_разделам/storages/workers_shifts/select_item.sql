USE ISWildberries;

SELECT 
       sw.storage_id,
	   sw.started_shift_at, 
	   sw.finished_shift_at,
	   sw.worker_id

  FROM storage_worker_shifts sw
 WHERE sw.storage_id       = @storage_id
   AND sw.started_shift_at = @started_shift_at
   AND sw.worker_id        = @worker_id
;