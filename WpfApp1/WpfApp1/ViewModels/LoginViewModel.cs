using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        string _userLogin;
        public string UserLogin
        {
            get { return _userLogin; }
            set
            {
                _userLogin = value;
                OnPropertyChanged();
            }
        }

        byte[] _password;
        public byte[] Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        Login _loginForm;
        AccessService _accessService;
        RelayCommand? _authenticationCommand;

        public RelayCommand AuthenticationCommand
        {
            get {
                return _authenticationCommand ??
                        (_authenticationCommand = new RelayCommand((object obj) => {
                            _password = CryptionService.hashSHA255((obj as PasswordBox).Password);
                            Authentication();
                        }));
            }

        }


        public LoginViewModel(Login loginForm)
        {
            _userLogin = "";
            _accessService = App.AccessService;
            _loginForm = loginForm;
        }

        private void Authentication()
        {

            if (_accessService.IsLogin(_userLogin, _password))
            {
                MainWindow mainWindow = App.MainWindow;
                mainWindow.Show();
                _loginForm.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

    }
}
