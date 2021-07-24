using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GUIProject
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            DescriptionAttribute description = value.GetType().GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>();
            if (description is not null)
                return description.Description;
            return "";
        }

        public static ColorScheme GetScheme(this MessageType type)
        {
            switch (type)
            {
                case MessageType.Info:
                    return ColorScheme.Default;
                case MessageType.Warning:
                    return new ColorScheme(ConsoleColor.Black, ConsoleColor.Yellow);
                case MessageType.Error:
                    return new ColorScheme(ConsoleColor.Black, ConsoleColor.Red);
                default:
                    throw new ArgumentException("Переданный тип сообщений не поддерживается " + type);
            }
        }

        public static void Apply(this ColorScheme scheme)
        {
            Console.BackgroundColor = scheme.Backcolor;
            Console.ForegroundColor = scheme.Fontcolor;
        }

        public static void Apply(this ColorScheme scheme, Action printAction)
        {
            if (printAction is null)
            {
                throw new ArgumentNullException(nameof(printAction));
            }

            if (scheme.Backcolor == Console.BackgroundColor && scheme.Fontcolor == Console.ForegroundColor)
            { 
                printAction();
                return;
            }

            var oldScheme = ColorScheme.FromConsole();
            scheme.Apply();
            try
            {
                printAction();
            }
            finally
            {
                oldScheme.Apply();
            }
        }
    }
}
