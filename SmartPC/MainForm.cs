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
        //public

        public ObservableCollection<WipParameterInfo> ParameterList { get; set; } = new ObservableCollection<WipParameterInfo>();

        //private

        private int cv_CurrentPageIndex = 1;
        private int cv_TotalPageSize = 0;



        public MainForm()
        {
            InitializeComponent();
            InitializeParameters();
            CalculateTotalPage();
            ShowParameters();
        }

        private void CalculateTotalPage()
        {
            cv_TotalPageSize = ParameterList.Count / tlp.RowCount;

            if (ParameterList.Count % tlp.RowCount > 0)
            {
                cv_TotalPageSize++;
            }
        }

        private void InitializeParameters()
        {
            var rnd = new Random();
            ParameterList.Clear();
            foreach (var index in Enumerable.Range(1, rnd.Next(2, 100)))
            {
                int standard = rnd.Next(index, 100);
                int upper = rnd.Next(standard, 100);
                int lower = rnd.Next(index, standard);
                uint type = (uint)rnd.Next(0, 2);

                ParameterList.Add(new WipParameterInfo
                {
                    Sequential = index,
                    ItemID = $"ItemID_{index}",
                    ItemCaption = $"ItemCaption_{index}",
                    UpperLimit = $"{upper}",
                    LowerLimit = $"{lower}",
                    DefaultValue = $"{standard}",
                    ValueType = type
                });
            }
        }

        private void ShowParameters(int m_PageIndex = 1)
        {
            int? prev = cv_CurrentPageIndex == 1 ? (int?)null : cv_CurrentPageIndex - 1;
            int? next = cv_TotalPageSize == cv_CurrentPageIndex ? (int?)null : cv_CurrentPageIndex + 1;
            btnPrev.Text = $"{prev}<<";
            btnNext.Text = $">>{next}";

            var list = ParameterList.Skip(4 * (m_PageIndex - 1)).Take(tlp.RowCount).ToList();
            
            tlp.Controls.Clear();
            for (int r = 0; r < list.Count; r++)
            {
                tlp.Controls.Add(InitializeLabel(list[r].Sequential.ToString()), 0, r);
                tlp.Controls.Add(InitializeLabel(list[r].ItemCaption), 1, r);
                tlp.Controls.Add(InitializeLabel(list[r].DefaultValue), 2, r);
                tlp.Controls.Add(InitializeLabel(list[r].LimitRange), 3, r);
                tlp.Controls.Add(InitializeTextBox(list[r], "ParameterValue"), 4, r);
            }            
        }

        private TextBox InitializeTextBox<T>(T m_Source, string m_ParameterName) where T : INotifyPropertyChanged
        {
            var tb = new TextBox
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(5),
                Multiline = true,
                TextAlign = HorizontalAlignment.Center
            };

            tb.Font = new Font("新細明體", 27f);
            tb.DataBindings.Add("Text", m_Source, m_ParameterName);

            return tb;
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

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (cv_CurrentPageIndex > 1)
            {
                cv_CurrentPageIndex--;
                ShowParameters(cv_CurrentPageIndex);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (cv_TotalPageSize > cv_CurrentPageIndex)
            {
                cv_CurrentPageIndex++;
                ShowParameters(cv_CurrentPageIndex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            List<string> msg = new List<string>();

            foreach (var param in ParameterList)
            {
                if (param.ValueType == 1 || param.ValueType == 2)
                {
                    decimal value = 0;
                    decimal upper = string.IsNullOrWhiteSpace(param.UpperLimit) ? 0 : Convert.ToDecimal(param.UpperLimit);
                    decimal lower = string.IsNullOrWhiteSpace(param.LowerLimit) ? 0 : Convert.ToDecimal(param.LowerLimit);

                    if (string.IsNullOrWhiteSpace(param.ParameterValue))
                    {
                        msg.Add($"項目{param.Sequential}:{param.ItemCaption} 必填!!");
                    }
                    else if (!decimal.TryParse(param.ParameterValue, out value))
                    {
                        msg.Add($"項目{param.Sequential}:{param.ItemCaption} 必須是數值!!");
                    }
                    else if (value < lower)
                    {
                        msg.Add($"項目{param.Sequential}:{param.ItemCaption} 小於下限值!!");
                    }
                    else if (value > upper)
                    {
                        msg.Add($"項目{param.Sequential}:{param.ItemCaption} 大於上限值!!");
                    }
                }
            }

            if (msg.Count <= 0)
            {
                MessageBox.Show("Success !!");
            }
            else
            {
                var error_string = msg.Aggregate(new StringBuilder(), (sb, s) =>
                {
                    sb.Append(s + Environment.NewLine);
                    return sb;
                });
                MessageBox.Show(error_string.ToString());
            }
        }
    }
}
