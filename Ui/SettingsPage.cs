using System.Collections.Generic;
using System.Linq;
using Game;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Ui
{
    public sealed partial class SettingsPage : Page
    {
        private Settings _settings;

        private IEnumerable<int> _zeroTo20;
        private IEnumerable<int> _twoTo20;
        private IEnumerable<int> _twoTo50;
        private int[] _miliSec;

        public SettingsPage()
        {
            InitializeComponent();
            int[] arr = new int[51];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
            _miliSec = new int[31];
            for (int i = 0; i < _miliSec.Length; i++)
            {
                _miliSec[i] = i * 100;
            }

            _zeroTo20 = arr.Take(21);
            _twoTo20 = arr.Skip(2).Take(19);
            _twoTo50 = arr.Skip(2).Take(49);
        }

        private void Button_back_Click(object sender,RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),_settings);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _settings = e.Parameter as Settings;
            if (_settings != null)
            {
                enemyStartCount.SelectedItem = _settings.EnemyStartingCount;
                maxRadiusEnemy.SelectedItem = (int)_settings.EnemyMaxRadius;
                minRadiusEnemy.SelectedItem = (int)_settings.EnemyMinRadius;
                maxSpeedEnemy.SelectedItem = (int)_settings.EnemyMaxSpeed;
                minSpeedEnemy.SelectedItem = (int)_settings.EnemyMinSpeed;
                playerRadius.SelectedItem = (int)_settings.HumanRadius;
                playerSpeed.SelectedItem = (int)_settings.HumanSpeed;
                respawnRate.SelectedItem = _settings.RespawnRate;
                playerColor.Color = _settings.HumanColor;
            }
        }

        private void Button_save_Click(object sender,RoutedEventArgs e)
        {
            Color a = playerColor.Color;

            _settings = new Settings
            {
                EnemyMaxRadius = (int)maxRadiusEnemy.SelectedItem,
                EnemyMaxSpeed = (int)maxSpeedEnemy.SelectedItem,
                EnemyMinRadius = (int)minRadiusEnemy.SelectedItem,
                EnemyMinSpeed = (int)minSpeedEnemy.SelectedItem,
                HumanRadius = (int)playerRadius.SelectedItem,
                HumanSpeed = (int)playerSpeed.SelectedItem,
                HumanColor = playerColor.Color,
                EnemyStartingCount = (int)enemyStartCount.SelectedItem,
                RespawnRate = ((int)respawnRate.SelectedItem),
            };
        }

        private void Button_load_Click(object sender,RoutedEventArgs e)
        {
        }

        private void Button_clear_Click(object sender,RoutedEventArgs e)
        {
        }

        private void PlayerColor_ColorChanged(ColorPicker sender,ColorChangedEventArgs args)
        {
        }
    }
}