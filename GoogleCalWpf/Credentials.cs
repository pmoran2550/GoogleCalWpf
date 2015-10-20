using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Security.Cryptography;

namespace Schuss.GoogleCalWpf
{
    public class Credentials
    {
        private const string ApplicationFolderName = "Schuss.GoogleCalWpf";
        private const string ClientCredentialsFileName = "client.dat";
        private const string FileKeyApiKey = "ProtectedApiKey";
        private const string FileKeyClientId = "ProtectedClientId";
        private const string FileKeyClientSecret = "ProtectedClientSecret";

        private const string PromptCreate = "You need to input your Google API information (you can find your key " +
            "at https://code.google.com/apis/console/#:access).";

        public Credentials()
        {

        }

        private static FileInfo CredentialsFile
        {
            get
            {
                string applicationDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string googleAppDirectory = Path.Combine(applicationDate, ApplicationFolderName);
                var directoryInfo = new DirectoryInfo(googleAppDirectory);
                if (directoryInfo.Exists == false)
                {
                    directoryInfo.Create();
                }
                return new FileInfo(Path.Combine(googleAppDirectory, ClientCredentialsFileName));
            }
        }

        /// <summary>
        ///     Returns a IDictionary of keys and values from the CredentialsFile which is expected to be of the form
        ///     <example>
        ///       key=value
        ///       key2=value2
        ///     </example>
        /// </summary>
        private IDictionary<string, string> ParseFile()
        {
            var parsedValues = new Dictionary<string, string>(5);
            using (StreamReader sr = CredentialsFile.OpenText())
            {
                string currentLine = sr.ReadLine();
                while (currentLine != null)
                {
                    int firstEquals = currentLine.IndexOf('=');
                    if (firstEquals > 0 && firstEquals + 1 < currentLine.Length)
                    {
                        string key = currentLine.Substring(0, firstEquals);
                        string value = currentLine.Substring(firstEquals + 1);
                        parsedValues.Add(key, value);
                    }
                    currentLine = sr.ReadLine();
                }
            }

            return parsedValues;
        }

        /// <summary>
        /// By prompting the user this constructs <code>FullClientCredentials</code> and stores them in the 
        /// <code>CredentialsFile</code>
        /// </summary>
        public FullClientCredentials CreateFullClientCredentials(bool isExtension)
        {
            FullClientCredentials fullCredentials = new FullClientCredentials();

            CredentialDialog credDialog = new CredentialDialog();
            credDialog.Closed += credDialog_Closed;
            credDialog.ShowDialog();

            //FullClientCredentials fullCredentials = CommandLine.CreateClassFromUserinput<FullClientCredentials>();
            fullCredentials.ApiKey = credDialog.ViewModel.APIID;
            fullCredentials.ClientId = credDialog.ViewModel.ClientID;
            fullCredentials.ClientSecret = credDialog.ViewModel.ClientSecret;

            using (FileStream fStream = CredentialsFile.OpenWrite())
            {
                using (TextWriter tw = new StreamWriter(fStream))
                {
                    tw.WriteLine("{0}={1}", FileKeyApiKey, Protect(fullCredentials.ApiKey));
                    tw.WriteLine("{0}={1}", FileKeyClientId, Protect(fullCredentials.ClientId));
                    tw.WriteLine("{0}={1}", FileKeyClientSecret, Protect(fullCredentials.ClientSecret));
                }
            }
            return fullCredentials;
        }

        void credDialog_Closed(object sender, EventArgs e)
        {
            CredentialDialog dialog = (CredentialDialog)sender;

            if (dialog != null)
            {

            }
        }

        /// <summary>
        /// Encrypts the clearText using the current users key, this prevents other users being able to read this
        /// but does not stop the current user from reading this.
        /// </summary>
        private string Protect(string clearText)
        {
            byte[] encryptedData = ProtectedData.Protect(
                Encoding.ASCII.GetBytes(clearText), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// The inverse of <code>Protect</code> this decrypts the passed-in string.
        /// </summary>
        private string Unprotect(string encrypted)
        {
            byte[] encryptedData = Convert.FromBase64String(encrypted);
            byte[] clearText = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            return Encoding.ASCII.GetString(clearText);
        }

        /// <summary>
        /// Fetches the users ApiKey, ClientId and ClientSecreat either from local disk or 
        /// prompts the user in the command line.
        /// </summary>
        public FullClientCredentials EnsureFullClientCredentials(bool useExisting)
        {
            if (CredentialsFile.Exists == false || useExisting == false)
            {
                return CreateFullClientCredentials(false);
            }

            IDictionary<string, string> values = ParseFile();
            if (values.ContainsKey(FileKeyApiKey) == false ||
                values.ContainsKey(FileKeyClientId) == false ||
                values.ContainsKey(FileKeyClientSecret) == false)
            {
                return CreateFullClientCredentials(true);
            }

            return new FullClientCredentials()
            {
                ApiKey = Unprotect(values[FileKeyApiKey]),
                ClientId = Unprotect(values[FileKeyClientId]),
                ClientSecret = Unprotect(values[FileKeyClientSecret])
            };

        }

        /// <summary>
        /// Removes the stored credentials from this computer
        /// </summary>
        public void ClearClientCredentials()
        {
            FileInfo clientCredentials = CredentialsFile;
            if (clientCredentials.Exists)
            {
                clientCredentials.Delete();
            }
        }
        public class FullClientCredentials : SimpleClientCredentials
        {
            public string ClientId;
            public string ClientSecret;
        }
        public class SimpleClientCredentials
        {
            public string ApiKey;
        }
    }
}
