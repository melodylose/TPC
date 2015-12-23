using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPC
{
    public partial class MainForm : Form
    {
        public ObservableCollection<WipParameterInfo> ParameterList { get; set; } = new ObservableCollection<WipParameterInfo>();


        public MainForm()
        {
            InitializeComponent();
            InitializeParameters();
            ShowParameters();
        }

        private void InitializeParameters()
        {
            foreach (var index in Enumerable.Range(1, 8))
            {
                ParameterList.Add(new WipParameterInfo
                {
                    Sequential = index,
                    ItemID = $"ItemID_{index}",
                    ItemCaption = $"ItemCaption_{index}"
                });
            }
        }

        private void ShowParameters(int m_PageIndex = 1)
        {
            var list = ParameterList.Skip(4 * (m_PageIndex - 1)).Take(4).ToList();

            for (int r = 0; r < tlp.RowCount; r++)
            {
                tlp.Controls.Add(InitializeLabel(list[r].Sequential.ToString()), 0, r);
                tlp.Controls.Add(InitializeLabel(list[r].ItemID), 1, r);
                tlp.Controls.Add(InitializeLabel(list[r].ItemCaption), 2, r);
                tlp.Controls.Add(InitializeLabel(list[r].ItemCaption), 3, r);
                tlp.Controls.Add(InitializeTextBox(), 4, r);
            }
        }

        private TextBox InitializeTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Multiline = true
            };
        }

        private Label InitializeLabel(string m_Text)
        {
            var lb = new Label
            {
                Text = m_Text,
                BackColor = Color.FromArgb(224, 224, 224),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5)
            };
            
            lb.Font = new Font("微軟正黑體", 12f);
            return lb;
        }
    }
}
