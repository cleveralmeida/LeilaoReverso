using System;
using System.Collections.Generic;

namespace LR.Models
{
    public class TextValue3
    {
        string text;
        double value;
        public TextValue3(string text, double value)
        {
            this.Text = text;
            this.Value = value;
        }
        public string Text { get; set; }
        public double Value { get; set; }
    }
}
