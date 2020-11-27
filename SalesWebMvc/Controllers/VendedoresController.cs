﻿using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using System.Diagnostics;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ModelViews;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService)
        {
            _vendedorService = vendedorService;
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var ViewModel = new VendedorFormViewModel { Departamentos = departamentos };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
            await _vendedorService.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vendedorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj =  await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {               
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            List<Departamento> departments = await _departamentoService.FindAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid)
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos };
                return View(viewModel);
            }
                if (id != vendedor.Id)
            {
                
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _vendedorService.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

         public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);

        }

    }
}
