using Microsoft.EntityFrameworkCore;

namespace PersonalToDoList.Data.Repositories
{
    public class GenericRepository<T> where T : class,new()
    {
        private readonly Context.Context cnt;

        public GenericRepository(Context.Context context)
        {
            cnt = context;
        }
        public List<T> TList()
        {
            return cnt.Set<T>().ToList();
        }

        public void TAdd(T c)
        {
            cnt.Set<T>().Add(c);
            cnt.SaveChanges();
        }

        public void TDelete(T c)
        {
            cnt.Set<T>().Remove(c);
            cnt.SaveChanges();
        }

        public void TUpdate(T c)
        {
            cnt.Set<T>().Update(c);
            cnt.SaveChanges();
        }
        public T TGetById(int id)
        {
            return cnt.Set<T>().Find(id);
        }

        public List<T> TList(string p)
        {
            return cnt.Set<T>().Include(p).ToList();
        }
    }
}
