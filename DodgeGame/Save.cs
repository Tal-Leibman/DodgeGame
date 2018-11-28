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
        private int _laserAmmo;

        public Player Player { get { return _player; } }
        public Enemy[] Enemies { get { return _enemies; } }
        public int LaserAmmo { get { return _laserAmmo; } }

        // save the state of a game

        public Save(Player player, Enemy[] enemies, int laserAmmo)
        {
            _player = new Player(player.X, player.Y, player.Radius);
            _enemies = new Enemy[enemies.Length];

            // copy the x , y ,speed and radius of each enemy in array

            for (int i = 0; i < enemies.Length; i++)
            {
                _enemies[i] =
                    new Enemy(
                    enemies[i].X,
                    enemies[i].Y,
                    enemies[i].Speed,
                    enemies[i].Radius);

                if (!enemies[i].IsAlive)
                {
                    _enemies[i].Dead();
                }
            }

            _laserAmmo = laserAmmo;
        }

    }
}
