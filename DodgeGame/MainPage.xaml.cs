﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DodgeGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Declare board
        Board board;
        DispatcherTimer timer;
        // bool for keyboard direction true when pressed , false on release
        bool up, down, left, right;
        // win lose log
        int win = 0;
        int lose = 0;
        // enemy speed and amount default 
        double enemySpeed = 6;
        int enemyCount = 10;
        string gameMode = "Normal";

        public MainPage()
        {
            this.InitializeComponent();
            // Keyboard event movement player
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            // timer for running the game
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
            Welcome();
        }



        private void Timer_Tick(object sender, object e)
        {
            // move player
            board.Player.Move(up, down, left, right, board);
            //move enemies and check collisions 
            board.MoveEnemies();
            // sync game state to ui
            SyncGameState();
            //sync ui with board
            UpdateCanvas();
        }

        //add player and enemies Ellipse to canvas 
        private void AddToCanvas()
        {
            canvasBoard.Children.Add(board.Player.Circle);
            for (int i = 0; i < board.Enemies.Length; i++)
            {
                canvasBoard.Children.Add(board.Enemies[i].Circle);
            }
        }

        //updates player and enemies location to canvas
        private void UpdateCanvas()
        {
            Canvas.SetTop(board.Player.Circle, board.Player.Y - board.Player.Radius);
            Canvas.SetLeft(board.Player.Circle, board.Player.X - board.Player.Radius);
            for (int i = 0; i < board.Enemies.Length; i++)
            {
                Canvas.SetTop(board.Enemies[i].Circle, board.Enemies[i].Y - board.Enemies[i].Radius);
                Canvas.SetLeft(board.Enemies[i].Circle, board.Enemies[i].X - board.Enemies[i].Radius);
            }
        }

        // Sync game state (win/lose) from board
        public void SyncGameState()
        {
            switch (board.GameState())
            {
                case "GameWon":
                    win++;
                    StatuesBar("You Win :)");
                    timer.Stop();
                    break;
                case "GameLost":
                    lose++;
                    StatuesBar("Game over :(");
                    timer.Stop();
                    break;
                default:
                    StatuesBar("Play");
                    break;
            }
        }

        // keyboard event for moving the player update bool up,down,left,right true
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
                case VirtualKey.Control:
                    PauseResume();
                    break;
                default:
                    break;
            }
        }

        // keyboard event when key is released  update bool up,down,left,right false
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
                default:
                    break;
            }
        }

        //starts a new game with 
        private void StartNewGame()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            up = false;
            down = false;
            left = false;
            right = false;
            canvasBoard.Children.Clear();
            board = new Board();
            canvasBoard.Height = board.BoardY;
            canvasBoard.Width = board.BoardX;
            AddToCanvas();
            timer.Start();
        }

        // change the game state from pause to UnPause 
        private void PauseResume()
        {
            if (timer.IsEnabled)
            {
                Pause.Icon = new SymbolIcon(Symbol.Play);
                StatuesBar("Pause");
                timer.Stop();
            }
            else
            {
                Pause.Icon = new SymbolIcon(Symbol.Pause);
                StatuesBar("Play");
                timer.Start();
            }
        }

        // statues bar in command bar to show game information 
        private void StatuesBar(string gameState)
        {
            GameCounter.Text = "Games won: " + win + "\tGame State:\t"+ gameState
                 + "\nGames lost: " + lose + "\tDifficulty:\t"+gameMode  ;
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            PauseResume();
        }


        private void LoadGame()
        {
            board.Load();
            canvasBoard.Children.Clear();
            AddToCanvas();
            if (!timer.IsEnabled)
            {
                PauseResume();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                PauseResume();
            }

            Welcome();
        }

        private void Hard_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Hard_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        //loads a saved game state and stops timer
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadGame();
        }

        //saves a game state and stops timer
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                PauseResume();
            }

            board.Save();
        }

        private async void Welcome()
        {
            string msg =
                "You control the space ship with the keyboard direction keys.\n" +
                "The goal is to avoid all the UFO'S.\n" +
                "Click Refresh to start a new game\n" +
                "The save button stops the game, click resume to keep playing.\n" +
                "Hit Ctrl to pause/resume" +
                "Click Help button to see this message again\n";
            await new MessageDialog(msg, "Welcome to DodgeGame").ShowAsync();
        }

    }
}
