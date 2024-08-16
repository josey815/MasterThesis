using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DPT_Test
{
    public partial class Form1 : Form
    {
        #region PARAMETERS
        // environment parameters (unchanged during whole horizon)
        public int[] meanTotal = { 70, 80, 90, 100, 110, 120, 130, 140 };
        public int[] valueTotal = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }; // my own value
        //public int[] valueCTotal = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }; // competitor's value
        public int[] valueCTotal = { 15 }; // competitor's value
        public int[] priceC = { 15 }; // competitor's price

        int Horizon; //整體時程
        public int numberOfProduct; //總商品數
        public double arrivalProb = 0.7; //顧客到達機率
        public int max_customer = 20; //最大來客數
        public int mean_customer; //平均來客數      

        //public double[,] salesSitu; // 銷售情形 0->價錢  1->賣幾個  2->利潤
        //public double[,] salesSitu_best;
        //public int LowerBound_price = 10; //商品價錢下限
        //public int UpperBound_price = 200; //商品價錢上限
        public int LowerBound_price = 12; //商品價錢下限      
        public int UpperBound_price = 27; //商品價錢上限
        public int priceInterval = 1; //價錢間隔

        public int numberOfPrice;
        public int[] price; //每個價錢的值
        //public int[] sold;

        //public double[] purchase_situ; // 每期每個售價的銷售情形
        public double[] purchase_prob; // 每個價錢下購買的機率

        public double[,] valueTableA; // 算出本期利潤

        public double[] bestValueA;  // 最佳利潤
        public double[] bestValueB;  // 每期結束時 B<-A 計算時參考B表, B表紀錄未來最佳期望利潤
        public int[] bestActionA;  // 最佳利潤的決策
        public int[] bestActionB;

        public double[,] tranProb; // 本期會賣掉幾張的機率 [i,j]->在價錢j時下一期變成X(t)-i的機率
        public double[] comeProb; // 來幾個人的機率
        public double[,] reward; // 當期的預期利潤(庫存足跟不足的利潤) [i,j]->在價錢j時當期的利潤 i=0則庫存>=最大來客數
                                 // i=1 -> 庫存<=最大來客數-1
        public double[,,] result; // 最後結果

        public double[,] single_result;
        #endregion

        public Form1()
        {
            InitializeComponent();
            #region INITIALIZATION
            numberOfProduct = 300;
            //Horizon = 300;
            Horizon = 250;
            result = new double[Horizon, numberOfProduct+1, 2]; // 在時間剩幾期 , 商品數量還有幾個時的 最佳定價&利潤
            double[] r = new double[2]; //一個結果
            int numberOfValue = valueTotal[valueTotal.Length - 1] - valueTotal[0] + 1;
            int numberOfValueC = valueCTotal[valueCTotal.Length - 1] - valueCTotal[0] + 1;
            int numberOfMean = meanTotal[meanTotal.Length - 1] - meanTotal[0] + 1;

            numberOfPrice = (UpperBound_price - LowerBound_price) / priceInterval + 1;
            price = new int[numberOfPrice];
            purchase_prob = new double[numberOfPrice];

            valueTableA = new double[numberOfProduct + 1, numberOfPrice];
            tranProb = new double[max_customer + 1, numberOfPrice];
            comeProb = new double[max_customer + 1];

            reward = new double[max_customer, numberOfPrice]; // 沒有人來就不用計算利潤
            bestValueA = new double[numberOfProduct + 1];
            bestValueB = new double[numberOfProduct + 1];
            bestActionA = new int[numberOfProduct + 1];
            bestActionB = new int[numberOfProduct + 1];

            single_result = new double[numberOfProduct+1, 2];
            double[,] temp_result = new double[numberOfProduct+1, 2];

            for (int j = 0; j < numberOfPrice; j++)
            {
                price[j] = j + LowerBound_price;
            }
            for (int j = 0; j < max_customer + 1; j++)
            {
                comeProb[j] = CombinationProb(max_customer, j) * Math.Pow(arrivalProb, j) * Math.Pow((1 - arrivalProb), max_customer - j);
            }
            int[] STD = { 15 };
            #endregion
            #region MAIN FUNCTION
            for (int m = 0; m < priceC.Length; m++)
            {
                for (int i = 0; i < numberOfValue; i++)
                {
                    for (int j = 0; j < numberOfValueC; j++)
                    {
                        // determine system parameters
                        int value = valueTotal[i];
                        int valueC = valueCTotal[j];
                        int price_C = priceC[m];

                        // calculate buying probability
                        for (int k = 0; k < numberOfPrice; k++)
                        {
                            purchase_prob[k] = 0;

                            int utility = value - price[k];
                            int utility_C = valueC - price_C;
                            double exp_u = Math.Exp(utility);
                            double exp_uC = Math.Exp(utility_C);
                            purchase_prob[k] = (exp_u) / (exp_u + exp_uC + 1); //有參考價格
                            //purchase_prob[k] = (exp_u) / (exp_u + 1); //無參考價格
                        }

                        // calculate transition probability
                        for (int l = 0; l < max_customer + 1; l++) // buying customer
                        {
                            for (int p = 0; p < numberOfPrice; p++)
                            {
                                tranProb[l, p] = 0;
                                for (int k = l; k < max_customer + 1; k++) // coming customers
                                {
                                    double temp = comeProb[l] * CombinationProb(k, l) * Math.Pow(purchase_prob[p], l) * Math.Pow((1 - purchase_prob[p]), (k - l));
                                    double come = comeProb[k];
                                    double comb = CombinationProb(k, l);
                                    double buy = Math.Pow(purchase_prob[p], l);
                                    double nobuy = Math.Pow((1 - purchase_prob[p]), (k - l));
                                    tranProb[l, p] += comeProb[k] * CombinationProb(k, l) * Math.Pow(purchase_prob[p], l) * Math.Pow((1 - purchase_prob[p]), (k - l));
                                    // tranProb[l,p] ->在價錢p下 賣掉l個的機率
                                }
                            }
                        }

                        // calculate reward
                        for (int p = 0; p < numberOfPrice; p++)
                        {
                            for (int l = 0; l < max_customer; l++) // inventory enough or not (l=0: enough; l>0: not enough)
                            {
                                double reward_sum = 0;
                                double prob_total = 0;
                                reward[l, p] = 0;
                                for (int k = 0; k < max_customer + 1 - l; k++) // buying
                                {
                                    //double prob = tranProb[k, p];
                                    if (k == 0) // no one buying = no reward
                                        prob_total += tranProb[k, p];
                                    else if (k != max_customer - l)
                                    {
                                        prob_total += tranProb[k, p];
                                        reward_sum += tranProb[k, p] * price[p] * (k);
                                    }
                                    else
                                    {
                                        double last_prob = 1 - prob_total;
                                        reward_sum += last_prob * price[p] * (k);
                                    }
                                }
                                reward[l, p] = reward_sum;
                            }
                        }

                        // store result
                        for (int t = 1; t < Horizon + 1; t++)
                        {
                            temp_result = BestProfitNew(value, valueC, price_C, t);
                            for (int k = 0; k < numberOfProduct + 1; k++)
                            {
                                result[t - 1, k, 0] = temp_result[k, 0];
                                result[t - 1, k, 1] = temp_result[k, 1];
                            }
                        }
                        string folderName = ("price_C" + priceC[m].ToString());
                        folderName = "240423_noRefePrice_test";
                        writeFileNew(i, j, value, valueC, price_C, folderName);
                    }
                }
            }
            #endregion
        }
        #region BEST PROFIT
        public double[,] BestProfitNew(double value, double valueC, double priceC, int horizon)
        {
            DateTime start = DateTime.Now;

            for (int i = 0; i < numberOfProduct + 1; i++)
            {
                bestActionA[i] = 0;
                bestActionB[i] = 0;
                bestValueA[i] = 0;
                bestValueB[i] = 0;
            }

            for (int i = 0; i < horizon; i++)
            {
                for (int j = 0; j < numberOfProduct + 1; j++)
                {
                    double bestValue = 0;
                    int bestAction = 0;

                    for (int p = 0; p < numberOfPrice; p++)
                    {
                        valueTableA[j, p] = 0;
                        if (j >= max_customer) // enough inventory
                        {
                            valueTableA[j, p] = reward[0, p]; // nothing happens' reward
                            for (int l = 0; l < max_customer + 1; l++)
                                valueTableA[j, p] += tranProb[l, p] * bestValueB[j - l]; // original j, sell l, left (j-l)
                        }
                        else if (j == 0) // sold out
                        {
                            valueTableA[j, p] = 0;
                        }
                        else
                        {
                            valueTableA[j, p] = reward[max_customer - j, p];
                            double prob = 0;
                            for (int l = 0; l < j+1; l++)
                            {
                                prob += tranProb[l, p];
                                valueTableA[j, p] += tranProb[l, p] * bestValueB[j - l];
                            }
                        }
                        if (valueTableA[j, p] > bestValue)
                        {
                            bestValue = valueTableA[j, p];
                            bestAction = price[p];
                        }
                    }
                    bestValueA[j] = bestValue;
                    bestActionA[j] = bestAction;
                }

                for (int j = 0; j < numberOfProduct + 1; j++)
                {
                    bestActionB[j] = bestActionA[j];
                    bestValueB[j] = bestValueA[j];
                }
            }


            DateTime end = DateTime.Now;
            TimeSpan ts = end.Subtract(start);

            for (int i = 0; i < numberOfProduct + 1; i++)
            {
                single_result[i, 0] = bestActionB[i];
                single_result[i, 1] = bestValueB[i];
            }

            return single_result;
        }
        #endregion
        
        #region WRITE FILE
        public void writeFileNew(int name1, int name2, int value, int valueC, int priceC, string folderName)
        {
            string Name1 = name1.ToString();
            string Name2 = name2.ToString();
            using (var file = new StreamWriter(@"C:\Users\Josey\Desktop\test\DP_test\" + folderName + "\\" + Name1 + Name2 + ".csv", false))//需要修改地址！
            {
                for (int i = 0; i < Horizon; i++)
                {
                    double startPrice = 0;
                    for (int j = 1; j < numberOfProduct + 1; j++)
                    {
                        // value, valueC, priceC, 剩餘時間, 剩餘庫存, 最佳定價, 最佳利益
                        file.WriteLine(value + "," + valueC + "," + priceC + "," + (i + 1) + "," + j + "," + result[i, j, 0] + "," + result[i, j, 1]); //有參考價格
                        //file.WriteLine(value + "," + (i + 1) + "," + j + "," + result[i, j, 0] + "," + result[i, j, 1]); //沒參考價格
                        startPrice = result[i, j, 0];
                    }
                }
                Console.WriteLine();
            }
        }
        #endregion

        public double CombinationProb(int n, int k) // Cn取k
        {
            double n_factorial = 1;
            double k_factorial = 1;
            double n_minus_k_factorial = 1;
            for (int i = 1; i < (n + 1); i++)
                n_factorial *= i;
            for (int i = 1; i < (k + 1); i++)
                k_factorial *= i;
            for (int i = 1; i < (n - k + 1); i++)
                n_minus_k_factorial *= i;

            return (n_factorial / (k_factorial * n_minus_k_factorial));
        }
    }
}
