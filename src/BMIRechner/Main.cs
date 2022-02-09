// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BMIRechner;

/// <summary>
/// The main form.
/// </summary>
public partial class Main : Form
{
    /// <summary>
    /// The language manager.
    /// </summary>
    private readonly ILanguageManager languageManager = new LanguageManager();

    /// <summary>
    /// The result.
    /// </summary>
    private double result;

    /// <summary>
    /// Initializes a new instance of the <see cref="Main"/> class.
    /// </summary>
    public Main()
    {
        this.InitializeComponent();
        this.InitializeLanguageManager();
        this.LoadLanguagesToCombo();
        this.LoadTitleAndDescription();
    }

    /// <summary>
    /// Loads the title and description.
    /// </summary>
    private void LoadTitleAndDescription()
    {
        this.Text = Application.ProductName + string.Empty + Application.ProductVersion;
    }

    /// <summary>
    /// Handles the result button click event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ButtonResultClick(object sender, EventArgs e)
    {
        var mass = Convert.ToDouble(this.numericUpDown_Weight.Value);
        var length = Convert.ToDouble(this.numericUpDown_Size.Value);
        this.result = Math.Round(mass / Math.Pow(length / 100, 2), 2);
        this.TextBoxResult.Text = this.result.ToString(CultureInfo.InvariantCulture);
        var color = this.DetermineColor(this.result);
        this.TextBoxResult.BackColor = color;
        this.TextBoxResultText.BackColor = color;
        this.CheckColor(color);
    }

    /// <summary>
    /// Checks the color.
    /// </summary>
    /// <param name="color">The color.</param>
    private void CheckColor(Color color)
    {
        if (color.Equals(Color.Yellow))
        {
            this.TextBoxResult.ForeColor = Color.Black;
            this.TextBoxResultText.ForeColor = Color.Black;
        }
        else
        {
            this.TextBoxResult.ForeColor = Color.White;
            this.TextBoxResultText.ForeColor = Color.White;
        }
    }

    /// <summary>
    /// Determines the color.
    /// </summary>
    /// <param name="bmi">The BMI.</param>
    /// <returns>The corresponding color.</returns>
    private Color DetermineColor(double bmi)
    {
        if (bmi < 16)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("ExtremeUnderweight");
            return Color.DarkBlue;
        }

        if (bmi < 17)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("Underweight");
            return Color.Blue;
        }

        if (bmi < 18.5)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("LightUnderweight");
            return Color.CadetBlue;
        }

        if (bmi < 25)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("NormalWeight");
            return Color.Green;
        }

        if (bmi < 30)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("PreAdiposity");
            return Color.Yellow;
        }

        if (bmi < 35)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("AdiposityGrade1");
            return Color.Orange;
        }

        if (bmi < 40)
        {
            this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("AdiposityGrade2");
            return Color.Red;
        }

        this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord("AdiposityGrade3");
        return Color.DarkRed;
    }


    /// <summary>
    /// Initializes the language manager.
    /// </summary>
    private void InitializeLanguageManager()
    {
        this.languageManager.SetCurrentLanguage("de-DE");
        this.languageManager.OnLanguageChanged += this.OnLanguageChanged;
    }

    /// <summary>
    /// Loads the languages to the combo box.
    /// </summary>
    private void LoadLanguagesToCombo()
    {
        foreach (var lang in this.languageManager.GetLanguages())
        {
            this.comboBoxLanguage.Items.Add(lang.Name);
        }

        this.comboBoxLanguage.SelectedIndex = 0;
    }

    /// <summary>
    /// Handles the selected index changed event for the language combo box.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void ComboBoxLanguageSelectedIndexChanged(object sender, EventArgs e)
    {
        this.languageManager.SetCurrentLanguageFromName(this.comboBoxLanguage.SelectedItem.ToString());
    }

    /// <summary>
    /// Handles the on language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    private void OnLanguageChanged(object sender, EventArgs eventArgs)
    {
        this.Text = this.languageManager.GetCurrentLanguage().GetWord("Title") + string.Empty + Application.ProductVersion;
        this.LabelSize.Text = this.languageManager.GetCurrentLanguage().GetWord("Size");
        this.LabelWeight.Text = this.languageManager.GetCurrentLanguage().GetWord("Weight");
        this.LabelResult.Text = this.languageManager.GetCurrentLanguage().GetWord("Result");
        this.ButtonResult.Text = this.languageManager.GetCurrentLanguage().GetWord("Calculate");
        this.ButtonResultClick(sender, eventArgs);
    }
}
