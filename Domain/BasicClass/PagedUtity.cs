using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BasicClass
{
    /// <summary>
    /// 分页调用类
    /// </summary>
    public  class PagedUtity
    {
        /// <summary>
        /// 分页调用语句拼接
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页条数</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <returns></returns>
        public string PagedSql(string tableName, int page, int limit, string condition,string orderBy)
        {
            StringBuilder stringBu = new StringBuilder();
            stringBu.Append("select top ");
            stringBu.Append(" " + limit + " ");
            stringBu.Append(" *  from ( SELECT ROW_NUMBER() OVER (ORDER BY ");
            stringBu.Append(orderBy + " ) AS RowNumber,* FROM ");
            stringBu.Append(tableName + "  WHERE 1=1");
            if (!string.IsNullOrEmpty(condition))
            {
                stringBu.Append(condition);
            }
            stringBu.Append(" ) A WHERE A.RowNumber > "+limit+"*("+page+"-1)");
            return stringBu.ToString();
        }
    }
}
