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
        public int Horizen; //整體時程
        public int simulationTime; //每個價錢模擬的次數
        public int NumberOfSeat; //總商品數

        public int LowerBound_mean;
        public int UpperBound_mean;
        public double LowerBound_std;
        public double UpperBound_std;

        public double arrivalRate;

        public int LowerBound_price;
        public int UpperBound_price;

        public int priceInterval;

        public double revenue;
        public double[,] purchase_prob;
        public int numberOfPrice;
        public int[] price_range;
        
        public int[,,] purchaseSituiation;
        public int[,] testPS;
        public int observeTime; //觀察的時間
        public int recordTime;  //紀錄次數
    
        
        public Random rnd = new Random();
        
        

        public int testMean;
        public double testVar;

        public int modelNum ;

        public int meanStart;
        public int stdStart;
        public int meanAdd = 5;
        public double[] std = {0.08, 0.1, 0.15, 0.17, 0.2, 0.25, 0.3, 0.4 };

        public SimulationCustomer()
        {
            InitializeComponent();

            

            modelNum = stdNum * meanNum;
            
            
        }
        
        private void btn_start_Click(object sender, EventArgs e)
        {

            //utilityMean[0] = Convert.ToDouble(tbx_mean1.Text);

            simulationTime = Convert.ToInt32(tbx_period);
            observeTime = Convert.ToInt32(tbx_changePriceTime);
            recordTime = simulationTime / observeTime;



            arrivalRate = Convert.ToDouble(tbx_arrivalRate.Text);
            UpperBound_price = Convert.ToInt32(tbx_UB.Text);
            LowerBound_price = Convert.ToInt32(tbx_LB.Text);
            priceInterval = Convert.ToInt32(tbx_priceInterval.Text);

            numberOfPrice = 1 + (UpperBound_price - LowerBound_price) / priceInterval;

            
            price_range = new int[numberOfPrice];
            //purchaseSituiation = new int[modelNum, numberOfPrice, recordTime];
            //purchaseSituiation[model, recordTime, 0] = price
            //purchaseSituiation[model, recordTime, 0] = salesamount
            purchaseSituiation = new int[modelNum, recordTime, 2];
            testPS = new int[recordTime, 2];
            for (int i = 0; i < numberOfPrice; i++)
            {
                price_range[i] = LowerBound_price + i * priceInterval;
            }

            int meanValue = 20;
            meanStart = meanValue;
            for (int i =0;i<meanNum;i++)
            {
                //for (int j = 0; j < NumberOfFlight; j++)
                  //utility[i, j] = meanValue;
                utilityMean[i,0] = meanValue;

                
                for (int j = 0; j < stdNum; j++)
                {
                    //for (int k = 0; k < NumberOfFlight; k++)
                        utilityStd[j, 0] = std[j] * utilityMean[i,0];
                }
                
                simulate(i);

                meanValue += 5;
            }
            
                writeFile();
            
        }

        

        public void simulate(int meanCount)
        {
            int purchaseCount;
            
            for (int l = 0; l < stdNum; l++)
            {
                purchaseCount = 0;
                for (int j = 0; j < recordTime; j++)
                {
                    int price = rnd.Next(LowerBound_price, (UpperBound_price+1));
                    
                    for (int k = 0; k < observeTime; k++)
                    {
                        double arrive = rnd.NextDouble();
                        double purchasePrice;
                        if (arrive >= arrivalRate)
                        {
                            purchasePrice = customerPrice(utilityMean[meanCount, 0], utilityStd[l, 0]);
                            if (price <= purchasePrice)
                                purchaseCount++;
                        }
                    }
                    purchaseSituiation[stdNum * meanCount + l, j, 0] = price;
                    purchaseSituiation[stdNum * meanCount + l,j, 1] = purchaseCount;
                }
            }
            purchaseCount = 0;
            for(int i =0; i<recordTime;i++)
            {
                int price = rnd.Next(LowerBound_price, (UpperBound_price + 1));
                for (int j = 0; j < observeTime; j++)
                {
                    double arrive = rnd.NextDouble();
                    double purchasePrice;
                    if (arrive >= arrivalRate)
                    {
                        purchasePrice = customerPrice(testMean, testVar);
                        if (price <= purchasePrice)
                            purchaseCount++;
                    }
                }
                testPS[ i, 0] = price;
                testPS[ i, 1] = purchaseCount;
            }


        }

        public void writeFile()
        {

            using (var file = new StreamWriter(@"C:\Users\WuGroup\Desktop\RNN\record" + recordTime + "\\simulation" + "0112_"
             + "recordTime" + recordTime + "_train.csv", false))//true繼續寫
            {
                // simulationTime + price_range + (t)purchaseSit + (t-1)purchaseSit + ... + (t-14)purchaseSituiation + mean + std
                

                for (int a = 0; a < meanNum; a++)
                {
                    for (int k = 0; k < stdNum; k++)
                    {
                        for (int j = 0; j < recordTime; j++)
                        {
                            file.WriteLine((j + 1) + ","
                            + purchaseSituiation[k + stdNum * a, j, 0] + "," + purchaseSituiation[k + stdNum * a, j, 1] +
                            "," + utilityMean[a, 0] + "," + utilityMean[a, 0]*std[k]);
                        }
                    }
                }
                Console.WriteLine();
                //MessageBox.Show(purchaseSituiation.Length.ToString());
                
            }

            using (var file = new StreamWriter(@"C:\Users\WuGroup\Desktop\RNN\record" + recordTime + "\\simulation" + "0112_"
             + "recordTime" + recordTime + "_test.csv", false))//true繼續寫
            {
                testMean = 37;
                testVar = testMean * 0.2;
                for (int i = 0; i < recordTime; i++)
                {
                    file.WriteLine((i + 1) + ","
                    + testPS[i, 0] + "," + testPS[i, 1] +
                    "," + testMean + "," + testVar);
                }
                Console.WriteLine();
                //MessageBox.Show(purchaseSituiation.Length.ToString());

            }


            

        }

        
        public double customerPrice(double u, double d)
        {
            double u1, u2, z, x;
            //Random ram = new Random();
            if (d <= 0)
            {

                return u;
            }
            u1 = rnd.NextDouble();
            u2 = rnd.NextDouble();

            z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);

            x = u + d * z;
            return x;

        }
    }
}
