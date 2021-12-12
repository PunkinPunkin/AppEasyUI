using System.ComponentModel.DataAnnotations;

namespace AppEasyUI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [MaxLength(50)]
        public string Account { get; set; } = "";
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(20)]
        public string Title { get; set; } = "";
        public DateTime? Birthday { get; set; } = new DateTime();

        private IEnumerable<User>? allTestUsers;
        public IEnumerable<User> GetTestUsers(string searchType = "", string keyword = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            if (allTestUsers == null)
            {
                allTestUsers = new List<User>() {
                    new User{ Id = 1, Account = "user1@test.com", Name = "Mary", Title = "Administrator", Birthday = new DateTime(1911, 10, 10) },
                    new User{ Id = 2, Account = "user2@test.com", Name = "Robert", Title = "QA", Birthday = new DateTime(1990, 10, 1) },
                    new User{ Id = 3, Account = "user3@test.com", Name = "John", Title = "Chairman", Birthday = new DateTime(1985, 12, 19) },
                    new User{ Id = 4, Account = "user4@test.com", Name = "Patricia", Title = "QA", Birthday = new DateTime(1991, 9, 10) },
                    new User{ Id = 5, Account = "user5@test.com", Name = "Michael", Title = "CEO", Birthday = new DateTime(1959, 1, 1) }
                };
            }

            IEnumerable<User> users;
            if (string.IsNullOrWhiteSpace(keyword))
            {
                users = allTestUsers;
            }
            else
            {
                users = searchType.ToLower() switch
                {
                    "account" => allTestUsers.Where(o => o.Account.Contains(keyword)),
                    "name" => allTestUsers.Where(o => o.Name.Contains(keyword)),
                    "title" => allTestUsers.Where(o => o.Title.Contains(keyword)),
                    _ => allTestUsers.Where(o => o.Account.Contains(keyword) || o.Name.Contains(keyword) || o.Title.Contains(keyword)),
                };
            }

            if (startDate.HasValue)
            {
                users = users.Where(o => o.Birthday >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                users = users.Where(o => o.Birthday <= endDate.Value);
            }

            return users;
        }
    }
}
