﻿using Course.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Course.Web.Services.Interfaces;

namespace Course.Web.Controllers
{
	public class AuthController : Controller
	{
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInInput signinInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var response = await _identityService.SignIn(signinInput);

            if (!response.IsSuccessful)
            {
                response.Errors.ForEach(x =>
                {
                    ModelState.AddModelError(String.Empty, x);
                });

                return View();
            }

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
