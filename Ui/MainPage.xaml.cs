using System;
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
        private Settings _settings;
        private Engine _engine;
        private DispatcherTimer _mainTimer;
        private DispatcherTimer _newEnemyTimer;
        private PlayerInput _playerInput;

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

        private void NewEnemyTimer_Tick(object sender,object e)
        {
            Enemy tmp = _engine.AddEnemy();
            canvas_game.Children.Add(tmp.Circle);
            Canvas.SetLeft(tmp.Circle,tmp.X - tmp.Radius);
            Canvas.SetTop(tmp.Circle,tmp.Y - tmp.Radius);
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

        private void Timer_Tick(object sender,object e)
        {
            GameState state = _engine.GameCycle(_playerInput);
            switch (state)
            {
                case GameState.EnemiesAreSame:
                    MoveObjectsOnCanvas();
                    break;

                case GameState.EnemiesChanged:
                    ReDrawCanvas();
                    break;

                case GameState.Lost:
                    ReDrawCanvas();
                    GameOver(GameState.Lost);
                    break;

                case GameState.Won:
                    ReDrawCanvas();
                    GameOver(GameState.Won);
                    break;

                default: break;
            }
        }

        private void ReDrawCanvas()
        {
            canvas_game.Children.Clear();
            canvas_game.Children.Add(_engine.Human.Circle);
            Canvas.SetLeft(_engine.Human.Circle,_engine.Human.X - _engine.Human.Radius);
            Canvas.SetTop(_engine.Human.Circle,_engine.Human.Y - _engine.Human.Radius);
            _engine.Enemies.ForEach(e =>
            {
                canvas_game.Children.Add(e.Circle);
                Canvas.SetLeft(e.Circle,e.X - e.Radius);
                Canvas.SetTop(e.Circle,e.Y - e.Radius);
            });
        }

        private void GameOver(GameState state)
        {
            _newEnemyTimer.Stop();
            _mainTimer.Stop();
            _settings.HighScore = Math.Max(_settings.HighScore,_engine.Score);
        }

        private void MoveObjectsOnCanvas()
        {
            _engine.Enemies.ForEach(e =>
            {
                Canvas.SetLeft(e.Circle,e.X - e.Radius);
                Canvas.SetTop(e.Circle,e.Y - e.Radius);
            });
            Canvas.SetLeft(_engine.Human.Circle,_engine.Human.X - _engine.Human.Radius);
            Canvas.SetTop(_engine.Human.Circle,_engine.Human.Y - _engine.Human.Radius);
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Settings newSet)
            {
                _settings = newSet;
            }
        }

        private void DefaultSettings()
        {
            double height = stackPanel.ActualHeight - commandBar.ActualHeight;
            _settings = new Settings
            {
                BoardHeight = height,
                BoardWidth = canvas_game.ActualWidth,
                EnemyStartingCount = 6,
                EnemyMaxRadius = 35,
                EnemyMinRadius = 4,
                HumanRadius = 12,
                HumanSpeed = 12,
                HumanColor = Colors.Green,
                EnemyMaxSpeed = 10,
                EnemyMinSpeed = 6,
                RespawnRate = 500
            };
        }

        private void Button_newGame_Click(object sender,RoutedEventArgs e)
        {
            canvas_game.Children.Clear();
            if (_settings == null)
            {
                DefaultSettings();
            }
            else
            {
                double height = stackPanel.ActualHeight - commandBar.ActualHeight;
                _settings.BoardHeight = height;
                _settings.BoardWidth = canvas_game.ActualWidth;
            }
            _playerInput = new PlayerInput();
            _engine = new Engine(_settings);
            ReDrawCanvas();

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
            Frame.Navigate(typeof(SettingsPage),_settings);
        }
    }
}