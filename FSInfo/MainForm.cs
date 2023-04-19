/**
 * This is open-source software licensed under the terms of the MIT License.
 *
 * Copyright (c) 2020-2023 Petr Červinka - FortSoft <cervinka@fortsoft.eu>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 **
 * Version 1.1.1.0
 */

using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FSInfo {
    public partial class MainForm : Form {
        private Form dialog;

        public MainForm(string filePath) {
            Icon = Properties.Resources.Icon;
            Text = Properties.Resources.CaptionInfo;

            ContextMenu = new ContextMenu();
            ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemAbout,
                new EventHandler(ShowAbout)));

            InitializeComponent();
            BuildContextMenu();

            richTextBox.LoadFile(filePath);
        }

        private void BuildContextMenu() {
            richTextBox.ContextMenu = new ContextMenu();
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemCopy,
                new EventHandler(Copy)));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemSelectAll,
                new EventHandler((sender, e) => richTextBox.SelectAll())));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemWordWrap,
                new EventHandler((sender, e) => richTextBox.WordWrap = !richTextBox.WordWrap)));
            richTextBox.ContextMenu.MenuItems.Add(Constants.Hyphen.ToString());
            richTextBox.ContextMenu.MenuItems.Add(new MenuItem(Properties.Resources.MenuItemAbout,
                new EventHandler(ShowAbout)));
            richTextBox.ContextMenu.Popup += new EventHandler((sender, e) => {
                richTextBox.ContextMenu.MenuItems[4].Checked = richTextBox.WordWrap;
            });
        }

        private void Copy(object sender, EventArgs e) {
            try {
                if (!string.IsNullOrWhiteSpace(richTextBox.SelectedText)) {
                    Clipboard.SetText(richTextBox.SelectedText.Trim());
                }
            } catch (Exception exception) {
                ShowException(exception);
            }
        }

        private void GripStyle(object sender, EventArgs e) {
            SizeGripStyle = WindowState.Equals(FormWindowState.Normal) ? SizeGripStyle.Show : SizeGripStyle.Hide;
        }

        private void KeyDownHandler(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.Escape)) {
                Close();
            } else if (e.Control && e.KeyCode.Equals(Keys.W)) {
                richTextBox.WordWrap = !richTextBox.WordWrap;
            } else if (e.Control && e.KeyCode.Equals(Keys.C)) {
                e.SuppressKeyPress = true;
                Copy(sender, e);
            }
        }

        private void SelectionChanged(object sender, EventArgs e) {
            richTextBox.ContextMenu.MenuItems[0].Visible = richTextBox.SelectedText.Trim().Length > 0;
            richTextBox.ContextMenu.MenuItems[2].Visible = !richTextBox.SelectedText.Trim().Length.Equals(richTextBox.Text.Trim().Length)
                || richTextBox.SelectionStart > 0;
            richTextBox.ContextMenu.MenuItems[1].Visible = richTextBox.ContextMenu.MenuItems[0].Visible
                && richTextBox.ContextMenu.MenuItems[2].Visible;
            richTextBox.ContextMenu.MenuItems[3].Visible = richTextBox.ContextMenu.MenuItems[0].Visible
                || richTextBox.ContextMenu.MenuItems[2].Visible;
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

        private void OpenHelp(object sender, EventArgs e) {
            if (InvokeRequired) {
                Invoke(new EventHandler(OpenHelp), sender, e);
            } else {
                try {
                    StringBuilder url = new StringBuilder()
                        .Append(Properties.Resources.Website.TrimEnd(Constants.Slash).ToLowerInvariant())
                        .Append(Constants.Slash)
                        .Append(Application.ProductName.ToLowerInvariant())
                        .Append(Constants.Slash);
                    Process.Start(url.ToString());
                } catch (Exception exception) {
                    ShowException(exception);
                }
            }
        }

        private void ShowException(Exception exception) {
            Debug.WriteLine(exception);
            ErrorLog.WriteLine(exception);
            StringBuilder title = new StringBuilder()
                .Append(Program.GetTitle())
                .Append(Constants.Space)
                .Append(Constants.EnDash)
                .Append(Constants.Space)
                .Append(Properties.Resources.CaptionError);
            dialog = new MessageForm(this, exception.Message, title.ToString(), MessageForm.Buttons.OK, MessageForm.BoxIcon.Error);
            dialog.HelpRequested += new HelpEventHandler(OpenHelp);
            dialog.ShowDialog(this);
        }
    }
}
