﻿namespace Productivity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Productivity.Models;
    using Productivity.Analysis;

    public partial class ProductivityView : Form
    {
        private EventsConnection db;

        public ProductivityView()
        {
            this.db = new EventsConnection();
            InitializeComponent();

            var timelineAnalyzer = new TimelineAnalyzer(this.db);
            var startTime = DateTime.UtcNow.Date;
            timelineAnalyzer.Analyze(startTime, startTime.AddDays(1));
        }

        private void ProductivityView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}