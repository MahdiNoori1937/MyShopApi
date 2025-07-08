using AutoMapper;
using Moq;
using MyShop.Application.Commonn.Security;
using MyShop.Application.Feature.User.Command;
using MyShop.Application.Feature.User.DTOs;
using MyShop.Application.Feature.User.Handler;
using MyShop.Application.Feature.User.Validators.UserValidateService;
using MyShop.Domain.Entities.UserEntity;
using MyShop.Domain.Interfaces.IUnitOfWorkInterface;
using MyShop.Domain.Interfaces.IUserInterface;

namespace MyShop.Test;

public class UserTest
{
    #region constructor

    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IUnitOfWorkRepository> _mockUnit;
    private readonly Mock<IUserValidateService> _mockValidator;
    private readonly Mock<IMapper> _mockMapper;
    
    public UserTest()
    {
        _mockRepo = new Mock<IUserRepository>();
        _mockUnit = new Mock<IUnitOfWorkRepository>();
        _mockValidator = new Mock<IUserValidateService>();
        _mockMapper = new Mock<IMapper>();
    }
    

    #endregion
    
    #region Handle_ShouldCreateUser_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldCreateUser_WhenValidationIsSuccessful()
    {
        CreateUserDto dto = new()
        {
            FullName = "ali",
            Email = "ali@ali.com",
            Phone = "15451541",
            Password = "15145151"
        };
        User user = new()
        {
            Id = 10,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.Password,
        };
        
        _mockRepo.Setup(c=>c.IsUserExistAsyncEmailOrMobile(dto.Email, dto.Phone)).ReturnsAsync(false);
        _mockRepo.Setup(c=>c.AddAsync(user)).Returns(Task.CompletedTask);
        _mockMapper.Setup(c => c.Map<User>(dto)).Returns(user);
        _mockValidator.Setup(c => c.CreateValidate(dto)).ReturnsAsync(CreateUserStatusDto.Success);
        _mockUnit.Setup(c => c.SaveChangesAsync()).Returns(Task.CompletedTask);

        CreateUserCommand command = new(dto);
        CreateUserHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockMapper.Object,_mockValidator.Object);
        CreateUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(CreateUserStatusDto.Success, status);

