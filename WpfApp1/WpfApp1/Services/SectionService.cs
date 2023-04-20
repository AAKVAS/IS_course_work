using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с данными, связанными с разделами.
    /// </summary>
    public class SectionService
    {
        private static readonly ISWildberriesContext _context = App.Context;

        /// <summary>
        /// Метод, возвращающий корневой родительский раздел переданного раздела.
        /// </summary>
        /// <param name="sections">Раздел, для которого нужно найти корневой родительский раздел.</param>
        /// <returns>Корневой родительский раздел.</returns>
        public static Sections GetSectionParent(Sections sections)
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
                    .FirstOrDefault() ?? new Sections();
        }

        /// <summary>
        /// Метод, возвращающий раздел по его английскому названию.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Раздел.</returns>
        public static Sections GetSectionBySectionKey(string sectionKey)
        {
            return _context.Sections.Where(s => s.SectionKey == sectionKey).ToList().FirstOrDefault();
        }
    }
}
