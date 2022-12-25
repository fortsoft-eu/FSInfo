using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace FSInfo {
    public partial class MainForm : Form {
        private Form dialog;

        public MainForm(string filePath) {
            Text = Properties.Resources.CaptionInfo;
            Icon = Properties.Resources.Icon;

            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemAbout, new EventHandler(ShowAbout)));

            InitializeComponent();

            BuildContextMenu();

            richTextBox.LoadFile(filePath);
        }

        private void BuildContextMenu() {
            richTextBox.ContextMenu = new ContextMenu();
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemCopy, new EventHandler(Copy)));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemSelectAll, new EventHandler((sender, e) => richTextBox.SelectAll())));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemWordWrap, new EventHandler((sender, e) => richTextBox.WordWrap = !richTextBox.WordWrap)));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemAbout, new EventHandler(ShowAbout)));
            richTextBox.ContextMenu.Popup += new EventHandler((sender, e) => richTextBox.ContextMenu.MenuItems[4].Checked = richTextBox.WordWrap);
        }

        private void Copy(object sender, EventArgs e) {
            try {
                if (!string.IsNullOrWhiteSpace(richTextBox.SelectedText)) {
                    Clipboard.SetText(richTextBox.SelectedText.Trim());
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception);
                ErrorLog.WriteLine(exception);
                dialog = new MessageForm(this, exception.Message, Program.GetTitle() + Constants.Space + Constants.EnDash + Constants.Space + Properties.Resources.CaptionError, MessageForm.Buttons.OK, MessageForm.BoxIcon.Error);
                dialog.ShowDialog();
            }
        }

        private void GripStyle(object sender, EventArgs e) {
            SizeGripStyle = WindowState == FormWindowState.Normal ? SizeGripStyle.Show : SizeGripStyle.Hide;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                Close();
            } else if (e.Control && e.KeyCode == Keys.W) {
                richTextBox.WordWrap = !richTextBox.WordWrap;
            } else if (e.Control && e.KeyCode == Keys.C) {
                e.SuppressKeyPress = true;
                try {
                    if (!string.IsNullOrWhiteSpace(richTextBox.SelectedText)) {
                        Clipboard.SetText(richTextBox.SelectedText.Trim());
                    }
                } catch (Exception exception) {
                    Debug.WriteLine(exception);
                    ErrorLog.WriteLine(exception);
                    dialog = new MessageForm(this, exception.Message, Program.GetTitle() + Constants.Space + Constants.EnDash + Constants.Space + Properties.Resources.CaptionError, MessageForm.Buttons.OK, MessageForm.BoxIcon.Error);
                    dialog.ShowDialog();
                }
            }
        }

        private void SelectionChanged(object sender, EventArgs e) {
            richTextBox.ContextMenu.MenuItems[0].Visible = richTextBox.SelectedText.Trim().Length > 0;
            richTextBox.ContextMenu.MenuItems[2].Visible = richTextBox.SelectedText.Trim().Length != richTextBox.Text.Trim().Length || richTextBox.SelectionStart > 0;
            richTextBox.ContextMenu.MenuItems[1].Visible = richTextBox.ContextMenu.MenuItems[0].Visible && richTextBox.ContextMenu.MenuItems[2].Visible;
            richTextBox.ContextMenu.MenuItems[3].Visible = richTextBox.ContextMenu.MenuItems[0].Visible || richTextBox.ContextMenu.MenuItems[2].Visible;
        }

        private void ShowAbout(object sender, EventArgs e) {
            dialog = new AboutForm();
            dialog.ShowDialog();
        }

        private void FormActivated(object sender, EventArgs e) {
            if (dialog != null) {
                dialog.Activate();
            }
        }

        private void OpenHelp(object sender, HelpEventArgs hlpevent) {
            try {
                Process.Start(Properties.Resources.Website.TrimEnd(Constants.Slash).ToLowerInvariant() + Constants.Slash + Application.ProductName.ToLowerInvariant() + Constants.Slash);
            } catch (Exception exception) {
                Debug.WriteLine(exception);
                ErrorLog.WriteLine(exception);
                dialog = new MessageForm(this, exception.Message, Program.GetTitle() + Constants.Space + Constants.EnDash + Constants.Space + Properties.Resources.CaptionError, MessageForm.Buttons.OK, MessageForm.BoxIcon.Error);
                dialog.ShowDialog();
            }
        }
    }
}
