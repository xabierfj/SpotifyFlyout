using Microsoft.UI;
using System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using SpotifyFlyout.ViewModels;
using Windows.Graphics;
using WinRT.Interop;
using System.Runtime.InteropServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SpotifyFlyout
{
    public sealed partial class MainWindow : Window
    {
        private AppWindow _appWindow;

        public MainViewModel ViewModel { get; } = new MainViewModel();
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(null);

            GetAppWindow();
            CustomizeWindow();
            Test.DataContext = ViewModel;
        }

        private void CustomizeWindow()
        {
            if (_appWindow != null)
            {
                _appWindow.SetPresenter(AppWindowPresenterKind.Overlapped); // No title bar, no resize controls
                _appWindow.Resize(new SizeInt32(300, 200)); // Set window size
                _appWindow.IsShownInSwitchers = false;
                PositionFlyout();
            }
        }

        private void GetAppWindow()
        {
            var hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            _appWindow = AppWindow.GetFromWindowId(myWndId);
        }
        private void PositionFlyout()
        {
            var displayArea = DisplayArea.GetFromWindowId(_appWindow.Id, DisplayAreaFallback.Primary);
            var workArea = displayArea.WorkArea;

            // Position at the top-right corner
            int x = (int)(workArea.X + workArea.Width - 300); // 300 = Window Width
            int y = (int)workArea.Y; // Align to top

            _appWindow.Move(new PointInt32(x, y));
        }
    }
}
