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
        public int[] valueTotal = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }; // my own value
        public int[] valueCTotal = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }; // competitor's value
        public int[] priceC = { 10, 15, 20 }; // competitor's price

        int Horizon; //整體時程
        public int numberOfProduct; //總商品數
        public double arrivalProb = 0.7; //顧客到達機率
        public int max_customer = 20; //最大來客數
        public int mean_customer; //平均來客數      

        public int LowerBound_price = 1; //商品價錢下限
        public int UpperBound_price = 27; //商品價錢上限
        public int priceInterval = 1; //價錢間隔

        public int numberOfPrice;
        public int numberOfPriceC;
        public int[] price; //每個價錢的值

        public double[,] purchase_prob; // [pc.p] >> 當期每個對手價格和自身價格下的購買機率
        public double[] purchase_prob_nc;

        public double[,,] valueTableA; // 算出本期利潤 [pc, inventory, p]
        public double[,] valueTableA_nc;

        public double[,] bestValueA;  // 最佳利潤
        public double[] bestValueA_nc;
        public double[,] bestValueB;  // 每期結束時 B<-A 計算時參考B表, B表紀錄未來最佳期望利潤
        public double[] bestValueB_nc;
        public int[,] bestActionA;  // 最佳利潤的決策
        public int[] bestActionA_nc;
        public int[,] bestActionB;
        public int[] bestActionB_nc;

        public double[,,,] tranProb; // 本期會賣掉幾張的機率 [c, p, k, fc]->在價格p&對手價格c時, 下一期變成X(t)-k&對手價格為fc的機率
        public double[,] tranProb_nc;
        public double[] comeProb; // 來幾個人的機率
        public double[,,] reward; // 當期的預期利潤(庫存足跟不足的利潤) [i,j]->在價錢j時當期的利潤 i=0則庫存>=最大來客數
                                  // i=1 -> 庫存<=最大來客數-1 // [pc,p,l] >> 當期對手價格pc+我方價格p+存貨不足l個?
        public double[,] reward_nc;
        public double[,,,] result; // 最後結果 [pc, t, i, action & reward] >> 對手價格為pc, 剩餘時間為t, 剩餘庫存為i, 最佳定價及利潤
        public double[,,] result_nc;

        public double[,] single_result;
        #endregion

        public Form1()
        {
            InitializeComponent();
            #region INITIALIZATION
            numberOfProduct = 300;
            //Horizon = 300;
            Horizon = 250;
            result = new double[priceC.Length, Horizon, numberOfProduct + 1, 2]; // 在時間剩幾期 , 商品數量還有幾個時的 最佳定價&利潤
            result_nc = new double[Horizon, numberOfProduct + 1, 2];
            double[] r = new double[2]; //一個結果
            int numberOfValue = (int)((valueTotal[valueTotal.Length - 1] - valueTotal[0]) + 1);
            int numberOfValueC = (int)((valueCTotal[valueCTotal.Length - 1] - valueCTotal[0]) + 1);

            numberOfPrice = (int)((UpperBound_price - LowerBound_price) / priceInterval + 1);
            numberOfPriceC = priceC.Length;
            price = new int[numberOfPrice];
            purchase_prob = new double[numberOfPriceC, numberOfPrice];
            purchase_prob_nc = new double[numberOfPrice];

            valueTableA = new double[numberOfPriceC, numberOfProduct + 1, numberOfPrice];
            valueTableA_nc = new double[numberOfProduct + 1, numberOfPrice];
            tranProb = new double[numberOfPriceC, numberOfPrice, max_customer + 1, numberOfPriceC];
            tranProb_nc = new double[numberOfPrice, max_customer + 1];
            comeProb = new double[max_customer + 1];

            reward = new double[numberOfPriceC, numberOfPrice, max_customer]; // 沒有人來就不用計算利潤
            reward_nc = new double[numberOfPrice, max_customer];
            bestValueA = new double[numberOfPriceC, numberOfProduct + 1];
            bestValueA_nc = new double[numberOfProduct + 1];
            bestValueB = new double[numberOfPriceC, numberOfProduct + 1];
            bestValueB_nc = new double[numberOfProduct + 1];
            bestActionA = new int[numberOfPriceC, numberOfProduct + 1];
            bestActionA_nc = new int[numberOfProduct + 1];
            bestActionB = new int[numberOfPriceC, numberOfProduct + 1];
            bestActionB_nc = new int[numberOfProduct + 1];

            single_result = new double[numberOfProduct + 1, 2];
            double[,] temp_result = new double[numberOfProduct + 1, 2];

            for (int j = 0; j < numberOfPrice; j++) // record possible action (price)
            {
                price[j] = j * priceInterval + LowerBound_price;
            }
            for (int j = 0; j < max_customer + 1; j++) // calculate coming probability
            {
                comeProb[j] = CombinationProb(max_customer, j) * Math.Pow(arrivalProb, j) * Math.Pow((1 - arrivalProb), (max_customer - j));
            }
            int[] STD = { 15 };
            #endregion
            #region MAIN FUNCTION
            for (int i = 0; i < numberOfValue; i++)
            {
                for (int j = 0; j < numberOfValueC; j++)
                {
                    // determine system parameters
                    double value = valueTotal[i];
                    double valueC = valueCTotal[j];

                    // calculate buying probability
                    for (int c = 0; c < numberOfPriceC; c++)
                    {
                        double price_C = priceC[c];
                        for (int p = 0; p < numberOfPrice; p++)
                        {
                            purchase_prob[c, p] = 0;
                            double utility = value - price[p];
                            double utility_C = valueC - price_C;
                            double exp_u = Math.Exp(utility);
                            double exp_uC = Math.Exp(utility_C);
                            purchase_prob[c, p] = (exp_u) / (exp_u + exp_uC + 1);
                        }
                    }
                    // no priceC purchase probability
                    //for (int p = 0; p < numberOfPrice; p++)
                    //{
                    //    purchase_prob_nc[p] = 0;
                    //    for (int c = 0; c < numberOfPriceC; c++)
                    //    {
                    //        double utility = value - price[p];
                    //        double utility_C = valueC - priceC[c];
                    //        double exp_u = Math.Exp(utility);
                    //        double exp_uC = Math.Exp(utility_C);
                    //        purchase_prob_nc[p] += (exp_u) / (exp_u + exp_uC + 1);
                    //    }
                    //    purchase_prob_nc[p] /= numberOfPriceC;
                    //}
                    // calculate transition probability
                    for (int c = 0; c < numberOfPriceC; c++) // current price_C
                    {
                        for (int p = 0; p < numberOfPrice; p++) // current price
                        {
                            for (int l = 0; l < max_customer + 1; l++) // buying customer
                            {
                                double temp_prob = 0;
                                for (int k = l; k < max_customer + 1; k++) // coming customers
                                {
                                    //double temp = comeProb[l] * CombinationProb(k, l) * Math.Pow(purchase_prob[p], l) * Math.Pow((1 - purchase_prob[p]), (k - l));
                                    double come = comeProb[k];
                                    double comb = CombinationProb(k, l);
                                    double buy = Math.Pow(purchase_prob[c, p], l);
                                    double nobuy = Math.Pow((1 - purchase_prob[c, p]), (k - l));
                                    temp_prob += comeProb[k] * CombinationProb(k, l) * Math.Pow(purchase_prob[c, p], l) * Math.Pow((1 - purchase_prob[c, p]), (k - l));
                                    // tranProb[l,p] ->在價錢p下 賣掉l個的機率
                                }
                                temp_prob /= numberOfPriceC;
                                for (int fc = 0; fc < numberOfPriceC; fc++) // future price_C
                                {
                                    tranProb[c, p, l, fc] = temp_prob;
                                }
                            }
                        }
                    }
                    //// transition probability without priceC
                    //for (int p = 0; p < numberOfPrice; p++)
                    //{
                    //    for (int l = 0; l < max_customer + 1; l++)
                    //    {
                    //        double temp_prob = 0;
                    //        for (int k = l; k < max_customer + 1; k++)
                    //        {
                    //            double come = comeProb[k];
                    //            double comb = CombinationProb(k, l);
                    //            double buy = Math.Pow(purchase_prob_nc[p], l);
                    //            double nobuy = Math.Pow((1 - purchase_prob_nc[p]), (k - l));
                    //            temp_prob += comeProb[k] * CombinationProb(k, l) * Math.Pow(purchase_prob_nc[p], l) * Math.Pow((1 - purchase_prob_nc[p]), (k - l));
                    //        }
                    //        tranProb_nc[p, l] = temp_prob;
                    //    }
                    //}
                    // calculate reward
                    for (int c = 0; c < numberOfPriceC; c++)
                    {
                        for (int p = 0; p < numberOfPrice; p++)
                        {
                            for (int l = 0; l < max_customer; l++) // inventory enough or not (l=0: enough; l>0: not enough)
                            {
                                double reward_sum = 0;
                                double prob_total = 0;
                                reward[c, p, l] = 0;
                                for (int k = 0; k < max_customer + 1 - l; k++) // buying
                                {
                                    for (int fc = 0; fc < numberOfPriceC; fc++)
                                    {
                                        if (k == 0) // no one buying = no reward
                                        {
                                            prob_total += tranProb[c, p, k, fc];
                                        }
                                        else if (k != max_customer - l)
                                        {
                                            prob_total += tranProb[c, p, k, fc];
                                            reward_sum += tranProb[c, p, k, fc] * price[p] * (k);
                                        }
                                        else
                                        {
                                            if ((fc == (numberOfPriceC - 1)) & (k == (max_customer - l)))
                                            {
                                                double last_prob = 1 - prob_total;
                                                reward_sum += last_prob * price[p] * (k);
                                            }
                                            else
                                            {
                                                prob_total += tranProb[c, p, k, fc];
                                                reward_sum += tranProb[c, p, k, fc] * price[p] * (k);
                                            }
                                        }
                                    }
                                }
                                reward[c, p, l] = reward_sum;
                            }
                        }
                    }
                    //// without priceC reward
                    //for (int p = 0; p < numberOfPrice; p++)
                    //{
                    //    for (int l = 0; l < max_customer; l++) // inventory enough or not (l=0: enough; l>0: not enough)
                    //    {
                    //        double reward_sum = 0;
                    //        double prob_total = 0;
                    //        reward_nc[p, l] = 0;
                    //        for (int k = 0; k < max_customer + 1 - l; k++) // buying
                    //        {
                    //            if (k == 0) // no one buying = no reward
                    //            {
                    //                prob_total += tranProb_nc[p, k];
                    //            }
                    //            else if (k != max_customer - l)
                    //            {
                    //                prob_total += tranProb_nc[p, k];
                    //                reward_sum += tranProb_nc[p, k] * price[p] * (k);
                    //            }
                    //            else
                    //            {
                    //                double last_prob = 1 - prob_total;
                    //                reward_sum += last_prob * price[p] * (k);
                    //            }
                    //        }                     
                    //        reward_nc[p, l] = reward_sum;
                    //    }
                    //}
                    // store result
                    for (int c = 0; c < numberOfPriceC; c++)
                    {
                        for (int t = 1; t < Horizon + 1; t++)
                        {
                            temp_result = BestProfitNew(value, valueC, c, t);
                            for (int k = 0; k < numberOfProduct + 1; k++)
                            {
                                result[c, t - 1, k, 0] = temp_result[k, 0];
                                result[c, t - 1, k, 1] = temp_result[k, 1];
                            }
                        }
                    }
                    //// without priceC result
                    //for (int t = 1; t < Horizon + 1; t++)
                    //{
                    //    temp_result = BestProfitNew_nc(value, valueC, t);
                    //    for (int k = 0; k < numberOfProduct + 1; k++)
                    //    {
                    //        result_nc[t - 1, k, 0] = temp_result[k, 0];
                    //        result_nc[t - 1, k, 1] = temp_result[k, 1];
                    //    }
                    //}

                    //string folderName = ("price_C" + priceC[m].ToString());
                    string folderName = "240607_pc=5_15_25";
                    writeFileNew(i, j, value, valueC, folderName);
                }
            }
            #endregion
        }
        #region BEST PROFIT
        public double[,] BestProfitNew(double value, double valueC, int pc_id, int horizon)
        {
            DateTime start = DateTime.Now;

            for (int c = 0; c < numberOfPriceC; c++)
            {
                for (int i = 0; i < numberOfProduct + 1; i++)
                {
                    bestActionA[c, i] = 0;
                    bestActionB[c, i] = 0;
                    bestValueA[c, i] = 0;
                    bestValueB[c, i] = 0;
                }
            }

            for (int i = 0; i < horizon; i++)
            {
                for (int c = 0; c < numberOfPriceC; c++)
                {
                    for (int j = 0; j < numberOfProduct + 1; j++)
                    {
                        double bestValue = 0;
                        int bestAction = 0;
                        for (int p = 0; p < numberOfPrice; p++)
                        {
                            valueTableA[c, j, p] = 0;
                            if (j >= max_customer) // enough inventory
                            {
                                double prob = 0;
                                valueTableA[c, j, p] = reward[c, p, 0]; // nothing happens' reward
                                for (int l = 0; l < max_customer + 1; l++)
                                {
                                    for (int fc = 0; fc < numberOfPriceC; fc++)
                                    {
                                        if ((l == max_customer) & (fc == numberOfPriceC - 1))
                                        {
                                            double last_prob = 1 - prob;
                                            valueTableA[c, j, p] += last_prob * bestValueB[fc, j - l]; // original j, sell l, left (j-l)
                                        }
                                        else
                                        {
                                            prob += tranProb[c, p, l, fc];
                                            valueTableA[c, j, p] += tranProb[c, p, l, fc] * bestValueB[fc, j - l]; // original j, sell l, left (j-l)
                                        }
                                    }
                                }
                            }
                            else if (j == 0) // sold out
                            {
                                valueTableA[c, j, p] = 0;
                            }
                            else
                            {
                                double prob = 0;
                                valueTableA[c, j, p] = reward[c, p, max_customer - j];
                                for (int l = 0; l < j + 1; l++)
                                {
                                    for (int fc = 0; fc < numberOfPriceC; fc++)
                                    {
                                        if ((l == j) & (fc == numberOfPriceC - 1))
                                        {
                                            double last_prob = 1 - prob;
                                            valueTableA[c, j, p] += last_prob * bestValueB[fc, j - l]; // original j, sell l, left (j-l)
                                        }
                                        else
                                        {
                                            prob += tranProb[c, p, l, fc];
                                            valueTableA[c, j, p] += tranProb[c, p, l, fc] * bestValueB[fc, j - l]; // original j, sell l, left (j-l)
                                        }
                                    }
                                }
                            }
                            if (valueTableA[c, j, p] >= bestValue)
                            {
                                bestValue = valueTableA[c, j, p];
                                bestAction = price[p];
                            }
                        }
                        bestValueA[c, j] = bestValue;
                        bestActionA[c, j] = bestAction;
                    }
                }
                for (int c = 0; c < numberOfPriceC; c++)
                {
                    for (int j = 0; j < numberOfProduct + 1; j++)
                    {
                        bestActionB[c, j] = bestActionA[c, j];
                        bestValueB[c, j] = bestValueA[c, j];
                    }
                }
            }

            DateTime end = DateTime.Now;
            TimeSpan ts = end.Subtract(start);

            for (int i = 0; i < numberOfProduct + 1; i++)
            {
                single_result[i, 0] = bestActionB[pc_id, i];
                single_result[i, 1] = bestValueB[pc_id, i];
            }
            return single_result;
        }
        #endregion
        #region WRITE FILE
        public void writeFileNew(int name1, int name2, double value, double valueC, string folderName)
        {
            //int new_name1 = name1 + 2;
            //int new_name2 = name2 + 7;
            string Name1 = name1.ToString();
            string Name2 = name2.ToString();
            using (var file = new StreamWriter(@"C:\Users\Josey\Desktop\test\DP_test\" + folderName + "\\" + Name1 + Name2 + ".csv", false))//需要修改地址！
            {
                // with priceC
                for (int c = 0; c < numberOfPriceC; c++)
                {
                    for (int i = 0; i < Horizon; i++)
                    {
                        for (int j = 1; j < numberOfProduct + 1; j++)
                        {
                            // value, valueC, priceC, 剩餘時間, 剩餘庫存, 最佳定價, 最佳利益
                            file.WriteLine(value + "," + valueC + "," + priceC[c] + "," + (i + 1) + "," + j + "," + result[c, i, j, 0] + "," + result[c, i, j, 1]);
                        }
                    }
                }
                //// without priceC
                //for (int i = 0; i < Horizon; i++)
                //{
                //    for (int j = 1; j < numberOfProduct + 1; j++)
                //    {
                //        // value, valueC, 剩餘時間, 剩餘庫存, 最佳定價, 最佳利益
                //        file.WriteLine(value + "," + valueC + "," + (i + 1) + "," + j + "," + result_nc[i, j, 0] + "," + result_nc[i, j, 1]);
                //    }
                //}
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
