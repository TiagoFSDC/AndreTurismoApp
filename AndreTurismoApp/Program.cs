﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AndreTurismoApp.Data;
using AndreTurismoApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AndreTurismoAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AndreTurismoAppContext") ?? throw new InvalidOperationException("Connection string 'AndreTurismoAppContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<AddressService>();
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<HotelService>();
builder.Services.AddSingleton<TicketService>();
builder.Services.AddSingleton<PacketService>();

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
