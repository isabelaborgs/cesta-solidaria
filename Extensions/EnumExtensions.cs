using System.ComponentModel.DataAnnotations;
using System.Reflection;
using app.Enums;

namespace app.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                           .GetMember(enumValue.ToString())
                           .First()
                           .GetCustomAttribute<DisplayAttribute>()
                           ?.GetName() ?? enumValue.ToString();
        }

        public static DateTime GetProximaData(this DiaDaSemana diaDaSemana)
        {
            var dataAtual = DateTime.Now;
            var diferencaDias = (int)diaDaSemana - (int)dataAtual.DayOfWeek;

            if (diferencaDias < 0)
            {
                diferencaDias += 7;
            }

            var proximaData = dataAtual.AddDays(diferencaDias);

            return proximaData;
        }
    }
}
