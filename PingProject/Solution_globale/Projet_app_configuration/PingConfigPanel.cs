using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Projet_app_configuration {
    public partial class PingConfigPanel : Panel {
        Label lastSave;
        Label textName;
        TextBox nameField;
        Label textIP;
        NumericUpDown IPField1;
        Label dot1;
        NumericUpDown IPField2;
        Label dot2;
        NumericUpDown IPField3;
        Label dot3;
        NumericUpDown IPField4;
        Label textInterval;
        Label textError;
        List<Label> labels = new List<Label>();
        NumericUpDown numInterval;
        DataGridView dataGridView;
        Button btnAdd;
        Button btnDelete;
        PanelManager manager;
        GeneralConfigurationForm form;
        ConfigDevice deviceSelected;

        /// <summary>
        /// Constructs a PingConfigPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// Init all events
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public PingConfigPanel(GeneralConfigurationForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            InitPanel();
            this.Resize += (s, e) => CenterGrid();

            this.form.Controls.Add(this);
            
            this.nameField.Validated += (s, e) => ApplyChange();
            this.IPField1.Validated += (s, e) => ApplyChange();
            this.IPField2.Validated += (s, e) => ApplyChange();
            this.IPField3.Validated += (s, e) => ApplyChange();
            this.IPField4.Validated += (s, e) => ApplyChange();
            this.numInterval.Validated += (s, e) => ApplyChange();

            this.btnAdd.Click += (s, e) => AddInGridView();
            this.btnDelete.Click += (s, e) => DeleteInGridView();
            this.dataGridView.RowHeaderMouseClick += (s, e) => LoadField();
            this.dataGridView.MouseClick += (s, e) =>
            {
                var position = dataGridView.HitTest(e.X, e.Y);
                if (position.RowIndex < 0) {
                    dataGridView.ClearSelection();
                    deviceSelected = null;
                    this.btnDelete.Visible = false;
                    this.btnAdd.Visible = true;
                    nameField.Text = "";
                    IPField1.Value = 1;
                    IPField2.Value = 0;
                    IPField3.Value = 0;
                    IPField4.Value = 0;
                    numInterval.Value = 0;
                    textError.Text = "";

                }
                this.form.ValidateChildren();
            };
            this.MouseClick += (s, e) => {
                this.Focus();
                this.form.ValidateChildren();
            };
        }

        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the replaceField() method which will replace the fields with the fields from the json file.
        /// </summary>
        public void InitPanel() {
            var font = new Font("Arial", 10);
            lastSave = Utils.InitLabel("", new Point(form.ClientSize.Width - 325, 52), font);
            this.Controls.Add(lastSave);
            textName = Utils.InitLabel("Name",new Point(75,150), font);
            this.Controls.Add(textName);
            nameField = Utils.InitTextBox(BorderStyle.FixedSingle, new Point(100, 150), new Size(150, 30), font);
            this.Controls.Add(nameField);
            textIP = Utils.InitLabel("IP Adress", new Point(300, 150), font);
            this.Controls.Add(textIP);
            textInterval = Utils.InitLabel("Interval", new Point(700, 150), font);
            this.Controls.Add(textInterval);
            numInterval = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(800, 140), new Size(150, 30), font, 0, 0, 255);
            this.Controls.Add(numInterval);
            labels.Add(textName);
            labels.Add(textIP);
            labels.Add(textInterval);
            CenterLabel();

            Panel groupBoxIP = new Panel();
            groupBoxIP.BorderStyle = BorderStyle.None;
            groupBoxIP.BackColor = Color.Transparent;
            var widthNumeric = 75;
            var fontDot = new Font("Arial", 15, FontStyle.Bold);
            IPField1 = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(0, 0), new Size(widthNumeric, 30), font, 1, 1, 255);
            dot1 = Utils.InitLabel(".", new Point(widthNumeric, 0), fontDot);
            dot1.AutoSize = false;
            dot1.Size = new Size(16, 30);
            IPField2 = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(dot1.Location.X + dot1.Width, 0), new Size(widthNumeric, 30), font, 0, 0, 255);
            dot2 = Utils.InitLabel(".", new Point(IPField2.Location.X + widthNumeric, 0), fontDot);
            dot2.AutoSize = false;
            dot2.Size = new Size(16, 30);
            IPField3 = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(dot2.Location.X + dot2.Width, 0), new Size(widthNumeric, 30), font, 0, 0, 255);
            dot3 = Utils.InitLabel(".", new Point(IPField3.Location.X + widthNumeric, 0), fontDot);
            dot3.AutoSize = false;
            dot3.Size = new Size(16, 30);
            IPField4 = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(dot3.Location.X + dot3.Width, 0), new Size(widthNumeric, 30), font, 0, 0, 255);
            groupBoxIP.Size = new Size(IPField1.Width*4 + dot1.Width*3,IPField1.Height);
            var spaceBetweenLabelAndField = 20;
            groupBoxIP.Location = new Point(textIP.Left + textIP.Width / 2 - groupBoxIP.Width / 2, textIP.Top + spaceBetweenLabelAndField);

            groupBoxIP.Controls.Add(IPField1);
            groupBoxIP.Controls.Add(dot1);
            groupBoxIP.Controls.Add(IPField2);
            groupBoxIP.Controls.Add(dot2);
            groupBoxIP.Controls.Add(IPField3);
            groupBoxIP.Controls.Add(dot3);
            groupBoxIP.Controls.Add(IPField4);
            this.Controls.Add(groupBoxIP);

            nameField.Location = new Point(textName.Left + textName.Width/2 - nameField.Width/2, textName.Top + spaceBetweenLabelAndField);
            numInterval.Location = new Point(textInterval.Left + textInterval.Width / 2 - numInterval.Width / 2, textInterval.Top + spaceBetweenLabelAndField);

            btnAdd = Utils.InitButton("Add machine", new Point(nameField.Left, nameField.Top + nameField.Height + 10), new Size(140, 25), Color.FromArgb(26, 115, 232), Color.White, font, FlatStyle.Flat);
            this.Controls.Add(btnAdd);

            btnDelete = Utils.InitButton("Delete machine", new Point(btnAdd.Left + btnAdd.Width + 10, btnAdd.Top), new Size(140, 25), Color.FromArgb(26, 115, 232), Color.White, font, FlatStyle.Flat);
            btnDelete.Visible = false;
            this.Controls.Add(btnDelete);

            textError = Utils.InitLabel("",new Point(btnDelete.Left + btnDelete.Width + 30, btnDelete.Top), font);
            textError.ForeColor = Color.Red;
            this.Controls.Add(textError);
            createDataGridView();
            ReplaceField();
        }

        /// <summary>
        /// Create a data grid view
        /// </summary>
        private void createDataGridView() {
            dataGridView = new DataGridView();
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.MultiSelect = false;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ScrollBars = ScrollBars.Vertical;

            dataGridView.Size = new Size(form.ClientSize.Width - 250, 0);
            dataGridView.AutoGenerateColumns = false;

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Name = "Name"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IPAddress",
                HeaderText = "IP Address",
                Name = "IPAddress"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SecondInterval",
                HeaderText = "Interval",
                Name = "SecondInterval"
            });
            this.Controls.Add(dataGridView);
        }
    }
}