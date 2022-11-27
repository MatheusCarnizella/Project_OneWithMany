using Microsoft.EntityFrameworkCore;
using Project_RestricaoVideos.Context;
using Project_RestricaoVideos.Models;

namespace Project_RestricaoVideos.EndPoints;

public static class VideoEndPoint
{
    public static void MapVideoEndPoint(this WebApplication ep)
    {
        ep.MapPost("/video/cadastrarVideo", async (Video video, AppDbContext context) =>
        {
            await context.Videos.AddAsync(video);
            await context.SaveChangesAsync();

            return Results.Created($"/cadastrarVideo/{video.videosId}", video);
        })
            .WithTags("Videos");

        ep.MapGet("/video/mostrarVideo", async (AppDbContext context) =>
        await context.Videos.ToListAsync())
           .WithTags("Videos");

        ep.MapGet("/video/mostrarVideo/{Id:int}", async (int Id, AppDbContext context) =>
        {
            return await context.Videos.FindAsync(Id)
                is Video video
                ? Results.Ok(video)
                : Results.NotFound();
        })
            .WithTags("Videos");

        ep.MapPut("/video/mostrarVideo/{Id:int}", async (int Id, Video video, AppDbContext context) =>
        {
            var atualizar = context.Videos.Update(video);

            if (Id != video.videosId)
            {
                return Results.NotFound("Id não existe");
            }

            if (atualizar is null)
            {
                return Results.BadRequest("Tente de novo");
            }

            await context.SaveChangesAsync();
            return Results.Ok(video);
        })
            .WithTags("Videos");

        ep.MapDelete("/video/mostrarVideo/{Id:int}", async (int Id, AppDbContext context) =>
        {
            var atualizar = await context.Videos.FindAsync(Id);

            if (Id != atualizar.videosId)
            {
                return Results.NotFound("Id não existe");
            }

            if (atualizar == null)
            {
                return Results.BadRequest("Tente de novo");
            }

            context.Videos.Remove(atualizar);
            await context.SaveChangesAsync();
            return Results.Ok($"A restrição {atualizar.videosId}, {atualizar.videoNome} foi removida com sucesso");
        })
            .WithTags("Videos");
    }
}
