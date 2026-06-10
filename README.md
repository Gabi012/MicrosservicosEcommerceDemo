# 🛒 e-Commerce Microservices Architecture

Este é um ecossistema completo de e-Commerce baseado em uma arquitetura de microsserviços. O projeto foi desenvolvido aplicando os princípios da **Clean Architecture**, garantindo alta escalabilidade, desacoplamento, testabilidade e resiliência para plataformas de compras online.

O sistema utiliza um **API Gateway** como ponto único de entrada, gerenciando o tráfego e distribuindo as requisições entre microsserviços independentes e especializados.

---

## 🏗️ Arquitetura do Sistema

O projeto foi estruturado seguindo o padrão de **Monorepo**, onde cada microsserviço/solução possui sua própria estrutura independente, mas compartilham o mesmo ciclo de vida de versionamento.

### Padrões e Práticas Adotadas:
* **Microservices Architecture:** Serviços independentes divididos por contexto de negócio (Bounded Contexts).
* **Clean Architecture:** Separação clara de conceitos utilizando Bibliotecas de Classes (`Class Libraries`), garantindo que as regras de negócio não dependam de frameworks externos.
* **API Gateway:** Centralização de rotas e políticas globais.
* **Resiliência e Performance:** Estratégias nativas para mitigar falhas transientas e otimizar a experiência do usuário.

---

## 🛠️ Tecnologias e Bibliotecas Utilizadas

* **.NET 8 / .NET 9** (Ambiente de execução principal)
* **Ocelot:** API Gateway para roteamento de requisições, agregação de endpoints e segurança.
* **Entity Framework Core (EF Core):** ORM para mapeamento e persistência de dados.
* **Polly:** Implementação de políticas de resiliência como *Retry Strategies* (estratégias de tentativa).
* **Rate Limiting:** Controle de taxa de requisições por cliente para evitar abusos e garantir a estabilidade do sistema.
* **Caching:** Armazenamento em cache de dados frequentemente acessados (como catálogo de produtos) para ganho de performance e redução de carga no banco de dados.

---
