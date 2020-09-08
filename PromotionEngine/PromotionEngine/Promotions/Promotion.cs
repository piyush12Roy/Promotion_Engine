using Microsoft.Extensions.Configuration;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PromotionEngine.Utility;

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
            int returnValue = 0;
            if (activePromotionDetails.Value.ActivePromotionTypes.Count > 0)
            {
                foreach (var value in activePromotionDetails.Value.ActivePromotionTypes)
                {

                    switch (value)
                    {
                        case nameof(Promotiontypes.Sku_A_Promotion_Type):
                            returnValue = GetAValue(skuList.FirstOrDefault(x => x.SKUIds == 'A'), _cartDetails);
                            break;
                        case nameof(Promotiontypes.Sku_B_Promotion_Type):
                            returnValue += GetBValue(skuList.FirstOrDefault(x => x.SKUIds == 'B'), _cartDetails);
                            break;
                        case nameof(Promotiontypes.Sku_C_D_Promotion_Type):
                            returnValue += GetCDValue(skuList.Where(x => x.SKUIds == 'C' || x.SKUIds == 'D'), _cartDetails);
                            break;
                        default:
                            break;
                    }
                }
            }


            return returnValue;
        }

        private int GetCDValue(IEnumerable<SelectedCart> enumerable, Lazy<List<Cart>> cartDetails)
        {
            return 0;
        }

        private int GetBValue(SelectedCart selectedCart, Lazy<List<Cart>> cartDetails)
        {
            return 0;
        }

        private int GetAValue(SelectedCart selectedCart, Lazy<List<Cart>> cartDetails)
        {
            return 0;
        }
    }
}
