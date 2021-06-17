﻿using System;
using ReactiveTest.Models;
using System.Threading.Tasks;
using Couchbase.Lite;

namespace ReactiveTest.Repositories
{
    public interface IUserProfileRepository : IDisposable
    {
        Task<UserProfile> GetAsync(string userProfileId);
        Task<bool> SaveAsync(UserProfile userProfile);
    }

    public sealed class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository() : base("userprofile")
        { }

        public async Task<UserProfile> GetAsync(string userProfileId)
        {
            UserProfile userProfile = null;

            try
            {
                var database = await GetDatabaseAsync();

                if (database != null)
                {
                    var document = database.GetDocument(userProfileId);

                    if (document != null)
                    {
                        userProfile = new UserProfile
                        {
                            Id = document.Id,
                            Name = document.GetString("Name"),
                            Email = document.GetString("Email"),
                            Address = document.GetString("Address"),
                            ImageData = document.GetBlob("ImageData")?.Content
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return userProfile;
        }

        public async Task<bool> SaveAsync(UserProfile userProfile)
        {
            try
            {
                if (userProfile != null)
                {
                    var mutableDocument = new MutableDocument(userProfile.Id);
                    mutableDocument.SetString("Name", userProfile.Name);
                    mutableDocument.SetString("Email", userProfile.Email);
                    mutableDocument.SetString("Address", userProfile.Address);
                    mutableDocument.SetString("type", "user");

                    if (userProfile.ImageData != null)
                    {
                        mutableDocument.SetBlob("ImageData", new Blob("image/jpeg", userProfile.ImageData));
                    }

                    var database = await GetDatabaseAsync();

                    database.Save(mutableDocument);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UserProfileRepository Exception: {ex.Message}");
            }

            return false;
        }
    }
}