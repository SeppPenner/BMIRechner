// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BmiCalculator.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to calculate the body mass index and to describe the result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BMIRechner;

/// <summary>
/// A class to calculate the body mass index and to describe the result. It holds no user interface code, so that
/// the calculation can be tested without a form.
/// </summary>
public static class BmiCalculator
{
    /// <summary>
    /// Calculates the body mass index.
    /// </summary>
    /// <param name="massInKilogram">The mass in kilogram.</param>
    /// <param name="sizeInCentimeter">The size in centimeter.</param>
    /// <returns>The body mass index, rounded to two decimals.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the mass or the size is zero or negative. The numeric up downs of the form start at 1, so this
    /// cannot happen from the user interface.
    /// </exception>
    public static double Calculate(double massInKilogram, double sizeInCentimeter)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(massInKilogram);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(sizeInCentimeter);
        return Math.Round(massInKilogram / Math.Pow(sizeInCentimeter / 100, 2), 2);
    }

    /// <summary>
    /// Determines the category of the given body mass index. The bounds are the World Health Organization bands and
    /// every one of them is exclusive.
    /// </summary>
    /// <param name="bmi">The body mass index.</param>
    /// <returns>The corresponding <see cref="BmiCategory"/>.</returns>
    public static BmiCategory DetermineCategory(double bmi)
    {
        return bmi switch
        {
            < 16 => BmiCategory.ExtremeUnderweight,
            < 17 => BmiCategory.Underweight,
            < 18.5 => BmiCategory.LightUnderweight,
            < 25 => BmiCategory.NormalWeight,
            < 30 => BmiCategory.PreAdiposity,
            < 35 => BmiCategory.AdiposityGrade1,
            < 40 => BmiCategory.AdiposityGrade2,
            _ => BmiCategory.AdiposityGrade3
        };
    }

    /// <summary>
    /// Gets the key of the word that describes the given category in the language files.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <returns>The key of the word in the language files.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the category is not a known one.</exception>
    public static string GetLanguageKey(BmiCategory category)
    {
        return category switch
        {
            BmiCategory.ExtremeUnderweight => "ExtremeUnderweight",
            BmiCategory.Underweight => "Underweight",
            BmiCategory.LightUnderweight => "LightUnderweight",
            BmiCategory.NormalWeight => "NormalWeight",
            BmiCategory.PreAdiposity => "PreAdiposity",
            BmiCategory.AdiposityGrade1 => "AdiposityGrade1",
            BmiCategory.AdiposityGrade2 => "AdiposityGrade2",
            BmiCategory.AdiposityGrade3 => "AdiposityGrade3",
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, "The body mass index category is unknown.")
        };
    }
}
