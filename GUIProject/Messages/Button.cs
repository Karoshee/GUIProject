using System.ComponentModel;

namespace GUIProject
{
    public enum Button
    {
        [Description("Ok")]
        Ok = 0,
        [Description("Отмена")]
        Cancel = 1,
        [Description("Да")]
        Yes = 2,
        [Description("Нет")]
        No = 3,
        [Description("Принять")]
        Accept = 4,
        [Description("Отклонить")]
        Decline = 5
    }
}
