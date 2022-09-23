/*для доступа к списку, после запуска проекта добавьте /users 
  чтобы увидеть весь список пользователей*/


using SimpleWebAPIWithoutBDA;

//начальные данные
List<Person> users = new List<Person>
{
    //т.к база данных не используется, то ID создается автоматически с помощью Guid
    new Person{Id = Guid.NewGuid().ToString(), Name = "Ali", Age = 31 },
    new Person{Id = Guid.NewGuid().ToString(), Name = "Emin", Age = 41},
    new Person{Id = Guid.NewGuid().ToString(), Name = "Sarkhan", Age = 33}
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
//путь для списка пользователей
app.MapGet("/users", () => users);
//найти пользователя по ID
app.MapGet("/users/{id}", (string id) =>
{
    // id пользователя
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // если не найден, отправляем статусный код и сообщение об ошибке
    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    // если пользователь найден, выводим на экран
    return Results.Json(user);
});
//удаление пользователя
app.MapDelete("/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    //удаление пользователя
    users.Remove(user);

    return Results.Json(users);

});
//добавление нового пользователя
app.MapPost("/users", (Person user) =>
{
    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});
//изменение данных пользователя
app.MapPut("/users", (Person userData) =>
{
    var user = users.FirstOrDefault(u => u.Id == userData.Id);

    if (user == null) return Results.NotFound(new { message = "Пользователь не найден" });
    //изменение данных пользователя
    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();

