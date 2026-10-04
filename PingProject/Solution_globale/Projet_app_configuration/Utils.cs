using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace Projet_app_configuration {
    public static class Utils {
        /// <summary>
        /// Create a button with Text, Location, Size, ForeColor, BackColor, Font and FlatStyle
        /// </summary>
        /// <param name="text">The text of the button</param>
        /// <param name="location">The location of the button</param>
        /// <param name="size">The size of the button</param>
        /// <param name="foreColor">The foreColor of the button</param>
        /// <param name="backColor">The backColor of the button</param>
        /// <param name="font">The font of the button</param>
        /// <param name="flatStyle">The style of the button</param>
        /// <returns>a Button initialised</returns>
        public static Button InitButton(string text, Point location, Size size,Color foreColor, Color backColor, Font font, FlatStyle flatStyle) {
            return new Button()
            {
                Text = text,
                Location = location,
                Size = size,
                ForeColor = foreColor,
                BackColor = backColor,
                Font = font,
                FlatStyle = flatStyle
            };
        }

        /// <summary>
        /// Create a NumericUpDown with BorderStyle, Size, Location, Font, Value, Minimum and Maximum
        /// </summary>
        /// <param name="style">The style of the NumericUpDown</param>
        /// <param name="location">The location of the NumericUpDown</param>
        /// <param name="size">The size of the NumericUpDown</param>
        /// <param name="font">The font of the NumericUpDown</param>
        /// <param name="value">The default value of the NumericUpDown</param>
        /// <param name="min">The min value of the NumericUpDown</param>
        /// <param name="max">The max value of the NumericUpDown</param>
        /// <returns>a NumericUpDown initialised</returns>
        public static NumericUpDown InitNumericUpDown(BorderStyle style, Point location, Size size, Font font, int value, int min, int max) {
            return new NumericUpDown()
            {
                BorderStyle = style,
                Size = size,
                Location = location,
                Font = font,
                Value = value,
                Minimum = min,
                Maximum = max

            };
        }
        /// <summary>
        /// Create a Label with Text, Location, Font. The parameter Autosize is true
        /// </summary>
        /// <param name="text">The text of the Label</param>
        /// <param name="location">The location of the Label</param>
        /// <param name="font">The font of the Label</param>
        /// <returns>a Label initialised</returns>
        public static Label InitLabel(string text, Point location, Font font) {
            return new Label()
            {
                Text = text,
                Location = location,
                AutoSize = true,
                Font = font
            };
        }
        /// <summary>
        /// Create a TextBox with BorderStyle, Size, Location, Font. The parameter Autosize is true
        /// </summary>
        /// <param name="style">The style of the TextBox</param>
        /// <param name="location">The location of the TextBox</param>
        /// <param name="size">The size of the TextBox</param>
        /// <param name="font">The font of the TextBox</param>
        /// <returns>a TextBox initialised</returns>
        public static TextBox InitTextBox(BorderStyle style, Point location, Size size, Font font) {
            return new TextBox()
            {
                BorderStyle = style,
                Size = size,
                Location = location,
                Font = font
            };
        }
    }
}
