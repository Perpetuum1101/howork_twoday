using Domain.Data.Entities;

namespace Application.Services.Contracts;

public interface IRule
{
    Task Apply(Invoice invoice);
}
