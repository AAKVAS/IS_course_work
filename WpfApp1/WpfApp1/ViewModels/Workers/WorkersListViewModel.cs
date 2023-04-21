using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Workers.WorkersList;
using PasswordEvaluatorLib;
using System.Windows;
using ValidationLib;

namespace WpfApp1.ViewModels.Workers
{
    /// <summary>
    /// Модель представления для раздела "Сотрудники / Список сотрудников".
    /// </summary>
    public class WorkersListViewModel : SectionWidgetViewModel
    {
        /// <summary>
        /// Ссылка на окно работы с записью раздела. 
        /// </summary>
        private WorkersListItem _itemForm;

        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as WorkersListItem;
        }

        /// <summary>
        /// Команда  открытия окна изменения пароля сотрудника.
        /// </summary>
        private RelayCommand? _tryChangePasswordCommand;

        /// <summary>
        /// Команда открытия окна изменения пароля сотрудника. Доступна, если вошедший пользователь является администратором системы.
        /// </summary>
        public RelayCommand TryChangePasswordCommand
        {
            get
            {
                return _tryChangePasswordCommand ??
                        (_tryChangePasswordCommand = new RelayCommand((object obj) => {
                            ShowChangePasswordForm();
                        },
                        (obj) => _accessService.IsAdmin()));
            }
        }

        /// <summary>
        /// Команда изменения пароля сотрудника.
        /// </summary>
        private RelayCommand? _changePasswordCommand;
        /// <summary>
        /// Команда изменения пароля сотрудника. Доступна, если вошедший пользователь является администратором системы.
        /// </summary>
        public RelayCommand ChangePasswordCommand
        {
            get
            {
                return _changePasswordCommand ??
                    (_changePasswordCommand = new RelayCommand((object obj) => {
                       ChangePassword();
                    },
                    (obj) => _accessService.IsAdmin()));
            }
        }

        /// <summary>
        /// Ссылка на окно изменения пароля.
        /// </summary>
        private ChangePasswordForm _changePasswordForm;

        /// <summary>
        /// Коллекция должностей, используется для заполнения выпадающего списка в окне работы с записью раздела.
        /// </summary>
        public List<Posts> Posts { get; set; }

        /// <summary>
        /// Оценщик сложности пароля.
        /// </summary>
        private PasswordEvaluator _passwordEvaluator;

        /// <summary>
        /// Сложность введённого пароля.
        /// </summary>
        private PasswordComplexity _passwordComplexity;

        /// <summary>
        /// Сложность введённого пароля на русском языке.
        /// </summary>
        public string RusPasswordComplexity
        {
            get
            {
                EvaluatePassword();
                return PasswordEvaluator.PasswordComplexityToRus(_passwordComplexity);
            }
        }

        /// <summary>
        /// Конструктор класса WorkersListViewModel, в качестве параметра принимает ссылку на представление раздела.
        /// </summary>
        /// <param name="sectionWidget">Представление раздела.</param>
        public WorkersListViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _passwordEvaluator = new();
        }

        /// <summary>
        /// Метод, оценивающий сложность введённого пароля.
        /// </summary>
        private void EvaluatePassword()
        {
            _passwordComplexity = _passwordEvaluator.EvaluatePassword(_changePasswordForm.passwordBox.Password);
        }

        protected override void MakeCurrentItemEmpty()
        {
            CurrentItem = new Models.Workers();
        }

        protected override void CreateNewItemForm()
        {
            Posts = App.Context.Posts.ToList();
            _itemForm = new WorkersListItem(this);
        }

        protected override void AddCurrentItem()
        {
            App.Context.Workers.Add(CurrentItem);
        }

	    protected override void DeleteCurrentItem()
        {
            App.Context.Workers.Remove(CurrentItemFromContext);
        }

        public override void UpdateSectionData()
        {
            SectionData = WorkerService.GetWorkers();
        }

        protected override void FillItem()
        {
            CurrentItem.IsMale = _itemForm.rbMale.IsChecked ?? false;
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();
            int workerId = CurrentItem.Id;
            string login = CurrentItem.WorkerLogin;
            if (!StringValidator.IsValid(login))
            {
                errorBuilder.AppendLine("Поле \"Логин\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            else if (App.Context.Workers.Where(w => w.WorkerLogin == login && w.Id != workerId).Any())
            {
                errorBuilder.AppendLine("Такой \"Логин\" уже занят;");
            }
            if (!StringValidator.IsValid(CurrentItem.Lastname))
            {
                errorBuilder.AppendLine("Поле \"Фамилия\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Firstname))
            {
                errorBuilder.AppendLine("Поле \"Имя\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!StringValidator.IsValid(CurrentItem.Patronymic))
            {
                errorBuilder.AppendLine("Поле \"Отчество\" обязательно для заполнения, максимальная длина - 255 символов;");
            }
            if (!PhoneNumberValidator.IsValid(CurrentItem.PhoneNumber))
            {
                errorBuilder.AppendLine("Неверное значение поля \"Номер телефона\";");
            }
            if (CurrentItem.Post == null)
            {
                errorBuilder.AppendLine("Свойство \"Должность\" обязательно для заполнения;");
            }
            if (CurrentItem.DateOfBirthday == null || CurrentItem.DateOfBirthday < new DateTime(1900, 1, 1) || CurrentItem.DateOfBirthday > new DateTime(3000, 12, 31))
            {
                errorBuilder.AppendLine("Свойство \"Дата рождения\" обязательно для заполнения, допустимые значения от 1900.01.01 до 3000.12.31;");
            }
            return errorBuilder.ToString();
        }

        /// <summary>
        /// Метод, создающий и открывающий окно изменения пароля.
        /// </summary>
        private void ShowChangePasswordForm()
        {
            _changePasswordForm = new ChangePasswordForm(this);
            _changePasswordForm.ShowDialog();
        }

        /// <summary>
        /// Метод, изменяющий пароль, если тот не слабый.
        /// </summary>
        public void ChangePassword()
        {
            EvaluatePassword();
            if (_passwordComplexity == PasswordComplexity.Weak)
            {
                MessageBox.Show("Ваш пароль - слабый");
            }
            else
            {
                CurrentItem.WorkerPassword = CryptionService.HashSHA256(_changePasswordForm.passwordBox.Password);
                _changePasswordForm.Close();
            }
        }
    }
}
