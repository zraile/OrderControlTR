# OrderControlTR - Restaurant POS & Order Management System

A complete **ASP.NET Core 8** restaurant Point-of-Sale and order management system built with **Clean Architecture** principles.

## 🏗️ Architecture

This solution follows Clean Architecture with clear separation of concerns:

```
OrderControlTR/
├── src/
│   ├── OrderControlTR.Domain/          # Enterprise business rules
│   │   ├── Common/BaseEntity.cs
│   │   ├── Entities/                   # Domain entities
│   │   └── Enums/                      # Domain enumerations
│   │
│   ├── OrderControlTR.Application/     # Application business rules
│   │   ├── Common/Interfaces/          # Abstractions (IRepository, IUnitOfWork, etc.)
│   │   ├── DTOs/                       # Data Transfer Objects
│   │   ├── Mappings/                   # AutoMapper profiles
│   │   └── Validators/                 # FluentValidation validators
│   │
│   ├── OrderControlTR.Infrastructure/  # Infrastructure concerns
│   │   ├── Persistence/                # EF Core DbContext, Repository, UnitOfWork
│   │   └── Services/                   # AuthService, TenantService
│   │
│   └── OrderControlTR.API/            # Presentation layer
│       ├── Controllers/                # API Controllers
│       └── Middleware/                 # Exception & Tenant middleware
│
└── tests/
    ├── OrderControlTR.Domain.Tests/
    ├── OrderControlTR.Application.Tests/
    └── OrderControlTR.API.Tests/
```

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| ASP.NET Core 8 | Web API framework |
| Entity Framework Core 8 | ORM |
| PostgreSQL | Database |
| JWT Bearer | Authentication |
| BCrypt.Net-Next | Password hashing |
| AutoMapper | Object mapping |
| FluentValidation | Input validation |
| Swagger/OpenAPI | API documentation |
| xUnit | Unit testing |
| FluentAssertions | Test assertions |
| Moq | Mocking |

## 📦 Domain Entities

- **Restaurant** - Top-level tenant entity
- **Branch** - Restaurant branches
- **User** - System users with roles
- **Role** - Admin, Manager, Waiter, Chef, Cashier, Courier
- **MenuCategory** - Menu category groupings
- **MenuItem** - Individual menu items with pricing
- **Table** - Restaurant tables with status tracking
- **Order** - Customer orders (DineIn, TakeAway, Delivery)
- **OrderItem** - Individual items within an order
- **Payment** - Order payments (Cash, CreditCard, Online)
- **Customer** - Customer profiles

## 🔐 Authentication

JWT Bearer token authentication with refresh token support.

**Default Roles:**
- `Admin` - Full system access
- `Manager` - Branch management
- `Waiter` - Order taking
- `Chef` - Kitchen view
- `Cashier` - Payment processing
- `Courier` - Delivery management

## 📡 API Endpoints

### Auth
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login |
| POST | `/api/auth/refresh-token` | Refresh JWT token |
| GET | `/api/auth/me` | Get current user |

### Restaurants
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/restaurants` | List all restaurants |
| GET | `/api/restaurants/{id}` | Get restaurant |
| POST | `/api/restaurants` | Create restaurant (Admin) |
| PUT | `/api/restaurants/{id}` | Update restaurant (Admin) |
| DELETE | `/api/restaurants/{id}` | Delete restaurant (Admin) |

### Branches
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/branches` | List branches |
| GET | `/api/branches/{id}` | Get branch |
| POST | `/api/branches` | Create branch |
| PUT | `/api/branches/{id}` | Update branch |
| DELETE | `/api/branches/{id}` | Delete branch |

### Menu Categories & Items
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/menu-categories` | List categories |
| POST | `/api/menu-categories` | Create category |
| GET | `/api/menu-items` | List items |
| GET | `/api/menu-items/by-category/{id}` | Items by category |
| POST | `/api/menu-items` | Create item |

### Tables
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/tables` | List tables |
| GET | `/api/tables/by-branch/{id}` | Tables by branch |
| PUT | `/api/tables/{id}/status` | Update table status |

### Orders
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/orders` | List orders |
| GET | `/api/orders/active` | Active orders |
| GET | `/api/orders/by-table/{id}` | Orders by table |
| POST | `/api/orders` | Create order |
| PUT | `/api/orders/{id}/status` | Update order status |

### Payments
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/payments` | List payments |
| GET | `/api/payments/by-order/{id}` | Payments by order |
| POST | `/api/payments` | Create payment |

### Dashboard
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/dashboard/summary` | Daily summary |
| GET | `/api/dashboard/sales-by-date-range` | Sales analytics |
| GET | `/api/dashboard/payment-types-summary` | Payment breakdown |
| GET | `/api/dashboard/tables-status-summary` | Table status overview |

## 🚀 Running Locally

### Prerequisites
- .NET 8 SDK
- PostgreSQL 14+

### Setup

1. Clone the repository
2. Update connection string in `src/OrderControlTR.API/appsettings.Development.json`
3. Run migrations:
```bash
cd src/OrderControlTR.API
dotnet ef database update --project ../OrderControlTR.Infrastructure
```
4. Start the API:
```bash
dotnet run --project src/OrderControlTR.API
```
5. Open Swagger UI: `https://localhost:5001/swagger`

## 🐳 Running with Docker

```bash
docker-compose up -d
```

This starts:
- **API** on port `5000`
- **PostgreSQL** on port `5432`

## 🧪 Running Tests

```bash
dotnet test
```

## 🔧 Multi-tenancy

The system supports multi-tenancy via the `X-Restaurant-Id` HTTP header. Include this header in requests to scope data to a specific restaurant.

## 📝 Seed Data

The application seeds the following default data:
- 6 roles: Admin, Manager, Waiter, Chef, Cashier, Courier
- 1 demo restaurant
- 1 main branch
