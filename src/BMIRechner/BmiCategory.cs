// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BmiCategory.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The body mass index categories.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BMIRechner;

/// <summary>
/// The body mass index categories according to the World Health Organization. The names are the keys of the
/// matching words in the language files.
/// </summary>
public enum BmiCategory
{
    /// <summary>
    /// A body mass index below 16.
    /// </summary>
    ExtremeUnderweight,

    /// <summary>
    /// A body mass index from 16 to below 17.
    /// </summary>
    Underweight,

    /// <summary>
    /// A body mass index from 17 to below 18.5.
    /// </summary>
    LightUnderweight,

    /// <summary>
    /// A body mass index from 18.5 to below 25.
    /// </summary>
    NormalWeight,

    /// <summary>
    /// A body mass index from 25 to below 30.
    /// </summary>
    PreAdiposity,

    /// <summary>
    /// A body mass index from 30 to below 35.
    /// </summary>
    AdiposityGrade1,

    /// <summary>
    /// A body mass index from 35 to below 40.
    /// </summary>
    AdiposityGrade2,

    /// <summary>
    /// A body mass index of 40 and above.
    /// </summary>
    AdiposityGrade3
}
