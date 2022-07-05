
namespace DbInteractionPet
{
    partial class MainMenu
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
            this.AutomobilesRouter_button = new System.Windows.Forms.Button();
            this.MechanicsRouter_button = new System.Windows.Forms.Button();
            this.RepairOrdersRouter_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AutomobilesRouter_button
            // 
            this.AutomobilesRouter_button.BackColor = System.Drawing.Color.IndianRed;
            this.AutomobilesRouter_button.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.AutomobilesRouter_button.Location = new System.Drawing.Point(0, 0);
            this.AutomobilesRouter_button.Name = "AutomobilesRouter_button";
            this.AutomobilesRouter_button.Size = new System.Drawing.Size(339, 618);
            this.AutomobilesRouter_button.TabIndex = 0;
            this.AutomobilesRouter_button.Text = "Автомобили";
            this.AutomobilesRouter_button.UseVisualStyleBackColor = false;
            this.AutomobilesRouter_button.Click += new System.EventHandler(this.button_Click);
            // 
            // MechanicsRouter_button
            // 
            this.MechanicsRouter_button.BackColor = System.Drawing.Color.LightSkyBlue;
            this.MechanicsRouter_button.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.MechanicsRouter_button.Location = new System.Drawing.Point(337, 0);
            this.MechanicsRouter_button.Name = "MechanicsRouter_button";
            this.MechanicsRouter_button.Size = new System.Drawing.Size(374, 618);
            this.MechanicsRouter_button.TabIndex = 1;
            this.MechanicsRouter_button.Text = "Механики";
            this.MechanicsRouter_button.UseVisualStyleBackColor = false;
            this.MechanicsRouter_button.Click += new System.EventHandler(this.button_Click);
            // 
            // RepairOrdersRouter_button
            // 
            this.RepairOrdersRouter_button.BackColor = System.Drawing.Color.LightGreen;
            this.RepairOrdersRouter_button.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.RepairOrdersRouter_button.Location = new System.Drawing.Point(708, 0);
            this.RepairOrdersRouter_button.Name = "RepairOrdersRouter_button";
            this.RepairOrdersRouter_button.Size = new System.Drawing.Size(374, 618);
            this.RepairOrdersRouter_button.TabIndex = 2;
            this.RepairOrdersRouter_button.Text = "Заказы";
            this.RepairOrdersRouter_button.UseVisualStyleBackColor = false;
            this.RepairOrdersRouter_button.Click += new System.EventHandler(this.button_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 619);
            this.Controls.Add(this.RepairOrdersRouter_button);
            this.Controls.Add(this.MechanicsRouter_button);
            this.Controls.Add(this.AutomobilesRouter_button);
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AutomobilesRouter_button;
        private System.Windows.Forms.Button MechanicsRouter_button;
        private System.Windows.Forms.Button RepairOrdersRouter_button;
    }
}

