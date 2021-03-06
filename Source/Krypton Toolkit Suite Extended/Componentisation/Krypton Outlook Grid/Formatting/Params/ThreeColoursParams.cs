﻿//--------------------------------------------------------------------------------
// Copyright (C) 2013-2015 JDH Software - <support@jdhsoftware.com>
//
// This program is provided to you under the terms of the Microsoft Public
// License (Ms-PL) as published at https://kryptonoutlookgrid.codeplex.com/license
//
// Visit http://www.jdhsoftware.com and follow @jdhsoftware on Twitter
//
//--------------------------------------------------------------------------------

using System.Drawing;
using System.Xml;

namespace KryptonOutlookGrid.Formatting.Params
{
    /// <summary>
    /// Three scale colour class parameters
    /// </summary>
    /// <seealso cref="IFormatParams" />
    public class ThreeColoursParams : IFormatParams
    {
        /// <summary>
        /// The minimum colour
        /// </summary>
        public Color MinimumColour;
        /// <summary>
        /// The medium colour
        /// </summary>
        public Color MediumColour;
        /// <summary>
        /// The maximum colour
        /// </summary>
        public Color MaximumColour;
        /// <summary>
        /// The colour associated to the value
        /// </summary>
        public Color ValueColour;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreeColoursParams"/> class.
        /// </summary>
        /// <param name="minColour">The minimum colour.</param>
        /// <param name="mediumColour">Colour of the medium.</param>
        /// <param name="maxColour">The maximum colour.</param>
        public ThreeColoursParams(Color minColour, Color mediumColour, Color maxColour)
        {
            MinimumColour = minColour;
            MediumColour = mediumColour;
            MaximumColour = maxColour;
        }

        /// <summary>
        /// Crée un objet qui est une copie de l'instance actuelle.
        /// </summary>
        /// <returns>
        /// Nouvel objet qui est une copie de cette instance.
        /// </returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Persists the parameters.
        /// </summary>
        /// <param name="writer">The XML writer.</param>
        void IFormatParams.Persist(XmlWriter writer)
        {
            writer.WriteElementString("MinimumColour", MinimumColour.ToArgb().ToString());
            writer.WriteElementString("MediumColour", MediumColour.ToArgb().ToString());
            writer.WriteElementString("MaximumColour", MaximumColour.ToArgb().ToString());
        }
    }
}