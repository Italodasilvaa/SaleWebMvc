using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class RecordVendasService
    {
        private readonly SalesWebMvcContext _context;

        public RecordVendasService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<RecordVendedor>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RecordVendedor select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<IGrouping<Departamento,RecordVendedor>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RecordVendedor select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Date)
                .GroupBy(x=> x.Vendedor.Departamento)
                .ToListAsync();
        }
    }
}