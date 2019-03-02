using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ui
{
    public sealed partial class MainPage : Page
    {
        public Engine _engine;
        private DispatcherTimer _mainTimer;
        private DispatcherTimer _newEnemyTimer;
        private PlayerInput _playerInput = new PlayerInput();
        private Settings _settings;

        public MainPage()
        {
            MaximizeWindowOnLoad();

            InitializeComponent();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            _mainTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0,0,0,0,20)
            };
            _mainTimer.Tick += Timer_Tick;
        }

        private void Button_newGame_Click(object sender,RoutedEventArgs e)
        {
            canvas_game.Children.Clear();
            _settings = Settings.Init;
            _settings.BoardHeight = stackPanel.ActualHeight - commandBar.ActualHeight;
            _settings.BoardWidth = canvas_game.ActualWidth;
            _playerInput = new PlayerInput();
            _engine = new Engine(_settings);
            ReDrawCanvas(_engine.Human,_engine.Enemies);
            _newEnemyTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0,0,0,0,_settings.RespawnRate)
            };
            _newEnemyTimer.Tick += NewEnemyTimer_Tick;
            _newEnemyTimer.Start();
            _mainTimer.Start();
        }

        private void Button_settings_Click(object sender,RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender,Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    _playerInput.Up = true;
                    break;

                case VirtualKey.Down:
                    _playerInput.Down = true;
                    break;

                case VirtualKey.Left:
                    _playerInput.Left = true;
                    break;

                case VirtualKey.Right:
                    _playerInput.Right = true;
                    break;

                default: break;
            }
        }

        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender,Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.Up:
                    _playerInput.Up = false;
                    break;

                case VirtualKey.Down:
                    _playerInput.Down = false;
                    break;

                case VirtualKey.Left:
                    _playerInput.Left = false;
                    break;

                case VirtualKey.Right:
                    _playerInput.Right = false;
                    break;

                default: break;
            }
        }

        private void DrawEntityOnCanvas(Entity e)
        {
            canvas_game.Children.Add(e.Circle);
            Canvas.SetLeft(e.Circle,e.X - e.Radius);
            Canvas.SetTop(e.Circle,e.Y - e.Radius);
        }

        private void GameOver(GameState state)
        {
            _settings.HighScore = Math.Max(_settings.HighScore,_engine.CurrentScore);
            ShowScore();
            _newEnemyTimer.Stop();
            _mainTimer.Stop();
        }

        private void ShowScore()
        {
            commandBar.Content = string.Format("Score: {0} | High score: {1}",_engine.CurrentScore,_settings.HighScore);
        }

        private void MaximizeWindowOnLoad()
        {
            DisplayInformation view = DisplayInformation.GetForCurrentView();

            // Get the screen resolution (APIs available from 14393 onward).
            Size resolution = new Size(view.ScreenWidthInRawPixels,view.ScreenHeightInRawPixels);

            // Calculate the screen size in effective pixels.
            // Note the height of the Windows Taskbar is ignored here since the app will only be given the maxium available size.
            double scale = view.ResolutionScale == ResolutionScale.Invalid ? 1 : view.RawPixelsPerViewPixel;
            Size bounds = new Size(resolution.Width / scale,resolution.Height / scale);
            ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width,bounds.Height);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void NewEnemyTimer_Tick(object sender,object e)
        {
            Enemy newEnemy = _engine.AddEnemy();
            DrawEntityOnCanvas(newEnemy);
        }

        private void ReDrawCanvas(Player human,List<Enemy> survivors)
        {
            canvas_game.Children.Clear();
            DrawEntityOnCanvas(human);
            survivors.ForEach(e =>
            {
                DrawEntityOnCanvas(e);
            });
        }

        private void Timer_Tick(object sender,object e)
        {
            GameState state = _engine.GameCycle(_playerInput);
            ReDrawCanvas(_engine.Human,_engine.Enemies);
            ShowScore();
            switch (state)
            {
                case GameState.Lost:
                    GameOver(GameState.Lost);
                    break;

                case GameState.Won:
                    GameOver(GameState.Won);
                    break;

                default: break;
            }
        }
    }
}
