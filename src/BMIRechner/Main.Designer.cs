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
            this.ButtonResult = new System.Windows.Forms.Button();
            this.numericUpDown_Size = new System.Windows.Forms.NumericUpDown();
            this.LabelSize = new System.Windows.Forms.Label();
            this.LabelWeight = new System.Windows.Forms.Label();
            this.numericUpDown_Weight = new System.Windows.Forms.NumericUpDown();
            this.TextBoxResult = new System.Windows.Forms.TextBox();
            this.LabelResult = new System.Windows.Forms.Label();
            this.TextBoxResultText = new System.Windows.Forms.TextBox();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Size)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Weight)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonResult
            // 
            this.ButtonResult.Location = new System.Drawing.Point(3, 143);
            this.ButtonResult.Name = "ButtonResult";
            this.ButtonResult.Size = new System.Drawing.Size(135, 25);
            this.ButtonResult.TabIndex = 0;
            this.ButtonResult.Text = "Berechne BMI";
            this.ButtonResult.UseVisualStyleBackColor = true;
            this.ButtonResult.Click += new System.EventHandler(this.ButtonResultClick);
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
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.LabelSize.Location = new System.Drawing.Point(3, 0);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(67, 13);
            this.LabelSize.TabIndex = 2;
            this.LabelSize.Text = "Größe in cm:";
            // 
            // LabelWeight
            // 
            this.LabelWeight.AutoSize = true;
            this.LabelWeight.Location = new System.Drawing.Point(3, 70);
            this.LabelWeight.Name = "LabelWeight";
            this.LabelWeight.Size = new System.Drawing.Size(75, 13);
            this.LabelWeight.TabIndex = 3;
            this.LabelWeight.Text = "Gewicht in kg:";
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
            // TextBoxResult
            // 
            this.TextBoxResult.Location = new System.Drawing.Point(190, 38);
            this.TextBoxResult.Name = "TextBoxResult";
            this.TextBoxResult.ReadOnly = true;
            this.TextBoxResult.Size = new System.Drawing.Size(150, 20);
            this.TextBoxResult.TabIndex = 5;
            // 
            // LabelResult
            // 
            this.LabelResult.AutoSize = true;
            this.LabelResult.Location = new System.Drawing.Point(190, 0);
            this.LabelResult.Name = "LabelResult";
            this.LabelResult.Size = new System.Drawing.Size(51, 13);
            this.LabelResult.TabIndex = 6;
            this.LabelResult.Text = "Ergebnis:";
            // 
            // TextBoxResultText
            // 
            this.TextBoxResultText.Location = new System.Drawing.Point(190, 73);
            this.TextBoxResultText.Name = "TextBoxResultText";
            this.TextBoxResultText.ReadOnly = true;
            this.TextBoxResultText.Size = new System.Drawing.Size(150, 20);
            this.TextBoxResultText.TabIndex = 8;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(377, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(144, 21);
            this.comboBoxLanguage.TabIndex = 9;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLanguageSelectedIndexChanged);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.LabelSize, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.TextBoxResultText, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.comboBoxLanguage, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.numericUpDown_Size, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.TextBoxResult, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.LabelWeight, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.numericUpDown_Weight, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.LabelResult, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.ButtonResult, 0, 4);
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

        private System.Windows.Forms.Button ButtonResult;
        private System.Windows.Forms.NumericUpDown numericUpDown_Size;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.Label LabelWeight;
        private System.Windows.Forms.NumericUpDown numericUpDown_Weight;
        private System.Windows.Forms.TextBox TextBoxResult;
        private System.Windows.Forms.Label LabelResult;
        private System.Windows.Forms.TextBox TextBoxResultText;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
    }
}

