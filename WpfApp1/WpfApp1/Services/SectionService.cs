﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class SectionService
    {
        private readonly ISWildberriesContext _context;

        public SectionService(ISWildberriesContext context)
        {
            _context = context;
        }

        public Sections GetSectionParent(Sections sections)
        {
            string query = $@"WITH cte AS (
                            SELECT  id,
	                                title, 
		                            parent_id,
                                    section_key
                                FROM sections
	                            WHERE id = @id

	                        UNION ALL

	                            SELECT s.id,
	                                s.title, 
		                            s.parent_id,
                                    s.section_key
	                            FROM cte
	                                INNER JOIN sections s ON s.id = cte.parent_id
                        )

                        SELECT *
                            FROM cte
                            WHERE parent_id IS NULL";

            return _context.Sections
                    .FromSqlRaw(query, new SqlParameter("@id", sections.Id))
                    .ToList()
                    .FirstOrDefault();
        }

        public Sections GetSectionBySectionKey(string sectionKey)
        {
            return _context.Sections.Where(s => s.SectionKey == sectionKey).ToList().FirstOrDefault();
        }
    }
}
