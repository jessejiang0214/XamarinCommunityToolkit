﻿using System;
using Xamarin.CommunityToolkit.Exceptions;
using Xamarin.CommunityToolkit.Helpers;
using Xunit;

namespace Xamarin.CommunityToolkit.UnitTests.Helpers.WeakEventManagerTests
{
	public class WeakEventManager_EventHandlerT_Tests : BaseWeakEventManagerTests
	{
		[Fact]
		public void WeakEventManagerTEventArgs_HandleEvent_ValidImplementation()
		{
			// Arrange
			TestStringEvent += HandleTestEvent;

			const string stringEventArg = "Test";
			var didEventFire = false;

			void HandleTestEvent(object sender, string? e)
			{
				if (sender == null || e == null)
					throw new ArgumentNullException(nameof(sender));

				Assert.NotNull(sender);
				Assert.Equal(GetType(), sender.GetType());

				Assert.NotNull(e);
				Assert.Equal(stringEventArg, e);

				didEventFire = true;
				TestStringEvent -= HandleTestEvent;
			}

			// Act
			TestStringWeakEventManager.RaiseEvent(this, stringEventArg, nameof(TestStringEvent));

			// Assert
			Assert.True(didEventFire);
		}

		[Fact]
		public void WeakEventManageTEventArgs_HandleEvent_NullSender()
		{
			// Arrange
			TestStringEvent += HandleTestEvent;

			const string stringEventArg = "Test";

			var didEventFire = false;

			void HandleTestEvent(object sender, string e)
			{
				Assert.Null(sender);

				Assert.NotNull(e);
				Assert.Equal(stringEventArg, e);

				didEventFire = true;
				TestStringEvent -= HandleTestEvent;
			}

			// Act
			TestStringWeakEventManager.RaiseEvent(null, stringEventArg, nameof(TestStringEvent));

			// Assert
			Assert.True(didEventFire);
		}

		[Fact]
		public void WeakEventManagerTEventArgs_HandleEvent_NullEventArgs()
		{
			// Arrange
			TestStringEvent += HandleTestEvent;
			var didEventFire = false;

			void HandleTestEvent(object sender, string e)
			{
				if (sender == null)
					throw new ArgumentNullException(nameof(sender));

				Assert.NotNull(sender);
				Assert.Equal(GetType(), sender.GetType());

				Assert.Null(e);

				didEventFire = true;
				TestStringEvent -= HandleTestEvent;
			}

			// Act
#pragma warning disable CS8625 //Cannot convert null literal to non-nullable reference type
			TestStringWeakEventManager.RaiseEvent(this, null, nameof(TestStringEvent));
#pragma warning restore CS8625

			// Assert
			Assert.True(didEventFire);
		}

		[Fact]
		public void WeakEventManagerTEventArgs_HandleEvent_InvalidHandleEvent()
		{
			// Arrange
			TestStringEvent += HandleTestEvent;

			var didEventFire = false;

			void HandleTestEvent(object sender, string e) => didEventFire = true;

			// Act
			TestStringWeakEventManager.RaiseEvent(this, "Test", nameof(TestEvent));

			// Assert
			Assert.False(didEventFire);
			TestStringEvent -= HandleTestEvent;
		}

		[Fact]
		public void WeakEventManager_NullEventManager()
		{
			// Arrange
			WeakEventManager unassignedEventManager = null;

			// Act

			// Assert
#pragma warning disable CS8602 //Dereference of a possible null reference
			Assert.Throws<NullReferenceException>(() => unassignedEventManager.RaiseEvent(null, null, nameof(TestEvent)));
#pragma warning restore CS8602
		}

		[Fact]
		public void WeakEventManagerTEventArgs_UnassignedEventManager()
		{
			// Arrange
			var unassignedEventManager = new WeakEventManager<string>();
			var didEventFire = false;

			TestStringEvent += HandleTestEvent;
			void HandleTestEvent(object sender, string e) => didEventFire = true;

			// Act
#pragma warning disable CS8625 //Cannot convert null literal to non-nullable reference type
			unassignedEventManager.RaiseEvent(null, null, nameof(TestStringEvent));
#pragma warning restore CS8625

			// Assert
			Assert.False(didEventFire);
			TestStringEvent -= HandleTestEvent;
		}

		[Fact]
		public void WeakEventManagerTEventArgs_UnassignedEvent()
		{
			// Arrange
			var didEventFire = false;

			TestStringEvent += HandleTestEvent;
			TestStringEvent -= HandleTestEvent;
			void HandleTestEvent(object sender, string e) => didEventFire = true;

			// Act
			TestStringWeakEventManager.RaiseEvent(this, "Test", nameof(TestStringEvent));

			// Assert
			Assert.False(didEventFire);
		}

		[Fact]
		public void WeakEventManagerT_AddEventHandler_NullHandler()
		{
			// Arrange

			// Act

			// Assert
#pragma warning disable CS8625 //Cannot convert null literal to non-nullable reference type
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler((EventHandler<string>)null));
#pragma warning restore CS8625
		}

		[Fact]
		public void WeakEventManagerT_AddEventHandler_NullEventName()
		{
			// Arrange

			// Act

			// Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, null));
#pragma warning restore CS8625
		}

		[Fact]
		public void WeakEventManagerT_AddEventHandler_EmptyEventName()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, string.Empty));
		}

		[Fact]
		public void WeakEventManagerT_AddEventHandler_WhiteSpaceEventName()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, " "));
		}

		[Fact]
		public void WeakEventManagerT_RemoveEventHandler_NullHandler()
		{
			// Arrange

			// Act

			// Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.RemoveEventHandler((EventHandler<string>)null));
#pragma warning restore CS8625
		}

		[Fact]
		public void WeakEventManagerT_RemoveEventHandler_NullEventName()
		{
			// Arrange

			// Act

			// Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference
		}

		[Fact]
		public void WeakEventManagerT_RemoveEventHandler_EmptyEventName()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, string.Empty));
		}

		[Fact]
		public void WeakEventManagerT_RemoveEventHandler_WhiteSpaceEventName()
		{
			// Arrange

			// Act

			// Assert
			Assert.Throws<ArgumentNullException>(() => TestStringWeakEventManager.AddEventHandler(s => { var temp = s; }, string.Empty));
		}

		[Fact]
		public void WeakEventManagerT_HandleEvent_InvalidHandleEvent()
		{
			// Arrange
			TestStringEvent += HandleTestStringEvent;
			var didEventFire = false;

			void HandleTestStringEvent(object sender, string e) => didEventFire = true;

			// Act

			// Assert
			Assert.Throws<InvalidHandleEventException>(() => TestStringWeakEventManager.RaiseEvent("", nameof(TestStringEvent)));
			Assert.False(didEventFire);
			TestStringEvent -= HandleTestStringEvent;
		}
	}
}