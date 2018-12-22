using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HHVacancy
{
    static class VacancyConverter
    {
        public static Vacancy ConvertFromStringToVacancy(string json)
        {
            var vacancy = JsonConvert.DeserializeObject<Vacancy>(json);
            return (vacancy.Salary == null) ? null : vacancy;
        }
    }
}
