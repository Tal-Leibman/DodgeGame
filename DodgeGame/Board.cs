using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class Board
    {

        private Player _player;
        private Enemy[] _enemies;
        // board X dimension 
        public double BoardX { get; }
        // board Y dimension 
        public double BoardY { get; }
        // user controlled player on the board
        public Player Player { get { return _player; } }
        //an array of enemies 
        public Enemy[] Enemies { get { return _enemies; } }

        private Random _rnd = new Random();


        /*
         * Builds a new game board with default values.
         * if boardSizeX or boardSizeY is changed from default, check MainPage.xaml 
         * to make sure it fits.
         * spawns the enemies and the player on random locations and checks they are 
         * spread out on the board.
         */
        public Board(
            int enemyCount = 10,
            double enemySpeed = 9,
            double boardSizeX = 1200,
            double boardSizeY = 600)

        {

            BoardX = boardSizeX;
            BoardY = boardSizeY;

            _player = new Player();
            //place Player at random on the board with offset from the sides
            _player.LoadEntityData(RandomX(_player), RandomY(_player), _player.Radius);

            _enemies = new Enemy[enemyCount];

            bool IsPlaceEmpty = false;
            // set first enemy, check if place is empty
            do
            {
                _enemies[0] = new Enemy(0, 0, enemySpeed);

                _enemies[0].LoadEntityData(
                    RandomX(_enemies[0]),
                    RandomY(_enemies[0]),
                    _enemies[0].Radius);

                IsPlaceEmpty = IsPlacement(_enemies[0], _player);

            } while (!IsPlaceEmpty);
            //set rest of enemies check if place is empty for each one
            int i = 1;
            do
            {
                _enemies[i] = new Enemy(0, 0, enemySpeed);

                _enemies[i].LoadEntityData(
                    RandomX(_enemies[i]), 
                    RandomY(_enemies[i]), 
                    _enemies[i].Radius);

                IsPlaceEmpty =
                    IsPlacement(_enemies[i], _player) &&
                    IsPlacement(_enemies[i], _enemies[0]);
                if (IsPlaceEmpty)
                {
                    for (int j = 1; j < i; j++)
                    {
                        IsPlaceEmpty = IsPlacement(_enemies[i], _enemies[j]);
                        if (!IsPlaceEmpty)
                        {
                            break;
                        }
                    }
                }

                if (IsPlaceEmpty)
                {
                    i++;
                }

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
            return distance > (entity1.Radius + entity2.Radius) * 4;
        }

        //Functions for Constructor END

        //Functions for movement and collisions START

        // moves all the enemies on the board
        private void MoveEnemies()
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].Move(Player.X, Player.Y);
            }
        }

        // check for collision entity1 always dies
        private void Collison(Entity entity1, Entity entity2)
        {
            if (entity1.IsAlive && entity2.IsAlive)
            {
                double deltaX = entity1.X - entity2.X;
                double deltaY = entity1.Y - entity2.Y;
                double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
                if (distance < entity1.Radius + entity2.Radius)
                {
                    entity1.Dead();
                }
            }
        }

        //check collisions between enemies 
        private void EnemyVsEnemy()
        {
            for (int i = 0; i < _enemies.Length; i++)
            {
                for (int j = 0; j < _enemies.Length; j++)
                {
                    if (i != j)
                    {
                        Collison(_enemies[i], _enemies[j]);
                    }
                }
            }
        }

        //check collision between player and enemies
        private void PlayerVsEnemy()
        {
            foreach (Enemy enemy in _enemies)
            {
                Collison(_player, enemy);
            }
        }

        public void GameCycle()
        {
            MoveEnemies();
            PlayerVsEnemy();
            EnemyVsEnemy();
        }

        //Functions for movement and collisions END

        //check if player\enemies are dead to end game
        public string GameState()
        {
            if (!_player.IsAlive)
            {
                return "GameLost";
            }
            else if (AreEnemiesDead())
            {
                return "GameWon";
            }
            else
            {
                return "GameOn";
            }
        }

        // returns true if all enemies are dead
        private bool AreEnemiesDead()
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy.IsAlive)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
