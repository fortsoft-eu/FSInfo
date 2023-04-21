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
 * Version 1.1.1.1
 */

namespace FSInfo {

    /// <summary>
    /// Constants used in many places in the application.
    /// </summary>
    public static class Constants {

        /// <summary>
        /// The default width of the old AboutForm.
        /// </summary>
        public const int DefaultAboutFormWidth = 420;

        /// <summary>
        /// Windows API constant.
        /// </summary>
        public const int SC_CLOSE = 0xF060;

        /// <summary>
        /// Characters used in many places in the application code.
        /// </summary>
        public const char Colon = ':';
        public const char EnDash = '–';
        public const char Hyphen = '-';
        public const char Slash = '/';
        public const char Space = ' ';
        public const char VerticalTab = '\t';

        /// <summary>
        /// Strings used in many places in the application code.
        /// </summary>
        public const string ErrorLogEmptyString = "[Empty String]";
        public const string ErrorLogErrorMessage = "ERROR MESSAGE";
        public const string ErrorLogFileName = "Error.log";
        public const string ErrorLogNull = "[null]";
        public const string ErrorLogTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string ErrorLogWhiteSpace = "[White Space]";
        public const string ExtensionRtf = ".rtf";
    }
}
