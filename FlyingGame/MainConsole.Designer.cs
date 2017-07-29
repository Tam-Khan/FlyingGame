namespace FlyingGame
{
    partial class MainConsole
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
            this.PbConsole = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LblScore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblBombRem = new System.Windows.Forms.Label();
            this.LblStartEndBanner = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LblFireLimit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LblFireLine = new System.Windows.Forms.Label();
            this.LblBoss = new System.Windows.Forms.Label();
            this.LblBossRem = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblJetHp = new System.Windows.Forms.Label();
            this.LblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PbConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // PbConsole
            // 
            this.PbConsole.BackColor = System.Drawing.Color.SkyBlue;
            this.PbConsole.Location = new System.Drawing.Point(0, 0);
            this.PbConsole.Name = "PbConsole";
            this.PbConsole.Size = new System.Drawing.Size(816, 352);
            this.PbConsole.TabIndex = 0;
            this.PbConsole.TabStop = false;
            // 
            // LblScore
            // 
            this.LblScore.AutoSize = true;
            this.LblScore.BackColor = System.Drawing.Color.Silver;
            this.LblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblScore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.LblScore.Location = new System.Drawing.Point(904, 10);
            this.LblScore.Name = "LblScore";
            this.LblScore.Size = new System.Drawing.Size(82, 29);
            this.LblScore.TabIndex = 1;
            this.LblScore.Text = "Score";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(822, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Score:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(819, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bomb:";
            // 
            // LblBombRem
            // 
            this.LblBombRem.AutoSize = true;
            this.LblBombRem.BackColor = System.Drawing.Color.Silver;
            this.LblBombRem.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBombRem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.LblBombRem.Location = new System.Drawing.Point(938, 149);
            this.LblBombRem.Name = "LblBombRem";
            this.LblBombRem.Size = new System.Drawing.Size(27, 29);
            this.LblBombRem.TabIndex = 3;
            this.LblBombRem.Text = "5";
            // 
            // LblStartEndBanner
            // 
            this.LblStartEndBanner.AutoSize = true;
            this.LblStartEndBanner.BackColor = System.Drawing.Color.White;
            this.LblStartEndBanner.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblStartEndBanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LblStartEndBanner.Location = new System.Drawing.Point(139, 146);
            this.LblStartEndBanner.Name = "LblStartEndBanner";
            this.LblStartEndBanner.Size = new System.Drawing.Size(146, 32);
            this.LblStartEndBanner.TabIndex = 5;
            this.LblStartEndBanner.Text = "Shootemup";
            this.LblStartEndBanner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblStartEndBanner.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(819, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Bullet Limit";
            // 
            // LblFireLimit
            // 
            this.LblFireLimit.AutoSize = true;
            this.LblFireLimit.BackColor = System.Drawing.Color.Silver;
            this.LblFireLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFireLimit.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblFireLimit.Location = new System.Drawing.Point(938, 191);
            this.LblFireLimit.Name = "LblFireLimit";
            this.LblFireLimit.Size = new System.Drawing.Size(41, 29);
            this.LblFireLimit.TabIndex = 6;
            this.LblFireLimit.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Indigo;
            this.label5.Location = new System.Drawing.Point(819, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Active Gun";
            // 
            // LblFireLine
            // 
            this.LblFireLine.AutoSize = true;
            this.LblFireLine.BackColor = System.Drawing.Color.Silver;
            this.LblFireLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFireLine.ForeColor = System.Drawing.Color.Indigo;
            this.LblFireLine.Location = new System.Drawing.Point(938, 231);
            this.LblFireLine.Name = "LblFireLine";
            this.LblFireLine.Size = new System.Drawing.Size(41, 29);
            this.LblFireLine.TabIndex = 8;
            this.LblFireLine.Text = "10";
            // 
            // LblBoss
            // 
            this.LblBoss.AutoSize = true;
            this.LblBoss.BackColor = System.Drawing.Color.White;
            this.LblBoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBoss.ForeColor = System.Drawing.Color.Red;
            this.LblBoss.Location = new System.Drawing.Point(826, 298);
            this.LblBoss.Name = "LblBoss";
            this.LblBoss.Size = new System.Drawing.Size(80, 25);
            this.LblBoss.TabIndex = 11;
            this.LblBoss.Text = "BOSS:";
            this.LblBoss.Visible = false;
            // 
            // LblBossRem
            // 
            this.LblBossRem.AutoSize = true;
            this.LblBossRem.BackColor = System.Drawing.Color.White;
            this.LblBossRem.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBossRem.ForeColor = System.Drawing.Color.Red;
            this.LblBossRem.Location = new System.Drawing.Point(826, 323);
            this.LblBossRem.Name = "LblBossRem";
            this.LblBossRem.Size = new System.Drawing.Size(72, 29);
            this.LblBossRem.TabIndex = 10;
            this.LblBossRem.Text = "Fight";
            this.LblBossRem.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(819, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "HP";
            // 
            // LblJetHp
            // 
            this.LblJetHp.AutoSize = true;
            this.LblJetHp.BackColor = System.Drawing.Color.Silver;
            this.LblJetHp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblJetHp.ForeColor = System.Drawing.Color.Blue;
            this.LblJetHp.Location = new System.Drawing.Point(938, 106);
            this.LblJetHp.Name = "LblJetHp";
            this.LblJetHp.Size = new System.Drawing.Size(27, 29);
            this.LblJetHp.TabIndex = 12;
            this.LblJetHp.Text = "5";
            // 
            // LblInfo
            // 
            this.LblInfo.AutoSize = true;
            this.LblInfo.BackColor = System.Drawing.Color.White;
            this.LblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblInfo.ForeColor = System.Drawing.Color.SteelBlue;
            this.LblInfo.Location = new System.Drawing.Point(4, 367);
            this.LblInfo.Name = "LblInfo";
            this.LblInfo.Size = new System.Drawing.Size(414, 20);
            this.LblInfo.TabIndex = 14;
            this.LblInfo.Text = "Use Arrow Buttons to move, A for Fire, D for Bomb";
            // 
            // MainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 427);
            this.Controls.Add(this.LblInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LblJetHp);
            this.Controls.Add(this.LblBoss);
            this.Controls.Add(this.LblBossRem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblFireLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblFireLimit);
            this.Controls.Add(this.LblStartEndBanner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblBombRem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblScore);
            this.Controls.Add(this.PbConsole);
            this.Cursor = System.Windows.Forms.Cursors.No;
            this.KeyPreview = true;
            this.Name = "MainConsole";
            this.Text = "Game Console.";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainConsole_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainConsole_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PbConsole)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PbConsole;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label LblScore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblBombRem;
        private System.Windows.Forms.Label LblStartEndBanner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblFireLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LblFireLine;
        private System.Windows.Forms.Label LblBoss;
        private System.Windows.Forms.Label LblBossRem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblJetHp;
        private System.Windows.Forms.Label LblInfo;


    }
}

