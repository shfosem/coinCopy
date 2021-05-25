using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCopy
{
    public class rowData
    {
        public string stockName;             // 종목명
        public decimal quantity;             // 보유수량
        public decimal currentCost;          // 현재가
        public decimal averageCost;          // 매수평균가
        public decimal profitPercentage;     // 손익률
        public decimal valuationGainOrLoss;  // 평가손익
        public decimal valuationCost;        // 평가금액
        public decimal purchaseCost;         // 매입금액
    }
}
