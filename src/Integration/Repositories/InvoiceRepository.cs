using Application.Services;
using Domain.Data.Entities;
using Infrastructure.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class InvoiceRepository(DataContext context) : Repository<Invoice>(context), 
                                                        IInvoiceRepo
{

    public override async Task<List<Invoice>> GetAll()
    {
        var result = await _context.Invoices.Include(x => x.Approvers).ToListAsync();

        return result;
    }
}
