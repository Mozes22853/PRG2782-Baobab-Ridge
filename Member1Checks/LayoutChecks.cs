using BaobabRidge.PresentationLayer;

internal static partial class Program
{
    // Verifies that font auto-scaling keeps the grid and its actions separate and scrollable.
    private static void CheckScaledLayout(string? imagePath)
    {
        Application.EnableVisualStyles();
        using (MainForm form = new MainForm())
        {
            form.ShowInTaskbar = false;
            form.Show();
            Application.DoEvents();
            DataGridView grid = (DataGridView)form.Controls.Find("dgvAnimals", true)[0];
            Control[] actions = new[] { "lblSearchId", "txtSearchId", "btnSearch", "btnDelete" }
                .Select(name => form.Controls.Find(name, true)[0]).ToArray();
            Button delete = (Button)actions[3];
            Check(delete.Visible && delete.Enabled && delete.Text == "Delete selected", "Delete remains visible and usable after designer merges");
            grid.ClearSelection();
            delete.PerformClick();
            Label feedback = (Label)form.Controls.Find("lblFeedback", true)[0];
            Check(feedback.Text.Contains("Please select an animal row"), "Delete click remains connected to the selection validation handler");
            foreach (Control action in actions)
            {
                Check(action.Parent == form && action.Dock == DockStyle.None, action.Name + " remains an ordinary designer control");
                Check(action.Top >= grid.Bottom && !action.Bounds.IntersectsWith(grid.Bounds), action.Name + " stays below the scaled grid");
            }
            for (int i = 0; i < actions.Length; i++)
            {
                for (int j = i + 1; j < actions.Length; j++)
                {
                    Check(!actions[i].Bounds.IntersectsWith(actions[j].Bounds), actions[i].Name + " does not overlap " + actions[j].Name);
                }
            }
            Check(form.AutoScroll, "Small windows support scrolling instead of hiding controls");
            Check(grid.Rows.Count >= 8 && grid.Columns.Count == 7, "Starter records and all seven columns still load at the larger font");
            Check(grid.ColumnHeadersHeight >= grid.Font.Height + 4, "Column headers grow to fit the scaled font");
            Check(grid.Rows[0].InheritedStyle.Font!.Size == grid.Font.Size, "Grid cells inherit the scaled font");
            if (imagePath != null)
            {
                using (Bitmap image = new Bitmap(form.Width, form.Height))
                {
                    form.DrawToBitmap(image, new Rectangle(0, 0, form.Width, form.Height));
                    image.Save(imagePath);
                }
            }
            form.ClientSize = new Size(800, 500);
            Application.DoEvents();
            Check(form.HorizontalScroll.Visible && form.VerticalScroll.Visible, "Reduced window exposes both scrollbars when content does not fit");
            form.ScrollControlIntoView(actions[3]);
            Application.DoEvents();
            Check(form.ClientRectangle.Contains(actions[3].Bounds), "Delete can be scrolled fully into view in a smaller window");
            form.Close();
        }
    }
}
