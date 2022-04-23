using AutoMapper;
using KellermanSoftware.CompareNetObjects;

namespace BAL.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? NIC { get; set; }

    }
    public class Student
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Class { get; set; }
        public string? NIC { get; set; }
    }

    public class User : IUserInterface
    {
        private readonly IMapper _mapper;

        public User(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<IEnumerable<Student>> GetUsers()
        {
            try
            {
                var users = new List<UserDto>();
                users.Add(new UserDto() { Id = 1, Name = "fatima", Class = "A1" });
                users.Add(new UserDto() { Id = 2, Name = "Hina", Class = "A2" });
                var studentList = _mapper.Map<List<Student>>(users);
                var newStudentList = new List<Student>();
                newStudentList.Add(new Student() { Id = 1.Encode() , Name = "Taha", Class = "A1" });
                newStudentList.Add(new Student() { Id = 2.Encode(), Name = "Hina", Class = "A2" });
                CompareUsers(newStudentList, studentList);
                return await Task.FromResult(studentList);
            }
            catch (Exception)
            {
                return new List<Student>();
            }


        }
        void CompareUsers(List<Student> students1, List<Student> students2)
        {
            var comparer = new AppStudentComparer();
            //checking the difference in objects through compare .net Objects nuget
            CompareLogic compareLogic = new CompareLogic()
            {
                Config = new ComparisonConfig()
                { MaxDifferences = typeof(Student).GetProperties().Length }
            };
            if (students1.Any() && students2.Any())
            {
                foreach(var item in students1)
                {
                    var getStudent2 = students2.FirstOrDefault(z => z.Id == item.Id);
                    if(getStudent2 != null)
                    {
                        if (comparer.Equals(item, getStudent2))
                        {
                            //Perform task in case objects equel
                        }
                        else
                        {
                            
                            //checking the difference in objects
                            List<Difference> difference = compareLogic.Compare(item, getStudent2).Differences;
                        }
                    }                  
                    
                }
                
            }
            //Perform task in case objects not equel
        }
    }
    //Custom implementation of .Net object camparison
    class AppStudentComparer : IEqualityComparer<Student>, IComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name &&
                   x.Class == y.Class &&
                   x.NIC == y.NIC;
        }

        public int GetHashCode(Student obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Name);
            hashCode.Add(obj.Class);
            hashCode.Add(obj.NIC);

            var hashCodeValue = hashCode.ToHashCode();
            return hashCodeValue;
        }

        public int Compare(Student x, Student y)
        {
            return GetHashCode(x) - GetHashCode(y);
        }
    }
}
