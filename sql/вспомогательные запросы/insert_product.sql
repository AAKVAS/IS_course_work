USE ISWildberries


INSERT INTO products (
       title,
       price,
       supplier_id, 
       category_id, 
       description, 
       supplier_percent)
VALUES (
       '������������� ���������', 
	   ROUND((RAND(CHECKSUM(NEWID())) * (1600 - 6000 + 1) + 6000 ), 2), 
	   10,
	   19, 
	   '������������� ��������� ����� ������� ��������� �������, �������������� ������� ��������� � ����� ������������. �������� ��� ������� �������.',
	   ROUND((RAND(CHECKSUM(NEWID())) * (99 - 75 + 1) + 75 ), 2)
)

;



