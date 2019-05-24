namespace TowerDefense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.lblMoney = new System.Windows.Forms.Label();
            this.lblMoneyVal = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.pbBasicTower = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblRound = new System.Windows.Forms.Label();
            this.lblRoundVal = new System.Windows.Forms.Label();
            this.lblBasicTowerPrice = new System.Windows.Forms.Label();
            this.lblGameOver = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasicTower)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.Gray;
            this.pbCanvas.Image = global::TowerDefense.Properties.Resources.Forest_Map;
            this.pbCanvas.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbCanvas.InitialImage")));
            this.pbCanvas.Location = new System.Drawing.Point(13, 13);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(500, 500);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.updateGraphics);
            // 
            // lblMoney
            // 
            this.lblMoney.AutoSize = true;
            this.lblMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoney.Location = new System.Drawing.Point(524, 35);
            this.lblMoney.Name = "lblMoney";
            this.lblMoney.Size = new System.Drawing.Size(79, 24);
            this.lblMoney.TabIndex = 1;
            this.lblMoney.Text = "Money:";
            // 
            // lblMoneyVal
            // 
            this.lblMoneyVal.AutoSize = true;
            this.lblMoneyVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneyVal.Location = new System.Drawing.Point(605, 35);
            this.lblMoneyVal.Name = "lblMoneyVal";
            this.lblMoneyVal.Size = new System.Drawing.Size(32, 24);
            this.lblMoneyVal.TabIndex = 2;
            this.lblMoneyVal.Text = "00";
            // 
            // pbBasicTower
            // 
            this.pbBasicTower.BackColor = System.Drawing.Color.Transparent;
            this.pbBasicTower.Image = ((System.Drawing.Image)(resources.GetObject("pbBasicTower.Image")));
            this.pbBasicTower.Location = new System.Drawing.Point(529, 75);
            this.pbBasicTower.Name = "pbBasicTower";
            this.pbBasicTower.Size = new System.Drawing.Size(50, 50);
            this.pbBasicTower.TabIndex = 7;
            this.pbBasicTower.TabStop = false;
            this.pbBasicTower.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbBasicTower_MouseDown);
            this.pbBasicTower.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbBasicTower_MouseMove);
            this.pbBasicTower.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbBasicTower_MouseUp);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(538, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 50);
            this.button1.TabIndex = 8;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseClick);
            // 
            // lblRound
            // 
            this.lblRound.AutoSize = true;
            this.lblRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRound.Location = new System.Drawing.Point(525, 11);
            this.lblRound.Name = "lblRound";
            this.lblRound.Size = new System.Drawing.Size(78, 24);
            this.lblRound.TabIndex = 9;
            this.lblRound.Text = "Round:";
            // 
            // lblRoundVal
            // 
            this.lblRoundVal.AutoSize = true;
            this.lblRoundVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoundVal.Location = new System.Drawing.Point(605, 11);
            this.lblRoundVal.Name = "lblRoundVal";
            this.lblRoundVal.Size = new System.Drawing.Size(32, 24);
            this.lblRoundVal.TabIndex = 10;
            this.lblRoundVal.Text = "00";
            // 
            // lblBasicTowerPrice
            // 
            this.lblBasicTowerPrice.AutoSize = true;
            this.lblBasicTowerPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicTowerPrice.Location = new System.Drawing.Point(532, 124);
            this.lblBasicTowerPrice.Name = "lblBasicTowerPrice";
            this.lblBasicTowerPrice.Size = new System.Drawing.Size(44, 17);
            this.lblBasicTowerPrice.TabIndex = 11;
            this.lblBasicTowerPrice.Text = "$450";
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.BackColor = System.Drawing.Color.DarkOrange;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.Location = new System.Drawing.Point(225, 225);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(116, 24);
            this.lblGameOver.TabIndex = 12;
            this.lblGameOver.Text = "Game Over";
            this.lblGameOver.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 535);
            this.Controls.Add(this.lblGameOver);
            this.Controls.Add(this.lblBasicTowerPrice);
            this.Controls.Add(this.lblRoundVal);
            this.Controls.Add(this.lblRound);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbBasicTower);
            this.Controls.Add(this.lblMoneyVal);
            this.Controls.Add(this.lblMoney);
            this.Controls.Add(this.pbCanvas);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBasicTower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.Label lblMoney;
        private System.Windows.Forms.Label lblMoneyVal;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.PictureBox pbBasicTower;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblRound;
        private System.Windows.Forms.Label lblRoundVal;
        private System.Windows.Forms.Label lblBasicTowerPrice;
        private System.Windows.Forms.Label lblGameOver;
    }
}

