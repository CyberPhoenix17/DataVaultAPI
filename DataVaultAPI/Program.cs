using DataVaultAPI.Models;
using DataVaultAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registers the Dapper repository for Dependency Injection
builder.Services.AddScoped<IVaultRepository, VaultRepository>();

// Adds services to the container (for Swagger documentation)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configures the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// 1. GET: Retrieves all vault records
app.MapGet("/api/vault", async (IVaultRepository repo) =>
{
    var records = await repo.GetAllAsync();
    return Results.Ok(records);
});

// 2. GET: Retrieves a single record by ID
app.MapGet("/api/vault/{id}", async (int id, IVaultRepository repo) =>
{
    var record = await repo.GetByIdAsync(id);
    return record is not null ? Results.Ok(record) : Results.NotFound();
});

// 3. POST: Creates a new vault record
app.MapPost("/api/vault", async (VaultRecord record, IVaultRepository repo) =>
{
    var newId = await repo.CreateAsync(record);
    record.Id = newId;
    return Results.Created($"/api/vault/{newId}", record);
});

// 4. PUT: Updates an existing record
app.MapPut("/api/vault/{id}", async (int id, VaultRecord record, IVaultRepository repo) =>
{
    if (id != record.Id)
    {
        return Results.BadRequest("ID mismatch between route and payload.");
    }

    var rowsAffected = await repo.UpdateAsync(record);
    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
});

// 5. DELETE: Removes a record by ID
app.MapDelete("/api/vault/{id}", async (int id, IVaultRepository repo) =>
{
    var rowsAffected = await repo.DeleteAsync(id);
    return rowsAffected > 0 ? Results.NoContent() : Results.NotFound();
});

app.Run();