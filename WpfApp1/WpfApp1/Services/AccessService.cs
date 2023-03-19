using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public class AccessService
    {
        public Workers? LoginedWorker { get; set; }

        private List<Sections> _loginedWorkerSections;
        public List<Sections> LoginedWorkerSections
        {
            get { return _loginedWorkerSections ?? GetLoginedWorkerSections(); }
        }

        private readonly ISWildberriesContext _context;

        public AccessService(ISWildberriesContext context)
        {
            _context = context;
        }

        public bool IsLogin(string workerLogin, byte[] password)
        {
            SqlParameter workerLoginParam = new SqlParameter("@worker_login", workerLogin);
            SqlParameter passwordParam = new SqlParameter("@worker_password", System.Data.SqlDbType.VarBinary, 255);
            passwordParam.Value = password;

            LoginedWorker = _context.Workers.FromSqlRaw("SELECT * FROM workers w WHERE worker_login = @worker_login AND worker_password = @worker_password", workerLoginParam, passwordParam)
                .ToList()
                .FirstOrDefault();

            return LoginedWorker != null;
        }

        private List<Sections> GetLoginedWorkerSections()
        {
            _loginedWorkerSections = (from section     in _context.Sections
                                     join sectionRight in _context.SectionRights on section.Id          equals sectionRight.SectionId
                                     join post         in _context.Posts         on sectionRight.PostId equals post.Id
                                     join worker       in _context.Workers       on post.Id             equals worker.PostId
                                     where worker.Id == LoginedWorker.Id
                                     select section
                                     )
                                     .Include(s => s.SectionRights.Where(sr => sr.PostId == LoginedWorker.PostId))
                                        .ThenInclude(sr => sr.Right)
                                     .ToList();

            return _loginedWorkerSections;
        }

        public Sections GetSectionBySectionKey(string sectionKey)
        {
            return LoginedWorkerSections.Where(i => i.SectionKey == sectionKey).FirstOrDefault();
        }

        public List<SectionRights> GetSectionRightsBySectionKey()
        {
            return null;
        }

        public bool HasWorkerRightToSection(string sectionKey)
        {
            return LoginedWorkerSections.Where(i => i.SectionKey == sectionKey).Any();
        }

        public bool HasWorkerRightToInsert(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Вставка");
        }

        public bool HasWorkerRightToDelete(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Удаление");
        }

        public bool HasWorkerRightToUpdate(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Изменение");
        }

        public bool HasWorkerRightToRead(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Просмотр");
        }

        public bool HasWorkerRightToPDF(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "PDF");
        }

        private bool HasWorkerRightToSectionAction(string sectionKey, string actionName)
        {
            return LoginedWorkerSections
                    .Where(i => i.SectionKey == sectionKey)
                    .FirstOrDefault()
                    .SectionRights
                    .Where(i => i.Right.Title == actionName)
                    .Any();
        }
    }
}
