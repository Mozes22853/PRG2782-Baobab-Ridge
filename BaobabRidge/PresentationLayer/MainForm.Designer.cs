namespace BaobabRidge.PresentationLayer
{
    partial class MainForm
    {
        private TextBox txtAnimalId = null!;
        private TextBox txtName = null!;
        private TextBox txtSpecies = null!;
        private TextBox txtAge = null!;
        private TextBox txtRecoveryScore = null!;
        private Button btnAdd = null!;
        private DataGridView dgvAnimals = null!;
        private Label lblFeedback = null!;
        private Label lblClassification = null!;
        private Label lblLoadNotice = null!;

        // Creates standard WinForms controls and connects the Add and Load events.
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            layout = new TableLayoutPanel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            grpAdd = new GroupBox();
            btnSaveChanges = new Button();
            btnClear = new Button();
            btnSearch = new Button();
            inputLayout = new TableLayoutPanel();
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
            lblHint = new Label();
            btnAdd = new Button();
            lblClassification = new Label();
            dgvAnimals = new DataGridView();
            lblLoadNotice = new Label();
            lblFeedback = new Label();
            layout.SuspendLayout();
            grpAdd.SuspendLayout();
            inputLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).BeginInit();
            SuspendLayout();
            // 
            // layout
            // 
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 354F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(lblTitle, 0, 0);
            layout.Controls.Add(lblSubtitle, 0, 1);
            layout.Controls.Add(grpAdd, 0, 2);
            layout.Controls.Add(dgvAnimals, 1, 2);
            layout.Controls.Add(lblLoadNotice, 0, 3);
            layout.Controls.Add(lblFeedback, 0, 4);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(0, 0);
            layout.Margin = new Padding(3, 4, 3, 4);
            layout.Name = "layout";
            layout.Padding = new Padding(23, 27, 23, 27);
            layout.RowCount = 5;
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 73F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 77F));
            layout.Size = new Size(1385, 936);
            layout.TabIndex = 0;
            // 
            // lblTitle
            // 
            layout.SetColumnSpan(lblTitle, 2);
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(26, 27);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1333, 53);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Baobab Ridge";
            // 
            // lblSubtitle
            // 
            layout.SetColumnSpan(lblSubtitle, 2);
            lblSubtitle.Dock = DockStyle.Fill;
            lblSubtitle.Location = new Point(26, 80);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(1333, 53);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Wildlife Rehabilitation Records";
            // 
            // grpAdd
            // 
            grpAdd.Controls.Add(btnSaveChanges);
            grpAdd.Controls.Add(btnClear);
            grpAdd.Controls.Add(btnSearch);
            grpAdd.Controls.Add(inputLayout);
            grpAdd.Dock = DockStyle.Fill;
            grpAdd.Location = new Point(23, 133);
            grpAdd.Margin = new Padding(0, 0, 18, 0);
            grpAdd.Name = "grpAdd";
            grpAdd.Padding = new Padding(14, 16, 14, 16);
            grpAdd.Size = new Size(336, 626);
            grpAdd.TabIndex = 2;
            grpAdd.TabStop = false;
            grpAdd.Text = "Add an animal";
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(217, 589);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(102, 33);
            btnSaveChanges.TabIndex = 3;
            btnSaveChanges.Text = "SaveChanges";
            btnSaveChanges.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(117, 589);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 33);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.TextAlign = ContentAlignment.TopCenter;
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(17, 589);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 33);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.TextAlign = ContentAlignment.TopCenter;
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // inputLayout
            // 
            inputLayout.ColumnCount = 1;
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            inputLayout.Controls.Add(lblAnimalId, 0, 0);
            inputLayout.Controls.Add(txtAnimalId, 0, 1);
            inputLayout.Controls.Add(lblName, 0, 2);
            inputLayout.Controls.Add(txtName, 0, 3);
            inputLayout.Controls.Add(lblSpecies, 0, 4);
            inputLayout.Controls.Add(txtSpecies, 0, 5);
            inputLayout.Controls.Add(lblAge, 0, 6);
            inputLayout.Controls.Add(txtAge, 0, 7);
            inputLayout.Controls.Add(lblRecoveryScore, 0, 8);
            inputLayout.Controls.Add(txtRecoveryScore, 0, 9);
            inputLayout.Controls.Add(lblHint, 0, 10);
            inputLayout.Controls.Add(btnAdd, 0, 11);
            inputLayout.Controls.Add(lblClassification, 0, 12);
            inputLayout.Dock = DockStyle.Fill;
            inputLayout.Location = new Point(14, 36);
            inputLayout.Margin = new Padding(3, 4, 3, 4);
            inputLayout.Name = "inputLayout";
            inputLayout.RowCount = 13;
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 67F));
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            inputLayout.Size = new Size(308, 574);
            inputLayout.TabIndex = 0;
            // 
            // lblAnimalId
            // 
            lblAnimalId.AutoSize = true;
            lblAnimalId.Location = new Point(3, 0);
            lblAnimalId.Name = "lblAnimalId";
            lblAnimalId.Size = new Size(75, 20);
            lblAnimalId.TabIndex = 0;
            lblAnimalId.Text = "Animal ID";
            // 
            // txtAnimalId
            // 
            txtAnimalId.AccessibleName = "Animal ID";
            txtAnimalId.Dock = DockStyle.Fill;
            txtAnimalId.Location = new Point(3, 37);
            txtAnimalId.Margin = new Padding(3, 4, 3, 4);
            txtAnimalId.Name = "txtAnimalId";
            txtAnimalId.PlaceholderText = "WR-0007";
            txtAnimalId.Size = new Size(302, 27);
            txtAnimalId.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 78);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.AccessibleName = "Name";
            txtName.Dock = DockStyle.Fill;
            txtName.Location = new Point(3, 115);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(302, 27);
            txtName.TabIndex = 1;
            // 
            // lblSpecies
            // 
            lblSpecies.AutoSize = true;
            lblSpecies.Location = new Point(3, 156);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new Size(59, 20);
            lblSpecies.TabIndex = 2;
            lblSpecies.Text = "Species";
            // 
            // txtSpecies
            // 
            txtSpecies.AccessibleName = "Species";
            txtSpecies.Dock = DockStyle.Fill;
            txtSpecies.Location = new Point(3, 193);
            txtSpecies.Margin = new Padding(3, 4, 3, 4);
            txtSpecies.Name = "txtSpecies";
            txtSpecies.Size = new Size(302, 27);
            txtSpecies.TabIndex = 2;
            // 
            // lblAge
            // 
            lblAge.AutoSize = true;
            lblAge.Location = new Point(3, 234);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(84, 20);
            lblAge.TabIndex = 3;
            lblAge.Text = "Age (years)";
            // 
            // txtAge
            // 
            txtAge.AccessibleName = "Age";
            txtAge.Dock = DockStyle.Fill;
            txtAge.Location = new Point(3, 271);
            txtAge.Margin = new Padding(3, 4, 3, 4);
            txtAge.Name = "txtAge";
            txtAge.PlaceholderText = "0 to 100";
            txtAge.Size = new Size(302, 27);
            txtAge.TabIndex = 3;
            // 
            // lblRecoveryScore
            // 
            lblRecoveryScore.AutoSize = true;
            lblRecoveryScore.Location = new Point(3, 312);
            lblRecoveryScore.Name = "lblRecoveryScore";
            lblRecoveryScore.Size = new Size(110, 20);
            lblRecoveryScore.TabIndex = 4;
            lblRecoveryScore.Text = "Recovery Score";
            // 
            // txtRecoveryScore
            // 
            txtRecoveryScore.AccessibleName = "Recovery Score";
            txtRecoveryScore.Dock = DockStyle.Fill;
            txtRecoveryScore.Location = new Point(3, 349);
            txtRecoveryScore.Margin = new Padding(3, 4, 3, 4);
            txtRecoveryScore.Name = "txtRecoveryScore";
            txtRecoveryScore.PlaceholderText = "0 to 100";
            txtRecoveryScore.Size = new Size(302, 27);
            txtRecoveryScore.TabIndex = 4;
            // 
            // lblHint
            // 
            lblHint.Dock = DockStyle.Fill;
            lblHint.Location = new Point(3, 390);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(302, 53);
            lblHint.TabIndex = 5;
            lblHint.Text = "All fields are required. Status and housing are calculated automatically.";
            // 
            // btnAdd
            // 
            btnAdd.Dock = DockStyle.Fill;
            btnAdd.Location = new Point(3, 447);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(302, 59);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add animal";
            btnAdd.Click += btnAdd_Click;
            // 
            // lblClassification
            // 
            lblClassification.Dock = DockStyle.Fill;
            lblClassification.Location = new Point(3, 510);
            lblClassification.Name = "lblClassification";
            lblClassification.Padding = new Padding(0, 16, 0, 0);
            lblClassification.Size = new Size(302, 64);
            lblClassification.TabIndex = 6;
            lblClassification.Text = "No animal added yet.";
            lblClassification.Click += lblClassification_Click;
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
            dgvAnimals.Dock = DockStyle.Fill;
            dgvAnimals.Location = new Point(380, 137);
            dgvAnimals.Margin = new Padding(3, 4, 3, 4);
            dgvAnimals.MultiSelect = false;
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersVisible = false;
            dgvAnimals.RowHeadersWidth = 51;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.Size = new Size(979, 618);
            dgvAnimals.TabIndex = 3;
            dgvAnimals.CellClick += dgvAnimals_CellClick;
            // 
            // lblLoadNotice
            // 
            layout.SetColumnSpan(lblLoadNotice, 2);
            lblLoadNotice.Dock = DockStyle.Fill;
            lblLoadNotice.Location = new Point(26, 759);
            lblLoadNotice.Name = "lblLoadNotice";
            lblLoadNotice.Padding = new Padding(0, 13, 0, 0);
            lblLoadNotice.Size = new Size(1333, 73);
            lblLoadNotice.TabIndex = 4;
            // 
            // lblFeedback
            // 
            lblFeedback.BackColor = SystemColors.ControlLight;
            layout.SetColumnSpan(lblFeedback, 2);
            lblFeedback.Dock = DockStyle.Fill;
            lblFeedback.Location = new Point(26, 832);
            lblFeedback.Name = "lblFeedback";
            lblFeedback.Padding = new Padding(11, 13, 11, 13);
            lblFeedback.Size = new Size(1333, 77);
            lblFeedback.TabIndex = 5;
            lblFeedback.Text = "Enter the animal's details, then select Add animal.";
            // 
            // MainForm
            // 
            AcceptButton = btnAdd;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1385, 936);
            Controls.Add(layout);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1277, 958);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Baobab Ridge Wildlife Rehabilitation";
            Load += MainForm_Load;
            layout.ResumeLayout(false);
            grpAdd.ResumeLayout(false);
            inputLayout.ResumeLayout(false);
            inputLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).EndInit();
            ResumeLayout(false);
        }
        private TableLayoutPanel layout;
        private Label lblTitle;
        private Label lblSubtitle;
        private GroupBox grpAdd;
        private TableLayoutPanel inputLayout;
        private Label lblAnimalId;
        private Label lblName;
        private Label lblSpecies;
        private Label lblAge;
        private Label lblRecoveryScore;
        private Label lblHint;
        private Button btnSaveChanges;
        private Button btnClear;
        private Button btnSearch;
    }
}
