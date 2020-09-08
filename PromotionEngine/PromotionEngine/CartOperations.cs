using Microsoft.Extensions.Configuration;
using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PromotionEngine.Utility;

namespace PromotionEngine
{
   public class CartOperations : ICartOperations
    {
        public delegate int CalculateOrderValue(List<SelectedCart> skuList);//Delegate to create active promotion class method
        private readonly IConfiguration _config;

        public CartOperations(IConfiguration config)
        {
            _config = config;
        }
        public int CheckOut(List<SelectedCart> selectedSKUs)
        {

            try
            {
                //Get active promotion Name and active promotion type from appdettings .json
                ActivePromotionDetails activePromotionDetails = new ActivePromotionDetails();
                _config.GetSection(nameof(ActivePromotionDetails)).Bind(activePromotionDetails);

                //Get cart and their unit price
                List<Cart> cartDetails = new List<Cart>();
                _config.GetSection(nameof(Cart)).Bind(cartDetails);

                //assing active promotion to delegate
                CalculateOrderValue calculateOrderValue = GetActivePromotion(activePromotionDetails);

                //There is a active promotion available else exclude promotion
                if (calculateOrderValue != null)
                    return calculateOrderValue(selectedSKUs);
                else
                {
                    int orderValue = 0;
                    Cart temp = new Cart();
                    foreach (var item in selectedSKUs)
                    {
                        switch (item.SKUIds)
                        {
                            case 'A':
                                temp = cartDetails.Where(x => x.SKUIds == 'A').FirstOrDefault();
                                orderValue += temp.UnitPrice * item.Quantity;
                                break;
                            case 'B':
                                temp = cartDetails.Where(x => x.SKUIds == 'B').FirstOrDefault();
                                orderValue += temp.UnitPrice * item.Quantity; ;
                                break;
                            case 'C':
                                temp = cartDetails.Where(x => x.SKUIds == 'C').FirstOrDefault();
                                orderValue += temp.UnitPrice * item.Quantity; ;
                                break;
                            case 'D':
                                temp = cartDetails.Where(x => x.SKUIds == 'D').FirstOrDefault();
                                orderValue += temp.UnitPrice * item.Quantity; ;
                                break;
                            default:
                                break;
                        }
                    }
                    return orderValue;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private CalculateOrderValue GetActivePromotion(ActivePromotionDetails activePromotionDetails)
        {
            CalculateOrderValue calculateOrderValue = null;
            switch (activePromotionDetails.PromotionName)
            {
                case nameof(PromotionList.Promotion1):
                    calculateOrderValue = null;
                    break;
                default:
                    break;
            }

            return calculateOrderValue;

        }
    }
}
