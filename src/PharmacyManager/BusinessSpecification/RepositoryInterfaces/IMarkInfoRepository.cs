using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IMarkInfoRepository : IDisposable
    {
        IEnumerable<MarkInfo> GetMarkInfos();
        MarkInfo GetMarkInfoById(int id);
        void Create(MarkInfo markInfo);
        void Update(MarkInfo markInfo);
        void Delete(int id);
        void Truncate();
    }
}
