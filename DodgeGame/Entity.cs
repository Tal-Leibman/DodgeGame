using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace DodgeGame
{
    class Entity
    {
        protected double _x;

        protected double _y;

        protected bool _isAlive;

        protected double _radius;

        protected Ellipse _circle;

        //Parent class for Player(keyboard controlled) and Enemy(chase the player)

        public Entity(double x, double y, double radius)
        {
            _radius = radius;
            _x = x;
            _y = y;
            _isAlive = true;

            _circle = new Ellipse
            {
                Height = _radius * 2,
                Width = _radius * 2,
            };
        }

        public Ellipse Circle { get { return _circle; } }

        public double X { get { return _x; } set { _x = value; } }

        public double Y { get { return _y; } set { _y = value; } }

        public bool IsAlive { get { return _isAlive; } set { _isAlive = value; } }

        public double Radius { get { return _radius; } set { _radius = value; } }

        public virtual void Dead() { }

        public virtual void Revive() { }

    }
}
