using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HHVacancy
{
    class Vacancy
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string NameOfJob { get; set; }
        [JsonProperty("salary")]
        public Salary Salary { get; set; }
        [JsonProperty("key_skills")]
        public List<Skill> KeySkills { get; set; }

        public override string ToString()
        {
            var skills = new StringBuilder(string.Empty);
            if (KeySkills != null)
            {
                foreach (var skill in KeySkills)
                    skills.Append(string.Format("{0}, ", skill.Name));
                if (skills.Length != 0)
                    skills.Remove(skills.Length - 1, 1);
            }
            return string.Format("{0}\n{1}", NameOfJob, skills.ToString());
        }
    }
}
