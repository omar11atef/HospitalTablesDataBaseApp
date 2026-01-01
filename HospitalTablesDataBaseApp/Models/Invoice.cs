using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalTablesDataBaseApp.Models
{
    public class Invoice
    {
        
            public int Id { get; set; }
            public DateTime DateIssued { get; set; }
            public decimal TotalAmount { get; set; }
            public bool IsPaid { get; set; }

            // relationship to Patient
            public int PatientId { get; set; }
            public Patients? Patient { get; set; }

            // detailed items in the invoice
            public ICollection<InvoiceItem> Items { get; set; }
     }

            public class InvoiceItem
            {
                public int Id { get; set; }
                public string? ServiceName { get; set; } // مثال: "كشف عيادة", "إقامة 3 أيام", "تحليل دم"
                public decimal Price { get; set; }

                public int InvoiceId { get; set; }
                public Invoice? Invoice { get; set; }
            }
    
}
