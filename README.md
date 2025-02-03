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
- **In-Memory Database (for testing purposes)**
- **xUnit** (for unit testing)
- **Swagger (OpenAPI)**
- **Docker** (optional, for containerization)

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

### 2️⃣ Run the API Locally
```sh
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

The API should be running on `http://localhost:5119` by default.

### 3️⃣ Run Tests
```sh
dotnet test
```

### 4️⃣ Swagger Documentation
Once the API is running, open your browser and access:
```
http://localhost:5119/swagger
```

This will provide a user-friendly interface to test API endpoints.

---

## 🔗 API Endpoints

### 📌 **Sales**
#### Create a Sale
`POST /api/Sale`
```json
{
    "saleNumber": "987652",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "items": [
        {
            "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "productDescription": "Gaming Chair",
            "quantity": 1,
            "unitPrice": 500
        }
    ]
}
```

#### Get a Sale by ID
`GET /api/Sale/{id}`

#### Update a Sale
`PUT /api/Sale/{id}`
```json
{
    "id": SALE_ID_HERE
    "saleNumber": "987652",
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "branchId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "items": [
        {
            "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "productDescription": "Gaming Chair",
            "quantity": 2,
            "unitPrice": 450
        }
    ]
}
```

#### Delete a Sale
`DELETE /api/Sale/{id}`

---

## 📌 Additional Considerations
- Events such as `SaleCreated`, `SaleModified`, `SaleCancelled`, and `ItemCancelled` could be logged or published to a message broker for further processing.
- The repository follows **Clean Code**, **SOLID**, and **DRY** principles for maintainability.
- Further improvements could include **unit testing coverage reports** and **database persistence with SQL Server** instead of an in-memory database.

---

## 📜 License
This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👥 Authors
- **Bruno Ternavisk** - https://github.com/brunoternavisk/AmbtevTech-Test

---

Feel free to modify the documentation as needed! 🚀

