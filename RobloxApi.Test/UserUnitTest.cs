﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace RobloxApi.Test
{
    [TestClass]
    public class UserUnitTest
    {
        [TestMethod]
        public void GetUser()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            Console.WriteLine("Id: {0} Username: {1}", user.ID, user.Username);
        }

        [TestMethod]
        public void UserCanManageAsset()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            bool canManageAsset = user.CanManageAsset((Asset)TestConstants.TestAssetId).WaitForResult(TestConstants.MaxMilisecondTimeout);

#pragma warning disable 0162

            if (TestConstants.ExpectedManageResult)
                Assert.IsTrue(canManageAsset);
            else
                Assert.IsFalse(canManageAsset);

#pragma warning restore 0162

            Console.WriteLine("Can \"{0}\" managet asset {1}? {2}", user, TestConstants.TestAssetId, canManageAsset);
        }

        [TestMethod]
        public void UserOwnsAsset()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            bool ownsAsset = user.OwnsAsset((Asset)TestConstants.TestAssetId).WaitForResult(TestConstants.MaxMilisecondTimeout);

#pragma warning disable 0162

            if (TestConstants.ExpectedAssetOwnResult)
                Assert.IsTrue(ownsAsset);
            else
                Assert.IsFalse(ownsAsset);

#pragma warning restore 0162

            Console.WriteLine("Does \"{0}\" own asset {1}? {2}", user, TestConstants.TestAssetId, ownsAsset);
        }

        [TestMethod]
        public void GetFriendsPage1FromUser()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            FriendList.Page firstPage = user.FriendList.Get(1).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(firstPage);

            Console.WriteLine("1st Page Count:{0}", firstPage);
        }

        [TestMethod]
        public void GetFriendsFromUser()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            FriendList.Page[] friendPages = user.FriendList.GetPagesAsArray().WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(friendPages);

            int pageIndex = 1;
            int entryCount = 0;
            foreach(FriendList.Page page in friendPages)
            {
                Console.WriteLine("Page {0}:", pageIndex);
                pageIndex++;
                foreach (FriendList.Entry entry in page)
                {
                    entryCount++;
                    Console.WriteLine("User {0} ({1}). IsOnline: {2}", entry.User.Username, entry.User.ID, entry.IsOnline);
                }
                Console.WriteLine("----");
            }

            Console.WriteLine("Total Pages: {0} Total Count: {1}", pageIndex, entryCount);
        }

        [TestMethod]
        public void IsUserFriendsWith()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            User friendTestUser = User.FromID(5762824).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Console.WriteLine("User {0} friends with {1}? {2}", user, friendTestUser, user.IsFriendsWith(friendTestUser).WaitForResult(TestConstants.MaxMilisecondTimeout));
        }

        [TestMethod]
        public void GetUserMembership()
        {
            User user = User.FromID(TestConstants.TestUserId).WaitForResult(TestConstants.MaxMilisecondTimeout);

            Assert.IsNotNull(user);

            Console.WriteLine("User {0}'s membership level: {1}", user.Username, user.GetMembershipLevel().WaitForResult(TestConstants.MaxMilisecondTimeout));
        }
    }
}
