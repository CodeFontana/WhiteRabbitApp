using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Resources;
using WpfUI.Commands;

namespace WpfUI.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    private readonly NotifyIcon _notifyIcon = new();
    private bool _disposed;

    public MainViewModel()
    {
        NavigateCommand = new NavigateCommand(this);
        NavigateCommand.Execute("Home");
        WindowState = WindowState.Normal;
        ShowInTaskbar = true;

        _notifyIcon.Icon = LoadTrayIcon();
        _notifyIcon.Text = "WhiteRabbit Active";

        _notifyIcon.Click += (_, _) =>
        {
            WindowState = WindowState.Normal;
        };

        ContinuousDisplay();
    }

    private static Icon LoadTrayIcon()
    {
        Uri[] candidates =
        [
            new Uri("pack://application:,,,/Resources/WhiteRabbit.ico"),
            new Uri("/Resources/WhiteRabbit.ico", UriKind.Relative),
            new Uri("Resources/WhiteRabbit.ico", UriKind.Relative)
        ];

        foreach (Uri uri in candidates)
        {
            try
            {
                StreamResourceInfo? info = System.Windows.Application.GetResourceStream(uri);
                if (info?.Stream is { } stream)
                {
                    using (stream)
                    {
                        return new Icon(stream);
                    }
                }
            }
            catch (ArgumentException)
            {
                // Try next URI
            }
            catch (IOException)
            {
                // Try next URI
            }
        }

        return SystemIcons.Application;
    }

    private ViewModelBase? _currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    private WindowState _windowState;
    public WindowState WindowState
    {
        get => _windowState;
        set
        {
            _windowState = value;

            if (_windowState == WindowState.Minimized)
            {
                _notifyIcon.Visible = true;
                ShowInTaskbar = false;
            }
            else
            {
                _notifyIcon.Visible = false;
                ShowInTaskbar = true;
            }

            OnPropertyChanged(nameof(WindowState));
        }
    }

    private bool _showInTaskbar;
    public bool ShowInTaskbar
    {
        get => _showInTaskbar;
        set
        {
            _showInTaskbar = value;
            OnPropertyChanged(nameof(ShowInTaskbar));
        }
    }

    public ICommand NavigateCommand { get; set; }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [Flags]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    public static void ContinuousDisplay()
    {
        SetThreadExecutionState(
            EXECUTION_STATE.ES_CONTINUOUS |
            EXECUTION_STATE.ES_DISPLAY_REQUIRED |
            EXECUTION_STATE.ES_SYSTEM_REQUIRED);
    }

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }

        _disposed = true;
        base.Dispose(disposing);
    }
}
