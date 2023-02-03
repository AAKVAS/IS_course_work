USE ISWildberries;

SELECT tf.id,
       f.id as file_id,
       f.file_title,
	   f.file_path
  FROM table_files tf
  JOIN files f        ON tf.f_id = f.id
                     AND table_name = @table_name
					 AND record_id  = @record_id
; 
