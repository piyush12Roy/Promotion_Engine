using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Model
{
   public class SelectedCart
    {
        public char SKUIds { get; set; }
        public int Quantity { get; set; }
    }
    public class ActivePromotionDetails
    {
        public string PromotionName { get; set; }
        public List<string> ActivePromotionTypes { get; set; }

    }
}
