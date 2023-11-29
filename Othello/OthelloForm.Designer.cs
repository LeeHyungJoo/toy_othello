namespace Othello
{
    partial class OthelloForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OthelloForm));
            groupBox1 = new GroupBox();
            lb_blackScore = new Label();
            lb_whiteScore = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lb_whiteScore);
            groupBox1.Controls.Add(lb_blackScore);
            groupBox1.Location = new Point(264, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(60, 72);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // lb_blackScore
            // 
            lb_blackScore.AutoSize = true;
            lb_blackScore.BackColor = SystemColors.ControlDarkDark;
            lb_blackScore.ForeColor = SystemColors.Control;
            lb_blackScore.Location = new Point(6, 19);
            lb_blackScore.Name = "lb_blackScore";
            lb_blackScore.Size = new Size(39, 15);
            lb_blackScore.TabIndex = 0;
            lb_blackScore.Text = "label1";
            // 
            // lb_whiteScore
            // 
            lb_whiteScore.AutoSize = true;
            lb_whiteScore.BackColor = SystemColors.ControlLightLight;
            lb_whiteScore.Location = new Point(6, 44);
            lb_whiteScore.Name = "lb_whiteScore";
            lb_whiteScore.Size = new Size(39, 15);
            lb_whiteScore.TabIndex = 1;
            lb_whiteScore.Text = "label2";
            // 
            // OthelloForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 256);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "OthelloForm";
            Text = "Othello";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label lb_whiteScore;
        private Label lb_blackScore;
    }
}