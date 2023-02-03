USE ISWildberries;

--@status_changes_at_new - ���������� �������� ���� ��������� �������
--@status_changes_at_old - �������� ���� ��������� ������� �� ����������
--@worker_id_new         - �������� id ���������� ����� ����������
--@worker_id_old         - �������� id ���������� �� ����������

UPDATE oh
   SET 
       oh.status_id          = @status_id,
	   oh.current_storage_id = @current_storage_id,
       oh.status_changed_at  = @status_changes_at_new
  FROM 
       order_history oh 
  JOIN orders o ON oh.order_id = o.id
  WHERE oh.order_id            = @order_id
    AND oh.status_changed_at   = @status_changed_at_old
;

UPDATE workers_in_orders
   SET
       status_changed_at  = @status_changes_at_new,
	   worker_id          = @worker_id_new

  WHERE order_id          = @order_id
    AND status_changed_at = @status_changed_at_old 
	AND worker_id         = @worker_id_old
;