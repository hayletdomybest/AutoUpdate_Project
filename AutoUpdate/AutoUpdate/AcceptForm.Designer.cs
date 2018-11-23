namespace AutoUpdate
{
    partial class AcceptForm
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
            this.btn_update_Yes = new System.Windows.Forms.Button();
            this.btn_update_No = new System.Windows.Forms.Button();
            this.lab_title_lastest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_update_Yes
            // 
            this.btn_update_Yes.Location = new System.Drawing.Point(83, 107);
            this.btn_update_Yes.Name = "btn_update_Yes";
            this.btn_update_Yes.Size = new System.Drawing.Size(75, 23);
            this.btn_update_Yes.TabIndex = 0;
            this.btn_update_Yes.Text = "更新";
            this.btn_update_Yes.UseVisualStyleBackColor = true;
            this.btn_update_Yes.Click += new System.EventHandler(this.btn_update_Yes_Click);
            // 
            // btn_update_No
            // 
            this.btn_update_No.Location = new System.Drawing.Point(219, 107);
            this.btn_update_No.Name = "btn_update_No";
            this.btn_update_No.Size = new System.Drawing.Size(75, 23);
            this.btn_update_No.TabIndex = 1;
            this.btn_update_No.Text = "取消";
            this.btn_update_No.UseVisualStyleBackColor = true;
            this.btn_update_No.Click += new System.EventHandler(this.btn_update_No_Click);
            // 
            // lab_title_lastest
            // 
            this.lab_title_lastest.AutoSize = true;
            this.lab_title_lastest.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_title_lastest.Location = new System.Drawing.Point(91, 28);
            this.lab_title_lastest.Name = "lab_title_lastest";
            this.lab_title_lastest.Size = new System.Drawing.Size(143, 21);
            this.lab_title_lastest.TabIndex = 2;
            this.lab_title_lastest.Text = "有最新版本{0}";
            // 
            // AcceptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 165);
            this.Controls.Add(this.lab_title_lastest);
            this.Controls.Add(this.btn_update_No);
            this.Controls.Add(this.btn_update_Yes);
            this.Name = "AcceptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AcceptForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_update_Yes;
        private System.Windows.Forms.Button btn_update_No;
        private System.Windows.Forms.Label lab_title_lastest;
    }
}