using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Game
{
    public class Enemy : Entity
    {
        public Enemy(Settings settings)
        {
            Speed = settings.EnemySpeed;
            Radius = settings.EnemyRadius;
            Circle = new Ellipse();
            Circle.Stroke = new SolidColorBrush(Colors.Red);
            Circle.StrokeThickness = 2;
            Circle.Height = Radius * 2;
            Circle.Width = Radius * 2;
        }

        //chase player in a straight line with fixed speed
        public void Move(double playerX, double playerY)
        {

            double deltaX = Math.Abs(X - playerX);
            double deltaY = Math.Abs(Y - playerY);
            double alpha = Math.Atan(deltaY / deltaX);

            // move by X
            if (playerX < X)
            {
                X -= Speed * Math.Cos(alpha);
            }
            else if (playerX > X)
            {
                X += Speed * Math.Cos(alpha);
            }

            // move by Y
            if (playerY < Y)
            {
                Y -= Speed * Math.Sin(alpha);
            }
            else if (playerY > Y)
            {
                Y += Speed * Math.Sin(alpha);
            }

        }

    }
}
