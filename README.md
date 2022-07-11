# WhiteRabbit
This is a simple WPF application to prevent your Windows session from timing out to the lockscreen, effectively reducing the shear number of times you have to type your credentials in a given day.

## How to use
Just compile, run and enjoy. All you need to do is minimize the Window, and you'll see the White Rabbit in your system tray. To close the app, simply click on the system tray icon to restore the window, and close it. While running in the background it will prevent your system from locking due to inactivity timeout.

## How it works
Just like playing a video or doing a presentation, this app uses Platform Invoke (p/invoke) of SetThreadExecutionState() in kernel32.dll, signaling the app requires a continuous state requiring the display.  
  
## Why do you need this?
So if you're like me, and you're zipping in and out of several RDP sessions to do your work, it can be time consuming to have to type your credentials, over and over again.  
  
Picture the scenario where you need your 3pm coffee. You might have to grind the beans, wait for the water to heat up, and let the pour-over do its thing-- and by the time you return to your computer and unlock it-- all your RDP sessions are timed out to the lock screen. Or maybe you work from home, and there's no reason your system should timeout to the lock screen. Whatever your scenario, WhiteRabbit is for you!

## Use Responsibly and at Your Own Risk
This app is not designed to subvert security policies nor add risk to your organization. Remember to lock your desktop when you need to, as well as logoff any RDP sessions when you won't be using them. Use your common sense.

## For more information
https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate
