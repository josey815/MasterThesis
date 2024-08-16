using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.Integration;
using System.IO;


namespace Simulation
{
    public partial class SimulationCustomer : Form
    {
        public int Horizon; //整體時程
        public int observeTime; //觀察的時間
        
        public int numberOfProduct; //總商品數

        public int mean_of_mean; //效用值平均分佈的平均值
        public int std_of_mean; //效用值平均分佈的標準差
        public int mean_of_std; //效用值標準差分佈的平均值
        public int std_of_std; //效用值標準差分佈的標準差

        public double arrivalRate; //顧客到達率

        public int LowerBound_price; //商品價錢上限
        public int UpperBound_price; //商品價錢下限

        public int priceInterval; //價錢間隔

        public int simulationTime; //總共模擬幾次 1個Horizen結束算一次

        public double[,] purchase_prob;

        public int numberOfPrice;
        public int[] price_range; //每個價錢的值
        public int[,] purchaseSituiation; // 記錄每一個時間點有幾個人來幾個人買
        //public int[] purchasePrice; //每一張票賣掉的價錢



        public int changeDistributionTime; //幾期變一次分佈
        public int changeTime; //總共變幾次
        public int terminalPeriod;
        public double[,] realDistribution; //實際的mean, std 
        public int[] meanTotal = { 20, 25, 30, 35, 40, 45, 50, 55 };
        public int[] stdTotal = { 8, 10, 15, 17, 20, 25, 30, 40 };

        public Random rnd = new Random();
 
        //public int testMean;
        //public double testVar;
        //public int meanStart;
        //public int stdStart;
        

        public SimulationCustomer()
        {
            InitializeComponent();
        }

        public double soldNumProb(int number, double buyProb) // 賣掉幾張的機率
        {
            double prob = 0;
            for(int i = number; i<observeTime+1; i++) //總共能來幾個
            {
                double cal = CombinationProb(observeTime, i) * Math.Pow(arrivalRate, i) * Math.Pow(1 - arrivalRate, observeTime - i) * CombinationProb(i, number) * Math.Pow(buyProb, number) * Math.Pow(1 - buyProb, i - number);
                prob += cal;
            }
            return prob;
        }

