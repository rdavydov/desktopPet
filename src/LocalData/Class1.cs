﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.ApplicationModel;

namespace LocalData
{
    public class LocalData
    {
        static Windows.Storage.ApplicationDataContainer LocalSettings = null;
        static Windows.Storage.StorageFolder LocalFolder = null;

        private static string Images = "";
        private static string Icon = "";
        private static string Xml = "";
        private static double Volume = 0.3;
        private static bool WinForeGround = false;
        private static int AutostartPets = 1;
        private static bool MultiScreenEnabled = true;
        private static bool FirstBoot = false;
        private static bool Developer = false;
        private static string DeveloperPets = "";

        private static readonly String GITHUB_FOLDER = "https://raw.githubusercontent.com/Adrianotiger/desktopPet/master";
        public static readonly String GITHUB_PETDOCS = "https://adrianotiger.github.io/desktopPet/Pets/";
        private static readonly String GITHUB_PETFOLDER = "/Pets/";
        //public static readonly String GITHUB_APITREE = "https://api.github.com/repos/Adrianotiger/desktopPet/git/trees/9769cf227eaf8322c028d2be2a9671d692b9f293"; <<- can't be used without token/login

        public LocalData()
        {
            DeveloperPets = GITHUB_FOLDER + GITHUB_PETFOLDER + "pets.json";

            if (LocalSettings == null)
            {
                LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                LocalFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                if (!LocalSettings.Values.ContainsKey("MultiScreen"))
                    FirstBoot = true;

                if (!LocalSettings.Values.ContainsKey("CurrentPet"))
                    LocalSettings.Values["CurrentPet"] = "esheep64";

                if (LocalSettings.Values.ContainsKey("Volume"))
                    Volume = (double)LocalSettings.Values["Volume"];
                else
                    LocalSettings.Values["Volume"] = Volume;

                if (LocalSettings.Values.ContainsKey("WinForeGround"))
                    WinForeGround = (bool)LocalSettings.Values["WinForeGround"];
                else
                    LocalSettings.Values["WinForeGround"] = WinForeGround;

                if (LocalSettings.Values.ContainsKey("AutostartPets"))
                    AutostartPets = (int)LocalSettings.Values["AutostartPets"];
                else
                    LocalSettings.Values["AutostartPets"] = AutostartPets;

                if (LocalSettings.Values.ContainsKey("MultiScreen"))
                    MultiScreenEnabled = (bool)LocalSettings.Values["MultiScreen"];
                else
                    LocalSettings.Values["MultiScreen"] = MultiScreenEnabled;

                LoadXML();
                LoadImages();
                LoadIcon();
            }
        }

        public bool IsFirstBoot()
        {
            return FirstBoot;
        }

        public void SetVolume(double volume)
        {
            if (volume != Volume && Math.Abs(volume - Volume) > 0.04)
            {
                Volume = volume;
                LocalSettings.Values["Volume"] = volume;
            }
        }

        public double GetVolume()
        {
            return Volume;
        }

        public void SetMultiscreen(bool enable)
        {
            if (enable != MultiScreenEnabled)
            {
                MultiScreenEnabled = enable;
                LocalSettings.Values["MultiScreen"] = enable;
            }
        }

        public bool GetMultiscreen()
        {
            return MultiScreenEnabled;
        }

        public void SetWindowForeground(bool setOnCollision)
        {
            if (WinForeGround != setOnCollision)
            {
                WinForeGround = setOnCollision;
                LocalSettings.Values["WinForeground"] = setOnCollision;
            }
        }

        public bool GetWindowForeground()
        {
            return WinForeGround;
        }

        public void SetAutoStartPets(int startingPets)
        {
            if (AutostartPets != startingPets)
            {
                AutostartPets = startingPets;
                LocalSettings.Values["AutostartPets"] = startingPets;
            }
        }

        public int GetAutoStartPets()
        {
            return AutostartPets;
        }

        public void SetXml(string newXml, string folder)
        {
            if (Xml != newXml)
            {
                Xml = newXml;

                var buffer = Encoding.UTF8.GetBytes(newXml);
                File.Delete(LocalFolder.Path + "\\animations.xml");

                var f = File.Create(LocalFolder.Path + "\\animations.xml");
                f.Write(buffer, 0, buffer.Length);
                f.Close();

                LocalSettings.Values["CurrentPet"] = folder;
            }
        }

        public string GetCurrentPet()
        {
            return LocalSettings.Values["CurrentPet"].ToString();
        }

        public string GetXml()
        {
            return Xml;
        }

