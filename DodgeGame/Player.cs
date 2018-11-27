using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace DodgeGame
{
    class Player : Entity
    {
        private const double RADIANS_45 = 45 * Math.PI / 180;

        private double _speed;
        //date members for enemy alive image
        private BitmapImage _bitmapPlayerAlive;
        private Uri _uriPlayerAlive;
        private ImageBrush _imageBrushPlayerAlive;


        //builds the player with default values
        public Player(double x = 0, double y = 0, double radius = 17, double speed = 12.5)
            : base(x, y, radius)
        {
            _speed = speed;
            _bitmapPlayerAlive = new BitmapImage();
            _uriPlayerAlive = new Uri("ms-appx:///Assets/spaceShip.gif");
            _bitmapPlayerAlive.UriSource = _uriPlayerAlive;
            _imageBrushPlayerAlive = new ImageBrush();
            _imageBrushPlayerAlive.ImageSource = _bitmapPlayerAlive;
            _circle.Fill = _imageBrushPlayerAlive;
            _circle.Stroke = new SolidColorBrush(Colors.Black);
            _circle.StrokeThickness = 3;
        }

        public override void Revive()
        {
            _isAlive = true;
            _circle.Fill = _imageBrushPlayerAlive;
            _circle.Height = Radius * 2;
            _circle.Width = Radius * 2;
        }





        //kills the player and ends the game
        public override void Dead()
        {
            BitmapImage bitmap = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/explosion.png");
            bitmap.UriSource = uri;
            ImageBrush playerDead = new ImageBrush();
            playerDead.ImageSource = bitmap;
            _circle.Fill = playerDead;
            _circle.Height = 4 * Radius;
            _circle.Width = 4 * Radius;
            _isAlive = false;
        }

        // move the player with the keyboard direction keys
        public void Move(bool up, bool down, bool left, bool right, Board board)
        {
            if (_isAlive)
            {

                bool rangeLeft = X - _speed - Radius > 0;
                bool rangeRight = X + _speed + Radius < board.BoardX;
                bool rangeUp = Y - _speed - Radius > 0;
                bool rangeDown = Y + _speed + Radius < board.BoardY;

                // 1 direction only
                if ((left ^ right) ^ (up ^ down))
                {
                    if (left && rangeLeft)
                    {
                        _x -= _speed;
                    }
                    else if (right && rangeRight)
                    {
                        _x += _speed;
                    }
                    else if (up && rangeUp)
                    {
                        _y -= _speed;
                    }
                    else if (down && rangeDown)
                    {
                        _y += _speed;
                    }
                }
                // diagonal movement

                //Right and Up
                if (right && up && rangeRight && rangeUp && !down && !left)
                {
                    _x += _speed * Math.Sin(RADIANS_45);
                    _y -= _speed * Math.Sin(RADIANS_45);
                }

                //Right and Down
                else if (right && down && rangeRight && rangeDown && !up && !left)
                {
                    _x += _speed * Math.Sin(RADIANS_45);
                    _y += _speed * Math.Sin(RADIANS_45);
                }

                //Left and Down
                else if (left && down && rangeLeft && rangeDown && !up && !right)
                {
                    _x -= _speed * Math.Sin(RADIANS_45);
                    _y += _speed * Math.Sin(RADIANS_45);
                }

                //Left and Up
                else if (left && up && rangeLeft && rangeUp && !down && !right)
                {
                    _x -= _speed * Math.Sin(RADIANS_45);
                    _y -= _speed * Math.Sin(RADIANS_45);
                }

            }

        }

    }
}
