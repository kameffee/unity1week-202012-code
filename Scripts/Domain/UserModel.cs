using Unlocks.Data;

namespace Unlocks.Domain
{
    public class UserModel
    {
        private User _cache;

        public string UserName
        {
            get => _cache.UserName;
            set => _cache.UserName = value;
        }

        public UserModel()
        {
            
        }

        public void Initialize()
        {
            _cache = new User();
        }

        public void Save()
        {
            // No save
        }
    }
}