

using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Name ="Michael Jordan",
                Email ="mj@gmail.com",
                Address = new Address
                {
                    Street ="1234 Market St",
                    City ="Somewhere",
                    ZipCode =123456
                }
            },
            new User
            {
                Name ="Kobe Bryant",
                Email ="kobe@gmail.com",
                Address = new Address
                {
                    Street ="56778 Market St",
                    City ="Somewhere",
                    ZipCode =12688
                }
            },
            new User
            {
                Name ="Lebron James",
                Email ="lebron@gmail.com",
                Address = new Address
                {
                    Street ="14466 Market St",
                    City ="Somewhere",
                    ZipCode =6467
                }
            },

        };
    }
}
