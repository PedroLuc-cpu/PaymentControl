using System.Text.Json.Serialization;

namespace cadastro.Model
{
    public class Base
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}