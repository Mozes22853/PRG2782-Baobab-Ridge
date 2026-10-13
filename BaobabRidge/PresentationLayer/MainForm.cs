using BaobabRidge.BusinessLogicLayer;
using BaobabRidge.DataLayer;

namespace BaobabRidge.PresentationLayer
{
    public partial class MainForm : Form
    {
        private readonly FileHandler fileHandler;

        // Creates the form and the handler for the file beside the executable.
        public MainForm()
        {
            InitializeComponent();
            fileHandler = new FileHandler();
        }

        // Loads the current file when the application opens.
        private void MainForm_Load(object? sender, EventArgs e)
        {
            RefreshAnimals();
            txtAnimalId.Focus();
        }

        // Validates, classifies, and saves one animal before refreshing the view.
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!AnimalValidator.TryCreateAnimal(txtAnimalId.Text, txtName.Text, txtSpecies.Text,
                txtAge.Text, txtRecoveryScore.Text, out Animal? animal,
                out string invalidField, out string message))
            {
                ShowFeedback(message, true);
                FocusInvalidField(invalidField);
                return;
            }

            AnimalClassifier.Classify(animal!);
            if (!fileHandler.TryAppendAnimal(animal!, out bool duplicateId, out message))
            {
                ShowFeedback(message, true);
                if (duplicateId)
                {
                    FocusInvalidField("Animal ID");
                }
                return;
            }

            // A successful append stays successful even if a later refresh cannot read the file.
            ClearInputs();
            lblClassification.Text = "Saved " + animal!.Id + Environment.NewLine
                + "Status: " + animal.Status + Environment.NewLine
                + "Housing Unit: " + animal.HousingUnit;
            ShowFeedback("Animal added successfully. Ready for the next animal.", false);
            RefreshAnimals();
        }

        // Replaces the grid data without accumulating rows; Member 2 can extend this integration point.
        private void RefreshAnimals()
        {
            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out int skippedLines, out string message))
            {
                lblLoadNotice.Text = "View could not be refreshed. " + message;
                return;
            }
            dgvAnimals.DataSource = animals;
            lblLoadNotice.Text = animals.Count + " animal(s) loaded."
                + (skippedLines > 0 ? " Skipped " + skippedLines + " malformed line(s)." : string.Empty);
        }

        // Clears only editable input boxes after a successful save.
        private void ClearInputs()
        {
            txtAnimalId.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtAnimalId.Focus();
        }

        // Shows a persistent, readable success or error message without a stack trace.
        private void ShowFeedback(string message, bool isError)
        {
            lblFeedback.ForeColor = isError ? Color.Maroon : Color.DarkGreen;
            lblFeedback.Text = message;
        }

        // Returns the cursor to the first invalid field so the user can correct it.
        private void FocusInvalidField(string field)
        {
            TextBox input = txtAnimalId;
            switch (field)
            {
                case "Name":
                    input = txtName;
                    break;
                case "Species":
                    input = txtSpecies;
                    break;
                case "Age":
                    input = txtAge;
                    break;
                case "Recovery Score":
                    input = txtRecoveryScore;
                    break;
            }
            input.Focus();
            input.SelectAll();
        }
    }
}
