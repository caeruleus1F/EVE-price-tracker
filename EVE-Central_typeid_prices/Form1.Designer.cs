namespace EVE_Central_typeid_prices
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txbNextPull = new System.Windows.Forms.TextBox();
            this.requestTimer = new System.Windows.Forms.Timer(this.components);
            this.btnPull = new System.Windows.Forms.Button();
            this.txbSystem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Next pull:";
            // 
            // txbNextPull
            // 
            this.txbNextPull.Location = new System.Drawing.Point(12, 29);
            this.txbNextPull.Name = "txbNextPull";
            this.txbNextPull.Size = new System.Drawing.Size(283, 22);
            this.txbNextPull.TabIndex = 1;
            // 
            // requestTimer
            // 
            this.requestTimer.Interval = 300000;
            this.requestTimer.Tick += new System.EventHandler(this.requestTimer_Tick);
            // 
            // btnPull
            // 
            this.btnPull.Location = new System.Drawing.Point(12, 57);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(75, 23);
            this.btnPull.TabIndex = 2;
            this.btnPull.Text = "Pull";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // txbSystem
            // 
            this.txbSystem.Location = new System.Drawing.Point(243, 103);
            this.txbSystem.Name = "txbSystem";
            this.txbSystem.Size = new System.Drawing.Size(120, 22);
            this.txbSystem.TabIndex = 3;
            this.txbSystem.Text = "Jita";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "System:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 137);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbSystem);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.txbNextPull);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "EVE Central TypeID Price Puller";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbNextPull;
        private System.Windows.Forms.Timer requestTimer;
        private System.Windows.Forms.Button btnPull;
        private System.Windows.Forms.TextBox txbSystem;
        private System.Windows.Forms.Label label2;
    }
}

