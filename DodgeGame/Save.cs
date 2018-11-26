using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeGame
{
    class Save
    {
        private Player _player;
        private Enemy[] _enemies;
        private int _aliveEnemies;
        private int _laserAmmo;

        public Player Player { get { return _player; } }
        public Enemy[] Enemies { get { return _enemies; } }
        public int AliveEnemies { get { return _aliveEnemies; } }
        public int LaserAmmo { get { return _laserAmmo; } }


        public Save(Player player, Enemy[] enemies, int aliveEnemies, int laserAmmo)
        {
            _player = new Player(player.X, player.Y, player.Radius);
            _enemies = new Enemy[enemies.Length];
            for (int i = 0; i < enemies.Length; i++)
            {
                _enemies[i] = new Enemy(
                    (int)enemies[i].X, (int)enemies[i].Y,
                    enemies[i].Speed, enemies[i].Radius);
                if (!enemies[i].IsAlive)
                {
                    _enemies[i].Dead();
                }
            }
            _aliveEnemies = aliveEnemies;
            _laserAmmo = laserAmmo;
        }

    }
}
