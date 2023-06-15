using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CollisionQuadtree
{
    class StaticElement : BaseElement
    {
        public StaticElement(Game game, Texture2D texture, Random random)
        {
            _game = game;
            _texture = texture;

            int width = random.Next(5, 20);
            int height = random.Next(5, 20);

            float x = random.Next(0, _game.Window.ClientBounds.Width - width);
            float y = random.Next(0, _game.Window.ClientBounds.Height - height);

            _size = new Point(width, height);
            _position = new Vector2(x, y);
        }
    }
}
