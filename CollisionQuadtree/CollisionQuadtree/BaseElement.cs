using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CollisionQuadtree
{
    class BaseElement
    {
        protected Game _game;
        protected Texture2D _texture, _redTexture;
        protected Vector2 _position, _speed;
        protected Point _size;
        protected bool _isColliding;

        public bool IsColliding { get { return _isColliding; } set { _isColliding = value;  } }

        public virtual void Move(GameTime gameTime)
        { 
        }

        public virtual void Collision(BaseElement[] elements)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != this)
                {
                    if (Bounds.Intersects(elements[i].Bounds))
                    {
                        elements[i].IsColliding = true;
                        _isColliding = true;
                    }
                    else
                    {
                        elements[i].IsColliding = false;
                        _isColliding = false;
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = _isColliding ? _redTexture : _texture;
            spriteBatch.Draw(texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), Color.White);
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y); }
        }
    }
}
