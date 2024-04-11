using Domain.Data.Entities;

namespace Application.Services.Contracts;

public interface IApprovalService
{
    Task Process(Invoice invoice);

    Task<List<string>> GetAll();
}
