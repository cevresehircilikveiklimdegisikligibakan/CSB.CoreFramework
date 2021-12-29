using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace CSB.Core.Services
{
    internal class EnumService : IEnumService
    {
        public string GetDisplayName(Enum value)
        {
            return value.GetType()?
                        .GetMember(value.ToString())?.First()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .Name;
        }
    }
}