using System.Text.Json.Serialization;

namespace Project_RestricaoVideos.Models;

public class Restricao
{
    public int retricaoId { get; set; }
    public string? restricaoNome { get; set; }

    public ICollection<Video>? Videos { get; set; }
}
