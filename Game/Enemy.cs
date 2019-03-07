using System;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Game
{
    public class Enemy : Entity
    {

        public Enemy(Settings set)
        {
            double rndSpeed = _rnd.NextDouble() * (set.EnemyMaxSpeed - set.EnemyMinSpeed) + set.EnemyMinSpeed;
            Speed = rndSpeed;
            Radius = _rnd.Next((int)set.EnemyMinRadius,(int)set.EnemyMaxRadius);
            Circle = new Ellipse();
            byte[] arr = new byte[3];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (byte)_rnd.Next(0,256);
            }
            Color randomColor = Color.FromArgb(255,arr[0],arr[1],arr[2]);
            Circle.Fill = new SolidColorBrush(randomColor);
            Circle.StrokeThickness = 4;
            Circle.Height = Radius * 2;
            Circle.Width = Radius * 2;
        }

        //chase player in a straight line with fixed speed
        public void Move(double playerX,double playerY,Settings set)
        {
            double alpha = Math.Atan(Math.Abs(Y - playerY) / Math.Abs(X - playerX));

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
            X = Math.Min(Math.Max(Radius,X),set.BoardWidth - Radius);
            Y = Math.Min(Math.Max(Radius,Y),set.BoardHeight - Radius);
        }
        private static Random _rnd = new Random();
    }
}
