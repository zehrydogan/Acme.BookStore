﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.BookComments
{
    public class UserBookAppService :
     CrudAppService<
         BookComment,
         BookCommentDto, 
         Guid, 
         PagedAndSortedResultRequestDto,
         CreateUpdateBookCommentDto>, 
     IUserBookAppService 
    {
        public UserBookAppService(IRepository<BookComment, Guid> repository)
            : base(repository)
        {

        }
    }
}
