using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double  BaseSalario { get; set; }

        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }

        public ICollection<RecordVendedor> Vendas { get; set; } = new List<RecordVendedor>();

        public Vendedor()
        {

        }

        public Vendedor(int id, string nome, string email, DateTime birthDate, double baseSalario, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            BirthDate = birthDate;
            BaseSalario = baseSalario;
            Departamento = departamento;
        }

        public void AddVendas(RecordVendedor sr)
        {
            Vendas.Add(sr);
        }

        public void RemoveVendas(RecordVendedor sr)
        {
            Vendas.Remove(sr);
        }

        public double TotalVendas(DateTime inicio , DateTime final)
        {
            return Vendas.Where(sr => sr.Date >= inicio && sr.Date <= final).Sum(sr => sr.Amount);
            
        }

    }
}
