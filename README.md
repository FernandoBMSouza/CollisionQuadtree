# CollisionQuadtree

Planejamento do Projeto:

Requisitos:
- 1 Player (sprite, movimento, colisao)
- 50 objetos estaticos (sprite, colisao)
- 50 objetos dinamicos (sprite, movimento, colisao)
- Quadtree para checagem de colisao

Minha ideia:
- Criar uma classe abstrata elemento com uma implementacao basica de sprite, movimento para sobrescrever e colisao
- No player sobrescrever a movimentacao para as setas
- Nos objetos dinamicos a movimentacao sera automatica simples
- Nos objetos estaticos nao tera movimentacao, somente posicao inicial