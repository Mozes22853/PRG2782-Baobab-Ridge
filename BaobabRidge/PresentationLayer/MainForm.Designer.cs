namespace BaobabRidge.PresentationLayer
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null!;

        // Releases designer components when the form closes.
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Places ordinary controls directly on the form so they can be moved in the designer.
        private void InitializeComponent()
        {
            DataGridViewCellStyle animalCellStyle = new DataGridViewCellStyle();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblAnimalId = new Label();
            txtAnimalId = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            lblSpecies = new Label();
            txtSpecies = new TextBox();
            lblAge = new Label();
            txtAge = new TextBox();
            lblRecoveryScore = new Label();
            txtRecoveryScore = new TextBox();
            btnAdd = new Button();
            lblHint = new Label();
            lblClassification = new Label();
            dgvAnimals = new DataGridView();
            lblLoadNotice = new Label();
            lblFeedback = new Label();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colSpecies = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colScore = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colHousing = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.Location = new Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 34);
            lblTitle.Text = "Baobab Ridge";
            lblTitle.TabIndex = 10;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            //
            // lblSubtitle
            //
            lblSubtitle.Location = new Point(24, 58);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(600, 22);
            lblSubtitle.Text = "Wildlife Rehabilitation Records";
            lblSubtitle.TabIndex = 11;
            //
            // lblAnimalId
            //
            lblAnimalId.Location = new Point(24, 110);
            lblAnimalId.Name = "lblAnimalId";
            lblAnimalId.Size = new Size(120, 23);
            lblAnimalId.Text = "Animal ID";
            lblAnimalId.TabIndex = 12;
            //
            // txtAnimalId
            //
            txtAnimalId.Location = new Point(154, 107);
            txtAnimalId.Name = "txtAnimalId";
            txtAnimalId.Size = new Size(145, 23);
            txtAnimalId.TabIndex = 0;
            txtAnimalId.AccessibleName = "Animal ID";
            txtAnimalId.PlaceholderText = "WR-0007";
            //
            // lblName
            //
            lblName.Location = new Point(24, 155);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 23);
            lblName.Text = "Name";
            lblName.TabIndex = 14;
            //
            // txtName
            //
            txtName.Location = new Point(154, 152);
            txtName.Name = "txtName";
            txtName.Size = new Size(145, 23);
            txtName.TabIndex = 1;
            txtName.AccessibleName = "Name";
            //
            // lblSpecies
            //
            lblSpecies.Location = new Point(24, 200);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new Size(120, 23);
            lblSpecies.Text = "Species";
            lblSpecies.TabIndex = 16;
            //
            // txtSpecies
            //
            txtSpecies.Location = new Point(154, 197);
            txtSpecies.Name = "txtSpecies";
            txtSpecies.Size = new Size(145, 23);
            txtSpecies.TabIndex = 2;
            txtSpecies.AccessibleName = "Species";
            //
            // lblAge
            //
            lblAge.Location = new Point(24, 245);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(120, 23);
            lblAge.Text = "Age (years)";
            lblAge.TabIndex = 18;
            //
            // txtAge
            //
            txtAge.Location = new Point(154, 242);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(145, 23);
            txtAge.TabIndex = 3;
            txtAge.AccessibleName = "Age";
            txtAge.PlaceholderText = "0 to 100";
            //
            // lblRecoveryScore
            //
            lblRecoveryScore.Location = new Point(24, 290);
            lblRecoveryScore.Name = "lblRecoveryScore";
            lblRecoveryScore.Size = new Size(125, 23);
            lblRecoveryScore.Text = "Recovery Score";
            lblRecoveryScore.TabIndex = 20;
            //
            // txtRecoveryScore
            //
            txtRecoveryScore.Location = new Point(154, 287);
            txtRecoveryScore.Name = "txtRecoveryScore";
            txtRecoveryScore.Size = new Size(145, 23);
            txtRecoveryScore.TabIndex = 4;
            txtRecoveryScore.AccessibleName = "Recovery Score";
            txtRecoveryScore.PlaceholderText = "0 to 100";
            //
            // btnAdd
            //
            btnAdd.Location = new Point(154, 335);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(145, 36);
            btnAdd.Text = "Add animal";
            btnAdd.TabIndex = 5;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // lblHint
            //
            lblHint.Location = new Point(24, 390);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(275, 48);
            lblHint.Text = "All fields are required. Status and housing are calculated automatically.";
            lblHint.TabIndex = 23;
            //
            // lblClassification
            //
            lblClassification.Location = new Point(24, 455);
            lblClassification.Name = "lblClassification";
            lblClassification.Size = new Size(275, 90);
            lblClassification.Text = "No animal added yet.";
            lblClassification.TabIndex = 24;
            //
            // dgvAnimals
            //
            dgvAnimals.Location = new Point(330, 107);
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.Size = new Size(826, 438);
            dgvAnimals.TabIndex = 6;
            dgvAnimals.AccessibleName = "Animal records";
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.AllowUserToOrderColumns = false;
            dgvAnimals.AutoGenerateColumns = false;
            dgvAnimals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnimals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAnimals.BackgroundColor = SystemColors.Window;
            dgvAnimals.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colSpecies, colAge, colScore, colStatus, colHousing });
            animalCellStyle.WrapMode = DataGridViewTriState.True;
            dgvAnimals.DefaultCellStyle = animalCellStyle;
            dgvAnimals.MultiSelect = false;
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersVisible = false;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //
            // lblLoadNotice
            //
            lblLoadNotice.Location = new Point(24, 650);
            lblLoadNotice.Name = "lblLoadNotice";
            lblLoadNotice.Size = new Size(1132, 25);
            lblLoadNotice.TabIndex = 26;
            //
            // lblFeedback
            //
            lblFeedback.Location = new Point(24, 687);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Size = new Size(1132, 43);
            lblFeedback.Text = "Enter the animal's details, then select Add animal.";
            lblFeedback.TabIndex = 27;
            lblFeedback.BackColor = SystemColors.ControlLight;
            lblFeedback.Padding = new Padding(8);
            //
            // colId
            //
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Animal ID";
            colId.Name = "colId";
            colId.MinimumWidth = 70;
            colId.FillWeight = 100F;
            colId.ReadOnly = true;
            colId.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colName
            //
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.MinimumWidth = 70;
            colName.FillWeight = 100F;
            colName.ReadOnly = true;
            colName.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colSpecies
            //
            colSpecies.DataPropertyName = "Species";
            colSpecies.HeaderText = "Species";
            colSpecies.Name = "colSpecies";
            colSpecies.MinimumWidth = 90;
            colSpecies.FillWeight = 130F;
            colSpecies.ReadOnly = true;
            colSpecies.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colAge
            //
            colAge.DataPropertyName = "Age";
            colAge.HeaderText = "Age";
            colAge.Name = "colAge";
            colAge.MinimumWidth = 45;
            colAge.FillWeight = 50F;
            colAge.ReadOnly = true;
            colAge.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colScore
            //
            colScore.DataPropertyName = "RecoveryScore";
            colScore.HeaderText = "Recovery Score";
            colScore.Name = "colScore";
            colScore.MinimumWidth = 80;
            colScore.FillWeight = 100F;
            colScore.ReadOnly = true;
            colScore.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colStatus
            //
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Status";
            colStatus.Name = "colStatus";
            colStatus.MinimumWidth = 95;
            colStatus.FillWeight = 100F;
            colStatus.ReadOnly = true;
            colStatus.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // colHousing
            //
            colHousing.DataPropertyName = "HousingUnit";
            colHousing.HeaderText = "Housing Unit";
            colHousing.Name = "colHousing";
            colHousing.MinimumWidth = 120;
            colHousing.FillWeight = 170F;
            colHousing.ReadOnly = true;
            colHousing.SortMode = DataGridViewColumnSortMode.NotSortable;
            //
            // MainForm
            //
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 750);
            Controls.Add(lblFeedback);
            Controls.Add(lblLoadNotice);
            Controls.Add(dgvAnimals);
            Controls.Add(lblClassification);
            Controls.Add(lblHint);
            Controls.Add(btnAdd);
            Controls.Add(txtRecoveryScore);
            Controls.Add(lblRecoveryScore);
            Controls.Add(txtAge);
            Controls.Add(lblAge);
            Controls.Add(txtSpecies);
            Controls.Add(lblSpecies);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(txtAnimalId);
            Controls.Add(lblAnimalId);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Baobab Ridge Wildlife Rehabilitation";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle = null!;
        private Label lblSubtitle = null!;
        private Label lblAnimalId = null!;
        private TextBox txtAnimalId = null!;
        private Label lblName = null!;
        private TextBox txtName = null!;
        private Label lblSpecies = null!;
        private TextBox txtSpecies = null!;
        private Label lblAge = null!;
        private TextBox txtAge = null!;
        private Label lblRecoveryScore = null!;
        private TextBox txtRecoveryScore = null!;
        private Button btnAdd = null!;
        private Label lblHint = null!;
        private Label lblClassification = null!;
        private DataGridView dgvAnimals = null!;
        private Label lblLoadNotice = null!;
        private Label lblFeedback = null!;
        private DataGridViewTextBoxColumn colId = null!;
        private DataGridViewTextBoxColumn colName = null!;
        private DataGridViewTextBoxColumn colSpecies = null!;
        private DataGridViewTextBoxColumn colAge = null!;
        private DataGridViewTextBoxColumn colScore = null!;
        private DataGridViewTextBoxColumn colStatus = null!;
        private DataGridViewTextBoxColumn colHousing = null!;
    }
}
