﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diabet
{
    public partial class LoadingForm : Form
    {
        public LoadingForm(string caption)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            label1.Text = caption;
        }
    }
}
