// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BmiCalculatorTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to test the <see cref="BmiCalculator" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BMIRechner.Tests;

/// <summary>
/// A class to test the <see cref="BmiCalculator"/> class.
/// </summary>
[TestClass]
public class BmiCalculatorTests
{
    /// <summary>
    /// Checks whether the body mass index is the mass divided by the square of the size in meters, rounded to two
    /// decimals. 78 kg and 180 cm are the values of the screenshots in the readme file.
    /// </summary>
    [TestMethod]
    public void CalculateReturnsTheBodyMassIndexRoundedToTwoDecimals()
    {
        Assert.AreEqual(24.07, BmiCalculator.Calculate(78, 180));
        Assert.AreEqual(22.88, BmiCalculator.Calculate(80, 187));
        Assert.AreEqual(25d, BmiCalculator.Calculate(81, 180));
    }

    /// <summary>
    /// Checks whether the default values of the two numeric up downs still produce the value that the form shows
    /// when the user presses the button without entering anything.
    /// </summary>
    [TestMethod]
    public void CalculateReturnsTenThousandForTheDefaultValues()
    {
        Assert.AreEqual(10000d, BmiCalculator.Calculate(1, 1));
    }

    /// <summary>
    /// Checks whether a mass or a size of zero or less is rejected instead of returning infinity or a negative
    /// body mass index. The numeric up downs of the form start at 1, so this is a guard, not a reachable path.
    /// </summary>
    [TestMethod]
    public void CalculateThrowsForAMassOrASizeOfZeroOrLess()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => BmiCalculator.Calculate(0, 180));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => BmiCalculator.Calculate(-1, 180));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => BmiCalculator.Calculate(78, 0));
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => BmiCalculator.Calculate(78, -1));
    }

    /// <summary>
    /// Checks whether every band of the World Health Organization is left at its upper bound, which means that the
    /// bound itself already belongs to the next category.
    /// </summary>
    [TestMethod]
    public void DetermineCategoryTreatsTheUpperBoundOfEveryBandAsExclusive()
    {
        Assert.AreEqual(BmiCategory.ExtremeUnderweight, BmiCalculator.DetermineCategory(15.99));
        Assert.AreEqual(BmiCategory.Underweight, BmiCalculator.DetermineCategory(16));
        Assert.AreEqual(BmiCategory.Underweight, BmiCalculator.DetermineCategory(16.99));
        Assert.AreEqual(BmiCategory.LightUnderweight, BmiCalculator.DetermineCategory(17));
        Assert.AreEqual(BmiCategory.LightUnderweight, BmiCalculator.DetermineCategory(18.49));
        Assert.AreEqual(BmiCategory.NormalWeight, BmiCalculator.DetermineCategory(18.5));
        Assert.AreEqual(BmiCategory.NormalWeight, BmiCalculator.DetermineCategory(24.99));
        Assert.AreEqual(BmiCategory.PreAdiposity, BmiCalculator.DetermineCategory(25));
        Assert.AreEqual(BmiCategory.PreAdiposity, BmiCalculator.DetermineCategory(29.99));
        Assert.AreEqual(BmiCategory.AdiposityGrade1, BmiCalculator.DetermineCategory(30));
        Assert.AreEqual(BmiCategory.AdiposityGrade1, BmiCalculator.DetermineCategory(34.99));
        Assert.AreEqual(BmiCategory.AdiposityGrade2, BmiCalculator.DetermineCategory(35));
        Assert.AreEqual(BmiCategory.AdiposityGrade2, BmiCalculator.DetermineCategory(39.99));
        Assert.AreEqual(BmiCategory.AdiposityGrade3, BmiCalculator.DetermineCategory(40));
    }

    /// <summary>
    /// Checks whether the categories at both ends of the scale are still returned for values that the form can
    /// produce with its limits of 1 to 230 cm and 1 to 400 kg.
    /// </summary>
    [TestMethod]
    public void DetermineCategoryCoversTheWholeRangeTheFormCanProduce()
    {
        Assert.AreEqual(BmiCategory.ExtremeUnderweight, BmiCalculator.DetermineCategory(BmiCalculator.Calculate(1, 230)));
        Assert.AreEqual(BmiCategory.AdiposityGrade3, BmiCalculator.DetermineCategory(BmiCalculator.Calculate(400, 1)));
    }

    /// <summary>
    /// Checks whether every category is mapped to the key that is named after it, because that is what the language
    /// files use.
    /// </summary>
    [TestMethod]
    public void GetLanguageKeyReturnsTheNameOfEveryCategory()
    {
        foreach (var category in Enum.GetValues<BmiCategory>())
        {
            Assert.AreEqual(category.ToString(), BmiCalculator.GetLanguageKey(category));
        }
    }

    /// <summary>
    /// Checks whether a value that is not a defined category is rejected instead of silently returning a key that
    /// no language file knows.
    /// </summary>
    [TestMethod]
    public void GetLanguageKeyThrowsForAnUndefinedCategory()
    {
        Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => BmiCalculator.GetLanguageKey((BmiCategory)999));
    }
}
