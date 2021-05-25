using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCopy
{
    public class balance
    {
        // 자산 ( 보유 현금 + 유가 총액 ) 
        private decimal totalAsset;

        // 보유 현금
        private decimal cash;

        //손익 금액 (totalPrice - purchaseCost ) 
        private decimal profit;

        //매입 금액
        private decimal purchaseAmount;

        //유가 총액 ( 현시점 기준으로 보유한 주식의 현 시가의 총합 )
        private decimal currentTotalPrice;

        //수익률
        private decimal profitPercentage;

        public balance()
        {
            // 1억
            cash = 100000000000;
            totalAsset = cash;
            profit = 0;
            purchaseAmount = 0;
            currentTotalPrice = 0;
            profitPercentage = 0;
        }

        public decimal getProfitPercentage()
        {
            return this.profitPercentage;
        }

        public void setProfitPercentage(decimal percentage)
        {
            this.profitPercentage = percentage; 
        }

        public void profitPercentageCalculation()
        {
            this.profitPercentage = this.profit / this.purchaseAmount * 100;
        }

        public decimal getTotalAsset()
        {
            return this.totalAsset;
        }
        public void setTotalAsset(decimal amount)
        {
            this.totalAsset = amount;   
        }


        public decimal getCash()
        {
            return this.cash;
        }
        public void setCash(decimal amount)
        {
            this.cash = amount;
        }


        public decimal getProfit()
        {
            return (this.purchaseAmount - this.currentTotalPrice);
        }
        public void setProfit(decimal amount)
        {
            this.profit = amount;
        }


        public decimal getPurchaseAmount()
        {
            return this.purchaseAmount;
        }
        public void setPurchaseAmount(decimal amount)
        {
            this.purchaseAmount = amount;
        }


        public decimal getCurrentTotalPrice()
        {
            return this.currentTotalPrice;
        }
        public void setCurrentTotalPrice(decimal amount)
        {
            this.currentTotalPrice = amount;
        }

        /*
         * 수익 / 매입 금액 * 100 
         * 수익 == 총자산 - 매입금액
         * 퍼센트 반환하는 함수
         * 소수점 셋째 자리 반올림
         */
        public decimal calcProfitPercentage()
        {
            return Math.Round(this.profit / this.purchaseAmount * 100 , 3);
        }
    }
}
