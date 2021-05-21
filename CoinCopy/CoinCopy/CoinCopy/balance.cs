using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCopy
{
    class balance
    {
        // 자산 ( 보유 현금 + 유가 총액 ) 
        private double totalAsset;

        // 보유 현금
        private double cash;

        //손익 금액 ( purchase - totalPrice ) 
        private double profit;

        //매입 금액
        private double purchaseAmount;

        //유가 총액 ( 현시점 기준으로 보유한 주식의 현 시가의 총합 )
        private double currentTotalPrice;

        public balance()
        {
            // 1억
            cash = 100000000;
            totalAsset = cash;
            profit = 0;
            purchaseAmount = 0;
            currentTotalPrice = 0;
        }

        public double getTotalAsset()
        {
            return this.totalAsset;
        }
        public void setTotalAsset(double amount)
        {
            this.totalAsset = amount;   
        }


        public double getCash()
        {
            return this.cash;
        }
        public void setCash(double amount)
        {
            this.cash = amount;
        }


        public double getProfit()
        {
            return (this.purchaseAmount - this.currentTotalPrice);
        }
        public void setProfit(double amount)
        {
            this.profit = amount;
        }


        public double getPurchaseAmount()
        {
            return this.purchaseAmount;
        }
        public void setPurchaseAmount(double amount)
        {
            this.purchaseAmount = amount;
        }


        public double getCurrentTotalPrice()
        {
            return this.currentTotalPrice;
        }
        public void setCurrentTotalPrice(double amount)
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
            return Math.Round(this.profit / this.purchaseAmount * 100 , 3);
        }
    }
}
