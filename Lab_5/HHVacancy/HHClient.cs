using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
namespace HHVacancy
{
    class HHClient
    {
        private RestClient client;
        private List<Vacancy> vacancies;

        public HHClient(string host)
        {
            client = new RestClient(host);
            vacancies = new List<Vacancy>();
        }

        private void GetVacanciesFromHH()
        {
            var per_page = 100;
            var pagesNum = 2000 / per_page;
            for(var page = 0; page < pagesNum; page++)
            {
                var requestForManyVacncies = new RestRequest(string.Format("vacancies?per_page={0}&page={1}", per_page, page), Method.GET);
                var responseWithManyVacncies = client.Execute(requestForManyVacncies);
                if (responseWithManyVacncies.StatusCode == HttpStatusCode.OK)
                {
                    var vacancyJsonList = (JArray)JToken.Parse(responseWithManyVacncies.Content)["items"];
                    foreach (var vacancyJson in vacancyJsonList)
                    {
                        var request = new RestRequest(string.Format("vacancies/{0}", vacancyJson["id"].ToString()), Method.GET);
                        var response = client.Execute(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var vacancy = VacancyConverter.ConvertFromStringToVacancy(response.Content);
                            if (vacancy != null)
                                vacancies.Add(vacancy);
                        }
                    }
                }
            }
        }

        public List<Vacancy> GetVacancies()
        {
            GetVacanciesFromHH();
            return vacancies;
        }
    }
}
