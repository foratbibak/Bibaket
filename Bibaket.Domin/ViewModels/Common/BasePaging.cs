using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Domain.ViewModels.Common
{
    public class BasePaging<T>
    {
        public BasePaging()
        {
            PageId = 1;
            TakeEntity = 8;
            HowManyAfterAndBefore = 1;
            Entities = new List<T>();
        }
        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int AllEntitiesCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TakeEntity { get; set; }
        public int SkipEntity { get; set; }
        public int HowManyAfterAndBefore { get; set; }
        public List<T> Entities { get; set; }

        public async Task<BasePaging<T>> Paging(IQueryable<T> query)
        {
            var allEntitiesCount = query.Count();
            var pageCount = (int)Math.Ceiling(allEntitiesCount / (double)TakeEntity);
            PageId = pageCount < 1 ? PageId : PageId > pageCount ? pageCount : PageId;
            AllEntitiesCount = allEntitiesCount;

            SkipEntity=(PageId - 1) * TakeEntity;

            PageCount = pageCount;

            StartPage = PageId - HowManyAfterAndBefore <= 0 ? 1 : PageId - HowManyAfterAndBefore;
            EndPage = PageId + HowManyAfterAndBefore > PageCount ? PageCount:PageId+HowManyAfterAndBefore;

            Entities = query.Skip(SkipEntity).Take(TakeEntity).ToList();

            return this;
        }

        public PagingViewModel GetCurrentPaging()
        {
            return new PagingViewModel()
            {
                PageId =this.PageId,
                StartPage=this.StartPage,
                EndPage=this.EndPage

            };
        }
    }
    public class PagingViewModel
    {
        public int PageId { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
