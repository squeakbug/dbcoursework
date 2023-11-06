using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    class SqlMarkInfoRepository : IMarkInfoRepository
    {
        private Context _globalCtx;

        public SqlMarkInfoRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<MarkInfo> GetMarkInfos()
        {
            var converter = new MarkInfoConverter();
            var result = new List<MarkInfo>();
            foreach (var item in _globalCtx.Mark_info)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public MarkInfo GetMarkInfoById(int id)
        {
            var markInfo = _globalCtx.Mark_info.Find(id);
            var converter = new MarkInfoConverter();
            return  markInfo == null ? null : converter.MapToBusinessEntity(markInfo, _globalCtx);
        }

        public void Create(MarkInfo markInfo)
        {
            var converter = new MarkInfoConverter();
            _globalCtx.Mark_info.Add(converter.MapFromBusinessEntity(markInfo));
            _globalCtx.SaveChanges();
        }

        public void Update(MarkInfo markInfo)
        {
            var converter = new MarkInfoConverter();
            _globalCtx.Mark_info.Update(converter.MapFromBusinessEntity(markInfo));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var markInfo = _globalCtx.Mark_info.Find(id);
            if (markInfo == null)
                throw new MarkInfoException("No mark info with such id");

            _globalCtx.Mark_info.Remove(markInfo);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.mark_info");
            _globalCtx.SaveChanges();
        }

        private bool disposed = false;

        private void CleanUp(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _globalCtx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
    }
}
