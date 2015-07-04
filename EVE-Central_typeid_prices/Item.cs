using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVE_Central_typeid_prices
{
    class Item
    {
        private string _typeid = null;
        private string _buy_max = null;
        private string _sell_min = null;

        public Item(string typeid, string buy_max, string sell_min)
        {
            _typeid = typeid;
            _buy_max = buy_max;
            _sell_min = sell_min;
        }

        public string GetTypeID()
        {
            return _typeid;
        }

        public decimal GetBuyMax()
        {
            return Convert.ToDecimal(_buy_max);
        }

        public decimal GetSellMin()
        {
            return Convert.ToDecimal(_sell_min);
        }

        public void SetTypeID(string typeid)
        {
            _typeid = typeid;
        }

        public void SetBuyMax(string buy_max)
        {
            _buy_max = buy_max;
        }

        public void SetSellMin(string sell_min)
        {
            _sell_min = sell_min;
        }
    }
}
