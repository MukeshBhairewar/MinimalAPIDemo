using MinimalAPIDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Student> students = new List<Student>();

app.MapGet("/students", () =>
{
    return students;
});

app.MapGet("/students/{id}", (int id) =>
{
    int index = students.FindIndex(students => students.Id == id);
    if (index < 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(students[index]);
});

app.MapPost("/students", (Student student) =>
{

    students.Add(student);
    return Results.Ok();
});

app.MapPut("/students/{id}", (int id ,Student student) =>
{

    int index= students.FindIndex(x => x.Id == id);
    if(index <0)
    {
        return Results.NotFound();
    }
    students[index] = student;
    return Results.Ok();
});

app.MapDelete("/students/{id}", (int id) =>
{
     int count = students.RemoveAll(students => students.Id == id);
    if(count >0)
    {
        return Results.Ok();
    }
    return Results.NotFound();
});



app.Run();

