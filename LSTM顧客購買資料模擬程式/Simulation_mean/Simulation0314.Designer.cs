namespace Simulation
{
    partial class SimulationCustomer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbx_numberOfProduct = new System.Windows.Forms.TextBox();
            this.tbx_meanOfStd = new System.Windows.Forms.TextBox();
            this.tbx_arrivalRate = new System.Windows.Forms.TextBox();
            this.tbx_LB_P = new System.Windows.Forms.TextBox();
            this.tbx_priceInterval = new System.Windows.Forms.TextBox();
            this.tbx_meanOfMean = new System.Windows.Forms.TextBox();
            this.tbx_period = new System.Windows.Forms.TextBox();
            this.tbx_UB_P = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_stdOfMean = new System.Windows.Forms.TextBox();
            this.tbx_stdOfStd = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_observeTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbx_simuTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbx_changeDistributionTime = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "航班座位";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "顧客認為效用Mean分佈";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "顧客認為Std分佈 (% of mean)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "顧客到達率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "票價範圍";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "總期數";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 382);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "票價間隔";
            // 
            // tbx_numberOfProduct
            // 
            this.tbx_numberOfProduct.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_numberOfProduct.Location = new System.Drawing.Point(175, 104);
            this.tbx_numberOfProduct.Name = "tbx_numberOfProduct";
            this.tbx_numberOfProduct.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbx_numberOfProduct.Size = new System.Drawing.Size(100, 21);
            this.tbx_numberOfProduct.TabIndex = 10;
            this.tbx_numberOfProduct.Text = "2000";
            this.tbx_numberOfProduct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_meanOfStd
            // 
            this.tbx_meanOfStd.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_meanOfStd.Location = new System.Drawing.Point(175, 196);
            this.tbx_meanOfStd.Name = "tbx_meanOfStd";
            this.tbx_meanOfStd.Size = new System.Drawing.Size(100, 21);
            this.tbx_meanOfStd.TabIndex = 11;
            this.tbx_meanOfStd.Text = "10";
            this.tbx_meanOfStd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_arrivalRate
            // 
            this.tbx_arrivalRate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_arrivalRate.Location = new System.Drawing.Point(174, 287);
            this.tbx_arrivalRate.Name = "tbx_arrivalRate";
            this.tbx_arrivalRate.Size = new System.Drawing.Size(100, 21);
            this.tbx_arrivalRate.TabIndex = 12;
            this.tbx_arrivalRate.Text = " 0.6";
            this.tbx_arrivalRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_LB_P
            // 
            this.tbx_LB_P.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_LB_P.Location = new System.Drawing.Point(174, 339);
            this.tbx_LB_P.Name = "tbx_LB_P";
            this.tbx_LB_P.Size = new System.Drawing.Size(100, 21);
            this.tbx_LB_P.TabIndex = 13;
            this.tbx_LB_P.Text = "10";
            this.tbx_LB_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_priceInterval
            // 
            this.tbx_priceInterval.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_priceInterval.Location = new System.Drawing.Point(174, 377);
            this.tbx_priceInterval.Name = "tbx_priceInterval";
            this.tbx_priceInterval.Size = new System.Drawing.Size(100, 21);
            this.tbx_priceInterval.TabIndex = 14;
            this.tbx_priceInterval.Text = "1";
            this.tbx_priceInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_meanOfMean
            // 
            this.tbx_meanOfMean.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_meanOfMean.Location = new System.Drawing.Point(175, 152);
            this.tbx_meanOfMean.Name = "tbx_meanOfMean";
            this.tbx_meanOfMean.Size = new System.Drawing.Size(100, 21);
            this.tbx_meanOfMean.TabIndex = 15;
            this.tbx_meanOfMean.Text = "45";
            this.tbx_meanOfMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_period
            // 
            this.tbx_period.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_period.Location = new System.Drawing.Point(175, 24);
            this.tbx_period.Name = "tbx_period";
            this.tbx_period.Size = new System.Drawing.Size(100, 21);
            this.tbx_period.TabIndex = 16;
            this.tbx_period.Text = "500";
            this.tbx_period.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_UB_P
            // 
            this.tbx_UB_P.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_UB_P.Location = new System.Drawing.Point(320, 339);
            this.tbx_UB_P.Name = "tbx_UB_P";
            this.tbx_UB_P.Size = new System.Drawing.Size(100, 21);
            this.tbx_UB_P.TabIndex = 17;
            this.tbx_UB_P.Text = "100";
            this.tbx_UB_P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 342);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "~";
            // 
            // tbx_stdOfMean
            // 
            this.tbx_stdOfMean.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_stdOfMean.Location = new System.Drawing.Point(321, 152);
            this.tbx_stdOfMean.Name = "tbx_stdOfMean";
            this.tbx_stdOfMean.Size = new System.Drawing.Size(100, 21);
            this.tbx_stdOfMean.TabIndex = 21;
            this.tbx_stdOfMean.Text = "10";
            this.tbx_stdOfMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_stdOfStd
            // 
            this.tbx_stdOfStd.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_stdOfStd.Location = new System.Drawing.Point(321, 196);
            this.tbx_stdOfStd.Name = "tbx_stdOfStd";
            this.tbx_stdOfStd.Size = new System.Drawing.Size(100, 21);
            this.tbx_stdOfStd.TabIndex = 22;
            this.tbx_stdOfStd.Text = "1";
            this.tbx_stdOfStd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(330, 403);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 35);
            this.btn_start.TabIndex = 23;
            this.btn_start.Text = "模擬開始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 26;
            // 
            // tbx_observeTime
            // 
            this.tbx_observeTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_observeTime.Location = new System.Drawing.Point(175, 64);
            this.tbx_observeTime.Name = "tbx_observeTime";
            this.tbx_observeTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_observeTime.TabIndex = 29;
            this.tbx_observeTime.Text = "5";
            this.tbx_observeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "一期最多顧客數";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(26, 422);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 30;
            this.label14.Text = "模擬次數";
            // 
            // tbx_simuTime
            // 
            this.tbx_simuTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_simuTime.Location = new System.Drawing.Point(174, 417);
            this.tbx_simuTime.Name = "tbx_simuTime";
            this.tbx_simuTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_simuTime.TabIndex = 31;
            this.tbx_simuTime.Text = "10";
            this.tbx_simuTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(138, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "Mean";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(138, 201);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "Mean";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(294, 157);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 12);
            this.label15.TabIndex = 34;
            this.label15.Text = "Std";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(294, 201);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 12);
            this.label16.TabIndex = 35;
            this.label16.Text = "Std";
            // 
            // tbx_changeDistributionTime
            // 
            this.tbx_changeDistributionTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_changeDistributionTime.Location = new System.Drawing.Point(175, 240);
            this.tbx_changeDistributionTime.Name = "tbx_changeDistributionTime";
            this.tbx_changeDistributionTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_changeDistributionTime.TabIndex = 37;
            this.tbx_changeDistributionTime.Text = "500";
            this.tbx_changeDistributionTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(27, 243);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 12);
            this.label17.TabIndex = 36;
            this.label17.Text = "幾期變一次分佈";
            // 
            // SimulationCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 463);
            this.Controls.Add(this.tbx_changeDistributionTime);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbx_simuTime);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbx_observeTime);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.tbx_stdOfStd);
            this.Controls.Add(this.tbx_stdOfMean);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbx_UB_P);
            this.Controls.Add(this.tbx_period);
            this.Controls.Add(this.tbx_meanOfMean);
            this.Controls.Add(this.tbx_priceInterval);
            this.Controls.Add(this.tbx_LB_P);
            this.Controls.Add(this.tbx_arrivalRate);
            this.Controls.Add(this.tbx_meanOfStd);
            this.Controls.Add(this.tbx_numberOfProduct);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SimulationCustomer";
            this.Text = "simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbx_meanOfStd;
        private System.Windows.Forms.TextBox tbx_arrivalRate;
        private System.Windows.Forms.TextBox tbx_LB_P;
        private System.Windows.Forms.TextBox tbx_priceInterval;
        private System.Windows.Forms.TextBox tbx_meanOfMean;
        private System.Windows.Forms.TextBox tbx_period;
        private System.Windows.Forms.TextBox tbx_UB_P;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbx_numberOfProduct;
        private System.Windows.Forms.TextBox tbx_stdOfMean;
        private System.Windows.Forms.TextBox tbx_stdOfStd;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_observeTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbx_simuTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbx_changeDistributionTime;
        private System.Windows.Forms.Label label17;
    }
}

