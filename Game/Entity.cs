using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Game
{
    public abstract class Entity
    {

        public double X { get; protected set; }

        public double Y { get; protected set; }

        public double Speed { get; protected set; }

        public double Radius { get; protected set; }

        public void PlaceOnBoard(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Ellipse Circle {get;set;}


    }
}
