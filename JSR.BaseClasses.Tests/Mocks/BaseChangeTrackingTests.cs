using JSR.Asserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClasses.Tests.Mocks
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit tests")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit tests")]

    public class BaseChangeTrackingTests
    {
        [TestMethod]
        public void NotifiesChanged_WhenAcceptChanges()
        {
            MockBaseNotifyChanged notify = new();
            List<bool> changes = new();
            notify.OnChanged += (sender, changed) => changes.Add(changed);

            Assert.IsTrue(notify.IsChanged);

            notify.AcceptChanges();

            Assert.Equals(1, changes.Count);

            for (int i = 0; i < new Random().Next(5, 10); i++)
            {
                notify.AcceptChanges();
            }

            Assert.Equals(1, changes.Count);

            Assert.That.AcceptsChanges<MockBaseNotifyChanged>();
            Assert.That.AcceptsChanges<MockBaseNotifyChangedParent>();
        }

        [TestMethod]
        [DataRow(nameof(MockBaseNotifyChanged.IntegetProperty))]
        [DataRow(nameof(MockBaseNotifyChanged.StringProperty))]
        public void IsChanged_Notifies_WhenPropertyChanges(string propertyName)
        {
            MockBaseNotifyChanged notify = new();
            notify.AcceptChanges();

            List<bool> changes = new();
            notify.OnChanged += (sender, changed) => changes.Add(changed);

            ObjectUtilities.PopulatePropertyWithRandomValue(notify, propertyName);

            Assert.IsTrue(notify.IsChanged);
            Assert.AreEqual(1, changes.Count);

            Assert.That.NotifiesIsChangedWhenPropertiesChange<MockBaseNotifyChanged>();
            Assert.That.NotifiesIsChangedWhenPropertiesChange<MockBaseNotifyChangedParent>();
        }

        [TestMethod]
        public void IsChanged_Notifies_OnceWhenPropertiesChange()
        {
            Assert.That.NotifiesIsChangedWhenPropertiesChange<MockBaseNotifyChanged>();
            Assert.That.NotifiesIsChangedOnAcceptChanges<MockBaseNotifyChanged>();
        }
    }
}
