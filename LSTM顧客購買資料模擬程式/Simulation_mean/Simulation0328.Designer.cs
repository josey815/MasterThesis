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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbx_arrivalRate = new System.Windows.Forms.TextBox();
            this.tbx_LB_Pa = new System.Windows.Forms.TextBox();
            this.tbx_priceInterval = new System.Windows.Forms.TextBox();
            this.tbx_period = new System.Windows.Forms.TextBox();
            this.tbx_UB_Pa = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_start = new System.Windows.Forms.Button();
            this.tbx_observeTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbx_aLB = new System.Windows.Forms.TextBox();
            this.tbx_bLB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_aUB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_bUB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbx_UB_Pb = new System.Windows.Forms.TextBox();
            this.tbx_LB_Pb = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "顧客到達率";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 223);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "票價範圍";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "總期數";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "票價間隔";
            // 
            // tbx_arrivalRate
            // 
            this.tbx_arrivalRate.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_arrivalRate.Location = new System.Drawing.Point(170, 179);
            this.tbx_arrivalRate.Name = "tbx_arrivalRate";
            this.tbx_arrivalRate.Size = new System.Drawing.Size(100, 21);
            this.tbx_arrivalRate.TabIndex = 12;
            this.tbx_arrivalRate.Text = "0.7";
            this.tbx_arrivalRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_LB_Pa
            // 
            this.tbx_LB_Pa.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_LB_Pa.Location = new System.Drawing.Point(118, 218);
            this.tbx_LB_Pa.Name = "tbx_LB_Pa";
            this.tbx_LB_Pa.Size = new System.Drawing.Size(100, 21);
            this.tbx_LB_Pa.TabIndex = 13;
            this.tbx_LB_Pa.Text = "2";
            this.tbx_LB_Pa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_priceInterval
            // 
            this.tbx_priceInterval.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_priceInterval.Location = new System.Drawing.Point(171, 258);
            this.tbx_priceInterval.Name = "tbx_priceInterval";
            this.tbx_priceInterval.Size = new System.Drawing.Size(100, 21);
            this.tbx_priceInterval.TabIndex = 14;
            this.tbx_priceInterval.Text = "1";
            this.tbx_priceInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_period
            // 
            this.tbx_period.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_period.Location = new System.Drawing.Point(171, 96);
            this.tbx_period.Name = "tbx_period";
            this.tbx_period.Size = new System.Drawing.Size(100, 21);
            this.tbx_period.TabIndex = 16;
            this.tbx_period.Text = "250";
            this.tbx_period.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_UB_Pa
            // 
            this.tbx_UB_Pa.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_UB_Pa.Location = new System.Drawing.Point(266, 218);
            this.tbx_UB_Pa.Name = "tbx_UB_Pa";
            this.tbx_UB_Pa.Size = new System.Drawing.Size(100, 21);
            this.tbx_UB_Pa.TabIndex = 17;
            this.tbx_UB_Pa.Text = "27";
            this.tbx_UB_Pa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(239, 221);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "~";
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(345, 116);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(91, 58);
            this.btn_start.TabIndex = 23;
            this.btn_start.Text = "模擬開始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // tbx_observeTime
            // 
            this.tbx_observeTime.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_observeTime.Location = new System.Drawing.Point(171, 136);
            this.tbx_observeTime.Name = "tbx_observeTime";
            this.tbx_observeTime.Size = new System.Drawing.Size(100, 21);
            this.tbx_observeTime.TabIndex = 29;
            this.tbx_observeTime.Text = "20";
            this.tbx_observeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 28;
            this.label13.Text = "一期最多顧客數";
            // 
            // tbx_aLB
            // 
            this.tbx_aLB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_aLB.Location = new System.Drawing.Point(118, 22);
            this.tbx_aLB.Name = "tbx_aLB";
            this.tbx_aLB.Size = new System.Drawing.Size(100, 21);
            this.tbx_aLB.TabIndex = 30;
            this.tbx_aLB.Text = "10";
            this.tbx_aLB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_bLB
            // 
            this.tbx_bLB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_bLB.Location = new System.Drawing.Point(118, 56);
            this.tbx_bLB.Name = "tbx_bLB";
            this.tbx_bLB.Size = new System.Drawing.Size(100, 21);
            this.tbx_bLB.TabIndex = 31;
            this.tbx_bLB.Text = "10";
            this.tbx_bLB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "對手價值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "自身價值";
            // 
            // tbx_aUB
            // 
            this.tbx_aUB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_aUB.Location = new System.Drawing.Point(266, 22);
            this.tbx_aUB.Name = "tbx_aUB";
            this.tbx_aUB.Size = new System.Drawing.Size(100, 21);
            this.tbx_aUB.TabIndex = 34;
            this.tbx_aUB.Text = "20";
            this.tbx_aUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "~";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "~";
            // 
            // tbx_bUB
            // 
            this.tbx_bUB.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_bUB.Location = new System.Drawing.Point(266, 56);
            this.tbx_bUB.Name = "tbx_bUB";
            this.tbx_bUB.Size = new System.Drawing.Size(100, 21);
            this.tbx_bUB.TabIndex = 37;
            this.tbx_bUB.Text = "20";
            this.tbx_bUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(239, 297);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 41;
            this.label11.Text = "~";
            // 
            // tbx_UB_Pb
            // 
            this.tbx_UB_Pb.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_UB_Pb.Location = new System.Drawing.Point(266, 294);
            this.tbx_UB_Pb.Name = "tbx_UB_Pb";
            this.tbx_UB_Pb.Size = new System.Drawing.Size(100, 21);
            this.tbx_UB_Pb.TabIndex = 40;
            this.tbx_UB_Pb.Text = "20";
            this.tbx_UB_Pb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbx_LB_Pb
            // 
            this.tbx_LB_Pb.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_LB_Pb.Location = new System.Drawing.Point(118, 294);
            this.tbx_LB_Pb.Name = "tbx_LB_Pb";
            this.tbx_LB_Pb.Size = new System.Drawing.Size(100, 21);
            this.tbx_LB_Pb.TabIndex = 39;
            this.tbx_LB_Pb.Text = "10";
            this.tbx_LB_Pb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 299);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 38;
            this.label12.Text = "對手票價";
            // 
            // SimulationCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 337);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbx_UB_Pb);
            this.Controls.Add(this.tbx_LB_Pb);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbx_bUB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_aUB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbx_bLB);
            this.Controls.Add(this.tbx_aLB);
            this.Controls.Add(this.tbx_observeTime);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbx_UB_Pa);
            this.Controls.Add(this.tbx_period);
            this.Controls.Add(this.tbx_priceInterval);
            this.Controls.Add(this.tbx_LB_Pa);
            this.Controls.Add(this.tbx_arrivalRate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SimulationCustomer";
            this.Text = "Simulation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbx_arrivalRate;
        private System.Windows.Forms.TextBox tbx_LB_Pa;
        private System.Windows.Forms.TextBox tbx_priceInterval;
        private System.Windows.Forms.TextBox tbx_period;
        private System.Windows.Forms.TextBox tbx_UB_Pa;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.TextBox tbx_observeTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbx_aLB;
        private System.Windows.Forms.TextBox tbx_bLB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_aUB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_bUB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbx_UB_Pb;
        private System.Windows.Forms.TextBox tbx_LB_Pb;
        private System.Windows.Forms.Label label12;
    }
}

