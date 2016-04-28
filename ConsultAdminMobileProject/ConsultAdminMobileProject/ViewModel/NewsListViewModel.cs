using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;
using ConsultAdminMobileProject.Model;
using ConsultAdminMobileProject.Service;

namespace ConsultAdminMobileProject.ViewModel
{
    public class NewsListViewModel
    {
        public string UserFullname { get; set; }
        public List<News> News { get; set; }

        public NewsListViewModel() { }

        public async Task<bool> FillNewsList()
        {
            UserFullname = CurrentUser.FullName;
            NewsManager nm = new NewsManager();
            List<News> news = await nm.GetAllNews();

            if (news != null && news.Count > 0)
            {
                News = news;
                return true;
            }
            News = new List<News>();
            return false;
        }
    }
}
