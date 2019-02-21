using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class Engine
    {
        public Queue<Enemy> Enemies { get; private set; }
        public Player Human { get; private set; }
        public Settings Settings { get; set; }
        public GameState GameState { get; private set; }

        public Enemy GameCycle(bool up, bool down, bool left, bool right)
        {
            Human.Move(up, down, left, right, Settings);
            PlayerVsEnemy();
            MoveEnemies();
            return EnemyVsEnemy();
        }


        private static Random rnd = new Random();

        public Engine(Settings settings)
        {
            Settings = settings;
            Enemies = new Queue<Enemy>();
            GameState = GameState.Live;
            Human = new Player(Settings);
            Human.PlaceOnBoard(GetXInRange(Human), GetYInRange(Human));

            while (Enemies.Count < Settings.EnemyStartingCount)
            {
                AddEnemy();
            }
        }

        public Enemy AddEnemy()
        {
            while (true)
            {
                Enemy tmp = new Enemy(Settings);
                tmp.PlaceOnBoard(GetXInRange(tmp), GetYInRange(tmp));
                bool empty = IsPlaceEmpty(Human, tmp);
                if (empty)
                {
                    bool collideWithEnemy = false;
                    foreach (Enemy enemy in Enemies)
                    {
                        if (!IsPlaceEmpty(enemy, tmp))
                        {
                            collideWithEnemy = true;
                            break;
                        }
                    }
                    if (!collideWithEnemy)
                    {
                        Enemies.Enqueue(tmp);
                        return tmp;
                    }
                }
            }
        }



        private bool Collison(Entity entity1, Entity entity2)
        {
            double deltaX = entity1.X - entity2.X;
            double deltaY = entity1.Y - entity2.Y;
            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            if (distance < entity1.Radius + entity2.Radius)
            {
                return true;
            }
            return false;
        }

        private bool IsPlaceEmpty(Entity e1, Entity e2)
        {
            double deltaX = e1.X - e2.X;
            double deltaY = e1.Y - e2.Y;
            double distance = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            return distance > e1.Radius * Settings.PlacementBuffer;
        }

        private int GetXInRange(Entity e)
        {
            int range = (int)(Settings.BoardWidth - e.Radius);
            return rnd.Next((int)e.Radius, range);
        }

        private int GetYInRange(Entity e)
        {
            int range = (int)(Settings.BoardHeight - e.Radius);
            return rnd.Next((int)e.Radius, range);
        }

        private Enemy EnemyVsEnemy()
        {
            if (Enemies.Count > 0)
            {
                if (Enemies.Count == 1) { return Enemies.Dequeue(); }
                Enemy first = Enemies.Peek();
                do
                {
                    Enemy tmp = Enemies.Dequeue();
                    foreach (Enemy enemy in Enemies)
                    {
                        if (Collison(tmp, enemy))
                        {
                            return tmp;
                        }
                    }
                    Enemies.Enqueue(tmp);
                    if (Enemies.Peek() == first) { return null; }
                }
                while (Enemies.Count > 1);
            }
            GameState = GameState.Won;
            return null;
        }

        private void PlayerVsEnemy()
        {
            foreach (Enemy enemy in Enemies)
            {
                if (Collison(Human, enemy))
                {
                    GameState = GameState.Lost;
                }
            }
        }


        private void MoveEnemies()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Move(Human.X, Human.Y);
            }
        }


    }
}
