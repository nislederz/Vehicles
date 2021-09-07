﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: fix the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44333/images/noimage.png"
            : $"https://vehiclesnico.blob.core.windows.net/vehicles/{ImageId}";
    }
}