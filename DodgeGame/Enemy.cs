using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DodgeGame
{
    class Enemy : Entity
    {

        private double _speed;

        // builds enemy with default values
        public Enemy(int x = 0, int y = 0, double speed = 6 ,double raidus = 19)
            : base(x, y, raidus)
        {
            _speed = speed;
            BitmapImage bitmap = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/ufo.png");
            bitmap.UriSource = uri;
            ImageBrush enemy = new ImageBrush();
            enemy.ImageSource = bitmap;
            Circle.Fill = enemy;
            Circle.Stroke = new SolidColorBrush(Colors.Green);
            Circle.StrokeThickness = 2;
        }

        // enemy stops moving and don't interact with other entity's
        public override void Dead()
        {
            IsAlive = false;
            BitmapImage dead = new BitmapImage();
            Uri uriDead = new Uri("ms-appx:///Assets/bomb.png");
            dead.UriSource = uriDead;
            ImageBrush enemy = new ImageBrush();
            enemy.ImageSource = dead;
            Circle.Fill = enemy;
        }

        //chase player in a straight line with fixed speed
        public void Move(double playerX, double playerY)
        {
            double deltaX = Math.Abs(X - playerX);
            double deltaY = Math.Abs(Y - playerY);
            double alpha = Math.Atan(deltaY / deltaX);

            if (IsAlive)
            {
                // move by X
                if (playerX < X)
                {
                    X -= _speed * Math.Cos(alpha);
                }
                else if (playerX > X)
                {
                    X += _speed * Math.Cos(alpha);
                }

                // move by Y
                if (playerY < Y)
                {
                    Y -= _speed * Math.Sin(alpha);
                }
                else if (playerY > Y)
                {
                    Y += _speed * Math.Sin(alpha);
                }
            }

        }

    }
}
