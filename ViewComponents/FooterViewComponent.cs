﻿using FashKartWebsite.DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace FashKartWebsite.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {

        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            var footer = await _dbContext.Footers.SingleOrDefaultAsync();
            return View(footer);
        }
    }
}
