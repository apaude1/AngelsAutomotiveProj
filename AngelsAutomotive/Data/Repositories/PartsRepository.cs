using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class PartsRepository : GenericRepository<Parts>,  IPartsRepository
    {
        private readonly DataContext _context;

        public PartsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetAllParts()
        {
            var model = new AddServiceItemViewModel();
            var list = _context.Parts.Select(p => new SelectListItem
            {
                Text = p.PartName,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Part...)",
                Value = "0"
            });

            return list;
        }
    }
}
