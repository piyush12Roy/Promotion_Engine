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
            _cartDetails = new Lazy<List<Cart>>(() => LoadCartDetails()); // Loading card details from appsettings.json
            activePromotionDetails = new Lazy<ActivePromotionDetails>(() => LoadPromotionDetails());  // Loading promotion/Types details from appsettings.json

        }

      

        public int Promotion1(List<SelectedCart> skuList)
        {
            int orderValue = 0;
            if (activePromotionDetails.Value.ActivePromotionTypes.Count > 0)
            {
                foreach (var value in activePromotionDetails.Value.ActivePromotionTypes)
                {
                    //Swiching all active Promomotions types and calculating OrderValue
                    switch (value)
                    {
                        case nameof(Promotiontypes.Sku_A_Promotion_Type): 
                            orderValue = GetAValue(skuList.FirstOrDefault(x => x.SKUIds == 'A'), _cartDetails);
                            break;
                        case nameof(Promotiontypes.Sku_B_Promotion_Type):
                            orderValue += GetBValue(skuList.FirstOrDefault(x => x.SKUIds == 'B'), _cartDetails);
                            break;
                        case nameof(Promotiontypes.Sku_C_D_Promotion_Type):
                            orderValue += GetCDValue(skuList.Where(x => x.SKUIds == 'C' || x.SKUIds == 'D'), _cartDetails);
                            break;
                        default:
                            break;
                    }
                }
            }


            return orderValue;
        }

        #region Private method
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
        private int GetCDValue(IEnumerable<SelectedCart> selectedCart, Lazy<List<Cart>> cartDetails)
        {
            if (selectedCart == null) return 0;
            Cart valueC = cartDetails.Value.Where(x => x.SKUIds == 'C').FirstOrDefault();
            Cart valueD = cartDetails.Value.Where(x => x.SKUIds == 'D').FirstOrDefault();
            SelectedCart cartC = selectedCart.ToList().Where(x => x.SKUIds == 'C').FirstOrDefault();
            SelectedCart cartD = selectedCart.Where(x => x.SKUIds == 'D').FirstOrDefault();

            if (cartC == null && cartD == null) return 0;
            if (cartC == null) return (cartD.Quantity * valueD.UnitPrice);
            if (cartD == null) return (cartC.Quantity * valueC.UnitPrice);
            else
            {
                if (cartC.Quantity == cartD.Quantity) return (cartC.Quantity * 30);
                else if (cartC.Quantity < cartD.Quantity)
                {
                    return (cartC.Quantity * 30) + ((cartD.Quantity - cartC.Quantity) * 15);
                }
                else
                {
                    return (cartD.Quantity * 30) + ((cartC.Quantity - cartD.Quantity) * 20);
                }
            }

        }

        private int GetBValue(SelectedCart selectedCart, Lazy<List<Cart>> cartDetails)
        {
            Cart valueB = cartDetails.Value.Where(x => x.SKUIds == 'B').FirstOrDefault();
            if (selectedCart == null) return 0;
            if (selectedCart.Quantity < 2) return (selectedCart.Quantity * valueB.UnitPrice);

            else if (selectedCart.Quantity % 2 == 0) return ((selectedCart.Quantity / 2) * 45);

            else
            {
                int count = 0;
                int sum = 0;
                while (sum < selectedCart.Quantity)
                {
                    sum += 2;
                    if (sum < selectedCart.Quantity)
                    {
                        count++;
                    }
                }

                return (count * 45) + ((selectedCart.Quantity - (sum - 2)) * valueB.UnitPrice);
            }

        }

        private int GetAValue(SelectedCart selectedCart, Lazy<List<Cart>> cartDetails)
        {
            Cart valueA = cartDetails.Value.Where(x => x.SKUIds == 'A').FirstOrDefault();
            if (selectedCart == null) return 0;
            if (selectedCart.Quantity < 3) return (selectedCart.Quantity * valueA.UnitPrice);

            else if (selectedCart.Quantity % 3 == 0) return ((selectedCart.Quantity / 3) * 130);

            else
            {
                int count = 0;
                int sum = 0;
                while (sum < selectedCart.Quantity)
                {
                    sum += 3;
                    if (sum < selectedCart.Quantity)
                    {
                        count++;
                    }
                }

                return (count * 130) + ((selectedCart.Quantity - (sum - 3)) * valueA.UnitPrice);
            }
        }

        #endregion
    }
}
