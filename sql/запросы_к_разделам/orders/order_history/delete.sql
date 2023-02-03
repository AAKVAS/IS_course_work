USE ISWildberries;


--@status_changes_at - �������� ���� ��������� ������� �� ����������
--@worker_id         - �������� id ���������� �� ����������
--�������� �� ��������� �����

DELETE FROM workers_in_orders
  WHERE order_id          = @order_id
    AND status_changed_at = @status_changed_at 
	AND worker_id         = @worker_id
;

--�������� ������
DELETE FROM order_history
 WHERE order_id            = @order_id
   AND status_changed_at   = @status_changed_at
;
