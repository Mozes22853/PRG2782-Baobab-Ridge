using BaobabRidge.BusinessLogicLayer;

namespace BaobabRidge.PresentationLayer
{
    // Member 4's Delete handler shares the controls declared in MainForm.Designer.cs.
    public partial class MainForm
    {
        // Reads current file data, displays the statistics and overwrites the required report.
        private void btnGenerateSummary_Click(object? sender, EventArgs e)
        {
            if (!fileHandler.TryLoadAnimals(out List<Animal> animals, out int skipped, out string message))
            {
                txtSummary.Clear();
                ShowFeedback("Summary could not be generated. " + message, true);
                return;
            }
            AnimalSummary summary = new AnimalSummary(animals);
            string report = summary.BuildReport(DateTime.Now);
            txtSummary.Text = report;
            string skippedNotice = skipped > 0 ? " Skipped " + skipped + " malformed line(s)." : string.Empty;
            if (!fileHandler.TryWriteSummary(report, out message))
            {
                ShowFeedback("Summary is displayed but was not saved. " + message + skippedNotice, true);
                return;
            }
            ShowFeedback("Summary generated and saved to summary.txt beside the application." + skippedNotice, false);
        }

        // Validates the grid selection, asks permission and deletes one valid matching record.
        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvAnimals.SelectedRows.Count != 1
                || dgvAnimals.SelectedRows[0].DataBoundItem is not Animal animal)
            {
                ShowFeedback("Please select an animal row in the grid before clicking Delete.", true);
                return;
            }
            DialogResult answer = MessageBox.Show(
                "Delete " + animal.Id + " (" + animal.Name + ")? This cannot be undone.",
                "Confirm animal deletion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (answer != DialogResult.Yes)
            {
                ShowFeedback("Deletion cancelled. No animal was removed.", false);
                return;
            }
            if (!fileHandler.TryDeleteAnimal(animal, out string message))
            {
                ShowFeedback(message, true);
                return;
            }
            if (string.Equals(currentEditingId, animal.Id, StringComparison.OrdinalIgnoreCase))
            {
                SetEditMode(false);
                txtSearchId.Clear();
                lblClassification.Text = "No animal selected.";
            }
            RefreshAnimals();
            ShowFeedback("Deleted " + animal.Id + " (" + animal.Name + ") successfully.", false);
        }
    }
}
