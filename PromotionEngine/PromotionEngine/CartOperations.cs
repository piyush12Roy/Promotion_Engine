using Microsoft.Extensions.Configuration;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
   public class CartOperations : ICartOperations
    {
        private readonly IConfiguration _config;
        public CartOperations(IConfiguration config)
        {
            _config = config;
        }
        public int CheckOut()
        {
           
            return 0;
        }
    }
}
