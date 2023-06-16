using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollisionQuadtree
{
    class Quadtree
    {
        private const int MaxObjectsPerNode = 5;
        private const int MaxLevels = 10;

        private int level; //serve para controlar a altura da arvore
        private List<BaseElement> elements;
        private Rectangle bounds; //serve para definir os limites do box da quadtree
        private Quadtree[] nodes; //representa os nos internos da arvore

        public Quadtree(int level, Rectangle bounds)
        {
            this.level = level;
            elements = new List<BaseElement>();
            this.bounds = bounds;
            nodes = new Quadtree[4];
        }

        //Serve para reiniciar/limpar a quadtree
        public void Clear()
        {
            elements.Clear(); //limpa a lista de elementos

            for (int i = 0; i < nodes.Length; i++) //passa por todos os nos da arvore
            {
                if (nodes[i] != null) //Se existir algo nesse node
                {
                    nodes[i].Clear(); // A funcao e chamada recursivamente limpando cada no
                    nodes[i] = null; // Passa o valor null para o no
                }
            }
        }

        //Retorna em qual quadrante está um elemento
        public Quadtree GetQuadrant(BaseElement element)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(element);

                if (index != -1)
                {
                    return nodes[index].GetQuadrant(element);
                }
            }

            return this;
        }

        //Retorna os elementos de um quadrante
        public List<BaseElement> GetElementsInQuadrant(Quadtree quadrant, BaseElement element)
        {
            List<BaseElement> quadrantElements = new List<BaseElement>();

            if (!bounds.Intersects(quadrant.bounds))
            {
                return quadrantElements;
            }

            foreach (var e in elements)
            {
                if (e != element && quadrant.bounds.Intersects(e.Bounds))
                {
                    quadrantElements.Add(e);
                }
            }

            if (nodes[0] != null)
            {
                foreach (var node in nodes)
                {
                    quadrantElements.AddRange(node.GetElementsInQuadrant(quadrant, element));
                }
            }

            return quadrantElements;
        }



        //Divide a quadtree em quatro subquadrantes
        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));                // Quadrante Superior Direito
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));                           // Quadrante Superior Esquerdo
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));               // Quadrante Inferior Esquerdo
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));    // Quadrante Inferior Direito
        }

        //Essa funcao determina o indice do no filho adequado para colocar um objeto na quadtree
        private int GetIndex(BaseElement element)
        {
            int index = -1;

            float verticalMidpoint = bounds.X + (bounds.Width / 2);
            float horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            bool topQuadrant = (element.Position.Y < horizontalMidpoint && element.Position.Y + element.Size.Y < horizontalMidpoint);
            bool bottomQuadrant = (element.Position.Y > horizontalMidpoint);

            if (element.Position.X < verticalMidpoint && element.Position.X + element.Size.X < verticalMidpoint)
            {
                if (topQuadrant)
                    index = 1;
                else if (bottomQuadrant)
                    index = 2;
            }
            else if (element.Position.X > verticalMidpoint)
            {
                if (topQuadrant)
                    index = 0;
                else if (bottomQuadrant)
                    index = 3;
            }

            return index;
        }

        //Insere um elemento na Quadtree, passando o no correto para o objeto
        public void Insert(BaseElement element)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(element);

                if (index != -1)
                {
                    nodes[index].Insert(element);
                    return;
                }
            }

            elements.Add(element);

            if (elements.Count > MaxObjectsPerNode && level < MaxLevels)
            {
                if (nodes[0] == null)
                    Split();

                int i = 0;
                while (i < elements.Count)
                {
                    int index = GetIndex(elements[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(elements[i]);
                        elements.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }



        //Essa funcao serve para desenhar retangulos na tela
        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int thickness)
        {
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            // Linhas Horizontais
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, thickness), color);
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Bottom - thickness, rectangle.Width, thickness), color);

            // Linhas Verticais
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Left, rectangle.Top, thickness, rectangle.Height), color);
            spriteBatch.Draw(pixel, new Rectangle(rectangle.Right - thickness, rectangle.Top, thickness, rectangle.Height), color);
        }

        // Renderiza a Quadtree na tela
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawRectangle(spriteBatch, bounds, Color.Red, 1);

            foreach (var node in nodes)
            {
                if (node != null)
                    node.Draw(spriteBatch);
            }
        }
    }
}