        public double CombinationProb(int n, int k) // Cn取k
        {
            double n_factorial=1;
            double k_factorial = 1;
            double n_minus_k_factorial = 1;
            for (int i = 1; i < (n + 1); i++)
                n_factorial *= i;
            for (int i = 1; i < (k + 1); i++)
                k_factorial *= i;
            for (int i = 1; i < (n-k + 1); i++)
                n_minus_k_factorial *= i;

            return (n_factorial / (k_factorial* n_minus_k_factorial));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {

            Horizon = Convert.ToInt32(tbx_period.Text);
            observeTime = Convert.ToInt32(tbx_observeTime.Text);
            numberOfProduct = Convert.ToInt32(tbx_numberOfProduct.Text);
            

            arrivalRate = Convert.ToDouble(tbx_arrivalRate.Text);
            UpperBound_price = Convert.ToInt32(tbx_UB_P.Text);
            LowerBound_price = Convert.ToInt32(tbx_LB_P.Text);
            priceInterval = Convert.ToInt32(tbx_priceInterval.Text);


            mean_of_mean = Convert.ToInt32(tbx_meanOfMean.Text);
            std_of_mean = Convert.ToInt32(tbx_stdOfMean.Text);
            mean_of_std = Convert.ToInt32(tbx_meanOfStd.Text);
            std_of_std = Convert.ToInt32(tbx_stdOfStd.Text);

            changeDistributionTime = Convert.ToInt32(tbx_changeDistributionTime.Text);

            //mean_of_std = mean_of_mean * mean_of_std / 100;
            mean_of_std = 5;

            simulationTime = Convert.ToInt32(tbx_simuTime.Text);
            
            numberOfPrice = 1 + (UpperBound_price - LowerBound_price) / priceInterval;

            //purchasePrice = new int[NumberOfProduct];
            
            price_range = new int[numberOfPrice];
            purchaseSituiation = new int[Horizon, 4]; // 在每個record time, 0->price, 1->多少顧客來, 2->賣掉多少, 3->剩幾個
            changeTime = Horizon / changeDistributionTime;

            realDistribution = new double[changeTime, 2]; //  在每次變動分佈時紀錄, 0->mean, 1->std

            for (int i = 0; i < numberOfPrice; i++)
            {
                price_range[i] = LowerBound_price + i * priceInterval;
            }

            double mean;
            string dataClass;
            for (int i=0; i<8; i++)
            {
                if(i ==0)
                {
                    //test
                    dataClass = "test";
                    mean = GaussianVariable(mean_of_mean, std_of_mean);
                    while (mean <= 0)
                        mean = GaussianVariable(mean_of_mean, std_of_mean);
                    simulate(mean, mean * 15 / 100, mean_of_std, 0);
                    writeFile(0, mean, mean * 15 / 100, mean_of_std, 0, dataClass);
                    //validation
                    dataClass = "validation";
                    mean = GaussianVariable(mean_of_mean, std_of_mean);
                    while (mean <= 0)
                        mean = GaussianVariable(mean_of_mean, std_of_mean);
                    simulate(mean, mean * 15 / 100, mean_of_std, 0);
                    writeFile(0, mean, mean * 15 / 100, mean_of_std, 0, dataClass);
                }
                for(int j=0; j<8; j++)
                {
                    dataClass = "train";
                    mean = GaussianVariable(mean_of_mean, std_of_mean);
                    while (mean <= 0)
                        mean = GaussianVariable(mean_of_mean, std_of_mean);
                    simulate(meanTotal[i], meanTotal[i]*stdTotal[j]/100, meanTotal[i]*0.1, 0);
                    writeFile(i * 8 + j, meanTotal[i], mean * stdTotal[j] / 100, meanTotal[i] * 0.1, 0, dataClass);
                    //simulate(mean, mean * stdTotal[j] / 100, mean_of_std, 0);
                    //writeFile( i*8+j ,mean, mean * stdTotal[j] / 100, mean_of_std, 0, dataClass);
                }
            }
            
        }
        
       
        public void simulate(double mean_mean, double std_mean, double mean_std, double std_std)
        {
            terminalPeriod = Horizon;

            int product_in_stock = numberOfProduct;//剩幾張票
            bool flag = false; // terminal flag
            for (int i = 0; i < changeTime; i++)
            {
                realDistribution[i, 0] = 0;
                realDistribution[i, 1] = 0;
            }

            for (int i =0; i< changeTime; i++)
            {
                double mean = GaussianVariable(mean_mean, std_mean);
                double std = GaussianVariable(mean_std, std_std);

                realDistribution[i, 0] = mean;
                realDistribution[i, 1] = std;

                for (int j=0; j<changeDistributionTime; j++)
                {
                    int price = rnd.Next(LowerBound_price, (UpperBound_price + 1));
                    int arrivalCount = 0;
                    int purchaseCount = 0;

                    for (int k =0; k<observeTime;k++)
                    {
                        
                        double arrive = rnd.NextDouble();
                        int cPrice= Convert.ToInt32( Math.Floor( GaussianVariable(mean, std)));
                        if (arrive >= arrivalRate)
                        {
                            arrivalCount++;
                            cPrice =Convert.ToInt32(Math.Floor( GaussianVariable(mean, std)));
                            while(cPrice<0)
                                cPrice = Convert.ToInt32(Math.Floor(GaussianVariable(mean, std)));
                            if (price <= cPrice)
                            {
                                purchaseCount++;
                                product_in_stock--;
                            }
                            if (product_in_stock == 0)
                            {
                                flag = true;
                                terminalPeriod = i * changeDistributionTime + j;
                            }
                        }
                        if (flag)
                            break;
                    }
                    purchaseSituiation[i * changeDistributionTime + j, 0] = price;
                    purchaseSituiation[i * changeDistributionTime + j, 1] = arrivalCount;
                    purchaseSituiation[i * changeDistributionTime + j, 2] = purchaseCount;
                    purchaseSituiation[i * changeDistributionTime + j, 3] = product_in_stock;
                    if(flag)
                        break;
                }
                if (flag)
                    break;
            }
            
        }

        public void writeFile(int num, double mean_mean, double std_mean, double mean_std, double std_std, string dataClass)
        {
            int inputTime = 1; // RNN look back time
            string train_or_test;

            using (var file = new StreamWriter(@"C:\Users\WuGroup\Desktop\RNN\horizon" + Horizon + "\\simulation" + "0314_"
              + num + "_"+ dataClass + ".csv", false))//true繼續寫
            {
                // t + state(t) + action(t-1) + customerCount(t-1) + saleAmount(t-1) + mean的mean + mean的std ( + std的mean + std的std )<-以後做

                bool terminal=false;

                file.WriteLine(Horizon + "," + numberOfProduct + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + mean_mean.ToString("#0.00") + "," + std_mean.ToString("#0.00"));
                
                for (int i =0; i< changeTime; i++)
                {
                    for(int j =0; j<changeDistributionTime;j++)
                    {
                        if (i * changeDistributionTime + j == terminalPeriod-1)
                        {
                            terminal = true;
                            file.WriteLine((Horizon - i * changeDistributionTime - j - 1) + "," + purchaseSituiation[i * changeDistributionTime + j, 3] + "," + purchaseSituiation[i * changeDistributionTime + j, 0] +
                                "," + purchaseSituiation[i * changeDistributionTime + j, 1] + "," + purchaseSituiation[i * changeDistributionTime + j, 2] + "," + 1 + "," + mean_mean.ToString("#0.00") + "," + std_mean.ToString("#0.00"));
                            break;
                        }
                        else
                            file.WriteLine((Horizon-i* changeDistributionTime - j-1) + "," + purchaseSituiation[i * changeDistributionTime + j, 3] + "," + purchaseSituiation[i * changeDistributionTime + j, 0] +
                                "," + purchaseSituiation[i * changeDistributionTime + j, 1] + "," + purchaseSituiation[i * changeDistributionTime + j, 2] + "," + 0 + "," + mean_mean.ToString("#0.00") + "," + std_mean.ToString("#0.00"));
                        
                    }
                    if (terminal)
                        break;
                }
                

                /*
                for (int i =0;i<Horizon-inputTime+1;i++)
                {
                    for(int j =0; j<inputTime;j++)
                    {
                        if(i+j == terminalPeriod - 1)
                        {
                            terminal = true;
                            file.WriteLine((Horizon - i - j) + "," + purchaseSituiation[i + j, 3] + "," + purchaseSituiation[i + j, 0] +
                                "," + purchaseSituiation[i + j, 1] + "," + purchaseSituiation[i + j, 2] + "," + 1 + "," + mean_mean.ToString("#0.00") + "," + std_mean.ToString("#0.00"));
                            break;
                        }
                        else if (i == 0 && j == 0)
                            file.WriteLine(Horizon + "," + numberOfProduct + "," + 0 + "," + 0 + "," + 0 + "," + 0 );
                        else if(j == inputTime-1)
                            file.WriteLine(Horizon + "," + numberOfProduct + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + mean_mean.ToString("#0.00") + "," + std_mean.ToString("#0.00"));
                        else
                            file.WriteLine((Horizon - i - j) + "," + purchaseSituiation[i + j, 3] + "," + purchaseSituiation[i + j, 0] +
                                "," + purchaseSituiation[i + j, 1] + "," + purchaseSituiation[i + j, 2] + "," + 0 );
                       
                    }
                    if (terminal)
                        break;
                }
                */
                
                Console.WriteLine();
                //MessageBox.Show(purchaseSituiation.Length.ToString());
                
            }
            /*
            using (var file = new StreamWriter(@"C:\Users\WuGroup\Desktop\RNN\record" + recordTime + "\\simulation" + "0112_"
             + "recordTime" + recordTime + "_test.csv", false))//true繼續寫
            {
                

            }
            */

            

        }

        
        public double GaussianVariable(double u, double d)
        {
            double u1, u2, z, x;
            if (d == 0)
                return u;
            u1 = rnd.NextDouble();
            u2 = rnd.NextDouble();

            z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);

            x = u + d * z;
            return x;

        }
    }
}
