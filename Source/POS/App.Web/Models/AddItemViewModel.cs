﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Product")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a product.")]
        public int ProductId { get; set; }
        public int POId { get; set; }

        //public decimal Price { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "The quantiy must be a positive number")]
        public double Quantity { get; set; }
        public double Percent { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }

    }
}
