using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XNATools.ToolHelper.GUI
{
    public partial class InputWindow : Form
    {
        public InputWindow()
        {
            InitializeComponent();
            this.ShowIcon = false;
        }

        public InputWindow(List<string> inputs)
            : this()
        {
            LoadData(inputs);
        }

        public InputWindow(List<string> inputs, Icon icon)
            : this()
        {
            LoadData(inputs, icon);
        }

        //Determines if there any labels match
        private void InputWindow_Load(object sender, EventArgs e)
        {
            List<string> inputs = new List<string>();
            Label lbl = null;
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    lbl = c as Label;
                    inputs.Add(lbl.Text);
                }
            }

            if (InputsMatch(inputs))
            {
                MessageBox.Show("There are some inputs types that are the same.  Reload Data with new inputs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        //Loads inputs and calls Create Controls with them
        public void LoadData(List<string> inputs)
        {
            CreateControls(inputs);
        }

        public void LoadData(Icon icon)
        {
            this.Icon = icon;
            this.ShowIcon = true;
        }

        public void LoadData(List<string> inputs, Icon icon)
        {
            LoadData(icon);
            LoadData(inputs);
        }

        public void LoadData(List<string> inputs, List<string> defaultValues)
        {
            LoadData(inputs);
            for (int i = 0; i < defaultValues.Count; i++)
            {
                SetValue(inputs[i], defaultValues[i]);
            }
        }

        public void LoadData(List<string> inputs, List<string> defaultvalues, Icon icon)
        {
            LoadData(icon);
            LoadData(inputs, defaultvalues);
        }

        public void SetWindowWidth(int width)
        {
            this.Width = width;
        }

        //Creates labels, textboxes, and buttons based on the inputs
        private void CreateControls(List<string> inputs)
        {
            
            const int WALL_PADDING = 5;
            const int SPACE_AFTER_LABEL = 27;
            const int SPACE_BETWEEN_LABEL_AND_TB = 5;
            const int LABEL_PADDING = 2;

            int longestString = GetLongestLabelLength(inputs);
            int distanceBetween = SPACE_BETWEEN_LABEL_AND_TB + longestString;

            Label l = null;
            TextBox tb = null;
            Button bOK = new Button();
            Button bCancel = new Button();

            string s = string.Empty;

            for (int i = 0; i < inputs.Count; i++)
            {
                l = new Label();
                tb = new TextBox();
                tb.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                s = inputs[i] + ":";

                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(s, l.Font, 1000);
                    l.Width = (int)Math.Ceiling(size.Width + 1);
                }

                l.Text = s;
                l.Location = new Point(WALL_PADDING, WALL_PADDING + (SPACE_AFTER_LABEL * i) + LABEL_PADDING);

                tb.Width = bOK.Width + 5 + bCancel.Width;
                tb.Location = new Point(WALL_PADDING + distanceBetween, WALL_PADDING + (SPACE_AFTER_LABEL * i));
                tb.Tag = inputs[i];
                tb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.Controls.Add(l);
                this.Controls.Add(tb);
            }

            bCancel.Text = "Cancel";
            bCancel.Location = new Point(tb.Location.X + tb.Width - bCancel.Width, tb.Location.Y + tb.Height + 5);
            bCancel.Click += new EventHandler(Cancel);
            bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            bOK.Text = "OK";
            bOK.Location = new Point(bCancel.Location.X - 5 - bOK.Width, bCancel.Location.Y);
            bOK.Click += new EventHandler(OK);
            bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            this.Height = 40 + bCancel.Location.Y + bCancel.Height + WALL_PADDING;
            this.Width = 25 + tb.Location.X + tb.Width + WALL_PADDING + 150;

            this.Controls.Add(bCancel);
            this.Controls.Add(bOK);
        }

        private bool InputsMatch(List<string> inputs)
        {
            Dictionary<string, bool> check = new Dictionary<string, bool>();
            bool copy = false;
            foreach(string s in inputs)
            {
                if (check.ContainsKey(s))
                {
                    copy = true;
                    break;
                }
                else
                {
                    check.Add(s, false);
                }
            }

            return copy;
        }

        //Gets the length of the string with the larget number of characters
        private int GetLongestLabelLength(List<string> inputs)
        {
            int toReturn = 0;
            int width = 0;
            Label l = new Label();

            foreach (string s in inputs)
            {
                using (Graphics g = CreateGraphics())
                {
                    SizeF size = g.MeasureString(s, l.Font, 1000);
                    width = (int)Math.Ceiling(size.Width + 1);
                    toReturn = width > toReturn ? width : toReturn;
                }
            }

            return toReturn;
        }

        //Cancel event, sets dialog = Cancel and closes
        private void Cancel(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        //Ok event, sets dialog = OK and closes
        private void OK(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public string GetValue(string inputName)
        {
            string toReturn = string.Empty;
            TextBox tb = GetTextbox(inputName);

            if (tb != null)
            {
                toReturn = tb.Text;
            }

            return toReturn;
        }

        public Dictionary<string, string> GetValueDictionary()
        {
            Dictionary<string, string> toReturn = new Dictionary<string,string>();
            TextBox tb = null;
            string tag = string.Empty;

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    tb = c as TextBox;
                    tag = tb.Tag.ToString();
                    if (!toReturn.ContainsKey(tag))
                    {
                        toReturn.Add(tag, tb.Text);
                    }
                }
            }

            return toReturn;
        }

        private void SetValue(string inputName, string value)
        {
            TextBox tb = GetTextbox(inputName);
            if (tb != null)
            {
                tb.Text = value;
            }
        }

        public void SetTextboxReadOnly(string name, bool readOnly)
        {
            TextBox tb = GetTextbox(name);
            if (tb != null)
            {
                tb.ReadOnly = readOnly;
            }
        }

        private TextBox GetTextbox(string name)
        {
            TextBox tb = null;
            string tag = string.Empty;

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    tb = c as TextBox;
                    tag = tb.Tag.ToString();
                    if (tag.Equals(name))
                    {
                        return tb;
                    }
                }
            }

            return null;
        }

        private void InputWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
