/*��� ������� � ������, ����� ������� ������� �������� /users 
  ����� ������� ���� ������ �������������*/


using SimpleWebAPIWithoutBDA;

//��������� ������
List<Person> users = new List<Person>
{
    //�.� ���� ������ �� ������������, �� ID ��������� ������������� � ������� Guid
    new Person{Id = Guid.NewGuid().ToString(), Name = "Ali", Age = 31 },
    new Person{Id = Guid.NewGuid().ToString(), Name = "Emin", Age = 41},
    new Person{Id = Guid.NewGuid().ToString(), Name = "Sarkhan", Age = 33}
};

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
//���� ��� ������ �������������
app.MapGet("/users", () => users);
//����� ������������ �� ID
app.MapGet("/users/{id}", (string id) =>
{
    // id ������������
    Person? user = users.FirstOrDefault(u => u.Id == id);
    // ���� �� ������, ���������� ��������� ��� � ��������� �� ������
    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    // ���� ������������ ������, ������� �� �����
    return Results.Json(user);
});
//�������� ������������
app.MapDelete("/users/{id}", (string id) =>
{
    Person? user = users.FirstOrDefault(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    //�������� ������������
    users.Remove(user);

    return Results.Json(users);

});
//���������� ������ ������������
app.MapPost("/users", (Person user) =>
{
    user.Id = Guid.NewGuid().ToString();
    users.Add(user);
    return user;
});
//��������� ������ ������������
app.MapPut("/users", (Person userData) =>
{
    var user = users.FirstOrDefault(u => u.Id == userData.Id);

    if (user == null) return Results.NotFound(new { message = "������������ �� ������" });
    //��������� ������ ������������
    user.Age = userData.Age;
    user.Name = userData.Name;
    return Results.Json(user);
});

app.Run();

