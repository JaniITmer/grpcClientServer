

namespace GrpcClientApp
{
    partial class AutokForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutokForm));
            this.autoData = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            this.autoNameText = new System.Windows.Forms.TextBox();
            this.AutoTypeText = new System.Windows.Forms.TextBox();
            this.AutoPriceText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.loginPassword = new System.Windows.Forms.TextBox();
            this.loginUsername = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.regUsernameLabel = new System.Windows.Forms.Label();
            this.regPasswordLabel = new System.Windows.Forms.Label();
            this.regButton = new System.Windows.Forms.Button();
            this.registerName = new System.Windows.Forms.TextBox();
            this.registerPassword = new System.Windows.Forms.TextBox();
            this.IdText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.regLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.autoData)).BeginInit();
            this.SuspendLayout();
            // 
            // autoData
            // 
            this.autoData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.autoData.Location = new System.Drawing.Point(174, 12);
            this.autoData.Name = "autoData";
            this.autoData.RowTemplate.Height = 25;
            this.autoData.Size = new System.Drawing.Size(528, 316);
            this.autoData.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(140, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Autók megjelenítése";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.getButton_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(295, 338);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 34);
            this.button2.TabIndex = 2;
            this.button2.Text = "Autó hozzáadása";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.addButton_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button3.Location = new System.Drawing.Point(445, 338);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 34);
            this.button3.TabIndex = 3;
            this.button3.Text = "Autó szerkesztése";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button4.Location = new System.Drawing.Point(636, 338);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 34);
            this.button4.TabIndex = 4;
            this.button4.Text = "Autó törlése";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.Color.Red;
            this.logoutButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.logoutButton.Location = new System.Drawing.Point(662, 394);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(109, 41);
            this.logoutButton.TabIndex = 5;
            this.logoutButton.Text = "Kijelentkezés";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // autoNameText
            // 
            this.autoNameText.Location = new System.Drawing.Point(140, 404);
            this.autoNameText.Name = "autoNameText";
            this.autoNameText.Size = new System.Drawing.Size(137, 23);
            this.autoNameText.TabIndex = 6;
            // 
            // AutoTypeText
            // 
            this.AutoTypeText.Location = new System.Drawing.Point(306, 404);
            this.AutoTypeText.Name = "AutoTypeText";
            this.AutoTypeText.Size = new System.Drawing.Size(129, 23);
            this.AutoTypeText.TabIndex = 7;
            // 
            // AutoPriceText
            // 
            this.AutoPriceText.Location = new System.Drawing.Point(458, 403);
            this.AutoPriceText.Name = "AutoPriceText";
            this.AutoPriceText.Size = new System.Drawing.Size(112, 23);
            this.AutoPriceText.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Autó neve:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Autó meghajtása:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(487, 385);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Autó ára:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Felhasználónév:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Jelszó:";
            // 
            // loginPassword
            // 
            this.loginPassword.Location = new System.Drawing.Point(12, 121);
            this.loginPassword.Name = "loginPassword";
            this.loginPassword.Size = new System.Drawing.Size(100, 23);
            this.loginPassword.TabIndex = 14;
            // 
            // loginUsername
            // 
            this.loginUsername.Location = new System.Drawing.Point(12, 62);
            this.loginUsername.Name = "loginUsername";
            this.loginUsername.Size = new System.Drawing.Size(100, 23);
            this.loginUsername.TabIndex = 15;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.loginButton.Location = new System.Drawing.Point(12, 167);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(100, 37);
            this.loginButton.TabIndex = 16;
            this.loginButton.Text = "Bejelentkezés";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // regUsernameLabel
            // 
            this.regUsernameLabel.AutoSize = true;
            this.regUsernameLabel.Location = new System.Drawing.Point(12, 235);
            this.regUsernameLabel.Name = "regUsernameLabel";
            this.regUsernameLabel.Size = new System.Drawing.Size(90, 15);
            this.regUsernameLabel.TabIndex = 17;
            this.regUsernameLabel.Text = "Felhasználónév:";
            // 
            // regPasswordLabel
            // 
            this.regPasswordLabel.AutoSize = true;
            this.regPasswordLabel.Location = new System.Drawing.Point(37, 298);
            this.regPasswordLabel.Name = "regPasswordLabel";
            this.regPasswordLabel.Size = new System.Drawing.Size(40, 15);
            this.regPasswordLabel.TabIndex = 18;
            this.regPasswordLabel.Text = "Jelszó:";
            // 
            // regButton
            // 
            this.regButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.regButton.Location = new System.Drawing.Point(12, 345);
            this.regButton.Name = "regButton";
            this.regButton.Size = new System.Drawing.Size(100, 40);
            this.regButton.TabIndex = 19;
            this.regButton.Text = "Regisztráció";
            this.regButton.UseVisualStyleBackColor = false;
            this.regButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // registerName
            // 
            this.registerName.Location = new System.Drawing.Point(12, 263);
            this.registerName.Name = "registerName";
            this.registerName.Size = new System.Drawing.Size(100, 23);
            this.registerName.TabIndex = 20;
            // 
            // registerPassword
            // 
            this.registerPassword.Location = new System.Drawing.Point(12, 316);
            this.registerPassword.Name = "registerPassword";
            this.registerPassword.Size = new System.Drawing.Size(100, 23);
            this.registerPassword.TabIndex = 21;
            // 
            // IdText
            // 
            this.IdText.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.IdText.Location = new System.Drawing.Point(576, 345);
            this.IdText.Name = "IdText";
            this.IdText.Size = new System.Drawing.Size(54, 23);
            this.IdText.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(594, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Id";
            // 
            // regLink
            // 
            this.regLink.AutoSize = true;
            this.regLink.Location = new System.Drawing.Point(27, 149);
            this.regLink.Name = "regLink";
            this.regLink.Size = new System.Drawing.Size(66, 15);
            this.regLink.TabIndex = 24;
            this.regLink.TabStop = true;
            this.regLink.Text = "Regisztáció";
            this.regLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.regLinkLabel_LinkClicked);
            // 
            // AutokForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.regLink);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.IdText);
            this.Controls.Add(this.registerPassword);
            this.Controls.Add(this.registerName);
            this.Controls.Add(this.regButton);
            this.Controls.Add(this.regPasswordLabel);
            this.Controls.Add(this.regUsernameLabel);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loginUsername);
            this.Controls.Add(this.loginPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AutoPriceText);
            this.Controls.Add(this.AutoTypeText);
            this.Controls.Add(this.autoNameText);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.autoData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutokForm";
            this.Text = "AutoBázis";
            ((System.ComponentModel.ISupportInitialize)(this.autoData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView autoData;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button logoutButton;
        private TextBox autoNameText;
        private TextBox AutoTypeText;
        private TextBox AutoPriceText;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox loginPassword;
        private TextBox loginUsername;
        private Button loginButton;
        private Label regUsernameLabel;
        private Label regPasswordLabel;
        private Button regButton;
        private TextBox registerName;
        private TextBox registerPassword;
        private TextBox IdText;
        private Label label8;
        private LinkLabel regLink;
    }
}