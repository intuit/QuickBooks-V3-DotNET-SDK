////********************************************************************
// <copyright file="IntuitAnywhereConfigurationDialog.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This dialog receives the user input for the application token, consumer key and consumer secret key values.</summary>
////********************************************************************

namespace IntuitAnyWhereAddin
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// This dialog receives the user input for the application token, consumer key and consumer secret key values.
    /// </summary>
    public partial class IntuitAnywhereConfigurationDialog : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitAnywhereConfigurationDialog"/> class.
        /// </summary>
        public IntuitAnywhereConfigurationDialog()
        {
           this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets values for the keys
        /// </summary>
        internal DataTable Keys { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether key values are modified.
        /// </summary>
        internal bool KeysModified { get; set; }

        /// <summary>
        /// Handles the Load event of the IntuitAnywhereConfigurationDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void IntuitAnywhereConfigurationDialog_Load(object sender, EventArgs e)
        {
            this.KeysModified = false;
            this.keysDataGridView.DataSource = this.Keys;
            this.keysDataGridView.AllowUserToAddRows = false;
            this.keysDataGridView.Columns[0].Width = 150;
            this.keysDataGridView.Columns[0].ReadOnly = true;
            this.keysDataGridView.Columns[0].Frozen = true;
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb((int)(byte)224, (int)(byte)224, (int)(byte)224);
            this.keysDataGridView.Columns[0].DefaultCellStyle = dataGridViewCellStyle1;

            this.keysDataGridView.Columns[1].Width = 237;
            this.keysDataGridView.Columns[2].Width = 237;
            this.keysDataGridView.Columns[3].Width = 237;
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnSaveClick(object sender, EventArgs e)
        {
            this.KeysModified = true;
            this.Keys = (DataTable)keysDataGridView.DataSource;
            this.Close();
        }

        /// <summary>
        /// Handles the CellEnter event of the keysDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void KeysDataGridViewCellEnter(object sender, DataGridViewCellEventArgs e)
        {
            keysDataGridView.BeginEdit(true);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
