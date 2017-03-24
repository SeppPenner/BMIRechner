using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using BMIRechner.Properties;
using Languages.Implementation;
using Languages.Interfaces;

namespace BMIRechner
{
    public partial class Main : Form
    {
        private readonly ILanguageManager _lm = new LanguageManager();
        private double _result;

        public Main()
        {
            InitializeComponent();
            InitializeLanguageManager();
            LoadLanguagesToCombo();
            LoadTitleAndDescription();
        }

        private void LoadTitleAndDescription()
        {
            Text = Application.ProductName + Resources.Empty + Application.ProductVersion;
        }

        private void button_Result_Click(object sender, EventArgs e)
        {
            var mass = Convert.ToDouble(numericUpDown_Weight.Value);
            var length = Convert.ToDouble(numericUpDown_Size.Value);
            _result = Math.Round(mass / Math.Pow(length / 100, 2), 2);
            textBox_Result.Text = _result.ToString(CultureInfo.InvariantCulture);
            var color = DetermineColor(_result);
            textBox_Result.BackColor = color;
            textBox_ResultText.BackColor = color;
            CheckColor(color);
        }

        private void CheckColor(Color color)
        {
            if (color.Equals(Color.Yellow))
            {
                textBox_Result.ForeColor = Color.Black;
                textBox_ResultText.ForeColor = Color.Black;
            }
            else
            {
                textBox_Result.ForeColor = Color.White;
                textBox_ResultText.ForeColor = Color.White;
            }
        }

        private Color DetermineColor(double bmi)
        {
            if (bmi < 16)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("ExtremeUnderweight");
                return Color.DarkBlue;
            }
            if (bmi < 17)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("Underweight");
                return Color.Blue;
            }
            if (bmi < 18.5)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("LightUnderweight");
                return Color.CadetBlue;
            }
            if (bmi < 25)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("NormalWeight");
                return Color.Green;
            }
            if (bmi < 30)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("PreAdiposity");
                return Color.Yellow;
            }
            if (bmi < 35)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("AdiposityGrade1");
                return Color.Orange;
            }
            if (bmi < 40)
            {
                textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("AdiposityGrade2");
                return Color.Red;
            }
            textBox_ResultText.Text = _lm.GetCurrentLanguage().GetWord("AdiposityGrade3");
            return Color.DarkRed;
        }


        private void InitializeLanguageManager()
        {
            _lm.SetCurrentLanguage("de-DE");
            _lm.OnLanguageChanged += OnLanguageChanged;
        }

        private void LoadLanguagesToCombo()
        {
            foreach (var lang in _lm.GetLanguages())
                comboBoxLanguage.Items.Add(lang.Name);
            comboBoxLanguage.SelectedIndex = 0;
        }

        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _lm.SetCurrentLanguageFromName(comboBoxLanguage.SelectedItem.ToString());
        }

        private void OnLanguageChanged(object sender, EventArgs eventArgs)
        {
            Text = _lm.GetCurrentLanguage().GetWord("Title") + Resources.Empty + Application.ProductVersion;
            label_Size.Text = _lm.GetCurrentLanguage().GetWord("Size");
            label_Weight.Text = _lm.GetCurrentLanguage().GetWord("Weight");
            label_Result.Text = _lm.GetCurrentLanguage().GetWord("Result");
            button_Result.Text = _lm.GetCurrentLanguage().GetWord("Calculate");
            button_Result_Click(sender, eventArgs);
        }
    }
}