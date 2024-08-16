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
        public double arrivalRate; //顧客到達率

        public int UpperBound_delta; //顧客價值的上限
        public int LowerBound_delta; //顧客價值的下限
        public int UpperBound_delta_c; //參考價值的上限
        public int LowerBound_delta_c; //參考價值的下限

        public int UpperBound_price; //我方商品價錢上限
        public int LowerBound_price; //我方商品價錢下限
        public int UpperBound_price_c; //市場參考價格上限
        public int LowerBound_price_c; //市場參考價格下限
        public int priceInterval; //價錢間隔

        public int simulationTime; //總共模擬幾次 1個Horizen結束算一次

        public double[,] purchase_prob;
        public double purchase_threshold = 0.06; //確切值待討論

        public int numberOfDelta;
        public int[] value_range;
        public int numberOfDeltaC;
        public int[] valueC_range;

        public int numberOfPrice;
        public int[] price_range; //每個價錢的值
        public int numberOfPriceC;
        public int[] priceC_range;
        public double[,] purchaseSituation; // 記錄每一個時間點有幾個人來幾個人買
        public int price_c; //對手價格先假設為定值(level -1)

        public Random rnd = new Random();
 
        public SimulationCustomer()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            //時間軸相關
            Horizon = Convert.ToInt32(tbx_period.Text); //總時長
            observeTime = Convert.ToInt32(tbx_observeTime.Text); //每期觀察時間=最大來客數
            numberOfProduct = Horizon * observeTime; //無限機票
            arrivalRate = Convert.ToDouble(tbx_arrivalRate.Text);

            //價值相關
            UpperBound_delta = Convert.ToInt32(tbx_aUB.Text);
            LowerBound_delta = Convert.ToInt32(tbx_aLB.Text);
            UpperBound_delta_c = Convert.ToInt32(tbx_bUB.Text);
            LowerBound_delta_c = Convert.ToInt32(tbx_bLB.Text);
            //delta_b = Convert.ToInt32(tbx_bLB.Text);

            numberOfDelta = (int)(1 + (UpperBound_delta - LowerBound_delta) / 1); //價值個數
            value_range = new int[numberOfDelta];
            numberOfDeltaC = (int)(1 + (UpperBound_delta_c - LowerBound_delta_c) / 1); //價值個數
            //numberOfDeltaB = 2;
            valueC_range = new int[numberOfDeltaC];
            int[,] delta_delta_C = new int[numberOfDelta * numberOfDeltaC * 20, 2];

            //價格相關
            UpperBound_price = Convert.ToInt32(tbx_UB_Pa.Text);
            LowerBound_price = Convert.ToInt32(tbx_LB_Pa.Text);
            UpperBound_price_c = Convert.ToInt32(tbx_UB_Pb.Text);
            LowerBound_price_c = Convert.ToInt32(tbx_LB_Pb.Text);
            priceInterval = Convert.ToInt32(tbx_priceInterval.Text);

            numberOfPrice = (int)(1 + (UpperBound_price - LowerBound_price) / priceInterval); //價格個數
            price_range = new int[numberOfPrice];
            numberOfPriceC = (int)(1 + (UpperBound_price_c - LowerBound_price_c) / priceInterval);
            priceC_range = new int[numberOfPriceC];

            purchaseSituation = new double[Horizon, 4]; // 在每個record time, 0->price, 1->price_c, 2->多少顧客來, 3->售出數量
           
            for (int i = 0; i < numberOfPrice; i++)
            {
                price_range[i] = LowerBound_price + i * priceInterval;
            }

            for (int i = 0; i < numberOfDelta; i++)
            {
                value_range[i] = LowerBound_delta + i;
            }

            for (int i = 0; i < numberOfPriceC; i++)
            {
                priceC_range[i] = LowerBound_price_c + i * priceInterval;
            }

            for (int i = 0; i < numberOfDeltaC; i++)
            {
                valueC_range[i] = LowerBound_delta_c + i;
            }

            //參考價格版本
            for (int i = 0; i < 20; i++) // for each delta and delta_c, repeat several times
            {
                for (int j = 0; j < valueC_range.Length; j++) // delta_c的個數
                {
                    for (int k = 0; k < value_range.Length; k++) // delta的個數
                    {
                        int delta_C = valueC_range[j];
                        int delta = value_range[k];
                        simulate_referencePrice(delta, delta_C);
                        int name = i * valueC_range.Length * value_range.Length + j * value_range.Length + k;
                        writeFile_referencePrice(name, delta, delta_C);

                        delta_delta_C[name, 0] = delta;
                        delta_delta_C[name, 1] = delta_C;

                    }
                }
            }

            //參考價格版本
            using (var file = new StreamWriter(@"C:\Users\Josey\Desktop\test\horizon" + Horizon + "_240808_10-20_10-20_2420data\\delta_delta_c.csv", false)) //記得改路徑
            {
                for (int i = 0; i < delta_delta_C.Length/2; i++)
                {
                    file.WriteLine(delta_delta_C[i,0] + "," + delta_delta_C [i,1]);
                }
                Console.WriteLine();
            }
        }
        //參考價格版本
        public void simulate_referencePrice(double delta, double delta_c)
        {
            //price_b = rnd.Next(LowerBound_price_b, (UpperBound_price_b + 1));

            for (int i = 0; i < Horizon; i++)
            {
                int arrivalCount = 0;
                int purchaseCount = 0;
                int timesCount = 0;
                double price = 0;
                double price_c = 0;
                double price_c_id = 0;
                double predict_purchaseProb = 0;
                do
                {
                    timesCount = rnd.Next(0,numberOfPrice);
                    price = LowerBound_price + timesCount * priceInterval;
                    //price_c = rnd.Next(LowerBound_price_c, (UpperBound_price_c + 1)); // price_c between [10-20] (11種可能)
                    price_c_id = rnd.Next(0, 3); // price_c between {10,15,20} (3種可能)
                    price_c = price_c_id * 5 + 10;
                    predict_purchaseProb = calculate_purchaseProb(delta, delta_c, price, price_c);
                } while (predict_purchaseProb > (purchase_threshold*2) | predict_purchaseProb < (purchase_threshold*0.01)); //避免價格過低或過高,產生無用的資料

                for (int k = 0; k < observeTime; k++)
                {
                    double arrive = rnd.NextDouble();
                    double purchase_prob = rnd.NextDouble();
                    if (arrive <= arrivalRate)
                    {
                        arrivalCount++;
                        if (purchase_prob <= predict_purchaseProb)
                        {
                            purchaseCount++;
                        }
                    }
                }
                purchaseSituation[i, 0] = price;
                purchaseSituation [i, 1] = price_c;
                purchaseSituation[i, 2] = arrivalCount;
                purchaseSituation[i, 3] = purchaseCount;
            }
        }
        //參考價格版本
        public void writeFile_referencePrice(int num, double delta, double delta_c)
        {
            using (var file = new StreamWriter(@"C:\Users\Josey\Desktop\test\horizon" + Horizon + "_240808_10-20_10-20_2420data\\simulation" + "0825_"
              + num + "_x.csv", false))//true繼續寫, 路徑記得改
            {
                for (int i = 0; i < Horizon; i++)
                {
                    double time = (double)(Horizon - i); //剩餘時間
                    double price = (double)purchaseSituation[i, 0];
                    double price_c = (double)purchaseSituation[i, 1];
                    double arrival = (double)purchaseSituation[i, 2];
                    double salesV = ((double)purchaseSituation[i, 3]);
                    double pred_soldprob = calculate_purchaseProb(delta, delta_c, price, price_c);
                    double real_soldprob;
                    if (arrival == 0)
                    {
                        real_soldprob = 0;
                        pred_soldprob = 0;
                    }
                    else
                        real_soldprob = salesV / arrival;

                    double abs_error = Math.Abs(pred_soldprob - real_soldprob);
                    if (i == 0)
                    {
                        file.WriteLine(time + "," + 0 + "," + 0 + "," + 0 + "," + 0); // with reference-price version
                        //file.WriteLine(time + "," + 0 + "," + 0 + "," + 0); // without reference-price version
                    }
                    else
                    {
                        file.WriteLine(time + "," + price + "," + price_c + "," + arrival + "," + salesV); // with reference-price version
                        //file.WriteLine(time + "," + price + "," + arrival + "," + salesV); // without reference-price version
                    }
                }
                Console.WriteLine();
            }

            using (var file = new StreamWriter(@"C:\Users\Josey\Desktop\test\horizon" + Horizon + "_240808_10-20_10-20_2420data\\simulation" + "0825_"
              + num + "_y.csv", false)) //路徑記得改
            {
                for (int i = 0; i < Horizon; i++)
                    file.WriteLine(delta + "," + delta_c); // with reference-price version
                    //file.WriteLine(delta); // without reference-price version
                Console.WriteLine();
            }
        }

        public double calculate_purchaseProb(double delta, double delta_c, double price, double price_c) //算discrete choice model中的購買機率
        {
            double prob = 0;
            double value = delta - price;
            double value_c = delta_c - price_c;
            prob = (Math.Exp(value))/(Math.Exp(value)+Math.Exp(value_c)+1); // with reference-price version
            //prob = (Math.Exp(value)) / (Math.Exp(value) + 1); // without reference-price version
            return prob;
        }  
    }
}
