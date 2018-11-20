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
        private double _speed;
        //builds the player with default values
        public Player(double x = 0, double y = 0, double radius = 16, double speed = 12.5)
            : base(x, y, radius)
        {
            _speed = speed;
            BitmapImage bitmap = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/rocket.png");
            bitmap.UriSource = uri;
            ImageBrush playerAlive = new ImageBrush();
            playerAlive.ImageSource = bitmap;
            Circle.Fill = playerAlive;
            Circle.Stroke = new SolidColorBrush(Colors.Yellow);
            Circle.StrokeThickness = 4;
        }

        //kills the player and ends the game
        public override void Dead()
        {
            BitmapImage bitmap = new BitmapImage();
            Uri uri = new Uri("ms-appx:///Assets/explosion.png");
            bitmap.UriSource = uri;
            ImageBrush playerDead = new ImageBrush();
            playerDead.ImageSource = bitmap;
            Circle.Fill = playerDead;
            Circle.Height = 4 * Radius;
            Circle.Width = 4 * Radius;
            IsAlive = false;
        }

        // move the player with the keyboard direction keys
        public void Move(bool up, bool down, bool left, bool right, Board board)
        {
            if (IsAlive)
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
                        X -= _speed;
                    }
                    else if (right && rangeRight)
                    {
                        X += _speed;
                    }
                    else if (up && rangeUp)
                    {
                        Y -= _speed;
                    }
                    else if (down && rangeDown)
                    {
                        Y += _speed;
                    }
                }
                // diagonal movement

                //Right and Up
                if (right && up && rangeRight && rangeUp && !down && !left)
                {
                    X += _speed * Math.Sin(45);
                    Y -= _speed * Math.Sin(45);
                }

                //Right and Down
                else if (right && down && rangeRight && rangeDown && !up && !left)
                {
                    X += _speed * Math.Sin(45);
                    Y += _speed * Math.Sin(45);
                }

                //Left and Down
                else if (left && down && rangeLeft && rangeDown && !up && !right)
                {
                    X -= _speed * Math.Sin(45);
                    Y += _speed * Math.Sin(45);
                }

                //Left and Up
                else if (left && up && rangeLeft && rangeUp && !down && !right)
                {
                    X -= _speed * Math.Sin(45);
                    Y -= _speed * Math.Sin(45);
                }

            }

        }

    }
}
