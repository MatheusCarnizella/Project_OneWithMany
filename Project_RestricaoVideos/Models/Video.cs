using System.Text.Json.Serialization;

namespace Project_RestricaoVideos.Models;

public class Video
{
    public int videosId { get; set; }
    public string? videoNome { get; set; }
    public string? videoDescricao { get; set; }
    public DateTime videoData { get; set; }

    public int restricaoId { get; set; }

    [JsonIgnore]
    public Restricao? Restricao { get; set; }
}
