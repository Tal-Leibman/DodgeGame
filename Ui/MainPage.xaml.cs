using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Ui
{
    public sealed partial class MainPage : Page
    {
        public int counter;
        public int maxScore;
        Settings settings;
        Engine engine;
        DispatcherTimer mainTimer;
        DispatcherTimer newEnemyTimer;
        bool up;
        bool down;
        bool left;
        bool right;

        public MainPage()
        {
            MaximizeWindowOnLoad();
            
            this.InitializeComponent();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;


            

            mainTimer = new DispatcherTimer();
            mainTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            mainTimer.Tick += Timer_Tick;

        }

        private void NewEnemyTimer_Tick(object sender, object e)
        {
            Enemy tmp = engine.AddEnemy();
            canvas_game.Children.Add(tmp.Circle);
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    up = false;
                    break;
                case VirtualKey.Down:
                    down = false;
                    break;
                case VirtualKey.Left:
                    left = false;
                    break;
                case VirtualKey.Right:
                    right = false;
                    break;
                default: break;
            }
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    up = true;
                    break;
                case VirtualKey.Down:
                    down = true;
                    break;
                case VirtualKey.Left:
                    left = true;
                    break;
                case VirtualKey.Right:
                    right = true;
                    break;
                default: break;
            }
        }



        private void Timer_Tick(object sender, object e)
        {
            Entity tmp = engine.GameCycle(up, down, left, right);
            if (tmp != null)
            {
                counter++;
                commandBar.Content = string.Format("Score: {0} , High Score: {1}", counter, maxScore);
                canvas_game.Children.Remove(tmp.Circle);
            }
            UpdateCanvas();
            if (engine.GameState == GameState.Lost || engine.GameState == GameState.Won)
            {
                mainTimer.Stop();
                newEnemyTimer.Stop();
            }
        }

        private void UpdateCanvas()
        {
            foreach (Enemy item in engine.Enemies)
            {
                Canvas.SetLeft(item.Circle, item.X - item.Radius);
                Canvas.SetTop(item.Circle, item.Y - item.Radius);
            }
            Canvas.SetLeft(engine.Human.Circle, engine.Human.X - engine.Human.Radius);
            Canvas.SetTop(engine.Human.Circle, engine.Human.Y - engine.Human.Radius);
        }



        private void MaximizeWindowOnLoad()
        {
            var view = DisplayInformation.GetForCurrentView();

            // Get the screen resolution (APIs available from 14393 onward).
            var resolution = new Size(view.ScreenWidthInRawPixels, view.ScreenHeightInRawPixels);

            // Calculate the screen size in effective pixels. 
            // Note the height of the Windows Taskbar is ignored here since the app will only be given the maxium available size.
            double scale = view.ResolutionScale == ResolutionScale.Invalid ? 1 : view.RawPixelsPerViewPixel;
            Size bounds = new Size(resolution.Width / scale, resolution.Height / scale);
            ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width, bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void TestSettings()
        {
            double height = stackPanel.ActualHeight - commandBar.ActualHeight;
            settings = new Settings
            {
                Ammo = 0,
                BoardHeight = height,
                BoardWidth = canvas_game.ActualWidth,
                EnemyStartingCount = 6,
                EnemyMaxRadius = 35,
                EnemyMinRadius = 4,
                HumanRadius = 10,
                HumanSpeed = 12,
                EnemyMaxSpeed = 10,
                EnemyMinSpeed = 6,
                PlacementBuffer = 4,
                RespawnRate = 500
            };
        }

        private void Button_newGame_Click(object sender, RoutedEventArgs e)
        {
            maxScore = Math.Max(counter,maxScore);
            counter = 0;
            commandBar.Content = string.Format("Score: {0} , High Score: {1}", counter,maxScore);
            canvas_game.Children.Clear();
            TestSettings();
            engine = new Engine(settings);
            foreach (Enemy item in engine.Enemies)
            {
                canvas_game.Children.Add(item.Circle);
            }
            canvas_game.Children.Add(engine.Human.Circle);
            newEnemyTimer = new DispatcherTimer();
            newEnemyTimer.Interval = new TimeSpan(0,0,0,0,settings.RespawnRate);
            newEnemyTimer.Tick += NewEnemyTimer_Tick;
            newEnemyTimer.Start();
            mainTimer.Start();
        }


    }
}

