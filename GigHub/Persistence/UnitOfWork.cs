﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {       
        private readonly ApplicationDbContext _context;
        public IGigRepository Gigs { get; private set; }
        public IAttendenceRepository Attendence { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public IArtistRepository Artist { get; private set; }
        public INotificationRepository Notification { get; private set; }
        public ICompositionRepository Composition { get; private set; }        

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendence = new AttendenceRepository(_context);
            Following = new FollowingRepository(_context);
            Genre = new GenreRepository(_context);
            Notification = new NotificationRepository(_context);
            Composition = new CompositionRepository(_context);
            Artist = new ArtistRepository(_context);
        }
        /// <summary>
        /// Method to commit the changes
        /// </summary>
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}