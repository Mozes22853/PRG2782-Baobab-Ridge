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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            lblMode = new Label();
            lblSearchId = new Label();
            txtSearchId = new TextBox();
            btnSearch = new Button();
            btnSaveChanges = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(27, 32);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(686, 45);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "Baobab Ridge";
            // 
            // lblSubtitle
            // 
            lblSubtitle.Location = new Point(27, 77);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(686, 29);
            lblSubtitle.TabIndex = 11;
            lblSubtitle.Text = "Wildlife Rehabilitation Records";
            // 
            // lblAnimalId
            // 
            lblAnimalId.Location = new Point(27, 147);
            lblAnimalId.Name = "lblAnimalId";
            lblAnimalId.Size = new Size(137, 31);
            lblAnimalId.TabIndex = 12;
            lblAnimalId.Text = "Animal ID";
            // 
            // txtAnimalId
            // 
            txtAnimalId.AccessibleName = "Animal ID";
            txtAnimalId.Location = new Point(176, 143);
            txtAnimalId.Margin = new Padding(3, 4, 3, 4);
            txtAnimalId.Name = "txtAnimalId";
            txtAnimalId.PlaceholderText = "WR-0007";
            txtAnimalId.Size = new Size(165, 27);
            txtAnimalId.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Location = new Point(27, 207);
            lblName.Name = "lblName";
            lblName.Size = new Size(137, 31);
            lblName.TabIndex = 14;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.AccessibleName = "Name";
            txtName.Location = new Point(176, 203);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(165, 27);
            txtName.TabIndex = 1;
            // 
            // lblSpecies
            // 
            lblSpecies.Location = new Point(27, 267);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new Size(137, 31);
            lblSpecies.TabIndex = 16;
            lblSpecies.Text = "Species";
            // 
            // txtSpecies
            // 
            txtSpecies.AccessibleName = "Species";
            txtSpecies.Location = new Point(176, 263);
            txtSpecies.Margin = new Padding(3, 4, 3, 4);
            txtSpecies.Name = "txtSpecies";
            txtSpecies.Size = new Size(165, 27);
            txtSpecies.TabIndex = 2;
            // 
            // lblAge
            // 
            lblAge.Location = new Point(27, 327);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(137, 31);
            lblAge.TabIndex = 18;
            lblAge.Text = "Age (years)";
            // 
            // txtAge
            // 
            txtAge.AccessibleName = "Age";
            txtAge.Location = new Point(176, 323);
            txtAge.Margin = new Padding(3, 4, 3, 4);
            txtAge.Name = "txtAge";
            txtAge.PlaceholderText = "0 to 100";
            txtAge.Size = new Size(165, 27);
            txtAge.TabIndex = 3;
            // 
            // lblRecoveryScore
            // 
            lblRecoveryScore.Location = new Point(27, 387);
            lblRecoveryScore.Name = "lblRecoveryScore";
            lblRecoveryScore.Size = new Size(143, 31);
            lblRecoveryScore.TabIndex = 20;
            lblRecoveryScore.Text = "Recovery Score";
            // 
            // txtRecoveryScore
            // 
            txtRecoveryScore.AccessibleName = "Recovery Score";
            txtRecoveryScore.Location = new Point(176, 383);
            txtRecoveryScore.Margin = new Padding(3, 4, 3, 4);
            txtRecoveryScore.Name = "txtRecoveryScore";
            txtRecoveryScore.PlaceholderText = "0 to 100";
            txtRecoveryScore.Size = new Size(165, 27);
            txtRecoveryScore.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(27, 447);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(149, 48);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add animal";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // lblHint
            // 
            lblHint.Location = new Point(27, 573);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(314, 64);
            lblHint.TabIndex = 23;
            lblHint.Text = "All fields are required. Status and housing are calculated automatically.";
            // 
            // lblClassification
            // 
            lblClassification.Location = new Point(27, 653);
            lblClassification.Name = "lblClassification";
            lblClassification.Size = new Size(314, 120);
            lblClassification.TabIndex = 24;
            lblClassification.Text = "No animal added yet.";
            // 
            // dgvAnimals
            // 
            dgvAnimals.AccessibleName = "Animal records";
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnimals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAnimals.BackgroundColor = SystemColors.Window;
            dgvAnimals.ColumnHeadersHeight = 29;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvAnimals.DefaultCellStyle = dataGridViewCellStyle1;
            dgvAnimals.Location = new Point(377, 143);
            dgvAnimals.Margin = new Padding(3, 4, 3, 4);
            dgvAnimals.MultiSelect = false;
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersVisible = false;
            dgvAnimals.RowHeadersWidth = 51;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.Size = new Size(944, 584);
            dgvAnimals.TabIndex = 6;
            dgvAnimals.CellClick += dgvAnimals_CellClick;
            // 
            // lblLoadNotice
            // 
            lblLoadNotice.Location = new Point(27, 867);
            lblLoadNotice.Name = "lblLoadNotice";
            lblLoadNotice.Size = new Size(1294, 33);
            lblLoadNotice.TabIndex = 26;
            // 
            // lblFeedback
            // 
            lblFeedback.BackColor = SystemColors.ControlLight;
            lblFeedback.Location = new Point(27, 916);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Padding = new Padding(9, 11, 9, 11);
            lblFeedback.Size = new Size(1294, 57);
            lblFeedback.TabIndex = 27;
            lblFeedback.Text = "Enter the animal's details, then select Add animal.";
            // 
            // lblMode
            // 
            lblMode.Location = new Point(27, 113);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(314, 27);
            lblMode.TabIndex = 11;
            lblMode.Text = "Add mode";
            // 
            // lblSearchId
            // 
            lblSearchId.Location = new Point(377, 767);
            lblSearchId.Name = "lblSearchId";
            lblSearchId.Size = new Size(109, 31);
            lblSearchId.TabIndex = 10;
            lblSearchId.Text = "Search by ID";
            // 
            // txtSearchId
            // 
            txtSearchId.AccessibleName = "Search Animal ID";
            txtSearchId.Location = new Point(491, 763);
            txtSearchId.Margin = new Padding(3, 4, 3, 4);
            txtSearchId.Name = "txtSearchId";
            txtSearchId.PlaceholderText = "WR-0001";
            txtSearchId.Size = new Size(182, 27);
            txtSearchId.TabIndex = 8;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(690, 756);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(126, 43);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Enabled = false;
            btnSaveChanges.Location = new Point(187, 447);
            btnSaveChanges.Margin = new Padding(3, 4, 3, 4);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(154, 48);
            btnSaveChanges.TabIndex = 6;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(27, 507);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(149, 48);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // MainForm
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1349, 1000);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchId);
            Controls.Add(lblSearchId);
            Controls.Add(btnClear);
            Controls.Add(btnSaveChanges);
            Controls.Add(lblMode);
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
            Margin = new Padding(3, 4, 3, 4);
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
        private Label lblMode = null!;
        private Label lblSearchId = null!;
        private TextBox txtSearchId = null!;
        private Button btnSearch = null!;
        private Button btnSaveChanges = null!;
        private Button btnClear = null!;
        private DataGridViewTextBoxColumn colId = null!;
        private DataGridViewTextBoxColumn colName = null!;
        private DataGridViewTextBoxColumn colSpecies = null!;
        private DataGridViewTextBoxColumn colAge = null!;
        private DataGridViewTextBoxColumn colScore = null!;
        private DataGridViewTextBoxColumn colStatus = null!;
        private DataGridViewTextBoxColumn colHousing = null!;
    }
}
