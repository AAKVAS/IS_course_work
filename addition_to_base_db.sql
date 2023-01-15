USE ISWildberries;

CREATE TABLE files (
	id int identity(1, 1) primary key,
	file_title varchar(255),
	file_path varchar(255)
);

CREATE TABLE table_files (
	id int identity(1, 1) primary key,
	table_name varchar(255),
	record_id int,
	f_id int FOREIGN KEY REFERENCES files(id),
	unique (table_name, record_id, f_id)
);

CREATE TABLE section (
	id int identity(1, 1) primary key,
	title varchar(255),
	parent_id int
);

INSERT INTO section (title, parent_id)
VALUES ('������������', NULL),
	   ('��������', NULL),
	   ('������', NULL),
	   ('������', NULL),
	   ('����������', NULL),
	   ('����������', NULL),

	   ('����� ��������', 1),
	   ('������� ������� �������������', 1),
	   ('������ ������� � �������', 1),

	   ('������ ��������', 2),
	   ('�������� �� ��, ���, ����', 2),
	   ('������� �������', 2),

	   ('����������� �� ������', 3),
	   ('������ ����������� �� ������', 3),
	   ('������ �� �������', 3),

	   ('����� ��������', 4),
	   ('������ � ������', 4),
	   ('��������� �������', 4),
	   ('������� ���', 4),

	   ('������� ��������', 5),
	   ('�������', 5),

	   ('������ �����������', 6)
;

CREATE TABLE rights (
	id int identity(1, 1) primary key,
	title varchar(60)
);

INSERT INTO rights (title)
VALUES ('��������'),
		('�������'),
		('���������'),
		('��������');

INSERT INTO posts (title)
VALUES ('�������������'),
		('��������');

CREATE TABLE section_rights (
	id int identity(1, 1) primary key,
	section_id int FOREIGN KEY REFERENCES section(id),
	right_id int FOREIGN KEY REFERENCES rights(id),
	post_id int FOREIGN KEY REFERENCES posts(id)
);

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = '�������������'
   AND s.title NOT IN ('������� ������� �������������', '�������')
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = '�������������'
   AND s.title IN ('������� ������� �������������', '�������')
   AND r.title = '��������'
 ;


INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = '��������'
   AND r.title = '��������'
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = '�����������'
   AND r.title IN ('��������', '���������')
   AND s.title IN ('����������� �� ������', '������ �� �������');
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = '��������� ������ ������ �������'
   AND r.title IN ('��������', '���������')
   AND s.title IN ('�������� �� ��, ���, ����');
 ;