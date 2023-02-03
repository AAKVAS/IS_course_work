USE ISWildberries;

--������ ������
INSERT INTO order_history (
    order_id,
	status_changed_at,
	status_id,
	current_storage_id
)
VALUES (
    @order_id,
	@status_changed_at,
	@status_id,
	@current_storage_id
)
;

--������� ��������� �����
INSERT INTO workers_in_orders (
    order_id,
	status_changed_at,
	worker_id
)
VALUES (
    @order_id,
	@status_changed_at,
	@worker_id
)
;