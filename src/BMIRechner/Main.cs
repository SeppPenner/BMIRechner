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

        // The language changed event is subscribed after the combo box is filled on purpose. Setting the selected
        // index raises it, and that would calculate a body mass index from the default values before any input.
        this.languageManager.OnLanguageChanged += this.OnLanguageChanged!;
        this.LoadTitle();
    }

    /// <summary>
    /// Loads the window title from the current language and the version of the executable.
    /// </summary>
    private void LoadTitle()
    {
        this.Text = $"{this.languageManager.GetCurrentLanguage().GetWord("Title")} {Application.ProductVersion}";
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
        this.result = BmiCalculator.Calculate(mass, length);
        this.TextBoxResult.Text = this.result.ToString(CultureInfo.InvariantCulture);
        var category = BmiCalculator.DetermineCategory(this.result);
        this.TextBoxResultText.Text = this.languageManager.GetCurrentLanguage().GetWord(BmiCalculator.GetLanguageKey(category));
        var color = DetermineColor(category);
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
    /// Determines the color of the result boxes.
    /// </summary>
    /// <param name="category">The body mass index category.</param>
    /// <returns>The corresponding color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the category is not a known one.</exception>
    private static Color DetermineColor(BmiCategory category)
    {
        return category switch
        {
            BmiCategory.ExtremeUnderweight => Color.DarkBlue,
            BmiCategory.Underweight => Color.Blue,
            BmiCategory.LightUnderweight => Color.CadetBlue,
            BmiCategory.NormalWeight => Color.Green,
            BmiCategory.PreAdiposity => Color.Yellow,
            BmiCategory.AdiposityGrade1 => Color.Orange,
            BmiCategory.AdiposityGrade2 => Color.Red,
            BmiCategory.AdiposityGrade3 => Color.DarkRed,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, "The body mass index category is unknown.")
        };
    }

    /// <summary>
    /// Initializes the language manager with the language that is shown at startup.
    /// </summary>
    private void InitializeLanguageManager()
    {
        this.languageManager.SetCurrentLanguage("de-DE");
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
        this.languageManager.SetCurrentLanguageFromName(this.comboBoxLanguage.SelectedItem?.ToString() ?? string.Empty);
    }

    /// <summary>
    /// Handles the on language changed event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="eventArgs">The event args.</param>
    private void OnLanguageChanged(object sender, EventArgs eventArgs)
    {
        this.LoadTitle();
        this.LabelSize.Text = this.languageManager.GetCurrentLanguage().GetWord("Size");
        this.LabelWeight.Text = this.languageManager.GetCurrentLanguage().GetWord("Weight");
        this.LabelResult.Text = this.languageManager.GetCurrentLanguage().GetWord("Result");
        this.ButtonResult.Text = this.languageManager.GetCurrentLanguage().GetWord("Calculate");
        this.ButtonResultClick(sender, eventArgs);
    }
}
