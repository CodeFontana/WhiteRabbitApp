using System;
using System.Windows;
using WhiteRabbit.ViewModels;

namespace WhiteRabbit.Commands;
public class NavigateCommand : CommandBase
{
    private readonly MainViewModel _mainViewModel;

    public NavigateCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is string viewType)
        {
            switch (viewType)
            {
                case "Home":
                    _mainViewModel.CurrentViewModel = new HomeViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
