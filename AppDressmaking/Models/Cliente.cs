﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppDressmaking.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}