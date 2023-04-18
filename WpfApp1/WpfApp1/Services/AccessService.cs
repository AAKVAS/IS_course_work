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
    /// <summary>
    /// Класс, обеспечивающий авторизацию и аутентификацию в приложении.
    /// </summary>
    public class AccessService
    {
        private readonly ISWildberriesContext _context;
        private List<LoginedWorkerRights>? _loginedWorkerSections;

        /// <summary>
        /// Вошедший в систему сотрудник.
        /// </summary>
        public Workers? LoginedWorker { get; set; }
        
        /// <summary>
        /// Список разделов, доступных вошедшему сотруднику.
        /// </summary>
        public List<LoginedWorkerRights> LoginedWorkerSections
        {
            get { return _loginedWorkerSections ?? GetLoginedWorkerSections(); }
        }

        /// <summary>
        /// Конструктор класса AccessService
        /// </summary>
        public AccessService()
        {
            _context = App.Context;
        }

        /// <summary>
        /// Метод аутентификации сотрудника.
        /// </summary>
        /// <param name="workerLogin">Логин сотрудника.</param>
        /// <param name="password">Пароль сотрудника.</param>
        /// <returns>Возвращает, удалось ли пройти аутентификацию.</returns>
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

        /// <summary>
        /// Метод, возвращающий список разделов, доступных сотруднику.
        /// </summary>
        /// <returns>Разделы, доступные сотруднику.</returns>
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

        /// <summary>
        /// Метод, проверяющий, есть ли доступ у сотрудника к разделу. В качестве параметра принимает английское название раздела.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет права на работу с разделом.</returns>
        public bool HasWorkerRightToSection(string sectionKey)
        {
            return LoginedWorkerSections.Where(i => i.SectionKey == sectionKey).Any();
        }

        /// <summary>
        /// Метод, проверяющий, есть ли у сотрудника право на добавление записей в разделе.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет право на вставку записей в разделе.</returns>
        public bool HasWorkerRightToInsert(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Вставка");
        }

        /// <summary>
        /// Метод, проверяющий, есть ли у сотрудника право на удаление записей в разделе.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет право на удаление записей в разделе.</returns>
        public bool HasWorkerRightToDelete(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Удаление");
        }

        /// <summary>
        /// Метод, проверяющий, есть ли у сотрудника право на изменение записей в разделе.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет право на изменение записей в разделе.</returns>
        public bool HasWorkerRightToUpdate(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Изменение");
        }

        /// <summary>
        /// Метод, проверяющий, есть ли у сотрудника право на просмотр записей в разделе.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет право на просмотр записей в разделе.</returns>
        public bool HasWorkerRightToRead(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "Просмотр");
        }

        /// <summary>
        ///  Метод, проверяющий, есть ли у сотрудника право на создание PDF-документа на основе данных в разделе.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <returns>Истина, если сотрудник имеет право на создание PDF-документа на основе данных в разделе.</returns>
        public bool HasWorkerRightToPDF(string sectionKey)
        {
            return HasWorkerRightToSectionAction(sectionKey, "PDF");
        }

        /// <summary>
        /// Метод для проверка прав сотрудника на определённое действие в разделе. В качестве параметров принимает название раздела и название действия.
        /// </summary>
        /// <param name="sectionKey">Английское название раздела.</param>
        /// <param name="actionName">Название действия.</param>
        /// <returns>Истина, если сотрудники имеет право на действие.</returns>
        private bool HasWorkerRightToSectionAction(string sectionKey, string actionName)
        {
            return LoginedWorkerSections
                    .Where(s => s.SectionKey == sectionKey && s.RightTitle == actionName)
                    .Any();
        }

        /// <summary>
        /// Проверяет, является ли администратором вошедший сотрудник.
        /// </summary>
        /// <returns>Истина, если вошедший сотрудник - администратор.</returns>
        public bool IsAdmin()
        {
            return LoginedWorker.PostId == 4;
        }
    }
}
