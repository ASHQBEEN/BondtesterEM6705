using System.ComponentModel;
using System.Reflection;

namespace ashqTech
{
    public enum AxisState : ushort
    {
        [Description("ОСЬ ВЫКЛЮЧЕНА")]
        STA_AX_DISABLE,
        [Description("ОСЬ ГОТОВА")]
        STA_AX_READY,
        [Description("ОСТАНОВКА")]
        STA_AX_STOPPING,
        [Description("ОШИБКА")]
        STA_AX_ERROR_STOP,
        [Description("БАЗИРОВАНИЕ")]
        STA_AX_HOMING,
        [Description("ДВИЖЕНИЕ В ТОЧКУ")]
        STA_AX_PTP_MOT,
        [Description("ПОСТОЯННОЕ ДВИЖЕНИЕ")]
        STA_AX_CONTI_MOT,
        [Description("СИНХРО ДВИЖЕНИЕ")]
        STA_AX_SYNC_MOT,
        STA_AX_EXT_JOG,
        STA_AX_EXT_MPG,
        [Description("ПАУЗА")]
        STA_AX_PAUSE,
        [Description("ОСЬ ЗАНЯТА")]
        STA_AX_BUSY,
        STA_AX_WAIT_DI,
        STA_AX_WAIT_PTP,
        STA_AX_WAIT_VEL,
        STA_AX_EXT_JOG_READY
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(AxisState value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
