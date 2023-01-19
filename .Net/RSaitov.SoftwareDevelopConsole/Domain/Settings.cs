using System;
using System.Collections.Generic;
using System.Text;

namespace RSaitov.SoftwareDevelop.Domain
{
    public static class Settings
    {
        /// <summary>
        /// Количество рабочих часов в месяце
        /// </summary>
        public const byte WorkingHoursPerMonth = 160;

        /// <summary>
        /// Количество рабочих часов в дне
        /// </summary>
        public const byte WorkingHoursPerDay = 8;

        /// <summary>
        /// Зарплата фрилансера в час
        /// </summary>
        public const decimal FreelancerSalaryPerHour = 1000;

    }
}
