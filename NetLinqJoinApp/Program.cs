List<Address> addresses = new()
{
    new(){ Id = 1, City = "Moscow", Street = "Tverskaya"},
    new(){ Id = 2, City = "St. Peterburg", Street = "Nevsky"},
    new(){ Id = 3, City = "Moscow", Street = "Dmitrovka"},
    new(){ Id = 4, City = "Kazan", Street = "Sovetskaya"},
};


List<Company> companies = new()
{
    new(){ Id = 1, Title = "Yandex", City = "Moscow", AddressId = 1},
    new(){ Id = 2, Title = "PiterSoft", City = "St. Peterburg", AddressId = 2},
    new(){ Id = 3, Title = "Rambler", City = "Moscow", AddressId = 3},
    new(){ Id = 4, Title = "Game Lab", City = "Kazan", AddressId = 4},
};

List<User> users = new()
{
    new(){ Id = 1, Name = "Bob", Age = 34, CompanyId = 1 },
    new(){ Id = 1, Name = "Joe", Age = 22, CompanyId = 2 },
    new(){ Id = 1, Name = "Sam", Age = 29, CompanyId = 3 },
    new(){ Id = 1, Name = "Leo", Age = 41, CompanyId = 4 },
    new(){ Id = 1, Name = "Jim", Age = 32, CompanyId = 1 },
    new(){ Id = 1, Name = "Tim", Age = 21, CompanyId = 2 },
    new(){ Id = 1, Name = "Ben", Age = 42, CompanyId = 3 },
};

/*
var usersFullWhereO = from u in users
                      from c in companies
                      where u.CompanyId == c.Id
                      select new
                      {
                          Name = u.Name,
                          Age = u.Age,
                          Company = c.Title,
                          City = c.City,
                      };

foreach(var item in usersFullWhereO)
    Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Company: {item.Company}, City: {item.City}");

var usersFullJoinO = from u in users
                     join c in companies on u.CompanyId equals c.Id
                     select new
                     {
                         Name = u.Name,
                         Age = u.Age,
                         Company = c.Title,
                         City = c.City,
                     };

foreach (var item in usersFullWhereO)
    Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Company: {item.Company}, City: {item.City}");

var usersFullJoinM = users.Join(companies,
        u => u.CompanyId,
        c => c.Id,
        (u, c) => new
        {
            Name = u.Name,
            Age = u.Age,
            Company = c.Title,
            City = c.City,
        });
*/

var usersFullAddrO = from u in users
                     join c in companies on u.CompanyId equals c.Id
                     join a in addresses on c.AddressId equals a.Id
                     select new
                     {
                         Name = u.Name,
                         Age = u.Age,
                         Company = c.Title,
                         City = a.City,
                         Street = a.Street,
                     };

foreach (var item in usersFullAddrO)
    Console.WriteLine(@$"Name: {item.Name}, Age: {item.Age}, Company: {item.Company}, City: {item.City}, Street: {item.Street}");


var usersGroupJoinM = companies.GroupJoin(users,
    c => c.Id,
    u => u.CompanyId,
    (c, ulist) => new
    {
        Title = c.Title,
        Users = ulist
    });

foreach(var c in usersGroupJoinM)
{
    Console.WriteLine($"Company: {c.Title}");
    foreach(var u in c.Users)
        Console.WriteLine($"\tName: {u.Name}, Age: {u.Age}");
}

class User
{
    public int Id { set; get; }
    public string Name { set; get; }
    public int Age { set; get; }
    public int CompanyId { set; get; }
}

class Company
{
    public int Id { set; get; }
    public string Title { set; get; }
    public string City { set; get; }
    public int AddressId { set; get; }
}

class Address
{
    public int Id { set; get; }
    public string City { set; get; }
    public string Street { set; get; }
}
