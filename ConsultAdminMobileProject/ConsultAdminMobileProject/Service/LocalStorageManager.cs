using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsultAdminMobileProject.Interface;
using ConsultAdminMobileProject.Model;
using SQLite;
using Xamarin.Forms;

namespace ConsultAdminMobileProject.Service
{
    public class LocalStorageManager
    {
        private readonly ILogger _logger = new PCLLogger();
        private readonly SQLiteConnection _database;
        private static readonly object Locker = new object();

        public string UserId { get; set; }
        public string Password { get; set; }

        public LocalStorageManager()
        {
            _database = DependencyService.Get<ISQLite>().GetConnection();
            _database.CreateTable<SavedUser>();
        }

        public void SaveUseridAndPassword(string userid, string password)
        {
            lock (Locker)
            {
                SavedUser savedUser = new SavedUser
                {
                    Id = 1,
                    UserId = userid,
                    Password = password
                };
                int no = _database.InsertOrReplace(savedUser);
                _logger.LoggText($"SaveUseridAndPassword affected {no.ToString()} rows when set {userid}");
            }
        }

        public void GetUseridAndPassword()
        {
            lock (Locker)
            {
                SavedUser savedUser = _database.Table<SavedUser>().FirstOrDefault(c => c.Id == 1);
                if (savedUser != null)
                {
                    this.UserId = savedUser.UserId;
                    this.Password = savedUser.Password;
                }
                else
                {
                    this.UserId = "";
                    this.Password = "";
                }
            }
        }

        public void ClearUseridAndPassword()
        {
            lock (Locker)
            {
                _database.Delete<SavedUser>(1);
                _logger.LoggText("ClearUseridAndPassword");
            }
        }
    }
}
