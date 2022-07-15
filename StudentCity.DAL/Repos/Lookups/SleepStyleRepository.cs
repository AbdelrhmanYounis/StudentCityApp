﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StudentCity.DAL.Models.Lookups;
using StudentCity.DAL.Shared;
using StudentCity.DAL.Utilities;
using Microsoft.Extensions.Options;

namespace StudentCity.DAL.Repos.Lookups
{
   
    public class SleepStyleRepository : GenericRepository<SleepStyle>
    {
        public SleepStyleRepository(IOptions<DbSettings> settings) : base(settings, "SleepStyles")
        {
        }

    }
}
