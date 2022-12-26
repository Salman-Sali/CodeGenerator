﻿using CG.CqrsCrud.Attributes.Commons;
using CG.CqrsCrud.Attributes.MediatorAttributes.Commands;
using CG.CqrsCrud.Attributes.MediatorAttributes.Queries;

namespace CG.Domain.Entities
{
    [AddMediator]
    [UpdateMediator]
    [DeleteMediator]
    [GetMediator]
    [GetListMediator]
    public class Book
    {
        [PrimaryKey]
        [AutoGeneratedPrimaryKey]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
    }
}
