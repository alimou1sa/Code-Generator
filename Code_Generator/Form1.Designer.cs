namespace Code_Generator
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
            this.label7 = new System.Windows.Forms.Label();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerat = new Guna.UI2.WinForms.Guna2Button();
            this.label5 = new System.Windows.Forms.Label();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDataBaseName = new Guna.UI2.WinForms.Guna2ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSetting = new Guna.UI2.WinForms.Guna2Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 107);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 23);
            this.label7.TabIndex = 29;
            this.label7.Text = "Project Name: ";
            // 
            // tbProjectName
            // 
            this.tbProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProjectName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbProjectName.Location = new System.Drawing.Point(184, 93);
            this.tbProjectName.Margin = new System.Windows.Forms.Padding(4);
            this.tbProjectName.Multiline = true;
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.Size = new System.Drawing.Size(239, 37);
            this.tbProjectName.TabIndex = 17;
            this.tbProjectName.Validating += new System.ComponentModel.CancelEventHandler(this.tbProjectName_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(106, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(263, 43);
            this.label4.TabIndex = 26;
            this.label4.Text = "Code Generator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 245);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 23);
            this.label3.TabIndex = 25;
            this.label3.Text = "Paasword :";
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbPassword.Location = new System.Drawing.Point(184, 243);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.tbPassword.Multiline = true;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(239, 37);
            this.tbPassword.TabIndex = 23;
            this.tbPassword.Validating += new System.ComponentModel.CancelEventHandler(this.tbPassword_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 199);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 23);
            this.label2.TabIndex = 22;
            this.label2.Text = "User ID :";
            // 
            // tbUserID
            // 
            this.tbUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUserID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tbUserID.Location = new System.Drawing.Point(184, 193);
            this.tbUserID.Margin = new System.Windows.Forms.Padding(4);
            this.tbUserID.Multiline = true;
            this.tbUserID.Name = "tbUserID";
            this.tbUserID.Size = new System.Drawing.Size(239, 37);
            this.tbUserID.TabIndex = 20;
            this.tbUserID.Validating += new System.ComponentModel.CancelEventHandler(this.tbUserID_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 153);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Database Name :";
            // 
            // btnGenerat
            // 
            this.btnGenerat.BorderColor = System.Drawing.Color.Firebrick;
            this.btnGenerat.BorderRadius = 22;
            this.btnGenerat.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this.btnGenerat.BorderThickness = 3;
            this.btnGenerat.CheckedState.Parent = this.btnGenerat;
            this.btnGenerat.CustomImages.Parent = this.btnGenerat;
            this.btnGenerat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGenerat.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerat.ForeColor = System.Drawing.Color.Black;
            this.btnGenerat.HoverState.Parent = this.btnGenerat;
            this.btnGenerat.Location = new System.Drawing.Point(296, 294);
            this.btnGenerat.Name = "btnGenerat";
            this.btnGenerat.ShadowDecoration.Parent = this.btnGenerat;
            this.btnGenerat.Size = new System.Drawing.Size(127, 62);
            this.btnGenerat.TabIndex = 30;
            this.btnGenerat.Text = "Generate";
            this.btnGenerat.Click += new System.EventHandler(this.btnGenerat_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe Script", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(-2, 339);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 27);
            this.label5.TabIndex = 31;
            this.label5.Text = "V 1.0";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRememberMe.Location = new System.Drawing.Point(94, 294);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(113, 20);
            this.chkRememberMe.TabIndex = 32;
            this.chkRememberMe.Text = "Remember Me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Script MT Bold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(90, 346);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "Dev By: ALi Mousa";
            // 
            // cbDataBaseName
            // 
            this.cbDataBaseName.BackColor = System.Drawing.Color.Transparent;
            this.cbDataBaseName.BorderColor = System.Drawing.Color.Black;
            this.cbDataBaseName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbDataBaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBaseName.FocusedColor = System.Drawing.Color.Empty;
            this.cbDataBaseName.FocusedState.Parent = this.cbDataBaseName;
            this.cbDataBaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDataBaseName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cbDataBaseName.FormattingEnabled = true;
            this.cbDataBaseName.HoverState.Parent = this.cbDataBaseName;
            this.cbDataBaseName.ItemHeight = 28;
            this.cbDataBaseName.ItemsAppearance.Parent = this.cbDataBaseName;
            this.cbDataBaseName.Location = new System.Drawing.Point(184, 144);
            this.cbDataBaseName.Name = "cbDataBaseName";
            this.cbDataBaseName.ShadowDecoration.Parent = this.cbDataBaseName;
            this.cbDataBaseName.Size = new System.Drawing.Size(239, 34);
            this.cbDataBaseName.TabIndex = 34;
            this.cbDataBaseName.SelectedIndexChanged += new System.EventHandler(this.cbDataBaseName_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSetting
            // 
            this.btnSetting.BorderRadius = 14;
            this.btnSetting.CheckedState.Parent = this.btnSetting;
            this.btnSetting.CustomImages.Parent = this.btnSetting;
            this.btnSetting.FillColor = System.Drawing.Color.DarkGray;
            this.btnSetting.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSetting.ForeColor = System.Drawing.Color.White;
            this.btnSetting.HoverState.Parent = this.btnSetting;
            this.btnSetting.Image = global::Code_Generator.Properties.Resources.Settings36;
            this.btnSetting.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSetting.Location = new System.Drawing.Point(2, 2);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.ShadowDecoration.Parent = this.btnSetting;
            this.btnSetting.Size = new System.Drawing.Size(36, 36);
            this.btnSetting.TabIndex = 35;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(454, 368);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.cbDataBaseName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnGenerat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbProjectName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserID);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(1, 1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gode Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserID;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnGenerat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2ComboBox cbDataBaseName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Guna.UI2.WinForms.Guna2Button btnSetting;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
    }
}

