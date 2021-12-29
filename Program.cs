using BlogApp.Data;
using BlogApp.Graphql.DataLoaders;
using BlogApp.Graphql.Queries;
using BlogApp.Graphql.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
          .AddDbContextFactory<BlogDbContext>(options => options.UseSqlite("Data Source=blog.db"))
          .AddGraphQLServer()
          .AddQueryType()
          .AddTypeExtension<AuthorQueries>()
          .AddTypeExtension<PostQueries>()
          .AddType<AuthorType>()
          .AddType<PostType>()
          .RegisterDbContext<BlogDbContext>()
          .AddDataLoader<AuthorByIdDataLoader>()
          .AddDataLoader<PostByIdDataLoader>()
          .AddDataLoader<PostsByAuthorIdDataLoader>()
          .AddGlobalObjectIdentification();

var app = builder.Build();

app.MapGraphQL();

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/graphql");
    return Task.CompletedTask;
});

app.Run();
