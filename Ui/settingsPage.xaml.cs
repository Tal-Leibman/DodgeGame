using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Ui
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class settingsPage : Page
    {
        Settings settings;

        IEnumerable<int> zeroTo20;
        IEnumerable<int> twoTo20;
        IEnumerable<int> twoTo50;
        int[] miliSec;

        public settingsPage()
        {
            this.InitializeComponent();
            int[] arr = new int[51];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
            miliSec = new int[31];
            for (int i = 0; i < miliSec.Length; i++)
            {
                miliSec[i] = i * 100;
            }

            zeroTo20 = arr.Take(21);
            twoTo20 = arr.Skip(2).Take(19);
            twoTo50 = arr.Skip(2).Take(49);

        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), settings);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            settings = e.Parameter as Settings;
            if (settings != null)
            {
                enemyStartCount.SelectedItem = settings.EnemyStartingCount;
                maxRadiusEnemy.SelectedItem = (int)settings.EnemyMaxRadius;
                minRadiusEnemy.SelectedItem = (int)settings.EnemyMinRadius;
                maxSpeedEnemy.SelectedItem = (int)settings.EnemyMaxSpeed;
                minSpeedEnemy.SelectedItem = (int)settings.EnemyMinSpeed;
                playerRadius.SelectedItem = (int)settings.HumanRadius;
                playerSpeed.SelectedItem = (int)settings.HumanSpeed;
                respawnRate.SelectedItem = settings.RespawnRate;
                playerColor.Color = settings.HumanColor;
            }
        }


        private void Button_save_Click(object sender, RoutedEventArgs e)
        {

            Color a = playerColor.Color;

            settings = new Settings
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

        private void Button_load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayerColor_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
        {

        }


    }
}
