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
            _settings = Settings.Init;
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

        private void Button_save_Click(object sender,RoutedEventArgs e)
        {
            _settings.EnemyMaxRadius = (int)maxRadiusEnemy.SelectedItem;
            _settings.EnemyMaxSpeed = (int)maxSpeedEnemy.SelectedItem;
            _settings.EnemyMinRadius = (int)minRadiusEnemy.SelectedItem;
            _settings.EnemyMinSpeed = (int)minSpeedEnemy.SelectedItem;
            _settings.HumanRadius = (int)playerRadius.SelectedItem;
            _settings.HumanSpeed = (int)playerSpeed.SelectedItem;
            _settings.HumanColor = playerColor.Color;
            _settings.EnemyStartingCount = (int)enemyStartCount.SelectedItem;
            _settings.RespawnRate = ((int)respawnRate.SelectedItem);
        }
    }
}
