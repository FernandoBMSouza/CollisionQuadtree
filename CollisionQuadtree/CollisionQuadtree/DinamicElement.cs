using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CollisionQuadtree
{
    public enum Direction { UP, DOWN, LEFT, RIGHT };

    class DinamicElement : BaseElement
    {
        private Direction _direction;
        private Random _random;

        public DinamicElement(Game game, Texture2D texture, Texture2D redTexture, Random random)
        {
            _game = game;
            _texture = texture;
            _redTexture = redTexture;

            _random = random;

            int width = _random.Next(5, 20);
            int height = _random.Next(5, 20);

            float x = _random.Next(0, _game.Window.ClientBounds.Width - width);
            float y = _random.Next(0, _game.Window.ClientBounds.Height - height);

            _size = new Point(width, height);
            _position = new Vector2(x, y);
            _speed = _random.Next(60, 150);
            _direction = (Direction)_random.Next(4);
        }

        public override void Collision(BaseElement playerElement)
        {
            if (Bounds.Intersects(playerElement.Bounds))
            {
                playerElement.IsColliding = true;
                _isCollidingWithPlayer = true;

                switch (_direction)
                {
                    case Direction.UP:
                        _direction = Direction.DOWN;
                        break;
                    case Direction.DOWN:
                        _direction = Direction.UP;
                        break;
                    case Direction.RIGHT:
                        _direction = Direction.LEFT;
                        break;
                    case Direction.LEFT:
                        _direction = Direction.RIGHT;
                        break;
                }
            }
            else
            {
                _isColliding = false;
            }
        }

        public override void Collision(BaseElement[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        elements[i].IsColliding = true;
                        _isColliding = true;
                        switch (_direction)
                        {
                            case Direction.UP:
                                _direction = Direction.DOWN;
                                break;
                            case Direction.DOWN:
                                _direction = Direction.UP;
                                break;
                            case Direction.RIGHT:
                                _direction = Direction.LEFT;
                                break;
                            case Direction.LEFT:
                                _direction = Direction.RIGHT;
                                break;
                        }
                    }
                    else
                    {
                        _isColliding = false;
                    }
                }
            }
        }

        public override void Move(GameTime gameTime)
        {
            switch (_direction)
            {
                case Direction.DOWN:
                    _position.Y -= _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    if (_position.Y < 0)
                        _direction = Direction.UP;
                    break;

                case Direction.UP:
                    _position.Y += _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    if (_position.Y + _size.Y > _game.Window.ClientBounds.Height)
                        _direction = Direction.DOWN;
                    break;

                case Direction.LEFT:
                    _position.X -= _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    if (_position.X < 0)
                        _direction = Direction.RIGHT;
                    break;

                case Direction.RIGHT:
                    _position.X += _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                    if (_position.X + _size.X > _game.Window.ClientBounds.Width)
                        _direction = Direction.LEFT;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = _isCollidingWithPlayer ? _redTexture : _texture;
            spriteBatch.Draw(texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), Color.White);
        }
    }
}
