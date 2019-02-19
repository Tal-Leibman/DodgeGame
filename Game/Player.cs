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
    public class Player : Entity
    {
        private const double RADIANS_45 = 45 * Math.PI / 180;

        public bool IsAlive { get; private set; }

        public Player(Settings set)
        {
            Ammo = set.Ammo;
            Speed = set.HumanSpeed;
            Radius = set.HumanRadius;
            Circle = new Ellipse();
            Circle.Stroke = new SolidColorBrush(Colors.Blue);
            Circle.StrokeThickness = 2;
            Circle.Height = Radius * 2;
            Circle.Width = Radius * 2;
        }

        public int Ammo { get; set; }

        public bool Shoot()
        {
            if (Ammo > 0)
            {
                Ammo--;
                return true;
            }
            return false;
        }

        public void Move(bool up, bool down, bool left, bool right,Settings set)
        {
            bool rangeLeft = X - Speed - Radius > 0;
            bool rangeRight = X + Speed + Radius <set.BoardWidth;
            bool rangeUp = Y - Speed - Radius > 0;
            bool rangeDown = Y + Speed + Radius < set.BoardHeight;

            // 1 direction only
            if ((left ^ right) ^ (up ^ down))
            {
                if (left && rangeLeft)
                {
                    X -= Speed;
                }
                else if (right && rangeRight)
                {
                    X += Speed;
                }
                else if (up && rangeUp)
                {
                    Y -= Speed;
                }
                else if (down && rangeDown)
                {
                    Y += Speed;
                }
            }
            // diagonal movement

            //Right and Up
            if (right && up && rangeRight && rangeUp && !down && !left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y -= Speed * Math.Sin(RADIANS_45);
            }

            //Right and Down
            else if (right && down && rangeRight && rangeDown && !up && !left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }

            //Left and Down
            else if (left && down && rangeLeft && rangeDown && !up && !right)
            {
                X -= Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }

            //Left and Up
            else if (left && up && rangeLeft && rangeUp && !down && !right)
            {
                X -= Speed * Math.Cos(RADIANS_45);
                Y -= Speed * Math.Sin(RADIANS_45);
            }

        }

    }
}
