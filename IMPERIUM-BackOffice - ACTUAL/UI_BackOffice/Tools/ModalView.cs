using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_BackOffice.Tools
{
    public partial class ModalView : DevExpress.XtraEditors.XtraForm
    {
        public Color TitleBackColor = SystemColors.InactiveBorder;
        private bool isMaximized = false;


        public ModalView()//Size size
        {
            InitializeComponent();
            ShadowForm(this);
        }
        public void SetSize(Size size)
        {
            this.Size = size;
        }
        System.Drawing.Point normalLocation;
        System.Drawing.Size normalSize;
        private void ModalView_Load(object sender, EventArgs e)
        {
            title.Text = this.Text;
            this.normalSize = this.Size;
            SetTitleColors();
        }
        private void SetTitleColors()
        {
            pnlTopControl.BackColor = TitleBackColor;
            title.ForeColor = pnlTopControl.BackColor.GetBrightness() >= 0.8F ? Color.Black : Color.White;
        }
        void Maxmin()
        {
            if (isMaximized = !isMaximized)
            {
                this.Location = Screen.GetWorkingArea(this).Location;
                this.Size = Screen.GetWorkingArea(this).Size;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = normalSize;
                CenterLocation();
            }
        }
        void CenterLocation()
        {
            // Save values for future(for example, to center a form on next launch)
            int screen_x = Screen.FromControl(this).WorkingArea.X;
            int screen_y = Screen.FromControl(this).WorkingArea.Y;

            // Move it and center using correct screen/monitor
            this.Left = screen_x;
            this.Top = screen_y;
            this.Left += (Screen.FromControl(this).WorkingArea.Width - this.Width) / 2;
            this.Top += (Screen.FromControl(this).WorkingArea.Height - this.Height) / 2;
            normalLocation = new Point(this.Left, this.Top); //this.Location;
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
            this.Close();
        }

        private void cmdMaxim_Click(object sender, EventArgs e)
        {
            Maxmin();
        }

        private void cmdMinim_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ModalView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == (char)Keys.Escape)
                e.Handled = true;
        }
        #region Resize form
        // Constants for Windows messages
        private const int WM_NCHITTEST = 0x84;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;

        // Constants for the size of the resize border
        private const int RESIZE_BORDER_SIZE = 5;

        // Import the SendMessage function from user32.dll
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Import the ReleaseCapture and SetCursorPos functions from user32.dll
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                // Get the mouse cursor position in screen coordinates
                Point cursorPos = Cursor.Position;

                // Convert the mouse cursor position to form coordinates
                cursorPos = this.PointToClient(cursorPos);

                // Get the form's client rectangle
                Rectangle clientRect = this.ClientRectangle;

                // Create rectangles for each resize border area
                Rectangle leftRect = new Rectangle(0, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE, clientRect.Height - RESIZE_BORDER_SIZE * 2);
                Rectangle rightRect = new Rectangle(clientRect.Width - RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE, clientRect.Height - RESIZE_BORDER_SIZE * 2);
                Rectangle topRect = new Rectangle(RESIZE_BORDER_SIZE, 0, clientRect.Width - RESIZE_BORDER_SIZE * 2, RESIZE_BORDER_SIZE);
                Rectangle bottomRect = new Rectangle(RESIZE_BORDER_SIZE, clientRect.Height - RESIZE_BORDER_SIZE, clientRect.Width - RESIZE_BORDER_SIZE * 2, RESIZE_BORDER_SIZE);
                Rectangle topLeftRect = new Rectangle(0, 0, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE);
                Rectangle topRightRect = new Rectangle(clientRect.Width - RESIZE_BORDER_SIZE, 0, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE);
                Rectangle bottomLeftRect = new Rectangle(0, clientRect.Height - RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE);
                Rectangle bottomRightRect = new Rectangle(clientRect.Width - RESIZE_BORDER_SIZE, clientRect.Height - RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE, RESIZE_BORDER_SIZE);

                // Check if the cursor is in one of the resize border areas
                if (leftRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTLEFT;
                    return;
                }
                else if (rightRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTRIGHT;
                    return;
                }
                else if (topRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTTOP;
                    return;
                }
                else if (bottomRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTBOTTOM;
                    return;
                }
                else if (topLeftRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return;
                }
                else if (topRightRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return;
                }
                else if (bottomLeftRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return;
                }
                else if (bottomRightRect.Contains(cursorPos))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
            }

            base.WndProc(ref m);
        }
        private void ModalView_MouseDown(object sender, MouseEventArgs e)
        {
            // Check if the left mouse button was pressed
            if (e.Button == MouseButtons.Left)
            {
                // Release the mouse capture
                ReleaseCapture();

                // Set the cursor position to the top left corner of the form
                SetCursorPos(this.Left, this.Top);

                // Send the WM_NCLBUTTONDOWN message to the form
                SendMessage(this.Handle, 0xA1 /* WM_NCLBUTTONDOWN */, 2 /* HTCAPTION */, 0);
            }
        }
        #endregion Resize form

        #region Move form
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private void MoveForm(Form form)
        {
            ReleaseCapture();
            SendMessage(form.Handle, 0x112, 0xf012, 0);
        }
        private void title_MouseDown(object sender, MouseEventArgs e)
        {
            MoveForm(this);
        }
        #endregion Move form


        #region ShadowBOx form
        #region Fields

        //private readonly bool _isAeroEnabled = false;
        //private readonly bool _isDraggingEnabled = false;
        //#pragma warning disable IDE0051 // Remove unused private members
        //    private const int WM_NCHITTEST = 0x84;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int WS_MINIMIZEBOX = 0x20000;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int HTCLIENT = 0x1;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int HTCAPTION = 0x2;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int CS_DBLCLKS = 0x8;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int CS_DROPSHADOW = 0x00020000;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int WM_NCPAINT = 0x0085;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning disable IDE0051 // Remove unused private members
        private const int WM_ACTIVATEAPP = 0x001C;
#pragma warning restore IDE0051 // Remove unused private members

        #endregion

        #region Structures

        [EditorBrowsable(EditorBrowsableState.Never)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        #endregion

        #region Methods

        #region Public

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static bool IsCompositionEnabled()
        {
            if (Environment.OSVersion.Version.Major < 6) return false;

            bool enabled;
            DwmIsCompositionEnabled(out enabled);

            return enabled;
        }

        #endregion

        #region Private

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
         );

        private bool CheckIfAeroIsEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);

                return (enabled == 1) ? true : false;
            }
            return false;
        }

        #endregion

        #region Overrides

        private void ShadowForm(Form form)
        {
            var v = 2;
            DwmSetWindowAttribute(form.Handle, 2, ref v, 4);
            MARGINS margins = new MARGINS()
            {
                bottomHeight = 1,
                leftWidth = 0,
                rightWidth = 0,
                topHeight = 0
            };
            DwmExtendFrameIntoClientArea(form.Handle, ref margins);
        }



        #endregion

        #endregion

        #endregion  ShadowBOx form

        private void title_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                Maxmin();
            }
        }

        private void title_MouseDown_1(object sender, MouseEventArgs e)
        {
            MoveForm(this);
        }
    }
}