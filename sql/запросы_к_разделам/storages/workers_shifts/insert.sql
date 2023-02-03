USE ISWildberries;

INSERT INTO storage_worker_shifts (
       storage_id,
	   started_shift_at, 
	   finished_shift_at,
	   worker_id
)
VALUES (
       @storage_id,
	   @started_shift_at, 
	   @finished_shift_at,
	   @worker_id
)

