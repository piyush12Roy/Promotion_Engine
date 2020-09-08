using Microsoft.Extensions.Configuration;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Promotions
{
    public class Promotion : IPromotion
    {
        private static Lazy<List<Cart>> _cartDetails = null;
        private static Lazy<ActivePromotionDetails> activePromotionDetails;
        private readonly IConfiguration _config;

        public Promotion(IConfiguration config)
        {
            _config = config;
            _cartDetails = new Lazy<List<Cart>>(() => LoadCartDetails());
            activePromotionDetails = new Lazy<ActivePromotionDetails>(() => LoadPromotionDetails());

        }

        private ActivePromotionDetails LoadPromotionDetails()
        {
            throw new NotImplementedException();
        }

        private List<Cart> LoadCartDetails()
        {
            throw new NotImplementedException();
        }

        public int Promotion1(List<SelectedCart> skuList)
        {
            throw new NotImplementedException();
        }
    }
}
