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

        public double Speed { get { return _speed; } }

        //date members for enemy alive image
        private BitmapImage _bitmapEnemyAlive;
        private Uri _uriEnemyAlive;
        private ImageBrush _imageBrushEnemyAlive;

        // builds enemy with default values
        public Enemy(int x = 0, int y = 0, double speed = 6, double raidus = 20)
            : base(x, y, raidus)
        {
            _speed = speed;
            _bitmapEnemyAlive= new BitmapImage();
            _uriEnemyAlive = new Uri("ms-appx:///Assets/ufo.png");
            _bitmapEnemyAlive.UriSource = _uriEnemyAlive;
            _imageBrushEnemyAlive = new ImageBrush();
            _imageBrushEnemyAlive.ImageSource = _bitmapEnemyAlive;
            _circle.Fill = _imageBrushEnemyAlive;
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
        // revive enemy

        public override void Revive()
        {
            _isAlive = true;
            _circle.Fill = _imageBrushEnemyAlive;
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
