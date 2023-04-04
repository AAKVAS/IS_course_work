using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Models;
using WpfApp1.Models.DTO;

namespace WpfApp1.Services
{
    public class AccessService
    {
        public Workers? LoginedWorker { get; set; }

        private List<LoginedWorkerRights> _loginedWorkerSections;
        public List<LoginedWorkerRights> LoginedWorkerSections
        {
            get { return _loginedWorkerSections ?? GetLoginedWorkerSections(); }
        }

        private readonly ISWildberriesContext _context;

        public AccessService()
        {
            _context = App.Context;
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

        private List<LoginedWorkerRights> GetLoginedWorkerSections()
        {
            string query = @"SELECT 
                                    sr.id,
                                    sr.right_id as RightId,
	                                r.title as RightTitle,
	                                sr.section_id as SectionId,
	                                s.title as SectionTitle,
	                                s.parent_id as SectionParentId,
	                                sr.post_id as PostId,
	                                s.section_key as SectionKey
                               FROM 
                                     rights r
                                JOIN section_rights sr  ON sr.right_id   = r.id
                                JOIN sections s         ON sr.section_id = s.id
                                JOIN posts p            ON sr.post_id    = p.id
                                JOIN workers w          ON w.post_id     = p.id
                               WHERE 
                                     w.id = @worker_id";

            _loginedWorkerSections = _context.LoginedWorkerRights
                .FromSqlRaw(query, new SqlParameter("@worker_id", LoginedWorker.Id))
                .ToList();


            return _loginedWorkerSections;
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
                    .Where(s => s.SectionKey == sectionKey && s.RightTitle == actionName)
                    .Any();
        }

        public bool IsAdmin()
        {
            return LoginedWorker.PostId == 4;
        }
    }
}
