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
        protected Texture2D _texture;
        protected Vector2 _position, _speed;
        protected Point _size;

        public virtual void Move(GameTime gameTime)
        { 
        }

        public virtual void Collision()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y), Color.White);
        }
    }
}
