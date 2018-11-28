namespace DownLoadForm
{
    partial class DownLoadForm
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bar_rate = new System.Windows.Forms.ProgressBar();
            this.lab_FileName = new System.Windows.Forms.Label();
            this.lab_FileSize = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.lab_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(259, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "檔案大小";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(27, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "檔案名稱:";
            // 
            // bar_rate
            // 
            this.bar_rate.Location = new System.Drawing.Point(24, 41);
            this.bar_rate.Name = "bar_rate";
            this.bar_rate.Size = new System.Drawing.Size(524, 60);
            this.bar_rate.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.bar_rate.TabIndex = 5;
            // 
            // lab_FiileName
            // 
            this.lab_FileName.AutoSize = true;
            this.lab_FileName.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_FileName.Location = new System.Drawing.Point(109, 116);
            this.lab_FileName.Name = "lab_FiileName";
            this.lab_FileName.Size = new System.Drawing.Size(46, 16);
            this.lab_FileName.TabIndex = 8;
            this.lab_FileName.Text = "label3";
            // 
            // lab_FileSize
            // 
            this.lab_FileSize.AutoSize = true;
            this.lab_FileSize.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_FileSize.Location = new System.Drawing.Point(350, 116);
            this.lab_FileSize.Name = "lab_FileSize";
            this.lab_FileSize.Size = new System.Drawing.Size(46, 16);
            this.lab_FileSize.TabIndex = 9;
            this.lab_FileSize.Text = "label4";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(501, 182);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 10;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // lab_Title
            // 
            this.lab_Title.AutoSize = true;
            this.lab_Title.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Title.Location = new System.Drawing.Point(27, 9);
            this.lab_Title.Name = "lab_Title";
            this.lab_Title.Size = new System.Drawing.Size(72, 16);
            this.lab_Title.TabIndex = 11;
            this.lab_Title.Text = "下載進度";
            // 
            // DownLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 205);
            this.Controls.Add(this.lab_Title);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.lab_FileSize);
            this.Controls.Add(this.lab_FileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bar_rate);
            this.Name = "DownLoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownLoadForm";
            this.Load += new System.EventHandler(this.DownLoadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ProgressBar bar_rate;
        public System.Windows.Forms.Label lab_FileName;
        public System.Windows.Forms.Label lab_FileSize;
        public System.Windows.Forms.Button btn_Cancel;
        public System.Windows.Forms.Label lab_Title;
    }
}

