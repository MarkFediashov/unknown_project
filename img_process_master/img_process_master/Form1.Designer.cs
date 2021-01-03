
namespace img_process_master
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.selectImage = new System.Windows.Forms.Button();
            this.filterList = new System.Windows.Forms.ComboBox();
            this.processButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1598, 716);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.OnImageClick);
            // 
            // selectImage
            // 
            this.selectImage.Location = new System.Drawing.Point(15, 800);
            this.selectImage.Name = "selectImage";
            this.selectImage.Size = new System.Drawing.Size(120, 28);
            this.selectImage.TabIndex = 1;
            this.selectImage.Text = "Browse image";
            this.selectImage.UseVisualStyleBackColor = true;
            this.selectImage.Click += new System.EventHandler(this.OnImageSelect);
            // 
            // filterList
            // 
            this.filterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterList.FormattingEnabled = true;
            this.filterList.Location = new System.Drawing.Point(208, 800);
            this.filterList.Name = "filterList";
            this.filterList.Size = new System.Drawing.Size(199, 21);
            this.filterList.TabIndex = 2;
            this.filterList.SelectedIndexChanged += new System.EventHandler(this.OnFilterSelect);
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(1507, 800);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(103, 28);
            this.processButton.TabIndex = 3;
            this.processButton.Text = "Process image";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.OnProcessButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1622, 856);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.filterList);
            this.Controls.Add(this.selectImage);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "ImageSwarmProcessing";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button selectImage;
        private System.Windows.Forms.ComboBox filterList;
        private System.Windows.Forms.Button processButton;
    }
}

