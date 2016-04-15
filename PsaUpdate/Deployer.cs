using PsaUpdate.Properties;
using System;
using System.IO;
using System.Security.AccessControl;

namespace PsaUpdate
{
    class Deployer
    {
        private string oldFolderName = null;
        private string newSubFolderName = null;
        private string newFolderName = null;
        private string oldFileName = null;
        private string newFileName = null;

        public Deployer()
        {
            oldFolderName = SharedComponentsDir();
            newSubFolderName = "PSAIO_BAK";
            newFolderName = Path.Combine(oldFolderName, newSubFolderName);
            oldFileName = "PSAIO.dll";
            newFileName = "PSAIO_v12.dll";
        }

        public string OldFolderName
        {
            get { return oldFolderName; }
        }

        public string Run()
        {

            if (!Helper.IsAdministrator()) return "No administrator privileges associated with the current profile!";

            string oldFullFileName = Path.Combine(oldFolderName,oldFileName);
            string newFullFileName = Path.Combine(newFolderName,newFileName);

            string ProcessMessage = null;
 
            DirectorySecurity ds = new DirectorySecurity();
            ds.AddAccessRule(new FileSystemAccessRule(@"everyone",FileSystemRights.FullControl,AccessControlType.Allow));


            try
            {
                if (Directory.Exists(newFolderName)) Directory.Delete(newFolderName,true);

                DirectoryInfo destFolderName = Directory.CreateDirectory(newFolderName);

                if (File.Exists(oldFullFileName)) File.Move(oldFullFileName, newFullFileName);
                File.WriteAllBytes(oldFullFileName, Resources.PSAIO);

                ProcessMessage = "success";

            }
            catch (Exception e)
            {
                ProcessMessage = "failure (" + e.Message + ")";
            }

            return ProcessMessage;

        }

        public bool IsValidSharedComponentsDir()
        {
            if (Directory.Exists(this.oldFolderName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string SharedComponentsDir()
        {
            if (IsX64Arch())
                return @"C:\Program Files (x86)\ACNielsen\Shared Components";
            else
                return
                    @"C:\Program Files\ACNielsen\Shared Components"; 
        }
        public static bool IsX64Arch()
        {
            return Environment.Is64BitOperatingSystem;

            //if (string.Equals(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"),"AMD64",StringComparison.CurrentCultureIgnoreCase))
            //    return true;
            //else
            //    return false;
        }

        public static string IsX64ArchToString()
        {
            if (IsX64Arch())
                return "64 bit";
            else
                return "32 bit";

            //if (string.Equals(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"),"AMD64",StringComparison.CurrentCultureIgnoreCase))
            //    return true;
            //else
            //    return false;
        }

    }
}
