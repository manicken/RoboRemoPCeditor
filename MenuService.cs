using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using CommandID = System.ComponentModel.Design.CommandID;
using MenuCommand = System.ComponentModel.Design.MenuCommand;

namespace RoboRemoPC
{
    class MenuService : System.ComponentModel.Design.MenuCommandService
    {
        public delegate void OpenMenu(int x, int y);
        OpenMenu openMenuHandler;

        public MenuService(IServiceProvider serviceProvider, OpenMenu openMenuHandler) : base(serviceProvider)
        {
            this.openMenuHandler = openMenuHandler;
        }

        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            openMenuHandler(x, y);
        }
    }
}
