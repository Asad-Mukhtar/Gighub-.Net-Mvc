﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Models;
using GigHub.ViewModels;

namespace GigHub.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _context;
        public GigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGigsWithAttendences(int id)
        {
            return _context.Gigs
                .Include(g => g.Attendences.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id ==id);
        }

        public IEnumerable< Gig> GetGigsUserAttending(string userId)
        {
            return _context.Attendences
                .Where(g => g.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Genre)
                .Include(g => g.Artist)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g =>
                    g.ArtistId == userId &&
                    g.DateTime > DateTime.Now
                    && !g.IsCanceled)
                .Include(g => g.Artist)
                .ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context
                .Gigs
                .Include(g=>g.Artist)
                .Include(g=>g.Artist)
                .SingleOrDefault( g => g.Id == gigId );
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}