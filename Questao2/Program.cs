using System.Text.Json;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals + " goals in " + year);

        // Output esperado:
        // Team Paris Saint-Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoals(string team, int year)
    {
        int totalGoals = 0;
        int page = 1;

        using (HttpClient client = new HttpClient())
        {
            while (true)            {
                
                string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}";
                                
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();
                JsonDocument json = JsonDocument.Parse(responseBody);

                int totalPages = json.RootElement.GetProperty("total_pages").GetInt32();
                
                foreach (var match in json.RootElement.GetProperty("data").EnumerateArray())
                {
                    string team1GoalsString = match.GetProperty("team1goals").GetString() ?? "0";
                    int team1Goals = int.Parse(team1GoalsString);
                    totalGoals += team1Goals;
                }

               
                url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team2={team}&page={page}";
                response = await client.GetAsync(url);
                responseBody = await response.Content.ReadAsStringAsync();
                json = JsonDocument.Parse(responseBody);

                foreach (var match in json.RootElement.GetProperty("data").EnumerateArray())
                {
                    string team2GoalsString = match.GetProperty("team2goals").GetString() ?? "0";
                    int team2Goals = int.Parse(team2GoalsString);
                    totalGoals += team2Goals;
                }
                
                if (page >= totalPages)
                {
                    break;
                }

                page++;
            }
        }

        return totalGoals;
    }
}
