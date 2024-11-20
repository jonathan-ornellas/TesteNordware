# Documentação da API de Reserva de Produtos

## Pré-requisitos

- **.NET Core SDK 8**

---

## Sumário

- [Executando a Aplicação](#executando-a-aplicação)
- [Testando a API](#testando-a-api)
  - [Lista de Clientes Fixos](#lista-de-clientes-fixos)
  - [Lista de Produtos](#lista-de-produtos)
  - [Reservando um Produto](#reservando-um-produto)
  - [Listando Reservas de um Cliente](#listando-reservas-de-um-cliente)
- [Informações Adicionais](#informações-adicionais)

---

Testando a API
Lista de Clientes Fixos
A aplicação possui uma lista de clientes fixos para fins de demonstração. Você pode usar esses clientes ao interagir com a API.

Clientes Disponíveis:

João Silva
ID: aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa
Maria Oliveira
ID: bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb
Carlos Santos
ID: cccccccc-cccc-cccc-cccc-cccccccccccc
Lista de Produtos
Para obter a lista de produtos e seus IDs:

Endpoint:
GET /products
Exemplo de Requisição:
GET https://localhost:5001/products
Exemplo de Resposta:
[
  {
    "id": "e3d22b5a-1f2a-4e0c-9aef-1e6f9a9f9f9f",
    "name": "Notebook Dell XPS 13",
    "price": 9999.99,
    "status": "Available"
  },
  {
    "id": "a4b33c6d-2g3b-5h1d-8bfc-2f7g0a0g0g0g",
    "name": "Smartphone Samsung Galaxy S21",
    "price": 4999.99,
    "status": "Available"
  },
  {
    "id": "b5c44d7e-3h4c-6i2e-9cgd-3h8h1b1h1h1h",
    "name": "Smart TV LG 55\" 4K",
    "price": 2999.99,
    "status": "Available"
  }
]

Reservando um Produto
Para reservar um produto:

Endpoint:
POST /products/{productId}/reserve
Parâmetros:
{productId}: ID do produto que deseja reservar.
Descrição:
O sistema seleciona aleatoriamente um cliente cadastrado para fazer a reserva.
Ao reservar um produto, seu status muda para "Reserved" e não pode ser reservado por outro cliente até que a reserva expire.
Exemplo:
Reservando o produto "Notebook Dell XPS 13":
POST https://localhost:5001/products/e3d22b5a-1f2a-4e0c-9aef-1e6f9a9f9f9f/reserve

Resposta de Sucesso:
{
  "message": "Produto reservado com sucesso.",
  "productId": "e3d22b5a-1f2a-4e0c-9aef-1e6f9a9f9f9f",
  "productName": "Notebook Dell XPS 13",
  "customerId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
  "customerName": "Maria Oliveira"
}

Notas:
O customerId e customerName retornados indicam qual cliente foi selecionado aleatoriamente para a reserva.
Se o produto já estiver reservado, a API retornará uma mensagem de erro.

Listando Reservas de um Cliente
Para listar as reservas de um cliente específico:
Endpoint:
GET /customer/{customerId}/reservations
Parâmetros:
{customerId}: ID do cliente.
Exemplo:
Listando as reservas de "Maria Oliveira":
GET https://localhost:5001/customer/bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb/reservations
Exemplo de Resposta:
[
  {
    "id": "f6d55e8b-4i5d-7j3f-0dhe-4i9i2c2i2i2i",
    "customerId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
    "customerName": "Maria Oliveira",
    "productId": "e3d22b5a-1f2a-4e0c-9aef-1e6f9a9f9f9f",
    "productName": "Notebook Dell XPS 13",
    "reservationDate": "2023-10-10T12:34:56.789Z",
    "reservationExpirationDate": "2023-10-10T12:35:11.789Z",
    "status": "Ativa"
  }
]
Notas:
A reserva inclui a data de expiração (reservationExpirationDate) e o status da reserva.
O status pode ser "Ativa" ou "Expirada".
O status da reserva é atualizado automaticamente por um serviço em segundo plano.

Informações Adicionais
Expiração Automática das Reservas:

As reservas expiram automaticamente após 60 segundos (para fins de demonstração).
Um serviço em segundo plano verifica periodicamente as reservas e atualiza o status para "Expirada" quando o tempo de expiração é atingido.
Quando uma reserva expira:
O status da reserva é atualizado para "Expirada".
O status do produto associado é atualizado para "Disponivel", permitindo que outros clientes possam reservá-lo.



O serviço de expiração de reservas é implementado como um Hosted Service no ASP.NET Core.
Ele verifica as reservas ativas a cada 5 segundos e expira aquelas cujo tempo de expiração foi atingido.

Mensagens de Erro:
Se você tentar reservar um produto que já está reservado, receberá uma mensagem de erro:

{
  "error": "Produto não está disponível para reserva."
}

Exemplo de Reserva Expirada:

Após a reserva expirar, ao listar as reservas do cliente, o status será "Expirada":
[
  {
    "id": "f6d55e8b-4i5d-7j3f-0dhe-4i9i2c2i2i2i",
    "customerId": "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
    "customerName": "Maria Oliveira",
    "productId": "e3d22b5a-1f2a-4e0c-9aef-1e6f9a9f9f9f",
    "productName": "Notebook Dell XPS 13",
    "reservationDate": "2023-10-10T12:34:56.789Z",
    "reservationExpirationDate": "2023-10-10T12:35:11.789Z",
    "status": "Expirada"
  }
]

Produtos Disponíveis Novamente:

Após a reserva expirar, o produto retorna ao status "Disponivel".

Conclusão
Esta API permite a simulação de um sistema de reservas de produtos, onde:

Produtos podem ser reservados por clientes selecionados aleatoriamente.
Reservas expiram automaticamente após um período determinado.
O status de produtos e reservas é atualizado dinamicamente por um serviço em segundo plano.
A aplicação utiliza boas práticas de desenvolvimento.
Para alterar  o tempo da reserva, basta alterar a variável de ambiente "ReservationExpirationInSeconds" no arquivo de configuração appsettings.json.
