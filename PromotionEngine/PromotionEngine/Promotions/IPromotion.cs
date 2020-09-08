using PromotionEngine.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Promotions
{
   public interface IPromotion
    {
        int Promotion1(List<SelectedCart> skuList);
    }
}
