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
            this.tbx_seat1 = new System.Windows.Forms.TextBox();
            this.tbx_std1 = new System.Windows.Forms.TextBox();
            this.tbx_arrivalRate = new System.Windows.Forms.TextBox();
            this.tbx_LB = new System.Windows.Forms.TextBox();
            this.tbx_priceInterval = new System.Windows.Forms.TextBox();
            this.tbx_mean_lowB = new System.Windows.Forms.TextBox();
            this.tbx_period = new System.Windows.Forms.TextBox();
            this.tbx_UB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_mean_upB = new System.Windows.Forms.TextBox();
            this.tbx_std2 = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.tbx_recordTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbx_changePriceTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
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
            this.label3.Text = "顧客認為效用Mean範圍";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "顧客認為Std範圍 (% of mean)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "顧客到達率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 301);
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
            this.label9.Location = new System.Drawing.Point(27, 341);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "票價間隔";
            // 
            // tbx_seat1
            // 
            this.tbx_seat1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_seat1.Location = new System.Drawing.Point(175, 104);
            this.tbx_seat1.Name = "tbx_seat1";
            this.tbx_seat1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbx_seat1.Size = new System.Drawing.Size(100, 21);
            this.tbx_seat1.TabIndex = 10;
            this.tbx_seat1.Text = "100";
            this.tbx_seat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_std1
            // 
            this.tbx_std1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_std1.Location = new System.Drawing.Point(175, 196);
            this.tbx_std1.Name = "tbx_std1";
            this.tbx_std1.Size = new System.Drawing.Size(100, 21);
            this.tbx_std1.TabIndex = 11;
            this.tbx_std1.Text = "10";
            this.tbx_std1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_arrivalRate
            // 
            this.tbx_arrivalRate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_arrivalRate.Location = new System.Drawing.Point(175, 246);
            this.tbx_arrivalRate.Name = "tbx_arrivalRate";
            this.tbx_arrivalRate.Size = new System.Drawing.Size(100, 21);
            this.tbx_arrivalRate.TabIndex = 12;
            this.tbx_arrivalRate.Text = "0.5";
            this.tbx_arrivalRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_LB
            // 
            this.tbx_LB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_LB.Location = new System.Drawing.Point(175, 298);
            this.tbx_LB.Name = "tbx_LB";
            this.tbx_LB.Size = new System.Drawing.Size(100, 21);
            this.tbx_LB.TabIndex = 13;
            this.tbx_LB.Text = "10";
            this.tbx_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_priceInterval
            // 
            this.tbx_priceInterval.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_priceInterval.Location = new System.Drawing.Point(175, 336);
            this.tbx_priceInterval.Name = "tbx_priceInterval";
            this.tbx_priceInterval.Size = new System.Drawing.Size(100, 21);
            this.tbx_priceInterval.TabIndex = 14;
            this.tbx_priceInterval.Text = "1";
            this.tbx_priceInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_mean_lowB
            // 
            this.tbx_mean_lowB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_mean_lowB.Location = new System.Drawing.Point(175, 152);
            this.tbx_mean_lowB.Name = "tbx_mean_lowB";
            this.tbx_mean_lowB.Size = new System.Drawing.Size(100, 21);
            this.tbx_mean_lowB.TabIndex = 15;
            this.tbx_mean_lowB.Text = "45";
            this.tbx_mean_lowB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // tbx_UB
            // 
            this.tbx_UB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_UB.Location = new System.Drawing.Point(321, 298);
            this.tbx_UB.Name = "tbx_UB";
            this.tbx_UB.Size = new System.Drawing.Size(100, 21);
            this.tbx_UB.TabIndex = 17;
            this.tbx_UB.Text = "100";
            this.tbx_UB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(294, 301);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "~";
            // 
            // tbx_mean_upB
            // 
            this.tbx_mean_upB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_mean_upB.Location = new System.Drawing.Point(321, 152);
            this.tbx_mean_upB.Name = "tbx_mean_upB";
            this.tbx_mean_upB.Size = new System.Drawing.Size(100, 21);
            this.tbx_mean_upB.TabIndex = 21;
            this.tbx_mean_upB.Text = "60";
            this.tbx_mean_upB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_std2
            // 
            this.tbx_std2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_std2.Location = new System.Drawing.Point(321, 196);
            this.tbx_std2.Name = "tbx_std2";
            this.tbx_std2.Size = new System.Drawing.Size(100, 21);
            this.tbx_std2.TabIndex = 22;
            this.tbx_std2.Text = "15";
            this.tbx_std2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(334, 362);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 35);
            this.btn_start.TabIndex = 23;
            this.btn_start.Text = "模擬開始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // tbx_recordTime
            // 
            this.tbx_recordTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_recordTime.Location = new System.Drawing.Point(175, 376);
            this.tbx_recordTime.Name = "tbx_recordTime";
            this.tbx_recordTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_recordTime.TabIndex = 25;
            this.tbx_recordTime.Text = "100";
            this.tbx_recordTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "紀錄次數";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "~";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(294, 201);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "~";
            // 
            // tbx_changePriceTime
            // 
            this.tbx_changePriceTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_changePriceTime.Location = new System.Drawing.Point(175, 64);
            this.tbx_changePriceTime.Name = "tbx_changePriceTime";
            this.tbx_changePriceTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_changePriceTime.TabIndex = 29;
            this.tbx_changePriceTime.Text = "5";
            this.tbx_changePriceTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "幾期變一次價錢";
            // 
            // SimulationCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 427);
            this.Controls.Add(this.tbx_changePriceTime);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_recordTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.tbx_std2);
            this.Controls.Add(this.tbx_mean_upB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbx_UB);
            this.Controls.Add(this.tbx_period);
            this.Controls.Add(this.tbx_mean_lowB);
            this.Controls.Add(this.tbx_priceInterval);
            this.Controls.Add(this.tbx_LB);
            this.Controls.Add(this.tbx_arrivalRate);
            this.Controls.Add(this.tbx_std1);
            this.Controls.Add(this.tbx_seat1);
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
            this.Text = "Form1";
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
        private System.Windows.Forms.TextBox tbx_std1;
        private System.Windows.Forms.TextBox tbx_arrivalRate;
        private System.Windows.Forms.TextBox tbx_LB;
        private System.Windows.Forms.TextBox tbx_priceInterval;
        private System.Windows.Forms.TextBox tbx_mean_lowB;
        private System.Windows.Forms.TextBox tbx_period;
        private System.Windows.Forms.TextBox tbx_UB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbx_seat1;
        private System.Windows.Forms.TextBox tbx_mean_upB;
        private System.Windows.Forms.TextBox tbx_std2;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.TextBox tbx_recordTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbx_changePriceTime;
        private System.Windows.Forms.Label label13;
    }
}

