# 🚀 TravelRoute API

API RESTful para **gerenciamento e consulta de rotas de viagem** com suporte a cache e busca pela melhor rota entre origens e destinos.

---

## ⚙️ Tecnologias

- **ASP.NET Core 8**
- **C#**
- **Cache Redis**
- **Arquitetura Modular com Services e DTOs**
- **Docker e Docker-compose**

---

## 🚩 Como rodar localmente

1️⃣ **Clonar o repositório:**

```bash
git clone https://github.com/wsribeiro100/Projeto_idnt.git
```

2️⃣ **Acessar a pasta do projeto:**

```bash
cd ProjetoIDNT
```

3️⃣ **Executar o projeto:**

```bash
docker-compose up
```

4️⃣ **Acessar no navegador:**

```
https://localhost:8080/swagger
```

---

## 🤝 Contribuição


---

## 📧 Contato

Dúvidas ou sugestões? Entre em contato:

- Email: [wsribeiro100@gmail.com](mailto\:wsribeiro100@gmail.com)
- LinkedIn: [Wellyngton Sampaio](https://www.linkedin.com/in/wellyngtonsampaio)

---

## 📦 Endpoints

### 🔹 1️⃣ **Obter melhor rota entre origem e destino**

**GET** `/api/TravelRoute/{origem}/{destino}`

Retorna a melhor rota (menor custo) entre uma origem e um destino específico.

📥 **Request:**

- `origem` (string): aeroporto de origem
- `destino` (string): aeroporto de destino

📤 **Response:** `200 OK`

```json
"GRU - BRC - SCL"
```

### 🔹 2️⃣ **Listar todas as rotas**

**GET** `/api/TravelRoute`

Retorna uma lista de todas as rotas cadastradas.

📥 **Request:** Nenhum\
📤 **Response:** `200 OK`

```json
[
    {
        "id": 1,
        "origem": "GRU",
        "destino": "CDG",
        "valor": 75
    },
    ...
]
```

---

### 🔹 3️⃣ **Criar nova rota**

**POST** `/api/TravelRoute`

Cria uma nova rota no sistema.

📥 **Request Body:**

```json
{
    "origem": "CGB",
    "destino": "FOR",
    "valor": 88
}
```

📤 **Response:** `200 OK`

```json
{
    "id": 12,
    "origem": "CGB",
    "destino": "FOR",
    "valor": 88
}
```

---

### 🔹 4️⃣ **Atualizar rota existente**

**PUT** `/api/TravelRoute`

Atualiza uma rota existente no sistema.

📥 **Request Body:**

```json
{
    "id": 12,
    "origem": "CGB",
    "destino": "FOR",
    "valor": 90
}
```

📤 **Response:** `200 OK`

```json
{
    "id": 12,
    "origem": "CGB",
    "destino": "FOR",
    "valor": 90
}
```

---

### 🔹 5️⃣ **Deletar rota por ID**

**DELETE** `/api/TravelRoute/{id}`

Remove uma rota do sistema pelo `id`.

📥 **Request:**

- `id` (int): ID da rota a ser removida.

📤 **Response:** `200 OK`

```json
true
```
