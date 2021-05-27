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
        public long quantity;                // 보유수량
        public long currentCost;          // 현재가
        public long averageCost;          // 매수평균가
        public double profitPercentage;     // 손익률
        public long valuationGainOrLoss;  // 평가손익
        public long valuationCost;        // 평가금액
        public long purchaseCost;         // 매입금액
    }
}
