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
        
        //Parent class for Player(keyboard controlled) and Enemy(chase the player)

        public Entity(double x, double y, double radius)
        {
            Radius = radius;
            X = x;
            Y = y;
            IsAlive = true;

            Circle = new Ellipse
            {
                Width = Radius * 2,
                Height = Radius * 2,
            };
        }

        public Ellipse Circle { get; }

        public double X { get; set; }

        public double Y { get; set;}

        public bool IsAlive { get; set;}

        public double Radius { get; }

        public virtual void Dead() { }

    }
}
