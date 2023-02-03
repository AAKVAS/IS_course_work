USE ISWildberries;

SELECT 
       s.id,
       ISNULL(SUM(p.supplier_percent * o.price / 100 * o.product_count), 0) as supplier_profit
  FROM 
       suppliers s
  LEFT JOIN products p ON p.supplier_id = s.id
  LEFT JOIN orders o   ON o.product_id  = p.id
   WHERE s.id = @id
 GROUP BY 
          s.id

;