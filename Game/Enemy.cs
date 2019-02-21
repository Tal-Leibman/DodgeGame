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
        private static Random rnd = new Random();



        public Enemy(Settings set)
        {
            double rndSpeed = rnd.NextDouble() * (set.EnemyMaxSpeed - set.EnemyMinSpeed) + set.EnemyMinSpeed;
            Speed = rndSpeed;
            Radius = rnd.Next((int)set.EnemyMinRadius,(int)set.EnemyMaxRadius);
            Circle = new Ellipse();
            byte[] arr = new byte[3];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (byte)rnd.Next(0,256);
            }
            Color randomColor = Color.FromArgb(255,arr[0], arr[1], arr[2]);
            Circle.Fill = new SolidColorBrush(randomColor);
            Circle.StrokeThickness = 4;
            Circle.Height = Radius * 2;
            Circle.Width = Radius * 2;
        }

        //chase player in a straight line with fixed speed
        public void Move(double playerX, double playerY, Settings set)
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
            X = Math.Min(Math.Max(Radius, X), set.BoardWidth - Radius);
            Y = Math.Min(Math.Max(Radius, Y), set.BoardHeight - Radius);
        }

    }
}
