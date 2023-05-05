using System;
using System.Windows.Input;

namespace WpfApp1
{
    /// <summary>
    /// Реализация интерфейса ICommand, позволяющая использовать механизм команд в WPF.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Функция, которую нужно выполнить.
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Условие выполнения команды.
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Событие, возникающее, когда изменяется условие выполнения команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Конструктор класса RelayCommand. В качестве параметров принимает метод для выполнения и условие выполнения команды.
        /// </summary>
        /// <param name="execute">Функция, которую нужно выполнить.</param>
        /// <param name="canExecute">Условие выполнения команды.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Метод, определяющий, может ли выполниться команда.
        /// </summary>
        /// <param name="parameter">Параметр условия выполнения.</param>
        /// <returns>Истина, если услвоие выполнения соблюдено.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
