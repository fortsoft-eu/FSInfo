using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FSInfo {
    public static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT) {
                MessageBox.Show(Properties.Resources.MessageApplicationCannotRun, GetTitle() + Constants.Space + Constants.EnDash + Constants.Space + Properties.Resources.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try {
                string filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Application.ProductName + Constants.ExtensionRtf);
                if (File.Exists(filePath)) {
                    Application.Run(new MainForm(filePath));
                } else {
                    MessageBox.Show(string.Format(Properties.Resources.MessageCannotOpen, Path.GetFileName(filePath)), Properties.Resources.CaptionInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch (Exception exception) {
                Debug.WriteLine(exception);
                ErrorLog.WriteLine(exception);
                MessageBox.Show(exception.Message, GetTitle() + Constants.Space + Constants.EnDash + Constants.Space + Properties.Resources.CaptionError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Properties.Resources.MessageApplicationError, GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static string GetTitle() {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            string title = null;
            if (attributes.Length > 0) {
                AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)attributes[0];
                title = assemblyTitleAttribute.Title;
            }
            return string.IsNullOrEmpty(title) ? Application.ProductName : title;
        }

        public static bool IsDebugging {
            get {
                bool isDebugging = false;
                Debugging(ref isDebugging);
                return isDebugging;
            }
        }

        [Conditional("DEBUG")]
        private static void Debugging(ref bool isDebugging) {
            isDebugging = true;
        }
    }
}