        _mockValidator.Verify(c=>c.CreateValidate(dto), Times.Once());
        _mockRepo.Verify(c=>c.AddAsync(It.IsAny<User>()), Times.Once());
        _mockUnit.Verify(c=>c.SaveChangesAsync(), Times.Once());
        
        
    } 

    #endregion

    #region Handle_ShouldNotCreateUser_WhenValidationIsFails

    [Fact]
    public async Task Handle_ShouldNotCreateUser_WhenValidationIsFails()
    {
        CreateUserDto dto = new()
        {
            FullName = "ali",
            Email = "ali@ali.com",
            Phone = "15451541",
            Password = "15145151"
        };
        User user = new()
        {
            Id = 10,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.Password,
        };
        
        _mockRepo.Setup(c=>c.IsUserExistAsyncEmailOrMobile(dto.Email, dto.Phone)).ReturnsAsync(false);
        _mockRepo.Setup(c=>c.AddAsync(user)).Returns(Task.CompletedTask);
        _mockMapper.Setup(c => c.Map<User>(dto)).Returns(user);
        _mockValidator.Setup(c => c.CreateValidate(dto)).ReturnsAsync(CreateUserStatusDto.Failed);
        _mockUnit.Setup(c => c.SaveChangesAsync()).Returns(Task.CompletedTask);

        CreateUserCommand command = new(dto);
        CreateUserHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockMapper.Object,_mockValidator.Object);
        CreateUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(CreateUserStatusDto.Failed, status);

        _mockRepo.Verify(c=>c.AddAsync(It.IsAny<User>()), Times.Never());
        _mockUnit.Verify(c=>c.SaveChangesAsync(), Times.Never());
        
        
    }   

    #endregion
    
    #region Handle_ShouldUpdateUser_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldUpdateUser_WhenValidationIsSuccessful()
    {
        UpdateUserDto dto = new()
        {
            Id = 20,
            FullName = "ali",
            Email = "ali@ali.com",
            Phone = "15451541",
            NewPassword = "15145151",
            OldPassword = "1514515123"
        };
        User user1 = new()
        {
            Id = 20,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = "mahedi",
            Email = "mahedi@mahedi.com",
            Phone = "15451541",
            Password = SecretHasher.Hash("15145151"),
        };  
        User user2 = new()
        {
            Id = 3,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.NewPassword,
        };
        
        _mockRepo.Setup(c=>c.GetByIdAsync(dto.Id)).ReturnsAsync(user1);
        
        _mockValidator.Setup(c => c.UpdateValidate(dto,user1)).ReturnsAsync(UpdateUserStatusDto.Success);
        _mockRepo.Setup(c=>c.UpdateAsync(user2)).Returns(Task.CompletedTask);
        _mockMapper.Setup(m => m.Map<UpdateUserDto, User>(dto, user1))
            .Returns(user2);
        

        UpdateUserCommand command = new(dto);
        UpdateUserHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockMapper.Object,_mockValidator.Object);
        UpdateUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(UpdateUserStatusDto.Success, status);
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<User>()),Times.Once());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Once());
        
    }

    #endregion

    #region Handle_ShouldNotUpdateUser_WhenValidationIsFails

    [Fact]
    public async Task Handle_ShouldNotUpdateUser_WhenValidationIsFails()
    {
        UpdateUserDto dto = new()
        {
            Id = 20,
            FullName = "ali",
            Email = "ali@ali.com",
            Phone = "15451541",
            NewPassword = "15145151",
            OldPassword = "1514515123"
        };
        User user1 = new()
        {
            Id = 20,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = "mahedi",
            Email = "mahedi@mahedi.com",
            Phone = "15451541",
            Password = SecretHasher.Hash("15145151"),
        };  
        User user2 = new()
        {
            Id = 3,
            CreateDate = DateTime.Now,
            IsDelete = false,
            FullName = dto.FullName,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.NewPassword,
        };
        
        _mockRepo.Setup(c=>c.GetByIdAsync(dto.Id)).ReturnsAsync(user1);
        
        _mockValidator.Setup(c => c.UpdateValidate(dto,user1)).ReturnsAsync(UpdateUserStatusDto.Failed);
        
        _mockRepo.Setup(c=>c.UpdateAsync(user2)).Returns(Task.CompletedTask);
        _mockMapper.Setup(m => m.Map<UpdateUserDto, User>(dto, user1))
            .Returns(user2);
        
        UpdateUserCommand command = new(dto);
        UpdateUserHandler handler = new(_mockRepo.Object, _mockUnit.Object, _mockMapper.Object,_mockValidator.Object);
        UpdateUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(UpdateUserStatusDto.Failed, status);
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<User>()),Times.Never());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Never());
        
        
    }

    #endregion
    
    #region Handle_ShouldDeleteUser_WhenValidationIsSuccessful

    [Fact]
    public async Task Handle_ShouldDeleteUser_WhenValidationIsSuccessful()
    {

        User user = new()
        {
            Id = 10,
        };
        
        _mockRepo.Setup(c=>c.GetByIdAsync(user.Id)).ReturnsAsync(user);
        _mockValidator.Setup(c=>c.DeleteValidate(user)).ReturnsAsync(DeleteUserStatusDto.Success);  
        _mockRepo.Setup(c=>c.UpdateAsync(user)).Returns(Task.CompletedTask);

        DeleteUserCommand command = new(user.Id);
        
        DeleteUserHandler handler = new(_mockRepo.Object, _mockUnit.Object,_mockValidator.Object);
        
        DeleteUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(DeleteUserStatusDto.Success, status);
        
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<User>()),Times.Once());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Once());
        
    }

    #endregion

    #region Handle_ShouldNotDeleteUser_WhenValidationIsFails

    [Fact]
    public async Task Handle_ShouldNotDeleteUser_WhenValidationIsFails()
    {

        User user = new()
        {
            Id = 10,
        };
        
        _mockRepo.Setup(c=>c.GetByIdAsync(user.Id)).ReturnsAsync(user);
        _mockValidator.Setup(c=>c.DeleteValidate(user)).ReturnsAsync(DeleteUserStatusDto.Failed);  
     

        DeleteUserCommand command = new(user.Id);
        
        DeleteUserHandler handler = new(_mockRepo.Object, _mockUnit.Object,_mockValidator.Object);
        
        DeleteUserStatusDto status = await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(DeleteUserStatusDto.Failed, status);
        
        _mockRepo.Verify(c=>c.UpdateAsync(It.IsAny<User>()),Times.Never());
        _mockUnit.Verify(u => u.SaveChangesAsync(), Times.Never());
        
    }

    #endregion

}