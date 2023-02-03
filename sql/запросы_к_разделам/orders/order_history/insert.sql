USE ISWildberries;

--¬савка записи
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

--вставка табличной части
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