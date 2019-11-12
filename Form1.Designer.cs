using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RustWorkshopUploader
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDo = new System.Windows.Forms.Button();
            this.txtWorkshopDesc = new System.Windows.Forms.RichTextBox();
            this.txtWorkshopName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemType = new System.Windows.Forms.TextBox();
            this.txtWorkshopId = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshopId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtFolder.Location = new System.Drawing.Point(55, 141);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(348, 20);
            this.txtFolder.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(409, 141);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(106, 20);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Select";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Image";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Workshop ID:";
            // 
            // btnDo
            // 
            this.btnDo.Enabled = false;
            this.btnDo.Location = new System.Drawing.Point(273, 330);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(242, 63);
            this.btnDo.TabIndex = 5;
            this.btnDo.Text = "ЗАГРУЗИТЬ";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // txtWorkshopDesc
            // 
            this.txtWorkshopDesc.Enabled = false;
            this.txtWorkshopDesc.Location = new System.Drawing.Point(273, 266);
            this.txtWorkshopDesc.Name = "txtWorkshopDesc";
            this.txtWorkshopDesc.Size = new System.Drawing.Size(242, 19);
            this.txtWorkshopDesc.TabIndex = 6;
            this.txtWorkshopDesc.Text = "";
            this.txtWorkshopDesc.TextChanged += new System.EventHandler(this.txtWorkshopDesc_TextChanged);
            // 
            // txtWorkshopName
            // 
            this.txtWorkshopName.Enabled = false;
            this.txtWorkshopName.Location = new System.Drawing.Point(273, 188);
            this.txtWorkshopName.Name = "txtWorkshopName";
            this.txtWorkshopName.Size = new System.Drawing.Size(242, 20);
            this.txtWorkshopName.TabIndex = 7;
            this.txtWorkshopName.TextChanged += new System.EventHandler(this.txtWorkshopName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Workshop name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Workshop description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Item Type:";
            // 
            // txtItemType
            // 
            this.txtItemType.Enabled = false;
            this.txtItemType.Location = new System.Drawing.Point(273, 227);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(242, 20);
            this.txtItemType.TabIndex = 7;
            this.txtItemType.TextChanged += new System.EventHandler(this.txtItemType_TextChanged);
            // 
            // txtWorkshopId
            // 
            this.txtWorkshopId.Enabled = false;
            this.txtWorkshopId.Location = new System.Drawing.Point(273, 304);
            this.txtWorkshopId.Name = "txtWorkshopId";
            this.txtWorkshopId.Size = new System.Drawing.Size(208, 20);
            this.txtWorkshopId.TabIndex = 8;
            this.txtWorkshopId.ValueChanged += new System.EventHandler(this.txtWorkshopId_ValueChanged);
            this.txtWorkshopId.DoubleClick += new System.EventHandler(this.txtWorkshopId_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::RustWorkshopUploader.Properties.Resources.logo;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(8, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(510, 118);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(52, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 26);
            this.label6.TabIndex = 12;
            this.label6.Text = "РАЗРАБОТЧИК: Vlad-00003 \r\nЧто то добавил OxideBro";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.ErrorImage = null;
            this.pictureBox2.Image = global::RustWorkshopUploader.Properties.Resources.picture_01_512;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(35, 172);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(194, 194);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Cursor = System.Windows.Forms.Cursors.No;
            this.ProgressBar.Location = new System.Drawing.Point(12, 397);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(503, 23);
            this.ProgressBar.TabIndex = 14;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(487, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 20);
            this.button1.TabIndex = 15;
            this.button1.Text = "N";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 423);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtWorkshopId);
            this.Controls.Add(this.txtItemType);
            this.Controls.Add(this.txtWorkshopName);
            this.Controls.Add(this.txtWorkshopDesc);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Custom Rust Workshop Uploader";
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshopId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtFolder;
        private Button btnSelectFolder;
        private Label label1;
        private Label label2;
        private Button btnDo;
        private RichTextBox txtWorkshopDesc;
        private TextBox txtWorkshopName;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtItemType;
        private NumericUpDown txtWorkshopId;
        private PictureBox pictureBox1;
        private Label label6;
        private PictureBox pictureBox2;
        private ProgressBar ProgressBar;
        private Button button1;
    }
}

