﻿using System;

namespace StudentCity.DAL.Shared
{
   public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
