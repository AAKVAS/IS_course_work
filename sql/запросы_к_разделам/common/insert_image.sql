USE ISWildberries;

--вставил файл
INSERT INTO files (
       id,
       file_title,
	   file_path
)
VALUES (
       IIF((SELECT COUNT(1) FROM files) = 0, 1, IDENT_CURRENT('files') + 1),
	   @file_title,
	   @file_path
);

--связал с таблицей
INSERT INTO table_files (
       table_name,
	   record_id,
	   f_id
)
VALUES (
       @table_name,
	   @record_id,
       SELECT IIF((SELECT COUNT(1) FROM files) = 0, 1, IDENT_CURRENT('files') + 1)
)
;