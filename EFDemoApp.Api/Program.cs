using EFDemoApp.Api.App;
using EFDemoApp.Infrastructure.Database.DataAccess;
using EFDemoApp.Infrastructure.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeopleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region people
app.MapGet("api/people", async ([FromQuery] string? firstName, [FromQuery] string? surname, [FromQuery] int? page, [FromServices] PeopleContext peopleContext) =>
{
    var queryResult = peopleContext.People.Where(p => (string.IsNullOrEmpty(firstName) || p.FirstName.Contains(firstName)) && (string.IsNullOrEmpty(surname) || p.FirstName.Contains(surname)))
    .Include(p => p.Addresses)
    .Include(p => p.EmailAddresses)
    .Skip(page ?? 0 * 50)
    .Take(50)
    .Select(p => (PersonDto)p);

    return Results.Ok(queryResult.AsAsyncEnumerable());
})
.WithName("queryPeople")
.WithTags(ApiTags.GetPeopleTag())
.Produces<IAsyncEnumerable<PersonDto>>(200);

app.MapGet("api/people/{id:guid}", async ([FromRoute] Guid id, [FromServices] PeopleContext peopleContext) =>
{
    if (!await peopleContext.People.Where(p => p.Id == id).AnyAsync())
    {
        return Results.NotFound();
    }

    var searchResult = await peopleContext.People.Where(p => p.Id == id)
                                           .Include(p => p.Addresses)
                                           .Include(p => p.EmailAddresses)
                                           .Select(p => (PersonDto)p).FirstOrDefaultAsync();

    return Results.Ok(searchResult);
})
.WithName("searchPersonById")
.WithTags(ApiTags.GetPeopleTag())
.Produces(404)
.Produces<PersonDto>(200);

app.MapPost("api/people", async ([FromBody] PersonInputDto person, [FromServices] PeopleContext peopleContext) =>
{
    Person newPerson = person;

    await peopleContext.AddAsync(newPerson);
    await peopleContext.SaveChangesAsync();

    return Results.Ok((PersonDto)newPerson);
})
.WithName("CreateNewPerson")
.WithTags(ApiTags.GetPeopleTag())
.Produces<PersonDto>(200);


app.MapDelete("api/people/{id:guid}", async ([FromRoute] Guid id, [FromServices] PeopleContext peopleContext) =>
{
    if (!await peopleContext.People.Where(p => p.Id == id).AnyAsync())
    {
        return Results.NotFound();
    }

    //TODO: Improve delete
    var person = peopleContext.People.Where(p => p.Id == id).First();
    peopleContext.Entry(person).Property("IsDeleted").CurrentValue = true;

    await peopleContext.SaveChangesAsync();

    return Results.Ok();
})
.WithName("DeletePerson")
.WithTags(ApiTags.GetPeopleTag())
.Produces<PersonDto>(200);
#endregion

#region Inline
/// <summary>
/// The simplest way to implement a minimal api
/// You can improve its organization using #region approach
/// </summary>
app.MapGet("api/sample", () => Results.StatusCode(418))
    .Produces(418)
    .WithName("teapot")
    .WithTags(ApiTags.GetSampleTag());
#endregion

#region #2_Handlers
/// <summary>
/// You just have to write the correct handler
/// </summary>
app.MapGet("api/sample/how-old-am-i", CalculateHowOldIAm)
    .WithTags(ApiTags.GetSampleTag())
    .WithName("howOldAmIApi");
#endregion

#region #2_LocalFunctions
RegisterApiInformation();
#endregion


app.Run();

#region #1_LocalFunctions
/// <summary>
/// You can declare your function after app.Run() and call it from the top-level
/// </summary>
void RegisterApiInformation()
{
    app.MapGet("api/sample/info", [AllowAnonymous] async () => $"UTC: {DateTime.UtcNow}, Culture: {CultureInfo.CurrentCulture.DisplayName}")
        .WithTags(ApiTags.GetSampleTag())
        .WithName("apiInformation");
}
#endregion

#region #1_Handlers
/// <summary>
/// Calculate how old you are
/// </summary>
/// <remarks>Sample: /api/sample/how-old-am-i?myBirthday=1994-07-26</remarks>
[AllowAnonymous]
[ProducesResponseType(typeof(string), 200)]
[ProducesResponseType(typeof(string), 400)]
IResult CalculateHowOldIAm(DateOnly myBirthday)
{
    if (myBirthday > DateOnly.FromDateTime(DateTime.UtcNow))
        Results.BadRequest("maybe you haven't born yet!");

    int age = (DateOnly.FromDateTime(DateTime.UtcNow).DayNumber - myBirthday.DayNumber) / 365;
    return Results.Text($"You're {age} years old!");
}
#endregion