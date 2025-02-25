# Developer Evaluation Project

## 📌 READ CAREFULLY

### Instructions
You have up to 7 calendar days to deliver the project from the date you received this document.

- The code must be versioned in a public GitHub repository, and a link must be provided for evaluation once completed.
- Clone this repository and start working from it.
- Carefully read the instructions and make sure all requirements are addressed.
- The repository must provide clear instructions on how to configure, run, and test the project.
- Documentation and overall organization will be considered in the evaluation.

---

## 🛒 Use Case
You are a developer in the **DeveloperStore** team. Your task is to implement a **CRUD API** for managing sales records.

Since we follow **DDD (Domain-Driven Design)**, we use the **External Identities** pattern with entity description denormalization to reference entities from other domains.

The API should support the following functionalities:
- **Registering sales**
- **Retrieving a sale by ID**
- **Updating a sale**
- **Deleting a sale**

Each sale record must contain:
- **Sale number**
- **Sale date**
- **Customer ID**
- **Branch ID**
- **Total sale amount**
- **List of products sold, including:**
  - Product ID
  - Quantity
  - Unit price
  - Discounts applied
  - Total amount for each item
- **Sale status (Cancelled/Not Cancelled)**

---

## 📜 Business Rules
The following discount rules apply when adding items to a sale:

### Discount Tiers:
- Orders with **4 or more** identical items get a **10% discount**.
- Orders with **10 to 20** identical items get a **20% discount**.

### Restrictions:
- It's **not possible** to purchase more than **20 identical items**.
- Orders with **less than 4** items **are not eligible** for any discount.

---

## 🚀 Tech Stack
The project was developed using the following technologies:

- **.NET 8.0** (C#)
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **PostgreSQL & MongoDB**
- **xUnit & NSubstitute (for unit testing)**
- **Faker.NET (for test data generation)**
- **Swagger (OpenAPI)**
- **Docker (optional, for containerization)**

---

## 📂 Project Structure
```
backend/
├── src/
│   ├── Ambev.DeveloperEvaluation.WebApi/  # Main API
│   ├── Ambev.DeveloperEvaluation.Application/  # Application Layer
│   ├── Ambev.DeveloperEvaluation.Domain/  # Domain Layer
│   ├── Ambev.DeveloperEvaluation.Infrastructure/  # Data Persistence
│   └── Ambev.DeveloperEvaluation.Common/  # Cross-cutting Concerns
│
├── tests/
│   ├── Ambev.DeveloperEvaluation.Unit/  # Unit Tests
│   ├── Ambev.DeveloperEvaluation.Integration/  # Integration Tests
│   └── Ambev.DeveloperEvaluation.Functional/  # Functional Tests
│
└── .doc/  # Documentation files
```

---

## ⚙️ How to Run the Project

### 1️⃣ Clone the Repository
```sh
git clone https://github.com/brunoternavisk/AmbtevTech-Test
cd developer-evaluation
```

### 2️⃣ Set Up the Database
Ensure you have **PostgreSQL** and **MongoDB** running. Use the following commands to create the database:
```sh
psql -U postgres -c "CREATE DATABASE developer_evaluation;"
```
If using Docker, run:
```sh
docker-compose up -d
```

### 3️⃣ Run Migrations
```sh
dotnet ef database update --project src/Ambev.DeveloperEvaluation.Infrastructure
```

### 4️⃣ Run the API Locally
```sh
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```
The API should be running on `http://localhost:5119` by default.

### 5️⃣ Run Tests
```sh
dotnet test
```

### 6️⃣ Swagger Documentation
Once the API is running, open your browser and access:
```
http://localhost:5119/swagger
```
This will provide a user-friendly interface to test API endpoints.

---

## 🔗 API Endpoints

### 📌 **Carts**
#### Create a Cart
`POST /api/Cart`
```json
{
    "userId": 321,
    "products": [
        {
            "productId": 700,
            "quantity": 5,
            "unitPrice": 20.0
        }
    ]
}
```

#### Get a Cart by ID
`GET /api/Cart/{id}`

#### Update a Cart
`PUT /api/Cart/{id}`
```json
{
    "id": 3,
    "userId": 321,
    "products": [
        {
            "productId": 700,
            "quantity": 10,
            "unitPrice": 15.0
        }
    ]
}
```

#### Delete a Cart
`DELETE /api/Cart/{id}`

---

## 📌 Additional Considerations
- Events such as `CartCreated`, `CartUpdated`, `CartDeleted` could be logged or published to a message broker for further processing.
- The repository follows **Clean Code**, **SOLID**, and **DRY** principles for maintainability.
- Further improvements could include **unit testing coverage reports** and **database persistence with SQL Server** instead of PostgreSQL.

---

## 📜 License
This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👥 Authors
- **Bruno Ternavisk** - https://github.com/brunoternavisk/AmbtevTech-Test

---

Feel free to modify the documentation as needed! 🚀

