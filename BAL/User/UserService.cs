using App.Common.Abstract.Helpers;
using App.Utils;
using App.Utils.Extensions;
using AutoMapper;
using BAL.Model;
using BAL.UserLoginInfo;
using DAL;
using DAL.Entities;
using KellermanSoftware.CompareNetObjects;
using Z.EntityFramework.Extensions;
using System.Data.Entity;

namespace BAL.UserService
{
   

    public class UserService : IUserInterface
    {
        private readonly IMapper _mapper;
        private readonly TestContext _coreContext;
        private readonly IUsersLoginInfoService _usersInfoService;
        public UserService(IMapper mapper, TestContext coreContext, IUsersLoginInfoService usersInfoService)
        {
            _coreContext = coreContext;
            _mapper = mapper;
            _usersInfoService = usersInfoService;
        }
        public async Task<IEnumerable<Student>> GetUsers()
        {
            try
            {
                var users = new List<UserDto>();
                //users.Add(new UserDto() { Id = 1, Name = "fatima", Class = "A1" });
                //users.Add(new UserDto() { Id = 2, Name = "Hina", Class = "A2" });
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
        public async Task<string> AddUsers(UserDto model)
        {
            try
            {
                //var addUser = _mapper.Map<Product>(userObj); 
                //addUser.AddCreatedInfo(byUserId.ToString());
                //_coreContext.Products.Add(addUser);
                //await _coreContext.SaveChangesAsync();
                //return addUser.Id;
                var newUser = new User()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Created = NodaTimeHelper.Now
                };

                //newUser.AddCreatedInfo(byUserGuid);
                //var result = await userManager.CreateAsync(new ApplicationUser() { UserName = model.Name, StoreId = db.StoreLocations.Where(x => x.Id == Static.LoggedUserInfo.StoreLocationId).Select(s => s.Store.Id).FirstOrDefault(), Email = model.Email, StoreLocationId = Static.LoggedUserInfo.StoreLocationId.IsNullOrZero() ? 0 : Static.LoggedUserInfo.StoreLocationId, CreatedDate = DateTime.Now }, model.Password);
                var aspNetUser = await _usersInfoService.CheckUserEmail(model.Email);
                if(aspNetUser)
                    throw new ApplicationException($"{nameof(User)} already exist!");

                var getUser = await _usersInfoService.AddNewUser(newUser);
                await _usersInfoService.AddUserRole(newUser);
                return getUser.Id;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public async Task UpdateUsers(int id, UserDto userObj, int byUserId)
        {
            try
            {
                //var userEntity = await  _coreContext.Products.FirstOrDefaultAsync(z => z.Id == id);
                //if(userEntity == null)                
                //    throw new ApplicationException($"{nameof(Product)} does not exist!");
                //userEntity = _mapper.Map<Product>(userObj);
                //userEntity.AddUpdatedInfo(byUserId.ToString());
            }
            catch (Exception)
            {

            }
        }
        public async Task AddProducts(ProductDto model)
        {
            try
            {                
                if (model!=null)
                {
                    var product = _mapper.Map<Product>(model);
                    List<Product> products = new List<Product>();
                    for (int i = 0; i < 5000; i++)
                    {
                        products.Add(product);
                    }

                     await _coreContext.Products.AddRangeAsync(products);
                   // _coreContext.Products.BulkInsert(products);
                   // _coreContext.BulkInsert<Product>(products);
                    await _coreContext.SaveChangesAsync();
                }
                             
            }
            catch (Exception ex)
            {
                
            }
        }
    }
    //Custom implementation of .Net object camparison
    class AppStudentComparer : IEqualityComparer<Student>, IComparer<Student>
    {
        public bool Equals(Student? x, Student? y)
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

        public int Compare(Student? x, Student? y)
        {
            if (ReferenceEquals(x, null)) return 0;
            if (ReferenceEquals(y, null)) return 0;
            return GetHashCode(x) - GetHashCode(y);
        }
    }
}
