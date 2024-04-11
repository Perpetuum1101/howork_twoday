namespace API.Data;

public class RequestDTO
{
    public Author Author { get; set; } = null!;
    public Invoice Invoice { get; set; } = null!;
    public InvoicingCompany InvoicingCompany { get; set; } = null!;
    public Vat VAT { get; set; } = null!;
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class Invoice
{
    public string Number { get; set; } = null!;
    public string Department { get; set; } = null!;
    public DateTime Date { get; set; }
    public float TotalAmount { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public string DocumentLink { get; set; } = null!;
}

public class InvoicingCompany
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string CompanyCode { get; set; } = null!;
    public string VATNumber { get; set; } = null!;
}

public class Vat
{
    public float Rate { get; set; }
    public float Amount { get; set; }
}
