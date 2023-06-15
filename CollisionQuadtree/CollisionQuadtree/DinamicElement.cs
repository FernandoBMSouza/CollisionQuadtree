using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CollisionQuadtree
{
    class DinamicElement : BaseElement
    {
        private float _interval, _count;
        private int _state;
        private Random _random;

        public DinamicElement(Game game, Texture2D texture, Random random)
        {
            _game = game;
            _texture = texture;

            _random = random;

            int width = _random.Next(5, 20);
            int height = _random.Next(5, 20);

            float x = _random.Next(0, _game.Window.ClientBounds.Width - width);
            float y = _random.Next(0, _game.Window.ClientBounds.Height - height);

            _size = new Point(width, height);
            _position = new Vector2(x, y);
            _speed = new Vector2(_random.Next(-200, 201), _random.Next(-200, 201));

            _state = _random.Next(4);
            _interval = _random.Next(1000) / 500f;
        }

        public override void Move(GameTime gameTime)
        {
            _count += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if (_count > _interval)
            {
                _count = 0;
                switch (_state)
                {
                    case 0:
                        _state = _random.Next(1, 4);
                        break;
                    case 1:
                        do
                        {
                            _state = _random.Next(4);
                        } while (_state == 1);
                        break;
                    case 2:
                        do
                        {
                            _state = _random.Next(4);
                        } while (_state == 2);
                        break;
                    case 3:
                        do
                        {
                            _state = _random.Next(4);
                        } while (_state == 3);
                        break;
                }
            }

            switch (_state)
            {
                case 0:
                    _position.Y -= _speed.Y * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    break;

                case 1:
                    _position.Y += _speed.Y * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    break;

                case 2:
                    _position.X -= _speed.X * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    break;

                case 3:
                    _position.X += _speed.X * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    break;
            }

            if (this._position.X < 0)
            {
                this._position.X = 0;
            }

            else if (this._position.X + _size.X > _game.Window.ClientBounds.Width)
            {
                this._position.X = _game.Window.ClientBounds.Width - _size.X;
            }

            if (this._position.Y < 0)
            {
                this._position.Y = 0;
            }

            else if (this._position.Y + _size.Y > _game.Window.ClientBounds.Height)
            {
                this._position.Y = _game.Window.ClientBounds.Height - _size.Y;
            }
        }
    }
}
