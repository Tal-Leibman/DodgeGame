﻿using System;
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
        public Enemy(double x = 0, double y = 0, double speed = 6, double raidus = 22)
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
            BitmapImage bitmapDead = new BitmapImage();
            Uri uriDead = new Uri("ms-appx:///Assets/bomb.png");
            bitmapDead.UriSource = uriDead;
            ImageBrush brushDead = new ImageBrush();
            brushDead.ImageSource = bitmapDead;
            _circle.Fill = brushDead;
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

            if (_isAlive)
            {
                double deltaX = Math.Abs(_x - playerX);
                double deltaY = Math.Abs(_y - playerY);
                double alpha = Math.Atan(deltaY / deltaX);

                // move by X
                if (playerX < _x)
                {
                    _x -= _speed * Math.Cos(alpha);
                }
                else if (playerX > X)
                {
                    _x += _speed * Math.Cos(alpha);
                }

                // move by Y
                if (playerY < _y)
                {
                    _y -= _speed * Math.Sin(alpha);
                }
                else if (playerY > _y)
                {
                    _y += _speed * Math.Sin(alpha);
                }
            }

        }

    }
}
