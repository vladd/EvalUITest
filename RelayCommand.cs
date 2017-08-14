using System;
using System.Windows.Input;

namespace EvalUITest
{
    class RelayCommand : RelayCommand<object>
    {
        public RelayCommand(Action<object> execute) : base(execute) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute) { }
    }

    class RelayCommand<T> : ICommand
    {
        protected readonly Predicate<T> _canExecute;
        protected readonly Action<T> _execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute)
            : this(execute, _ => true) { }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        public bool CanExecute(object parameter) => _canExecute((T)parameter);
        public void Execute(object parameter) => _execute((T)parameter);

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
