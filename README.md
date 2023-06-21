# CollisionQuadtree

-------------- Grupo ---------------

Fernando Souza
Gustavo Dolavale
Vitor Braga

-------------- Comandos ---------------

F1: Liga/Desliga visisibilidade da Quadtree
F2: Liga/Desliga a Quadtree
    + No modo ON: usa a quadtree para otimizar as checagens de colisao
    + No modo OFF: realiza as checagens de colisao sem a otimizacao da quadtree

-------------- Speedup ---------------

------ Quadtree ON ------
0,3248ms
0,2984ms
0,2399ms
0,2465ms
0,2401ms

Media: 0,2699ms

----- Quadtree OFF ------
0,5725ms
0,5777ms
0,5769ms
0,579ms
0,632ms

Media: 0,58762ms

Speedup (QuadtreeON/QuadtreeOFF) = 0,2699ms/0,58762ms = 0,4593104387189ms
Aproximadamente = 0,5ms