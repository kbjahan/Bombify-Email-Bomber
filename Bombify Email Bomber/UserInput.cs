using System;
using System.IO;
using System.Threading;

namespace Bombify_Email_Bomber
{
    class UserInput
    {
        // GetSmtpInput method
        public static void GetSmtpInput()
        {
            SmtpTryAgain:

            // Display text
            Console.WriteLine("Smtp server list\nList:");

            // Get user input
            Variables.SmtpListPath = Console.ReadLine();

            // Check if smtp list exists
            if (!File.Exists(Variables.SmtpListPath))
            {
                // Show error message
                ShowErrorMessage("ERROR: Could not find smtp list, make sure the path spesifyed is correct");
                goto SmtpTryAgain;
            }

            // Check if smtp list is a txt file
            if (Path.GetExtension(Variables.SmtpListPath) == ".txt")
            {

            } else
            {
                // Show error message
                ShowErrorMessage("ERROR: You may only use txt files for the smtp server list");
                goto SmtpTryAgain;
            }

            Console.Clear();
        }

        // GetModeInput method
        public static void GetModeInput()
        {
            TryAgainMode:

            // Display text
            Console.WriteLine("1. Normal mode\n2. Annoy mode\n3. Annoy mode with random sender(needs smtp server support)\nMode:");

            try
            {
                // Get user input
                Variables.Mode = int.Parse(Console.ReadLine());
            } catch
            {
                // Show error message
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto TryAgainMode;
            }

            if (Variables.Mode > 3)
            {
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto TryAgainMode;
            }

            Console.Clear();
        }

        // GetSenderInput method
        public static void GetSenderInput()
        {
            SenderTryAgain:

            // Display text
            Console.WriteLine("1. Use default smtp server sender.\n2. Use own sender(needs smtp server support)\nSender:");

            // Try to read input
            try
            {
                // Get user input
                Variables.Choose = int.Parse(Console.ReadLine());
            } catch
            {
                // Show error message
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto SenderTryAgain;
            }

            // Check if choose's value is greater than 2
            if (Variables.Choose > 2)
            {
                // Show error message
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto SenderTryAgain;
            }

            // Check if user wants custom sender or not
            if (Variables.Choose == 1)
            {
                // Use custom sender
                Variables.Sender = "None";
            }
            else if (Variables.Choose == 2)
            {
                Console.Clear();
                TryAgainCustomSender:

                // Get user input
                Console.WriteLine("Choose custom sender\nSender:");
                Variables.Sender = Console.ReadLine();

                // Check if email is valid
                if (CheckEmailValidity(Variables.Sender) == false)
                {
                    // Show error message
                    ShowErrorMessage("ERROR: Invald email format");
                    goto TryAgainCustomSender;
                }
            }

            Console.Clear();
        }

        // GetTitleInput method
        public static void GetTitleInput()
        {
            TryAgainTitle:

            // Display text
            Console.WriteLine("Title:");

            // Get user input
            Variables.Title = Console.ReadLine();

            if (Variables.Title.Length < 1)
            {
                ShowErrorMessage("ERROR: Title cannot be empty");
                goto TryAgainTitle;
            }

            Console.Clear();
        }

        // GetBodyInput method
        public static void GetBodyInput()
        {
            TryAgainBody:

            // Display text
            Console.WriteLine("Body:");

            // Get user input
            Variables.Body = Console.ReadLine();

            if (Variables.Body.Length < 1)
            {
                ShowErrorMessage("ERROR: Title cannot be empty");
                goto TryAgainBody;
            }

            Console.Clear();
        }

        // GetToInput method
        public static void GetToInput()
        {
            TryAgainTo:

            // Display text
            Console.WriteLine("Send email to\nTo:");

            // Get user input
            Variables.To = Console.ReadLine();

            // Check if email is valid
            if (CheckEmailValidity(Variables.To) == false)
            {
                // Show error message
                ShowErrorMessage("ERROR: Invald email format");
                goto TryAgainTo;
            }

            Console.Clear();
        }

        // GetAttachmentInput method
        public static void GetAttachmentInput()
        {
            AttachmentTryAgain:

            // Display text 
            Console.WriteLine("Attachment Path(leave empty if not needed)\nPath:");

            // Get user input
            Variables.AttachmentPath = Console.ReadLine();

            // Check if user wants attachment or not
            if (Variables.AttachmentPath == "")
            {
                // Dont add attachment
                Variables.AttachmentPath = "None";
            }
            else
            {
                // Check if attachment exists
                if (!File.Exists(Variables.AttachmentPath))
                {
                    // Show error message
                    ShowErrorMessage("ERROR: Could not find file to attach, make sure the path spesifyed is correct");
                    goto AttachmentTryAgain;
                }

                // Get file info(size)
                FileInfo Fi = new FileInfo(Variables.AttachmentPath);
                long FileSize = Fi.Length;

                // Check if file size is greater than 2 MB
                if (FileSize > 2126840)
                {
                    // Show error message
                    ShowErrorMessage("ERROR: You may not use file sizes over 2 MB for attachments");
                    goto AttachmentTryAgain;
                }
            }

            Console.Clear();
        }

        // GetSslInput method
        public static void GetSslInput()
        {
            TryAgainSsl:

            // Display text
            Console.WriteLine("Use SSL\n1. Yes(needs smtp server support)\n2. No\nSSL:");

            try
            {
                // Get user input
                Variables.Ssl = int.Parse(Console.ReadLine());
            } catch
            {
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto TryAgainSsl;
            }

            if (Variables.Ssl > 2)
            {
                ShowErrorMessage("ERROR: You may only choose the above options");
                goto TryAgainSsl;
            }

            Console.Clear();
        }

        // GetThreadCount method
        public static void GetThreadCount()
        {
            ThreadsTryAgain:

            // Display text
            Console.WriteLine("Thread count(1 - 3)\nCount:");

            // Try to get user input
            try
            {
                // Get user input
                Variables.ThreadCount = int.Parse(Console.ReadLine());
            }
            catch
            {
                // Show error message
                ShowErrorMessage("ERROR: You can only choose between 1 - 3 threads");
                goto ThreadsTryAgain;
            }

            // Check if threadcount's value is greater than 3
            if (Variables.ThreadCount > 3)
            {
                // Show error message
                ShowErrorMessage("ERROR: You can only use a maximum of 3 threads");
                goto ThreadsTryAgain;
            }
        }

        // ShowErrorMessage method
        private static void ShowErrorMessage(string Message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            // Display error
            Console.WriteLine(Message);

            // Wait
            Thread.Sleep(2500);
            Console.Clear();
            Console.ResetColor();
        }

        // CheckEmailValidity method
        private static bool CheckEmailValidity(string email)
        {
            // Try to set new mail address
            try
            {
                // Set new mail address
                var addr = new System.Net.Mail.MailAddress(email);

                // Success, return true
                return addr.Address == email;
            }
            catch
            {
                // Failed, return false
                return false;
            }
        }
    }
}
