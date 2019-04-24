using log4net;
using OneCountryWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCountryWebApi.Repository.Base
{

    public class BaseRepository
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        internal ILog _Logger { get; set; }  
    }
}