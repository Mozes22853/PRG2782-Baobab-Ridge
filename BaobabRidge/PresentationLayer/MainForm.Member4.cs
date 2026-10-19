using BaobabRidge.BusinessLogicLayer;

namespace BaobabRidge.PresentationLayer
{
    // Member 4's Delete feature is separate from the existing Add/View/Update handlers.
    public partial class MainForm
    {
        private Button btnDelete = null!;

        // Shortens the grid and places the Delete action alongside the existing search controls.
        private void InitializeMember4Controls()
        {
            dgvAnimals.Height = 295;
            lblSearchId.Top = 423;
            txtSearchId.Top = 420;
            btnSearch.Top = 416;

            btnDelete = new Button
            {
                Name = "btnDelete",
                Text = "Delete selected",
                Location = new Point(750, 416),
                Size = new Size(135, 34),
                TabIndex = 10,
                UseVisualStyleBackColor = true
            };
            btnDelete.Click += btnDelete_Click;
            Controls.Add(btnDelete);
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
