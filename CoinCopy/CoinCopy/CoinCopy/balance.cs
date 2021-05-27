using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCopy
{
    public class balance
    {

        public long INIT_CASH = 100000000000;

        // 자산 ( 보유 현금 + 유가 총액 ) 
        private long totalAsset;

        // 보유 현금
        private long cash;

        //손익 금액 (totalPrice - purchaseCost ) 
        private long profit;

        //매입 금액
        private long purchaseAmount;

        //유가 총액 ( 현시점 기준으로 보유한 주식의 현 시가의 총합 )
        private long currentTotalPrice;

        //수익률
        private double profitPercentage;

        public balance()
        {
            // 1억
            cash = INIT_CASH;
            totalAsset = cash;
            profit = 0;
            purchaseAmount = 0;
            currentTotalPrice = 0;
            profitPercentage = 0;
        }

        public double getProfitPercentage()
        {
            return this.profitPercentage;
        }

        public void setProfitPercentage(double percentage)
        {
            this.profitPercentage = percentage; 
        }

        public void profitPercentageCalculation()
        {
            try
            {
                double pp = Convert.ToDouble(this.profit);
                double pa = Convert.ToDouble(INIT_CASH);
                this.profitPercentage = Math.Round( pp / pa * 100,5);                

            } catch (DivideByZeroException e)
            {
                this.profitPercentage = 0;
            }
            return;
        }

        public long getTotalAsset()
        {
            return this.totalAsset;
        }
        public void setTotalAsset(long amount)
        {
            this.totalAsset = amount;   
        }


        public long getCash()
        {
            return this.cash;
        }
        public void setCash(long amount)
        {
            this.cash = amount;
        }


        public long getProfit()
        {
            return this.profit;
        }
        public void setProfit(long amount)
        {
            this.profit = amount;
        }


        public long getPurchaseAmount()
        {
            return this.purchaseAmount;
        }
        public void setPurchaseAmount(long amount)
        {
            this.purchaseAmount = amount;
        }


        public long getCurrentTotalPrice()
        {
            return this.currentTotalPrice;
        }
        public void setCurrentTotalPrice(long amount)
        {
            this.currentTotalPrice = amount;
        }

        /*
         * 수익 / 매입 금액 * 100 
         * 수익 == 총자산 - 매입금액
         * 퍼센트 반환하는 함수
         * 소수점 셋째 자리 반올림
         */
        public double calcProfitPercentage()
        {
            double profit = Convert.ToDouble(this.profit);
            double pA = Convert.ToDouble(this.purchaseAmount);
            return Math.Round(profit / pA * 100 , 3);
        }
    }
}
