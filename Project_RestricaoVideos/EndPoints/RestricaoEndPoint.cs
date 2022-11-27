using Microsoft.EntityFrameworkCore;
using Project_RestricaoVideos.Context;
using Project_RestricaoVideos.Models;

namespace Project_RestricaoVideos.EndPoints;

public static class RestricaoEndPoint
{
    public static void MapRestricaoEndPoint(this WebApplication ep)
    {
        ep.MapPost("/restricao/cadastrarRestricao", async (Restricao restricao, AppDbContext context) =>
        {
            context.Restricoes.Add(restricao);
            await context.SaveChangesAsync();

            return Results.Created($"/cadastrarRestricao/{restricao.retricaoId}", restricao);
        })
            .WithTags("Restrições");

        ep.MapGet("/restricao/pegarRestricoes", async (AppDbContext context) =>
        await context.Restricoes.ToListAsync())
            .WithTags("Restrições");

        ep.MapGet("/restricao/pegarRestricoescomVideos", async (AppDbContext context) =>
        await context.Restricoes.Include(x => x.Videos).ToListAsync())
           .WithTags("Restrições");

        ep.MapGet("/restricao/pegarRestricoes/{Id:int}", async (int Id, AppDbContext context) =>
        {
            return await context.Restricoes.FindAsync(Id)
                is Restricao restricao
                ? Results.Ok(restricao)
                : Results.NotFound();            
        })    
             .WithTags("Restrições");

        ep.MapPut("/restricao/atuzalizarRestricoes/{Id:int}", async (int Id, Restricao restricao, AppDbContext context) =>
        {
            var atualizar = context.Restricoes.Update(restricao);

            if(Id != restricao.retricaoId)
            {
                return Results.NotFound("Id não existe");
            }

            if(atualizar is null)
            {
                return Results.BadRequest("Tente de novo");
            }

            await context.SaveChangesAsync();
            return Results.Ok(restricao);
        })
            .WithTags("Restrições");

        ep.MapDelete("/restricao/deletarRestricoes/{Id:int}", async (int Id, AppDbContext context) =>
        {
            var atualizar = await context.Restricoes.FindAsync(Id);

            if (Id != atualizar.retricaoId)
            {
                return Results.NotFound("Id não existe");
            }

            if (atualizar == null)
            {
                return Results.BadRequest("Tente de novo");
            }

            context.Restricoes.Remove(atualizar);
            await context.SaveChangesAsync();
            return Results.Ok($"A restrição {atualizar.retricaoId}, {atualizar.restricaoNome} foi removida com sucesso");
        })
            .WithTags("Restrições");
    }
}
