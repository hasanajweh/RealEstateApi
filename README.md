# RealEstateApi

This is a real estate management API built with ASP.NET Core 8. The API allows users to manage real estate properties, submit advertise requests, and perform admin operations such as approving or rejecting property submissions. The project is integrated with a SQL Server database and is hosted on Somee.com.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
- [API Documentation](#api-documentation)
- [Deployment](#deployment)
- [License](#license)

## Features

- **Property Management**: Create, update, and delete properties.
- **Advertise Requests**: Users can submit requests to advertise properties.
- **Admin Approval**: Admins can approve or reject property submissions.
- **Swagger UI**: API documentation and testing using Swagger.

## Technologies

- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server**
- **Swagger for API Documentation**
- **Somee.com for Deployment**

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or any preferred IDE

### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/your-username/RealEstateApi.git
    cd RealEstateApi
    ```

2. **Set up the database connection string:**

    Update the connection string in `appsettings.json` or `Program.cs`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=your_db_name;User Id=your_user_id;Password=your_password;"
    }
    ```

3. **Run database migrations:**

    Open the Package Manager Console in Visual Studio and run:

    
    Update-Database
  

4. **Run the application:**


    dotnet run


### Database Setup

Make sure you have SQL Server installed. The database will be automatically created using Entity Framework Core migrations. You can modify the connection string in `appsettings.json` to match your SQL Server instance.

## API Documentation

The API is documented and testable via Swagger. Once the application is running, navigate to the root URL to access the Swagger UI.

**Swagger URL:**
https://palestinerealestate.somee.com

### Available Endpoints

- **Get All Properties:** `GET /api/Property/GetAllProperties`
- **Get Property by ID:** `GET /api/Property/GetProperty/{id}`
- **Add Property:** `POST /api/Property/AddProperty`
- **Update Property:** `PUT /api/Property/UpdateProperty/{id}`
- **Delete Property:** `DELETE /api/Property/DeleteProperty/{id}`
- **Get Advertise Requests:** `GET /api/Property/Admin/GetAdvertiseRequests`
- **Approve Request:** `PUT /api/Property/Admin/ApproveRequest/{id}`
- **Reject Request:** `PUT /api/Property/Admin/RejectRequest/{id}`

## Deployment

The project is hosted on Somee.com. Here’s how to deploy it:

1. **Create an account** on [Somee.com](https://somee.com).
2. **Create a new ASP.NET Hosting service** and a SQL Server database.
3. **Update the connection string** in `appsettings.json` with the details from Somee.com.
4. **Publish the project** using Visual Studio:
   - Right-click on the project, select `Publish`.
   - Choose `Folder` as the target, then upload the published files to Somee.com using their File Manager.
5. **Access the deployed site**: Navigate to `https://palestinerealestate.somee.com`.




