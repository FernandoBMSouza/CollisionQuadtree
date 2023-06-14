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
    class Player
    {
        private Texture2D Texture;
        private Vector2 Position;
        private Vector2 Size;
        private float Speed;

        public Player(ContentManager content)
        {
            Texture = content.Load<Texture2D>("red");
            Size = new Vector2(20, 20);
            Position = new Vector2(Game1.SCREEN_WIDTH/2, Game1.SCREEN_HEIGHT/2);
            Speed = 5f;
        }

        public void Update()
        {
            //Cima (Seta Up ou W) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up)) && Position.Y > 0)
            {
                Position.Y -= Speed;
            }

            //Baixo (Seta Down ou S) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) && Position.Y < Game1.SCREEN_HEIGHT - Size.Y)
            {
                Position.Y += Speed;
            }

            //Esquerda (Seta Left ou A) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left)) && Position.X > 0)
            {
                Position.X -= Speed;
            }

            //Direita (Seta Rigth ou D) + Checagem se está nos limites da cena
            if ((Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right)) && Position.X < Game1.SCREEN_WIDTH - Size.X)
            {
                Position.X += Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
        }
    }
}
