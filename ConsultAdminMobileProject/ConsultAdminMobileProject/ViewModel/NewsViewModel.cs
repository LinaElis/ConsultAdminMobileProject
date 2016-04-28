using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdmin.Entities;

namespace ConsultAdminMobileProject.ViewModel
{
    public class NewsViewModel
    {
        public string Subject { get; set; }
        public string NewsBody { get; set; }
        public DateTime PublishedDate { get; set; }

        public NewsViewModel(object param)
        {
            var newsClicked = param as News;

            if (newsClicked == null) return;
            Subject = newsClicked.Subject;
            NewsBody = newsClicked.NewsBody;
            PublishedDate = newsClicked.PublishedDate;
        }
    }
}
