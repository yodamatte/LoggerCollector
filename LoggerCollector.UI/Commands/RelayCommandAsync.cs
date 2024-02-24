using System.Windows.Input;

namespace LoggerCollector.UI.Commands;

public class RelayCommandAsync<T> : ICommand
{
    private readonly Func<T, Task> _execute;
    private readonly Predicate<T> _canExecute;

    public RelayCommandAsync(Func<T, Task> execute)
        : this(execute, null)
    {
    }

    public RelayCommandAsync(Func<T, Task> execute, Predicate<T> canExecute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    ///<summary>
    ///Defines the method that determines whether the command can execute in its current state.
    ///</summary>
    ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    ///<returns>
    ///true if this command can be executed; otherwise, false.
    ///</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute((T)parameter);
    }


    public async void Execute(object parameter)
    {
        await ExecuteAsync((T)parameter);
    }

    public async Task ExecuteAsync(T parameter)
    {
        await _execute(parameter);
    }

    ///<summary>
    ///Occurs when changes occur that affect whether or not the command should execute.
    ///</summary>
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
