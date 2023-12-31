﻿using Console_Project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Project.Common.Enum;

namespace Console_Project.Common.Model
{
    public class Product : BaseEntity
    {
        private static int Code1 { get; set; }
        public Product()
        {
            Code = Code1;
            Code1++; // This part define Code of Every products 
        } 
        public string ProdcutName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public Categories Categories { get; set; }
    }
}
