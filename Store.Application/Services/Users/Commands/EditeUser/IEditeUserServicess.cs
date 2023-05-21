﻿using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Constant;
using Store.Common.Constant.Enum;
using Store.Common.ResultDto;
using Store.Domain.Entities.Contacts;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Users.Commands.EditeUser
{
    public interface IEditeUserServicess
    {
        public ResultDto Execute(UserEditeDetailDto request);
    }
    public class EditeUserServicess : IEditeUserServicess
    {
        private readonly IDataBaseContext _context;
        public EditeUserServicess(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(UserEditeDetailDto request)
        {
            var userItem = _context.Users
                .Include(p => p.Logins)
                .Include(p => p.UserInRolls)
                .Include(p => p.Contacts)
                .Where(p => p.Id==request.Id)
                .FirstOrDefault();

            if (userItem!=null)
            {
                userItem.Name=request.Name;
                userItem.LastName=request.LastName;
                userItem.FullName=request.Name+" "+request.LastName;
                userItem.IsActive=request.IsActive;
                userItem.UpdateTime=DateTime.Now;
                userItem.Gender=request.Gender;

                //Remove Last Rolls
                var userInRolls = userItem.UserInRolls.ToList();
                _context.UserInRolls.RemoveRange(userInRolls);

                //Add New Rolls
                List<UserInRoll> userInRollList = new List<UserInRoll>();
                foreach (long item in request.Rolls)
                {
                    userInRollList.Add(new UserInRoll
                    {
                        RollId= item,
                        UserId= request.Id,
                        InsertTime=DateTime.Now,
                    });
                }
                _context.UserInRolls.AddRange(userInRollList);

                //Remove Last Contacts
                var contacts = userItem.Contacts.ToList();
                _context.Contacts.RemoveRange(contacts);

                //Add New Contacts
                List<Contact> contactsList = new List<Contact>();
                contactsList.Add(new Contact
                {
                    ContactTypeId=(long)ContactTypeEnum.Mobile,
                    UserId=request.Id,
                    Title=ContactTypeTitle.Mobile,
                    Value=request.Mobile,
                    InsertTime=DateTime.Now
                });
                contactsList.Add(new Contact
                {
                    ContactTypeId=(long)ContactTypeEnum.Email,
                    UserId=request.Id,
                    Title=ContactTypeTitle.Email,
                    Value=request.Email,
                    InsertTime=DateTime.Now
                });
                if (request.Address?.Trim().Length>0)
                {
                    contactsList.Add(new Contact
                    {
                        ContactTypeId=(long)ContactTypeEnum.Address,
                        UserId=request.Id,
                        Title=ContactTypeTitle.Address,
                        Value=request.Address,
                        InsertTime=DateTime.Now
                    });
                }
                _context.Contacts.AddRange(contactsList);
                _context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess=true,
                    Message=Messages.Message.RegisterSuccess
                };
            }
            return new ResultDto
            {
                IsSuccess=false,
                Message=Messages.ErrorsMessage.RegisterFeaild
            };
        }
    }
    public class UserEditeDetailDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public long[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
    }
}
