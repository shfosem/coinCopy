using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCopy
{
    public class rowData
    {
        public string stockName;            // 종목명
        public double quantity;                // 보유수량
        public double currentCost;          // 현재가
        public double averageCost;          // 매수평균가
        public double profitPercentage;     // 손익률
        public double valuationGainOrLoss;  // 평가손익
        public double valuationCost;        // 평가금액
        public double purchaseCost;         // 매입금액
    }
}
