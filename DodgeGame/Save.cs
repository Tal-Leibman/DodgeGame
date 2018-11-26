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

        public Player Player { get {return _player; } }
        public Enemy[] Enemies { get { return _enemies; } } 
        public int AliveEnemies { get { return _aliveEnemies; } }
        public int LaserAmmo { get { return _laserAmmo; } }


        public Save(Player player , Enemy[] enemies , int aliveEnemies, int laserAmmo)
        {
            _player = player;
            _enemies = enemies;
            _aliveEnemies = aliveEnemies;
            _laserAmmo = laserAmmo;
        }

    }
}
