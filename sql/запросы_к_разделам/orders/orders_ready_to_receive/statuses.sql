USE ISWildberries;

SELECT os.id,
       os.description
  FROM order_statuses os
 WHERE os.id IN (11, 12, 13)

;