        public void LoadXML()
        {
            var buffer = new Byte[1024 * 64];
            if (!File.Exists(LocalFolder.Path + "\\animations.xml"))
            {
                var fs = File.Create(LocalFolder.Path + "\\animations.xml");
                fs.Close();
            }
            Xml = "";
            var f = File.OpenRead(LocalFolder.Path + "\\animations.xml");
            var bytesRead = 0;
            do
            {
                bytesRead = f.Read(buffer, 0, 1024 * 64);
                Xml += Encoding.UTF8.GetString(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            f.Close();
        }

        public void SetIcon(string newIcon)
        {
            Icon = newIcon;

            var buffer = Encoding.UTF8.GetBytes(newIcon);
            File.Delete(LocalFolder.Path + "\\icon.xml");
            var f = File.Create(LocalFolder.Path + "\\icon.xml");
            f.Write(buffer, 0, buffer.Length);
            f.Close();
        }

        public string GetIcon()
        {
            return Icon;
        }

        private void LoadIcon()
        {
            var buffer = new Byte[1024 * 64];
            if (!File.Exists(LocalFolder.Path + "\\icon.xml"))
            {
                var fs = File.Create(LocalFolder.Path + "\\icon.xml");
                fs.Close();
            }
            Icon = "";
            var f = File.OpenRead(LocalFolder.Path + "\\icon.xml");
            var bytesRead = 0;
            do
            {
                bytesRead = f.Read(buffer, 0, 1024 * 64);
                Icon += Encoding.UTF8.GetString(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            f.Close();
        }

        public void SetImages(string newImages)
        {
            Images = newImages;

            var buffer = Encoding.UTF8.GetBytes(newImages);
            File.Delete(LocalFolder.Path + "\\images.xml");
            var f = File.Create(LocalFolder.Path + "\\images.xml");
            f.Write(buffer, 0, buffer.Length);
            f.Close();
        }

        public string GetImages()
        {
            return Images;
        }

        private void LoadImages()
        {
            var buffer = new Byte[1024 * 64];
            if (!File.Exists(LocalFolder.Path + "\\images.xml"))
            {
                var fs = File.Create(LocalFolder.Path + "\\images.xml");
                fs.Close();
            }
            Images = "";
            var f = File.OpenRead(LocalFolder.Path + "\\images.xml");
            var bytesRead = 0;
            do
            {
                bytesRead = f.Read(buffer, 0, 1024 * 64);
                Images += Encoding.UTF8.GetString(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            f.Close();
        }

        public bool NeedToLoadNew(string petFolder, DateTimeOffset lastUpdate)
        {
            if(LocalSettings.Values.ContainsKey("pet_" + petFolder))
            {
                return ((DateTimeOffset)LocalSettings.Values["pet_" + petFolder] < lastUpdate);
            }
            else
            {
                return true;
            }
        }

        public void SavePetXML(string xml, string petName, DateTime lastUpdate)
        {
            var buffer = Encoding.UTF8.GetBytes(xml);
            var f = File.OpenWrite(LocalFolder.Path + "\\pet_" + petName + ".xml");
            f.Write(buffer, 0, buffer.Length);
            f.Close();
            DateTimeOffset dto = lastUpdate;
            LocalSettings.Values["pet_" + petName] = dto;
        }

        public string GetPetXML(string petName)
        {
            string retXML = "";
            var buffer = new Byte[1024 * 64];
            var f = File.OpenRead(LocalFolder.Path + "\\pet_" + petName + ".xml");
            var bytesRead = 0;
            do
            {
                bytesRead = f.Read(buffer, 0, 1024 * 64);
                retXML += Encoding.UTF8.GetString(buffer, 0, bytesRead);
            } while (bytesRead > 0);
            f.Close();
            return retXML;
        }

        public delegate void MyFunction(object source, FileSystemEventArgs e);

        public void ListenOnXMLChanged(MyFunction f)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = LocalFolder.Path;
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "animations.xml";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(f);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        public void SetDeveloper(bool isDev)
        {
            Developer = isDev;
        }

        public bool IsDeveloper()
        {
            return Developer;
        }

        public void SetDeveloperGithubPets(string url)
        {
            DeveloperPets = url;
        }

        public string GetDeveloperGitHubPets()
        {
            return DeveloperPets;
        }

        public string GetGitHubPetListFile()
        {
            if(Developer)
            {
                return DeveloperPets;
            }
            else
            {
                return GITHUB_FOLDER + "/Pets/pets.json";
            }
        }

        public string GetGitHubPetFile(string petId)
        {
            if (Developer)
            {
                return DeveloperPets.Remove(DeveloperPets.LastIndexOf("/") + 1) + petId + "/animations.xml";
            }
            else
            {
                return GITHUB_FOLDER + "/Pets/" + petId + "/animations.xml";
            }
        }
    }

}
