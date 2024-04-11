using Application.Services.Contracts;
using API.Data;
using Infrastructure;
using CInvoice = Domain.Data.Entities.Invoice;
using Application.Services;
using Application.Services.Rules;
using Domain.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.SetupRepos();
builder.Services.AddTransient<IRule, ApproveByAccountingManager>();
builder.Services.AddTransient<IRule, ApproveByDepartmentManger>();
builder.Services.AddTransient<IRule, ApproveByTeamManager>();
builder.Services.AddTransient<IApprovalService, ApprovalService>();

var app = builder.Build();
await app.Services.InitData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/submit", async (RequestDTO dto, IApprovalService approvalService) => {

    var invoice = Map(dto);
    await approvalService.Process(invoice);

    return Results.Ok();
})
.WithName("Submit")
.WithOpenApi();

app.MapGet("/approvers", async (IApprovalService approvalService) => {
    var result = await approvalService.GetAll();

    return Results.Ok(result);
})
.WithName("Approvers")
.WithOpenApi();

app.Run();

static CInvoice Map(RequestDTO request)
{
    var result = new CInvoice
    {
        EmployeeId = request.Author.Id,
        Date = request.Invoice.Date,
        Department = new Department() 
        {
            Name = request.Invoice.Department
        },
        DocumentLink = request.Invoice.DocumentLink,
        Number = request.Invoice.Number,
        PaymentDeadline = request.Invoice.PaymentDeadline,
        TotalAmount = request.Invoice.TotalAmount,
        VatAmount = request.VAT.Amount,
        VatRate = request.VAT.Rate,
    };

    return result;
}