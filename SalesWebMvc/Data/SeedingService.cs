using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departamento.Any() ||
                _context.Vendedor.Any() ||
                _context.RecordVendedor.Any())
            {
                return;//DB JA FOI POPULADO 
            }

            Departamento d1 = new Departamento(1, " Computadores");
            Departamento d2 = new Departamento(2, " Eletronicos");
            Departamento d3 = new Departamento(3, " Livros");
            Departamento d4 = new Departamento(4, " Fashion");

            Vendedor s1 = new Vendedor(1, " Bob Brown", " bob@gmail.com", new DateTime(1998, 4, 21), 1000.00, d1);
            Vendedor s2 = new Vendedor(2, " Maria", " Maria@gmail.com", new DateTime(1978, 4, 21), 1900.00, d2);
            Vendedor s3 = new Vendedor(3, "Brown", " brown@gmail.com", new DateTime(1958, 4, 11), 3000.00, d1);
            Vendedor s4 = new Vendedor(4, " Marcos", " marcos@gmail.com", new DateTime(1988, 4, 01), 1800.00, d4);
            Vendedor s5 = new Vendedor(5, " Julian", " julian@gmail.com", new DateTime(2002, 4, 06), 1200.00, d3);


            RecordVendedor v1 = new RecordVendedor(1, new DateTime(2018, 09, 25), 11000.0,StatusVendas.Billed , s1);
            RecordVendedor v2 = new RecordVendedor(2, new DateTime(2019, 09, 24), 21000.0,StatusVendas.Billed , s2);
            RecordVendedor v3 = new RecordVendedor(3, new DateTime(2018, 09, 05), 20000.0,StatusVendas.Billed , s3);
            RecordVendedor v4 = new RecordVendedor(4, new DateTime(2018, 09, 15), 8000.0,StatusVendas.Billed , s4);
            RecordVendedor v5 = new RecordVendedor(5, new DateTime(2018, 09, 06), 6000.0,StatusVendas.Billed , s5);

            _context.Departamento.AddRange(d1, d2, d3, d4);
            _context.Vendedor.AddRange(s1, s2, s3, s4, s5);
            _context.RecordVendedor.AddRange(v1,v2,v3,v4,v5);

            _context.SaveChanges();
        }
        
    }
}
