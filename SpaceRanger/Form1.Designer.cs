namespace SpaceRanger
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBattleSpace = new System.Windows.Forms.Panel();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelCounter = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(351, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.newGameToolStripMenuItem.Text = "New game";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // panelBattleSpace
            // 
            this.panelBattleSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this.panelBattleSpace.Location = new System.Drawing.Point(12, 68);
            this.panelBattleSpace.Name = "panelBattleSpace";
            this.panelBattleSpace.Size = new System.Drawing.Size(326, 445);
            this.panelBattleSpace.TabIndex = 1;
            this.panelBattleSpace.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBattleSpace_Paint);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScore.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelScore.Location = new System.Drawing.Point(12, 36);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(55, 20);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "Score:";
            // 
            // labelCounter
            // 
            this.labelCounter.AutoSize = true;
            this.labelCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelCounter.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelCounter.Location = new System.Drawing.Point(288, 36);
            this.labelCounter.Name = "labelCounter";
            this.labelCounter.Size = new System.Drawing.Size(18, 20);
            this.labelCounter.TabIndex = 2;
            this.labelCounter.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(47)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(351, 525);
            this.Controls.Add(this.labelCounter);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.panelBattleSpace);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Space Ranger";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panelBattleSpace;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelCounter;
    }
}

