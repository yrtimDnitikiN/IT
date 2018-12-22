using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHVacancy
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HHClient("https://api.hh.ru");
            var vacancies = client.GetVacancies();
            if (vacancies != null)
            {
                ShowFiltredVacancies(vacancies, false, 120000M);
                ShowFiltredVacancies(vacancies, true, 15000M);
            }
            else
                Console.WriteLine("Не удалось получить вакансии.");
            Console.ReadKey();
        }
       
        static void ShowFiltredVacancies(List<Vacancy> vacancies, bool less, decimal value)
        {
            Console.WriteLine("Вакансии с зарплатой {0} чем {1}:", less ? "менее" : "больше или равно", value);
            var count = 0;
            foreach (var vacancy in vacancies)
                if ((less && vacancy.Salary.GetValue() < value) || (!less && vacancy.Salary.GetValue() >= value))
                {
                    count++;
                    Console.WriteLine("{0}) {1}",count,vacancy);
                }
        }
    }
}
