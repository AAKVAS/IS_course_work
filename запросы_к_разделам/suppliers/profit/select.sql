USE ISWildberries;

SELECT 
       s.id as supplier_id,
       s.title as supplier, 
       sum(p.supplier_percent * o.price / 100 * o.product_count) as supplier_profit
  FROM suppliers s
  LEFT JOIN products p ON p.supplier_id = s.id
  LEFT JOIN orders o   ON p.id = o.product_id
 GROUP BY 
          s.id,
		  s.title
;