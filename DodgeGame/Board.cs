using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class Board
    {   
        //counter for live enemies on board
        private int AliveEnemies { get; set; }
        // board X dimension 
        public double BoardX { get; }
        // board Y dimension 
        public double BoardY { get; }
        // user controlled player on the board
        public Player Player { get; }
        //an array of enemies 
        public Enemy[] Enemies { get; set; }

        Random _rnd = new Random();
        // data members for the save function
        private int _saveEnemyCount;
        private Player _savePlayer;
        private Enemy[] _saveEnemies;


        /*
         * Builds a new game board with default values.
         * if boardSizeX or boardSizeY is changed from default, check MainPage.xaml 
         * to make sure it fits.
         * spawns the enemies and the player on random locations and checks they are 
         * spread out on the board.
         */
        public Board(
            int enemyCount = 10,
            double enemySpeed = 6,
            double boardSizeX = 1000,
            double boardSizeY = 600)

        {

            BoardX = boardSizeX;
            BoardY = boardSizeY;
            AliveEnemies = enemyCount;
            Player = new Player();
            Player.X = RandomX(Player);
            Player.Y = RandomY(Player);
            Enemies = new Enemy[enemyCount];
            //data members to save game
            _savePlayer = new Player();
            _saveEnemies = new Enemy[enemyCount];
            for (int k = 0; k < _saveEnemies.Length; k++)
            {
                _saveEnemies[k] = new Enemy(0,0,enemySpeed);
            }

            bool placeEmpty = false;
            // set first enemy, check if place is empty
            do
            {
                Enemies[0] = new Enemy(0,0,enemySpeed);
                Enemies[0].X = RandomX(Enemies[0]);
                Enemies[0].Y = RandomY(Enemies[0]);
                placeEmpty = IsPlacement(Enemies[0], Player);

            } while (!placeEmpty);
            //set rest of enemies check if place is empty for each one
            int i = 1;
            do
            {
                Enemies[i] = new Enemy(0,0,enemySpeed);
                Enemies[i].X = RandomX(Enemies[i]);
                Enemies[i].Y = RandomY(Enemies[i]);

                placeEmpty =
                    IsPlacement(Enemies[i], Player) &&
                    IsPlacement(Enemies[i], Enemies[0]);
                if (placeEmpty)
                {
                    for (int j = 1; j < i; j++)
                    {
                        placeEmpty = IsPlacement(Enemies[i], Enemies[j]);
                        if (!placeEmpty) { break; }
                    }
                }
                if (placeEmpty) { i++; }

            } while (i < enemyCount);

        }

        //Functions for Constructor START

        //gets a random x location based on board size X and entity radius
        private int RandomX(Entity entity)
        {

            return
                _rnd.Next((int)entity.Radius, (int)(BoardX - entity.Radius));
        }

        //gets a random y location based on board size Y and entity radius
        private int RandomY(Entity entity)
        {
            return
                _rnd.Next((int)entity.Radius, (int)(BoardY - entity.Radius));
        }

        // check if the board is empty to spawn new enemy
        private bool IsPlacement(Entity entity1, Entity entity2)
        {
            double deltaX = entity1.X - entity2.X;
            double deltaY = entity1.Y - entity2.Y;
            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            return distance > (entity1.Radius + entity2.Radius) * 3.5;
        }

        //Functions for Constructor END

        //Functions for movement and collisions START

        // moves all the enemies on the board and checks collisions 
        public void MoveEnemies()
        {
            for (int i = 0; i < Enemies.Length && Player.IsAlive; i++)
            {
                // move Enemies
                Enemies[i].Move(Player.X, Player.Y);
                // Check player alive
                PlayerVsEnemy();
                //check collisions
                EnemyVsEnemy();
            }
        }

        // check for collision entity1 always dies
        private void IsCollison(Entity entity1, Entity entity2)
        {
            if (entity1.IsAlive && entity2.IsAlive)
            {
                double deltaX = entity1.X - entity2.X;
                double deltaY = entity1.Y - entity2.Y;
                double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                if (distance < entity1.Radius + entity2.Radius)
                {
                    entity1.Dead();
                    if (entity1 is Enemy)
                    {
                        AliveEnemies--;
                    }
                }
            }
        }

        //check collisions between enemies 
        private void EnemyVsEnemy()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                for (int j = 0; j < Enemies.Length; j++)
                {
                    if (i != j)
                    {
                        IsCollison(Enemies[i], Enemies[j]);
                    }
                }
            }
        }

        //check collision between player and enemies
        private void PlayerVsEnemy()
        {
            foreach (Enemy enemy in Enemies)
            {
                IsCollison(Player, enemy);
            }
        }

        //Functions for movement and collisions END

        //Kill all enemies when game is won
        private void KillAllEnemies()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].IsAlive)
                {
                    Enemies[i].Dead();
                }
            }
        }

        //check if player is dead or 1 enemy left to end game
        public string GameState()
        {
            if (!Player.IsAlive)
            {
                return "GameLost";
            }
            else if (AliveEnemies == 1)
            {
                KillAllEnemies();
                return "GameWon";
            }
            else
            {
                return "GameOn";
            }
        }

        //save game state
        public void Save()
        {
            if (Player.IsAlive)
            {
                _saveEnemyCount = AliveEnemies;
                _savePlayer.X = Player.X;
                _savePlayer.Y = Player.Y;
                _savePlayer.IsAlive = Player.IsAlive;
                for (int i = 0; i < Enemies.Length; i++)
                {
                    _saveEnemies[i].X = Enemies[i].X;
                    _saveEnemies[i].Y = Enemies[i].Y;
                    _saveEnemies[i].IsAlive = Enemies[i].IsAlive;
                    _saveEnemies[i].Circle.Fill = Enemies[i].Circle.Fill;
                }
            }
        }

        //load saved game state
        public void Load()
        {
            AliveEnemies = _saveEnemyCount;
            Player.X = _savePlayer.X;
            Player.Y = _savePlayer.Y;
            Player.IsAlive = _savePlayer.IsAlive;
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].IsAlive = _saveEnemies[i].IsAlive;
                Enemies[i].X = _saveEnemies[i].X;
                Enemies[i].Y = _saveEnemies[i].Y;
                Enemies[i].Circle.Fill = _saveEnemies[i].Circle.Fill;
            }
        }
    }
}
