using Application.Common;
using Application.DTO;
using Application.Interface.Service;

using Domain.Entities;
using Moq;

namespace GTT.API.Test
{
    public class AccountTest
    {
        List<QueryAccountDTO> testData = new List<QueryAccountDTO>
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
        Mock<IAccountService> mockAccountService = new Mock<IAccountService>();
        [Fact]
        public async Task GetPaging_Success_GetAllAdminRoleAccount()
        {
            SearchDTO searchDTO = new SearchDTO
            {
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
            
            mockAccountService.Setup(mockAccountService => mockAccountService.GetPagingAsync(searchDTO))
                .ReturnsAsync(new ServiceResponse<IEnumerable<QueryAccountDTO>> 
                { Result = expectedResult, Success = true, Message = "Retrieve data successful" });
            var actualResult = await mockAccountService.Object.GetPagingAsync(searchDTO);
            Assert.NotNull(actualResult);
            Assert.True(actualResult.Success);
            Assert.Equal("Retrieve data successful", actualResult.Message);
            Assert.NotNull(actualResult.Result);
            Assert.IsAssignableFrom<IEnumerable<QueryAccountDTO>>(actualResult.Result);
        }


    }
}        