using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Vendedor> Vendedor { get; set; } = new List<Vendedor>();

        public Departamento()
        {

        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AddVendedor(Vendedor vendedor)
        {
            Vendedor.Add(vendedor);
        }

        public double TotalSales(DateTime inicio, DateTime final)
        {
            return Vendedor.Sum(vendedor => vendedor.TotalVendas(inicio, final));
        }
    }
}
