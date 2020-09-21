using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace yTools.Windows.TaskBar
{
    public class TaskBarManger
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("shell32.dll")]
        public static extern uint SHAppBarMessage(uint dwMessage, ref AppBarData pData);

        /// <summary>
        ///     Set the Taskbar State option
        /// </summary>
        /// <param name="option">AppBarState to activate</param>
        public void SetTaskbarState(AppBarStates option)
        {
            AppBarData msgData = new AppBarData();
            msgData.cbSize = Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            msgData.lParam = (int)option;
            SHAppBarMessage((uint)TaskBarEnum.SetState, ref msgData);
        }

        /// <summary>
        ///     Gets the current Taskbar state
        /// </summary>
        /// <returns>current Taskbar state</returns>
        public AppBarStates GetTaskbarState()
        {
            AppBarData msgData = new AppBarData();
            msgData.cbSize = Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            return (AppBarStates)SHAppBarMessage((uint)TaskBarEnum.GetState, ref msgData);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AppBarData
        {
            public int cbSize; // initialize this field using: Marshal.SizeOf(typeof(AppBarData));
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public Rect rc;
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left, Top, Right, Bottom;

            public Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public Rect(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            public int X
            {
                get { return Left; }
                set
                {
                    Right -= Left - value;
                    Left = value;
                }
            }

            public int Y
            {
                get { return Top; }
                set
                {
                    Bottom -= Top - value;
                    Top = value;
                }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public Point Location
            {
                get { return new Point(Left, Top); }
                set
                {
                    X = value.X;
                    Y = value.Y;
                }
            }

            public Size Size
            {
                get { return new Size(Width, Height); }
                set
                {
                    Width = value.Width;
                    Height = value.Height;
                }
            }

            public static implicit operator Rectangle(Rect r)
            {
                return new Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator Rect(Rectangle r)
            {
                return new Rect(r);
            }

            public static bool operator ==(Rect r1, Rect r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(Rect r1, Rect r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(Rect r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                switch (obj)
                {
                    case Rect rect:
                        return Equals(rect);
                    case Rectangle rectangle:
                        return Equals(new Rect(rectangle));
                    default:
                        return false;
                }
            }

            public override int GetHashCode()
            {
                return ((Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top,
                    Right, Bottom);
            }
        }
    }
}
