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
            this.components = new System.ComponentModel.Container();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRecreate = new System.Windows.Forms.Button();
            this.btnDo = new System.Windows.Forms.Button();
            this.tooltipWorkshopID = new System.Windows.Forms.ToolTip(this.components);
            this.txtWorkshopDesc = new System.Windows.Forms.RichTextBox();
            this.txtWorkshopName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtItemType = new System.Windows.Forms.TextBox();
            this.txtWorkshopId = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshopId)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(53, 5);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(348, 20);
            this.txtFolder.TabIndex = 0;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(407, 5);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(106, 20);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Workshop ID:";
            // 
            // btnRecreate
            // 
            this.btnRecreate.Location = new System.Drawing.Point(284, 178);
            this.btnRecreate.Name = "btnRecreate";
            this.btnRecreate.Size = new System.Drawing.Size(109, 23);
            this.btnRecreate.TabIndex = 4;
            this.btnRecreate.Text = "Create new entry";
            this.btnRecreate.UseVisualStyleBackColor = true;
            this.btnRecreate.Click += new System.EventHandler(this.btnRecreate_Click);
            // 
            // btnDo
            // 
            this.btnDo.Location = new System.Drawing.Point(399, 178);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(114, 23);
            this.btnDo.TabIndex = 5;
            this.btnDo.Text = "Upload to workshop";
            this.btnDo.UseVisualStyleBackColor = true;
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // txtWorkshopDesc
            // 
            this.txtWorkshopDesc.Location = new System.Drawing.Point(218, 50);
            this.txtWorkshopDesc.Name = "txtWorkshopDesc";
            this.txtWorkshopDesc.Size = new System.Drawing.Size(287, 96);
            this.txtWorkshopDesc.TabIndex = 6;
            this.txtWorkshopDesc.Text = "";
            this.txtWorkshopDesc.TextChanged += new System.EventHandler(this.txtWorkshopDesc_TextChanged);
            // 
            // txtWorkshopName
            // 
            this.txtWorkshopName.Location = new System.Drawing.Point(15, 50);
            this.txtWorkshopName.Name = "txtWorkshopName";
            this.txtWorkshopName.Size = new System.Drawing.Size(197, 20);
            this.txtWorkshopName.TabIndex = 7;
            this.txtWorkshopName.TextChanged += new System.EventHandler(this.txtWorkshopName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Workshop name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Workshop description:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Item Type:";
            // 
            // txtItemType
            // 
            this.txtItemType.Location = new System.Drawing.Point(15, 90);
            this.txtItemType.Name = "txtItemType";
            this.txtItemType.Size = new System.Drawing.Size(197, 20);
            this.txtItemType.TabIndex = 7;
            this.txtItemType.TextChanged += new System.EventHandler(this.txtItemType_TextChanged);
            // 
            // txtWorkshopId
            // 
            this.txtWorkshopId.Location = new System.Drawing.Point(83, 181);
            this.txtWorkshopId.Name = "txtWorkshopId";
            this.txtWorkshopId.Size = new System.Drawing.Size(195, 20);
            this.txtWorkshopId.TabIndex = 8;
            this.txtWorkshopId.ValueChanged += new System.EventHandler(this.txtWorkshopId_ValueChanged);
            this.txtWorkshopId.Enter += new System.EventHandler(this.txtWorkshopId_MouseEnter);
            this.txtWorkshopId.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtWorkshopId_MouseDown);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 211);
            this.Controls.Add(this.txtWorkshopId);
            this.Controls.Add(this.txtItemType);
            this.Controls.Add(this.txtWorkshopName);
            this.Controls.Add(this.txtWorkshopDesc);
            this.Controls.Add(this.btnDo);
            this.Controls.Add(this.btnRecreate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolder);
            this.Name = "frmMain";
            this.Text = "Custom Rust Workshop Uploader";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkshopId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtFolder;
        private Button btnSelectFolder;
        private Label label1;
        private Label label2;
        private Button btnRecreate;
        private Button btnDo;
        private ToolTip tooltipWorkshopID;
        private RichTextBox txtWorkshopDesc;
        private TextBox txtWorkshopName;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtItemType;
        private NumericUpDown txtWorkshopId;
    }
}

