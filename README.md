# InventoryManagement

A small, library-focused .NET 8 project that demonstrates an inventory management domain with:
- A flexible `Product` model that supports polymorphic JSON (`size` can be number, string, or object).
- A stateless disk-based source (`DiskInventorySource`) that performs CRUD directly on a JSON file.
- A cached inventory service (`CachedInventoryService`) with a 5-minute per-product cache.
- Unit tests using NUnit and NSubstitute.

This solution intentionally has no executable entry point; it focuses on finishing abandoned work and showcasing extensibility.

## Tech Stack

- .NET 8
- C# 12
- System.Text.Json
- NUnit (tests)
- NSubstitute (mocks)

## Project Structure

- `InventoryManagement/Model/Product.cs`  
  Product data structure with `JsonElement Size` for polymorphic sizes.

- `InventoryManagement/Source/IInventorySource.cs`  
  Contract for inventory sources.

- `InventoryManagement/Source/DiskInventorySource.cs`  
  Stateless CRUD implementation that reads/writes the JSON file each call.

- `InventoryManagement/Service/IInventoryService.cs`  
  High-level service contract for operations on products.

- `InventoryManagement/Service/InventoryService.cs`  
  Delegates to an `IInventorySource`.

- `InventoryManagement/Service/CachedInventoryService.cs`  
  Wraps an `IInventoryService` and caches product lookups for 5 minutes.

- `InventoryManagement.Test/*`  
  Unit tests and test utilities.

## Key Design Decisions

- Stateless disk access: Every CRUD call reads the full file and writes back changes; no in-memory persistence between calls.
- Polymorphic `size`: Represented as `JsonElement` to support number, string, or object from JSON.
- Caching: Per-product cache with a default timeout of 5 minutes. On cache miss or expiration, the inner service is queried.

## Getting Started

1. Open the solution in Visual Studio 2022.
2. Ensure the project targets .NET 8.
3. Restore packages:
   - Use __Project > Manage NuGet Packages__ on the test project, or run `dotnet restore`.
4. Run tests:
   - Use __Test > Run All Tests__ or `dotnet test`.