using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfApp1.Views;
using WpfApp1.Models;
using WpfApp1.Services;
using WpfApp1.Views.Workers.WorkersList;
using System.Windows.Controls;
using PasswordEvaluatorLib;
using System.Windows;

namespace WpfApp1.ViewModels.Workers
{
    public class WorkersListViewModel : SectionWidgetViewModel
    {
        private WorkersListItem _itemForm;
        public override ItemForm ItemForm
        {
            get => _itemForm as object as ItemForm;
            set => _itemForm = value as WorkersListItem;
        }

        private ObservableCollection<dynamic> _sectionData;
        public override ObservableCollection<dynamic> SectionData
        {
            get => _sectionData;
            set => _sectionData = value;
        }

        private dynamic _currentItem;
        public override dynamic? CurrentItem
        {
            get => _currentItem;
            set => _currentItem = value;
        }

        private RelayCommand? _tryChangePasswordCommand;
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

        private RelayCommand? _changePasswordCommand;
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

        private WorkerService _workerService;
        private ChangePasswordForm _changePasswordForm;

        public List<Posts> Posts { get; set; }

        private PasswordEvaluator _passwordEvaluator;
        private PasswordComplexity _passwordComplexity;
        public string RusPasswordComplexity
        {
            get
            {
                EvaluatePassword();
                return PasswordEvaluator.PasswordComplexityToRus(_passwordComplexity);
            }
        }

        public WorkersListViewModel(SectionWidget sectionWidget) : base(sectionWidget) {
            _workerService = App.WorkerService;
            _passwordEvaluator = new();
            UpdateSectionData();
        }

        private void EvaluatePassword()
        {
            _passwordComplexity = _passwordEvaluator.EvaluatePassword(_changePasswordForm.passwordBox.Password);
        }

        protected override void MakeCurrentItemEmpty()
        {
            _currentItem = new Models.Workers();
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
            _sectionData = _workerService.GetWorkers();
        }

        protected override void FillItem()
        {
            CurrentItem.IsMale = _itemForm.rbMale.IsChecked ?? false;
        }

        protected override string GetErrors()
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(CurrentItem.WorkerLogin))
            {
                errorBuilder.AppendLine("Поле \"Логин\" обязательно для заполнения;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Lastname))
            {
                errorBuilder.AppendLine("Поле \"Фамилия\" обязательно для заполнения;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Firstname))
            {
                errorBuilder.AppendLine("Поле \"Имя\" обязательно для заполнения;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.Patronymic))
            {
                errorBuilder.AppendLine("Поле \"Отчество\" обязательно для заполнения;");
            }
            if (string.IsNullOrWhiteSpace(CurrentItem.PhoneNumber))
            {
                errorBuilder.AppendLine("Поле \"Номер телефона\" обязательно для заполнения;");
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

        private void ShowChangePasswordForm()
        {
            _changePasswordForm = new ChangePasswordForm(this);
            _changePasswordForm.ShowDialog();
        }

        public void ChangePassword()
        {
            EvaluatePassword();
            if (_passwordComplexity == PasswordComplexity.Weak)
            {
                MessageBox.Show("Ваш пароль - слабый");
            }
            else
            {
                CurrentItem.WorkerPassword = CryptionService.hashSHA255(_changePasswordForm.passwordBox.Password);
                _changePasswordForm.Close();
            }
        }

    }
}
