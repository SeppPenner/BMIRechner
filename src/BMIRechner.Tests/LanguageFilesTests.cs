// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LanguageFilesTests.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class to test the language files against the keys the application asks for.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BMIRechner.Tests;

/// <summary>
/// A class to test the language files against the keys the application asks for. This matters because
/// <see cref="ILanguage.GetWord"/> returns <c>null</c> for an unknown key and does not fall back to another
/// language, so a missing key shows up as an empty label at runtime and not as an error.
/// </summary>
[TestClass]
public class LanguageFilesTests
{
    /// <summary>
    /// The keys of the words that do not belong to a <see cref="BmiCategory"/>.
    /// </summary>
    private static readonly string[] UserInterfaceKeys = ["Title", "Size", "Weight", "Result", "Calculate"];

    /// <summary>
    /// The language manager under test. It reads the language files from the languages folder of the output
    /// directory, which is the same mechanism the application uses.
    /// </summary>
    private readonly ILanguageManager languageManager = new LanguageManager();

    /// <summary>
    /// Checks whether both language files of the application are found and read, and whether they still carry the
    /// identifiers and names the form relies on.
    /// </summary>
    [TestMethod]
    public void BothLanguageFilesAreLoaded()
    {
        var languages = this.languageManager.GetLanguages();

        Assert.AreEqual(2, languages.Count);
        CollectionAssert.AreEquivalent(new[] { "de-DE", "en-US" }, languages.Select(language => language.Identifier).ToArray());
        CollectionAssert.AreEquivalent(new[] { "Deutsch", "English (US)" }, languages.Select(language => language.Name).ToArray());
    }

    /// <summary>
    /// Checks whether every language has a word for every body mass index category.
    /// </summary>
    [TestMethod]
    public void EveryLanguageHasAWordForEveryCategory()
    {
        foreach (var language in this.languageManager.GetLanguages())
        {
            foreach (var category in Enum.GetValues<BmiCategory>())
            {
                var key = BmiCalculator.GetLanguageKey(category);
                Assert.IsFalse(string.IsNullOrWhiteSpace(language.GetWord(key)), $"The language {language.Identifier} has no word for the key {key}.");
            }
        }
    }

    /// <summary>
    /// Checks whether every language has a word for every key that the form asks for besides the categories.
    /// </summary>
    [TestMethod]
    public void EveryLanguageHasAWordForEveryUserInterfaceKey()
    {
        foreach (var language in this.languageManager.GetLanguages())
        {
            foreach (var key in UserInterfaceKeys)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(language.GetWord(key)), $"The language {language.Identifier} has no word for the key {key}.");
            }
        }
    }

    /// <summary>
    /// Checks whether both language files define exactly the same keys, so that a key added to one of them is not
    /// forgotten in the other one.
    /// </summary>
    [TestMethod]
    public void BothLanguagesDefineTheSameKeys()
    {
        var keysPerLanguage = this.languageManager.GetLanguages()
            .Select(language => language.Words.Select(word => word.Key).OrderBy(key => key, StringComparer.Ordinal).ToArray())
            .ToArray();

        CollectionAssert.AreEqual(keysPerLanguage[0], keysPerLanguage[1]);
    }

    /// <summary>
    /// Checks whether the German words are still German and the English ones English, at least for the two values
    /// that were German in both files before.
    /// </summary>
    [TestMethod]
    public void TheEnglishFileDoesNotContainGermanWords()
    {
        var english = this.languageManager.GetLanguages().Single(language => language.Identifier == "en-US");

        Assert.AreEqual("Normal weight", english.GetWord("NormalWeight"));
        Assert.AreEqual("Adiposity grade II", english.GetWord("AdiposityGrade2"));
        Assert.AreEqual("Adiposity grade III", english.GetWord("AdiposityGrade3"));
    }
}
