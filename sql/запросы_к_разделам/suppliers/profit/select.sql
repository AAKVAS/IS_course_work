USE ISWildberries;

SELECT 
       s.id,
       s.title as supplier, 
       ISNULL(SUM(p.supplier_percent * o.price / 100 * o.product_count), 0) as supplier_profit
  FROM 
       suppliers s
  LEFT JOIN products p ON p.supplier_id = s.id
  LEFT JOIN orders o   ON o.product_id  = p.id
 GROUP BY 
          s.id,
		  s.title
;