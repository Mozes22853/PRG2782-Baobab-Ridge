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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
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
            lblTitle.Location = new Point(24, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(600, 34);
            lblTitle.TabIndex = 10;
            lblTitle.Text = "Baobab Ridge";
            //
            // lblSubtitle
            //
            lblSubtitle.Location = new Point(24, 58);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(600, 22);
            lblSubtitle.TabIndex = 11;
            lblSubtitle.Text = "Wildlife Rehabilitation Records";
            //
            // lblAnimalId
            //
            lblAnimalId.Location = new Point(24, 110);
            lblAnimalId.Name = "lblAnimalId";
            lblAnimalId.Size = new Size(120, 23);
            lblAnimalId.TabIndex = 12;
            lblAnimalId.Text = "Animal ID";
            //
            // txtAnimalId
            //
            txtAnimalId.AccessibleName = "Animal ID";
            txtAnimalId.Location = new Point(154, 107);
            txtAnimalId.Name = "txtAnimalId";
            txtAnimalId.PlaceholderText = "WR-0007";
            txtAnimalId.Size = new Size(145, 23);
            txtAnimalId.TabIndex = 0;
            //
            // lblName
            //
            lblName.Location = new Point(24, 155);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 23);
            lblName.TabIndex = 14;
            lblName.Text = "Name";
            //
            // txtName
            //
            txtName.AccessibleName = "Name";
            txtName.Location = new Point(154, 152);
            txtName.Name = "txtName";
            txtName.Size = new Size(145, 23);
            txtName.TabIndex = 1;
            //
            // lblSpecies
            //
            lblSpecies.Location = new Point(24, 200);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new Size(120, 23);
            lblSpecies.TabIndex = 16;
            lblSpecies.Text = "Species";
            //
            // txtSpecies
            //
            txtSpecies.AccessibleName = "Species";
            txtSpecies.Location = new Point(154, 197);
            txtSpecies.Name = "txtSpecies";
            txtSpecies.Size = new Size(145, 23);
            txtSpecies.TabIndex = 2;
            //
            // lblAge
            //
            lblAge.Location = new Point(24, 245);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(120, 23);
            lblAge.TabIndex = 18;
            lblAge.Text = "Age (years)";
            //
            // txtAge
            //
            txtAge.AccessibleName = "Age";
            txtAge.Location = new Point(154, 242);
            txtAge.Name = "txtAge";
            txtAge.PlaceholderText = "0 to 100";
            txtAge.Size = new Size(145, 23);
            txtAge.TabIndex = 3;
            //
            // lblRecoveryScore
            //
            lblRecoveryScore.Location = new Point(24, 290);
            lblRecoveryScore.Name = "lblRecoveryScore";
            lblRecoveryScore.Size = new Size(125, 23);
            lblRecoveryScore.TabIndex = 20;
            lblRecoveryScore.Text = "Recovery Score";
            //
            // txtRecoveryScore
            //
            txtRecoveryScore.AccessibleName = "Recovery Score";
            txtRecoveryScore.Location = new Point(154, 287);
            txtRecoveryScore.Name = "txtRecoveryScore";
            txtRecoveryScore.PlaceholderText = "0 to 100";
            txtRecoveryScore.Size = new Size(145, 23);
            txtRecoveryScore.TabIndex = 4;
            //
            // btnAdd
            //
            btnAdd.Location = new Point(24, 335);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 36);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add animal";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // lblHint
            //
            lblHint.Location = new Point(24, 430);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(275, 48);
            lblHint.TabIndex = 23;
            lblHint.Text = "All fields are required. Status and housing are calculated automatically.";
            //
            // lblClassification
            //
            lblClassification.Location = new Point(24, 490);
            lblClassification.Name = "lblClassification";
            lblClassification.Size = new Size(275, 90);
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
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAnimals.DefaultCellStyle = dataGridViewCellStyle2;
            dgvAnimals.Location = new Point(330, 107);
            dgvAnimals.MultiSelect = false;
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersVisible = false;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.Size = new Size(826, 438);
            dgvAnimals.TabIndex = 6;
            dgvAnimals.CellClick += dgvAnimals_CellClick;
            //
            // lblMode
            //
            lblMode.Location = new Point(24, 85);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(275, 20);
            lblMode.Text = "Add mode";
            //
            // btnSaveChanges
            //
            btnSaveChanges.Location = new Point(164, 335);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(135, 36);
            btnSaveChanges.TabIndex = 6;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.Enabled = false;
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            //
            // btnClear
            //
            btnClear.Location = new Point(24, 380);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(130, 36);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            //
            // lblSearchId
            //
            lblSearchId.Location = new Point(330, 575);
            lblSearchId.Name = "lblSearchId";
            lblSearchId.Size = new Size(95, 23);
            lblSearchId.Text = "Search by ID";
            //
            // txtSearchId
            //
            txtSearchId.Location = new Point(430, 572);
            txtSearchId.Name = "txtSearchId";
            txtSearchId.Size = new Size(160, 23);
            txtSearchId.TabIndex = 8;
            txtSearchId.AccessibleName = "Search Animal ID";
            txtSearchId.PlaceholderText = "WR-0001";
            //
            // btnSearch
            //
            btnSearch.Location = new Point(610, 568);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 32);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
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
            lblFeedback.BackColor = SystemColors.ControlLight;
            lblFeedback.Location = new Point(24, 687);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Padding = new Padding(8);
            lblFeedback.Size = new Size(1132, 43);
            lblFeedback.TabIndex = 27;
            lblFeedback.Text = "Enter the animal's details, then select Add animal.";
            //
            // MainForm
            //
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 750);
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
