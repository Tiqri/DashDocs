﻿using DashDocs.Models;
using DashDocs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DashDocs.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        public async Task<ActionResult> Enroll(Customer customer)
        {
            if (!Request.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(customer.Name))
                {
                    customer.Id = Guid.Parse(Request.QueryString["tid"].Trim());
                    var user = new User
                    {
                        Id = Guid.Parse(Request.QueryString["uid"].Trim()),
                        FirstName = Request.QueryString["fn"].Trim(),
                    };
                    customer.Users.Add(user);

                    var context = new DashDocsContext();
                    context.Customers.Add(customer);
                    await context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}