using App.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name ="Image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
