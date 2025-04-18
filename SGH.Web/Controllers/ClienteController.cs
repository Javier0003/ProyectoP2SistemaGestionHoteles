﻿using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using SGH.Web.Models;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Domain.Entities.Reservation;
using System.Diagnostics;

namespace SGHT.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, ILogger<ClienteController> logger, IMapper mapper)
        {
            _logger = logger;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async  Task<IActionResult> Index()
        {
            var result = await _clienteService.GetAll();

            if (!result.Success)
                return View();

            return View(result.Data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _clienteService.GetById(id);
            if (!result.Success) return View();

            var mappedResult = _mapper.Map<UpdateClienteDto>(result.Data);

            return View(mappedResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateClienteDto cliente)
        {
            cliente.IdCliente = id;

            var result = await _clienteService.UpdateById(cliente);
            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SaveClienteDto cliente)
        {
            var result = await _clienteService.Save(cliente);

            if (!result.Success) return View();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var model = new DeleteClienteDto
            {
                IdCliente = id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteByID(DeleteClienteDto clienteDto)
        {
            if (clienteDto.IdCliente == 0)
            {
                ModelState.AddModelError("IdCliente", "El id del cliente no puede ser 0");
                return View(clienteDto);
            }

            var result = await _clienteService.DeleteById(clienteDto);

            if (!result.Success)
            {
                ModelState.AddModelError("", "Error eliminando cliente.");
                return View(clienteDto);
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
