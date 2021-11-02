using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoboRemoPC
{
   
    public partial class DebugLogForm : Form
    {
        public DebugLogForm()
        {
            InitializeComponent();
        }
        
    }

    public static class Debug
    {
        public static DebugLogForm logForm;
        public static void AddLine(string text)
        {
            if (logForm == null)
            {
                logForm = new DebugLogForm();
                logForm.Show();
            }
            logForm.rtxt.AppendText(text + "\n");
        }
    }
}
