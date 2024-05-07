﻿using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.BookComments
{
    public interface IUserBookAppService : ICrudAppService< 
        BookCommentDto,
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateBookCommentDto> 
    {
    }
}
