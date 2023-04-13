USE ISWildberries;

SELECT 
       s.id,
       s.title as supplier, 
       ISNULL(ROUND(SUM(p.supplier_percent / 100 * o.price), 2), 0) as supplier_profit
  FROM 
       suppliers s
  LEFT JOIN products p ON p.supplier_id = s.id
  LEFT JOIN orders o   ON o.product_id  = p.id
 GROUP BY 
          s.id,
          s.title
;