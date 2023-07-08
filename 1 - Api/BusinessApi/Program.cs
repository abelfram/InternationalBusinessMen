using Business.ServiceContracts;
using Business.ServiceImplementation;
using Domain.RepositoryContracts;
using Infrastructure.Data.RepositoryImplementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IListTransactionsService, ListTransactionsService>();
builder.Services.AddTransient<IRatesRepository, RatesRepository>();
builder.Services.AddTransient<IGetElemetnsBySKURepository, GetElemetnsBySKURepository>();
builder.Services.AddTransient<IConvertCurrencyToEuro, ConvertCurrencyToEuro>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();