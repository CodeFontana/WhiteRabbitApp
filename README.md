# WhiteRabbit
This is a simple WPF application to reduce how often your Windows session drops to the lock screen due to idle timeouts, which can cut down the sheer number of times you type credentials in a day.

## How to use
Compile and run. Minimize the window to send WhiteRabbit to the **system tray**. **Left‑click** the tray icon to restore the window; **right‑click** for the menu (**Open** / **Exit**) without restoring first. While minimized in the tray, the app signals that the display should stay active so typical idle-based lock behavior is less likely to trigger.

## How it works
Similar to presentation or video playback, the app uses platform invoke (`SetThreadExecutionState` in `kernel32.dll`) to request continuous display and system availability.

## Lock screen and organization policy
Windows may still lock the workstation because of **Group Policy**, **credential providers**, or other settings that are separate from display power management. This app adjusts idle/display signaling; it does **not** override mandatory lock policies. Follow your employer’s security rules and lock your session when you step away.

## Why do you need this?
If you move between several RDP sessions, repeated lock timeouts can mean typing passwords often—similar pain when you grab coffee and return to a locked desktop. Use WhiteRabbit only where it aligns with policy and personal judgment.

## Use responsibly and at your own risk
This app is not intended to weaken legitimate security controls. Lock when you should, and disconnect RDP sessions you are not using.

## For more information
https://learn.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-setthreadexecutionstate
