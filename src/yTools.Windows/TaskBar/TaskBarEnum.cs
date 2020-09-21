namespace yTools.Windows.TaskBar
{
    public enum TaskBarEnum
    {
        New = 0x00,
        Remove = 0x01,
        QueryPos = 0x02,
        SetPos = 0x03,
        GetState = 0x04,
        GetTaskBarPos = 0x05,
        Activate = 0x06,
        GetAutoHideBar = 0x07,
        SetAutoHideBar = 0x08,
        WindowPosChanged = 0x09,
        SetState = 0x0a
    }

    public enum AppBarStates
    {
        AlwaysOnTop = 0x00,
        AutoHide = 0x01
    }
}
