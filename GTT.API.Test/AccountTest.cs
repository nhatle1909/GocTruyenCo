using Application.Common;
using Application.DTO;
using Application.Interface.Service;
using CloudinaryDotNet.Actions;
using Controller.Controllers;
using Domain.Entities;
using Moq;

namespace GTT.API.Test
{
    public class AccountTest
    {
       
      
        [Fact]

        public async Task GetPaging_Success_ReturnAllAdminRoleAccount()
        {
            //Arrange
            SearchDTO searchDTO = new SearchDTO{
                searchFields = ["Role"],
                searchValues = ["Admin"],
                sortField = "Username",
                sortAscending = true,
                pageSize = 10,
                skip = 1 
            };
            var expectedResult = new List<QueryAccountDTO>
            {
                new QueryAccountDTO
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "admin@gmail.com",
                    CreatedDate = DateTime.Now.ToString(),
                    isRestricted = false,
                }
            };
            var expectedReturnResult = new ServiceResponse<IEnumerable<QueryAccountDTO>>();
            expectedReturnResult.CustomResponse(expectedResult,true,  "Retrieve data successful");
          
            var mockAccountService = new Mock<IAccountService>();
            
            mockAccountService.Setup( mockAccountService => mockAccountService.GetPagingAsync(searchDTO)).ReturnsAsync(expectedReturnResult);
            //Act
            var actualResult = await mockAccountService.Object.GetPagingAsync(searchDTO);

            //Assert
            Assert.NotNull(actualResult);
            Assert.True(actualResult.Success);
            Assert.Equal(expectedReturnResult.Message, actualResult.Message);
            Assert.NotNull(actualResult.Result);
            Assert.IsAssignableFrom<IEnumerable<QueryAccountDTO>>(actualResult.Result); // Verify it's the correct type of collection

            // Now compare the contents of the data (the list of QueryAccountDTO)
            var expectedAccount = expectedResult.First();
            var actualAccount = actualResult.Result.First(); // Assuming there's at least one

            Assert.Equal(expectedAccount.Id, actualAccount.Id);
            Assert.Equal(expectedAccount.Username, actualAccount.Username);
            Assert.Equal(expectedAccount.Email, actualAccount.Email);

        }
    }
}        //private List<Account> accounts = new List<Account>
         //{

//    new Account
//    {
//        Id = Guid.NewGuid(),
//        Username = "admin",
//        Email = "admin@gmail.com",
//        Password = "admin",
//        Role = Domain.Enums.Role.Admin,
//        isRestricted = false,

//        isDeleted = false,
//        CreatedDate = DateTime.Now.ToString(),
//    },
//    new Account
//    {
//         Id = Guid.NewGuid(),
//        Username = "reader",
//        Email = "reader@gmail.com",
//        Password = "reader",
//        Role = Domain.Enums.Role.Reader,
//        isRestricted = false,

//        isDeleted = false,
//        CreatedDate = DateTime.Now.ToString(),
//    },
//    new Account
//    {
//         Id = Guid.NewGuid(),
//        Username = "reader",
//        Email = "reader@gmail.com",
//        Password = "reader",
//        Role = Domain.Enums.Role.Reader,
//        isRestricted = true,

//        isDeleted = false,
//        CreatedDate = DateTime.Now.ToString(),
//    }
//}