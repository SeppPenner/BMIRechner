namespace BMIRechner
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button_Result = new System.Windows.Forms.Button();
            this.numericUpDown_Size = new System.Windows.Forms.NumericUpDown();
            this.label_Size = new System.Windows.Forms.Label();
            this.label_Weight = new System.Windows.Forms.Label();
            this.numericUpDown_Weight = new System.Windows.Forms.NumericUpDown();
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.label_Result = new System.Windows.Forms.Label();
            this.textBox_ResultText = new System.Windows.Forms.TextBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Weight)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Result
            // 
            this.button_Result.Location = new System.Drawing.Point(3, 143);
            this.button_Result.Name = "button_Result";
            this.button_Result.Size = new System.Drawing.Size(135, 25);
            this.button_Result.TabIndex = 0;
            this.button_Result.Text = "Berechne BMI";
            this.button_Result.UseVisualStyleBackColor = true;
            this.button_Result.Click += new System.EventHandler(this.button_Result_Click);
            // 
            // numericUpDown_Size
            // 
            this.numericUpDown_Size.Location = new System.Drawing.Point(3, 38);
            this.numericUpDown_Size.Maximum = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.numericUpDown_Size.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Size.Name = "numericUpDown_Size";
            this.numericUpDown_Size.Size = new System.Drawing.Size(135, 20);
            this.numericUpDown_Size.TabIndex = 1;
            this.numericUpDown_Size.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label_Size
            // 
            this.label_Size.AutoSize = true;
            this.label_Size.Location = new System.Drawing.Point(3, 0);
            this.label_Size.Name = "label_Size";
            this.label_Size.Size = new System.Drawing.Size(67, 13);
            this.label_Size.TabIndex = 2;
            this.label_Size.Text = "Größe in cm:";
            // 
            // label_Weight
            // 
            this.label_Weight.AutoSize = true;
            this.label_Weight.Location = new System.Drawing.Point(3, 70);
            this.label_Weight.Name = "label_Weight";
            this.label_Weight.Size = new System.Drawing.Size(75, 13);
            this.label_Weight.TabIndex = 3;
            this.label_Weight.Text = "Gewicht in kg:";
            // 
            // numericUpDown_Weight
            // 
            this.numericUpDown_Weight.DecimalPlaces = 1;
            this.numericUpDown_Weight.Location = new System.Drawing.Point(3, 108);
            this.numericUpDown_Weight.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDown_Weight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Weight.Name = "numericUpDown_Weight";
            this.numericUpDown_Weight.Size = new System.Drawing.Size(135, 20);
            this.numericUpDown_Weight.TabIndex = 4;
            this.numericUpDown_Weight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox_Result
            // 
            this.textBox_Result.Location = new System.Drawing.Point(190, 38);
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.ReadOnly = true;
            this.textBox_Result.Size = new System.Drawing.Size(150, 20);
            this.textBox_Result.TabIndex = 5;
            // 
            // label_Result
            // 
            this.label_Result.AutoSize = true;
            this.label_Result.Location = new System.Drawing.Point(190, 0);
            this.label_Result.Name = "label_Result";
            this.label_Result.Size = new System.Drawing.Size(51, 13);
            this.label_Result.TabIndex = 6;
            this.label_Result.Text = "Ergebnis:";
            // 
            // textBox_ResultText
            // 
            this.textBox_ResultText.Location = new System.Drawing.Point(190, 73);
            this.textBox_ResultText.Name = "textBox_ResultText";
            this.textBox_ResultText.ReadOnly = true;
            this.textBox_ResultText.Size = new System.Drawing.Size(150, 20);
            this.textBox_ResultText.TabIndex = 8;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(377, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(144, 21);
            this.comboBoxLanguage.TabIndex = 9;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.label_Size, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.textBox_ResultText, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxLanguage, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.numericUpDown_Size, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.textBox_Result, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.label_Weight, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.numericUpDown_Weight, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.label_Result, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.button_Result, 0, 4);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 5;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(562, 177);
            this.tableLayoutPanelMain.TabIndex = 10;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 177);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "BMI Rechner";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Size)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Weight)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Result;
        private System.Windows.Forms.NumericUpDown numericUpDown_Size;
        private System.Windows.Forms.Label label_Size;
        private System.Windows.Forms.Label label_Weight;
        private System.Windows.Forms.NumericUpDown numericUpDown_Weight;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.Label label_Result;
        private System.Windows.Forms.TextBox textBox_ResultText;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
    }
}

