using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CollisionQuadtree
{
    class Player : BaseElement
    {
        public Player(Texture2D texture, Game game)
        {
            _game = game;
            _texture = texture;
            _size = new Point(10, 10);
            _position = new Vector2(_game.Window.ClientBounds.Width / 2, _game.Window.ClientBounds.Height / 2);
            _speed = 200f;
        }

        public override void Collision(BaseElement[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        elements[i].IsCollidingWithPlayer = true;
                        _isColliding = true;
                    }
                    else
                    {
                        elements[i].IsCollidingWithPlayer = false;
                        _isColliding = false;
                    }
                }
            }
        }

        public override void Move(GameTime gameTime)
        {
            //Cima (Seta Up ou W) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && _position.Y > 0)
            {
                _position.Y -= _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }

            //Baixo (Seta Down ou S) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && _position.Y < _game.Window.ClientBounds.Height - _size.Y)
            {
                _position.Y += _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }

            //Esquerda (Seta Left ou A) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && _position.X > 0)
            {
                _position.X -= _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }

            //Direita (Seta Rigth ou D) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && _position.X < _game.Window.ClientBounds.Width - _size.X)
            {
                _position.X += _speed * gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            }
        }
    }
}
