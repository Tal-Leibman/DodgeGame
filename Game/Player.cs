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

        public bool IsAlive { get;  set; }

        public Player(Settings set)
        {
            IsAlive = true;
            Speed = set.HumanSpeed;
            Radius = set.HumanRadius;
            Circle = new Ellipse
            {
                Fill = new SolidColorBrush(set.HumanColor),
                Height = Radius * 2,
                Width = Radius * 2
            };
        }


        public void Move(PlayerInput input, Settings set)
        {

            // 1 direction only
            if ((input.Left ^ input.Right) ^ (input.Up ^ input.Down))
            {
                if (input.Left)
                {
                    X -= Speed;
                }
                else if (input.Right)
                {
                    X += Speed;
                }
                else if (input.Up)
                {
                    Y -= Speed;
                }
                else if (input.Down)
                {
                    Y += Speed;
                }
            }
            // diagonal movement

            //input.Right and input.Up
            if (input.Right && input.Up && !input.Down && !input.Left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y -= Speed * Math.Sin(RADIANS_45);
            }

            //input.Right and input.Down
            else if (input.Right && input.Down && !input.Up && !input.Left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }

            //input.Left and input.Down
            else if (input.Left && input.Down && !input.Up && !input.Right)
            {
                X -= Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }
            //input.Left and input.Up
            else if (input.Left && input.Up && !input.Down && !input.Right)
            {
                X -= Speed * Math.Cos(RADIANS_45);
                Y -= Speed * Math.Sin(RADIANS_45);
            }
            //check new position is not outside of board
            X = Math.Min(Math.Max(Radius, X), set.BoardWidth - Radius);
            Y = Math.Min(Math.Max(Radius, Y), set.BoardHeight - Radius);

        }





    }
}
