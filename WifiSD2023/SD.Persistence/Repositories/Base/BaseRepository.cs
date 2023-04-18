﻿using SD.Persistence.Repositories.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Repositories;

namespace SD.Persistence.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        MovieDbContext movieDbContext;

        #region ctor
        public BaseRepository()
        {
            this.movieDbContext = new MovieDbContext();
        }

        public BaseRepository(MovieDbContext movieDbContext)
        {
            this.movieDbContext = movieDbContext;
        }
        #endregion

        #region [C]REATE

        public void Add<T>(T entity, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Add(entity);

            if (saveImmediately)
            {
                this.movieDbContext.SaveChanges();
            }
        }

        public async Task AddAsync<T>(T entity, bool saveImmediately = false, CancellationToken cancellation = default)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Add(entity);

            if (saveImmediately)
            {
                await this.movieDbContext.SaveChangesAsync(cancellation);
            }
        }

        #endregion

        #region [R]READ

        public IQueryable<T> QueryFrom<T>(Expression<Func<T, bool>> whereFilter)
            where T : class, IEntity
        {
            var query = this.movieDbContext.Set<T>();
            if (whereFilter != null)
            {
                return query.Where(whereFilter);
            }

            return query;
        }

        #endregion

        #region [U]PDATE
        public T Update<T>(T entity, object key, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = this.movieDbContext.Set<T>().Find(key);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    this.movieDbContext.SaveChanges();
                }
            }

            return toUpdate;
        }

        public async Task<T> UpdateAsync<T>(T entity, object key, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = await this.movieDbContext.Set<T>().FindAsync(key);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    await this.movieDbContext.SaveChangesAsync(cancellationToken);
                }
            }

            return toUpdate;
        }
        #endregion

        #region [D]ELETE
        public void Remove<T>(T entity, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Remove(entity);

            if (saveImmediately)
            {
                this.movieDbContext.SaveChanges();
            }
        }

        public async Task RemoveAsync<T>(T entity, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Remove(entity);

            if (saveImmediately)
            {
                await this.movieDbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public void RemoveByKey<T>(object key, bool saveImmediately = false)
            where T : class, IEntity
        {
            if (key == null)
            {
                return;
            }

            var toRemove = this.movieDbContext.Set<T>().Find(key);

            if (toRemove != null)
            {
                this.movieDbContext.Remove<T>(toRemove);

                if (saveImmediately)
                {
                    this.movieDbContext.SaveChanges();
                }
            }
            

            


        }



        public async Task RemoveAsyncByKey<T>(object key, bool saveImmediately = false, CancellationToken cancellationToken = default)
            where T : class, IEntity
        {
            if (key == null)
            {
                return;
            }

            var toRemove = this.movieDbContext.Set<T>().Find(key);

            if (toRemove != null)
            {
                this.movieDbContext.Remove<T>(toRemove);
            }

            if (saveImmediately)
            {
                await this.movieDbContext.SaveChangesAsync();
            }
        }

        #endregion

    }
}
