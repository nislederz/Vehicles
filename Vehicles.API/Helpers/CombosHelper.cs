﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Vehicles.API.Data;

namespace Vehicles.API.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;
        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboBrands()
        {
            List<SelectListItem> list = _context.Brands.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione una marca",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProcedures()
        {
            List<SelectListItem> list = _context.Procedures.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un procedimiento....",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetCombosDocumentTypes()
        {
            List<SelectListItem> list = _context.DocumentTypes.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un tipo de documento...",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboVehicleTypes()
        {
            List<SelectListItem> list = _context.VehiclesType.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "Seleccione un tipo de vehiculo",
                Value = "0"
            });

            return list;
        }
    }
}
