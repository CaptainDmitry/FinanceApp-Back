﻿using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using TestApi.Enums;

namespace TestApi.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(TransactionType))]
        public TransactionType TransactionType { get; set; }
    }
}
