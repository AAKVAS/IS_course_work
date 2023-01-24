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
VALUES ('Пользователи', NULL),
	   ('Доставки', NULL),
	   ('Склады', NULL),
	   ('Товары', NULL),
	   ('Поставщики', NULL),
	   ('Сотрудники', NULL),

	   ('Общие сведения', 1),
	   ('Средние затраты пользователей', 1),
	   ('Список товаров в корзине', 1),

	   ('Список доставок', 2),
	   ('Доставки по ПВ, ФИО, коду', 2),
	   ('История заказов', 2),

	   ('Поступления на склады', 3),
	   ('Работа сотрудников на складе', 3),
	   ('Товары на складах', 3),

	   ('Общие сведения', 4),
	   ('Отзывы к товару', 4),
	   ('Категории товаров', 4),
	   ('История цен', 4),

	   ('История поставок', 5),
	   ('Прибыль', 5),

	   ('Список сотрудников', 6)
;

CREATE TABLE rights (
	id int identity(1, 1) primary key,
	title varchar(60)
);

INSERT INTO rights (title)
VALUES ('Просмотр'),
		('Вставка'),
		('Изменение'),
		('Удаление');

INSERT INTO posts (title)
VALUES ('администратор'),
		('аналитик');

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
 WHERE p.title = 'администратор'
   AND s.title NOT IN ('Средние затраты пользователей', 'Прибыль')
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = 'администратор'
   AND s.title IN ('Средние затраты пользователей', 'Прибыль')
   AND r.title = 'Просмотр'
 ;


INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = 'аналитик'
   AND r.title = 'Просмотр'
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = 'сортировщик'
   AND r.title IN ('Просмотр', 'Изменение')
   AND s.title IN ('Поступления на склады', 'Товары на складах');
 ;

INSERT INTO section_rights (section_id, right_id, post_id) 
SELECT s.id,
	   r.id,
	   p.id
  FROM section s
 CROSS JOIN rights r
 CROSS JOIN posts p
 WHERE p.title = 'сотрудник пункта выдачи заказов'
   AND r.title IN ('Просмотр', 'Изменение')
   AND s.title IN ('Доставки по ПВ, ФИО, коду');
 ;