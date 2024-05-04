namespace Course_work
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
            this.components = new System.ComponentModel.Container();
            this.timerBG = new System.Windows.Forms.Timer(this.components);
            this.ground = new System.Windows.Forms.PictureBox();
            this.ceiling = new System.Windows.Forms.PictureBox();
            this.Player = new System.Windows.Forms.PictureBox();
            this.BG = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceiling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BG)).BeginInit();
            this.SuspendLayout();
            // 
            // timerBG
            // 
            this.timerBG.Interval = 1000;
            // 
            // ground
            // 
            this.ground.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ground.Location = new System.Drawing.Point(0, 650);
            this.ground.Margin = new System.Windows.Forms.Padding(0);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(1300, 50);
            this.ground.TabIndex = 1;
            this.ground.TabStop = false;
            // 
            // ceiling
            // 
            this.ceiling.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ceiling.Location = new System.Drawing.Point(0, 0);
            this.ceiling.Margin = new System.Windows.Forms.Padding(0);
            this.ceiling.Name = "ceiling";
            this.ceiling.Size = new System.Drawing.Size(1300, 50);
            this.ceiling.TabIndex = 2;
            this.ceiling.TabStop = false;
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Player.Location = new System.Drawing.Point(183, 597);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(100, 50);
            this.Player.TabIndex = 3;
            this.Player.TabStop = false;
            // 
            // BG
            // 
            this.BG.Location = new System.Drawing.Point(372, 189);
            this.BG.Name = "BG";
            this.BG.Size = new System.Drawing.Size(100, 50);
            this.BG.TabIndex = 4;
            this.BG.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.BG);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.ceiling);
            this.Controls.Add(this.ground);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ceiling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BG)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerBG;
        private System.Windows.Forms.PictureBox ground;
        private System.Windows.Forms.PictureBox ceiling;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.PictureBox BG;
    }
}

