using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ActionCenter
{
    internal class Program
    {
        public static ContextMenu menu;
        public static MenuItem mnuExit;
        private static MenuItem madeBy;
        public static NotifyIcon notificationIcon;

        static void Main(string[] args)
        {
            Thread notifyThread = new Thread(
                delegate ()
                {
                    menu = new ContextMenu();
                    mnuExit = new MenuItem("Exit");
                    madeBy = new MenuItem("Made by lxvdev");
                    menu.MenuItems.Add(0, mnuExit);
                    menu.MenuItems.Add(0, madeBy);

                    notificationIcon = new NotifyIcon()
                    {
                        Icon = ActionCenter.Properties.Resources.Icon,
                        ContextMenu = menu,
                        Text = "Action centre"
                    };
                    notificationIcon.Click += new EventHandler(OpenCentre);
                    mnuExit.Click += new EventHandler(mnuExit_Click);

                    notificationIcon.Visible = true;
                    Application.Run();
                }
            );
            notifyThread.Start();
            Console.ReadLine();
        }

        private static void OpenCentre(object sender, EventArgs e)
        {
            Process.Start("ms-actioncenter:");
        }

        static void mnuExit_Click(object sender, EventArgs e)
        {
            notificationIcon.Dispose();
            Application.Exit();
        }
    }
}
