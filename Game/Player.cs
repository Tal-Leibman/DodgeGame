﻿using System;
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
            Speed = set.HumanSpeed;
            Radius = set.HumanRadius;
            Circle = new Ellipse
            {
                Fill = new SolidColorBrush(set.HumanColor),
                Height = Radius * 2,
                Width = Radius * 2
            };
        }


        public void Move(bool up, bool down, bool left, bool right, Settings set)
        {

            // 1 direction only
            if ((left ^ right) ^ (up ^ down))
            {
                if (left)
                {
                    X -= Speed;
                }
                else if (right)
                {
                    X += Speed;
                }
                else if (up)
                {
                    Y -= Speed;
                }
                else if (down)
                {
                    Y += Speed;
                }
            }
            // diagonal movement

            //Right and Up
            if (right && up && !down && !left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y -= Speed * Math.Sin(RADIANS_45);
            }

            //Right and Down
            else if (right && down && !up && !left)
            {
                X += Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }

            //Left and Down
            else if (left && down && !up && !right)
            {
                X -= Speed * Math.Cos(RADIANS_45);
                Y += Speed * Math.Sin(RADIANS_45);
            }
            //Left and Up
            else if (left && up && !down && !right)
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
