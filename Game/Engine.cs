using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Game
{
    public class Engine
    {
        private static Random s_rnd = new Random();

        public Engine(Settings settings)
        {
            Settings = settings;
            Enemies = new List<Enemy>();
            State = GameState.On;
            Human = new Player(Settings);
            Human.PlaceOnBoard(GetXInRange(Human),GetYInRange(Human));
            CurrentScore = 0;
            while (Enemies.Count < Settings.EnemyStartingCount)
            {
                AddEnemy();
            }
        }

        public int CurrentScore { get; set; }
        public List<Enemy> Enemies { get; private set; }
        public Player Human { get; private set; }
        public Settings Settings { get; set; }
        public GameState State { get; private set; }

        public Enemy AddEnemy()
        {
            while (true)
            {
                Enemy tmp = new Enemy(Settings);
                tmp.PlaceOnBoard(GetXInRange(tmp),GetYInRange(tmp));
                bool empty = IsPlaceEmpty(Human,tmp);
                if (empty)
                {
                    bool collideWithEnemy = false;
                    foreach (Enemy enemy in Enemies)
                    {
                        if (!IsPlaceEmpty(enemy,tmp))
                        {
                            collideWithEnemy = true;
                            break;
                        }
                    }
                    if (!collideWithEnemy)
                    {
                        Enemies.Add(tmp);
                        return tmp;
                    }
                }
            }
        }

        public GameState GameCycle(PlayerInput input)
        {
            HandleMovement(input);
            Human.IsAlive = IsPlayerAlive();
            List<Enemy> survivors = SurvivingEnemies();
            CurrentScore += Enemies.Count - survivors.Count;
            Enemies = survivors;
            State = CheckGameState();
            return State;
        }

        private GameState CheckGameState()
        {
            if (!Human.IsAlive)
            {
                return GameState.Lost;
            }
            if (Enemies.Count == 0)
            {
                return GameState.Won;
            }
            return GameState.On;
        }

        private bool Collison(Entity entity1,Entity entity2)
        {
            if (entity1 == entity2) { return false; }
            double deltaX = entity1.X - entity2.X;
            double deltaY = entity1.Y - entity2.Y;
            double distance = Math.Sqrt(Math.Pow(deltaX,2) + Math.Pow(deltaY,2));
            return distance <= entity1.Radius + entity2.Radius;
        }

        private int GetXInRange(Entity e)
        {
            int range = (int)(Settings.BoardWidth - e.Radius);
            return s_rnd.Next((int)e.Radius,range);
        }

        private int GetYInRange(Entity e)
        {
            int range = (int)(Settings.BoardHeight - e.Radius);
            return s_rnd.Next((int)e.Radius,range);
        }

        private void HandleMovement(PlayerInput input)
        {
            Human.Move(input,Settings);
            Enemies.ForEach(x => x.Move(Human.X,Human.Y,Settings));
        }

        private bool IsPlaceEmpty(Entity e1,Entity e2)
        {
            double deltaX = e1.X - e2.X;
            double deltaY = e1.Y - e2.Y;
            double distance = Math.Sqrt(Math.Pow(deltaX,2) + Math.Pow(deltaY,2));
            return distance > e1.Radius * Settings.PLACEMENT_BUFFER;
        }

        private bool IsPlayerAlive()
        {
            if (Enemies.Any(e => Collison(e,Human))) { return false; }
            return true;
        }

        private List<Enemy> SurvivingEnemies()
        {
            List<Enemy> survivors = new List<Enemy>();
            foreach (Enemy enemy in Enemies)
            {
                if (!Enemies.Any(enemy2 => Collison(enemy,enemy2)))
                {
                    survivors.Add(enemy);
                }
            }
            if (survivors.Count == Enemies.Count)
            {
                return Enemies;
            }
            else
            {
                return survivors;
            }
        }
    }
}
