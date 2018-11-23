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
        public Enemy(int x = 0, int y = 0, double speed = 6 ,double raidus = 20)
            : base(x, y, raidus)
        {
            _speed = speed;
            BitmapImage bitmap = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/ufo.png");
            bitmap.UriSource = uri;
            ImageBrush enemy = new ImageBrush();
            enemy.ImageSource = bitmap;
            _circle.Fill = enemy;
            _circle.Stroke = new SolidColorBrush(Colors.Red);
            _circle.StrokeThickness = 2;
        }

        // enemy stops moving and don't interact with other entity's
        public override void Dead()
        {
            _isAlive = false;
            BitmapImage dead = new BitmapImage();
            Uri uriDead = new Uri("ms-appx:///Assets/bomb.png");
            dead.UriSource = uriDead;
            ImageBrush enemy = new ImageBrush();
            enemy.ImageSource = dead;
            _circle.Fill = enemy;
        }

        //chase player in a straight line with fixed speed
        public void Move(double playerX, double playerY)
        {
            double deltaX = Math.Abs(X - playerX);
            double deltaY = Math.Abs(Y - playerY);
            double alpha = Math.Atan(deltaY / deltaX);

            if (_isAlive)
            {
                // move by X
                if (playerX < X)
                {
                    _x -= _speed * Math.Cos(alpha);
                }
                else if (playerX > X)
                {
                    _x += _speed * Math.Cos(alpha);
                }

                // move by Y
                if (playerY < Y)
                {
                    _y -= _speed * Math.Sin(alpha);
                }
                else if (playerY > Y)
                {
                    _y += _speed * Math.Sin(alpha);
                }
            }

        }

    }
}
