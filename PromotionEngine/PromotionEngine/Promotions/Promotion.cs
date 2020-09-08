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
            ActivePromotionDetails activePromotionDetails = new ActivePromotionDetails();
            _config.GetSection(nameof(ActivePromotionDetails)).Bind(activePromotionDetails);
            return activePromotionDetails;
        }

        private List<Cart> LoadCartDetails()
        {

            List<Cart> cartDetails = new List<Cart>();
            _config.GetSection(nameof(Cart)).Bind(cartDetails);
            return cartDetails;
        }

        public int Promotion1(List<SelectedCart> skuList)
        {
            return 0;
        }
    }
}
