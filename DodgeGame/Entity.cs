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

        public Entity(double x, double y, double radius)
        {
            Radius = radius;
            X = x;
            Y = y;
            IsAlive = true;

            Circle = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
            };
        }

        public Ellipse Circle { get; }

        public double X { get; set; }

        public double Y { get; set; }

        public bool IsAlive { get; set; }

        public double Radius { get; set; }

        public virtual void Dead() { }

    }
}
