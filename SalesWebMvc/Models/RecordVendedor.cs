using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class RecordVendedor
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        public StatusVendas Status { get; set; }
        public Vendedor Vendedor { get; set; }

        public RecordVendedor()
        {

        }

        public RecordVendedor(int id, DateTime date, double amount, StatusVendas status, Vendedor vendedor)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Vendedor = vendedor;
        }
    }
}
