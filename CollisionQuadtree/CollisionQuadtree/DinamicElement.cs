﻿using System;
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
        private Vector2 testDirection;
        private Random _random;
        private Texture2D _redTexture;

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
            testDirection = new Vector2(1, 0);
        }

        public override void Collision(BaseElement playerElement)
        {
            if (Bounds.Intersects(playerElement.Bounds))
            {
                Rectangle thisRef = Bounds;
                Rectangle playerRef = playerElement.Bounds;
                Rectangle difference;

                Rectangle.Intersect(ref playerRef, ref thisRef, out difference);
                playerElement.IsColliding = true;
                _isCollidingWithPlayer = true;
                testDirection = new Vector2(thisRef.Center.X - difference.Center.X, 
                                              thisRef.Center.Y - difference.Center.Y);
                testDirection.Normalize();
            }
            else
            {
                _isColliding = false;
            }
        }

        public override void Collision(List<BaseElement> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        Rectangle thisRef = Bounds;
                        Rectangle playerRef = elements[i].Bounds;
                        Rectangle difference;

                        Rectangle.Intersect(ref playerRef, ref thisRef, out difference);

                        elements[i].IsColliding = true;
                        _isColliding = true;

                        testDirection = new Vector2(thisRef.Center.X - difference.Center.X,
                                              thisRef.Center.Y - difference.Center.Y);
                        if (testDirection != Vector2.Zero)
                            testDirection.Normalize();
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
            _position += testDirection * _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if (_position.X < 0 || _position.X > Game1.SCREEN_WIDTH)
                testDirection.X *= -1;

            if (_position.Y < 0 || _position.Y > Game1.SCREEN_HEIGHT)
                testDirection.Y *= -1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = _isCollidingWithPlayer ? _redTexture : _texture;
            spriteBatch.Draw(texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), Color.White);
        }
    }
}
