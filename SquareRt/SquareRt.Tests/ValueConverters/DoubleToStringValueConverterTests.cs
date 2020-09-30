using NUnit.Framework;

namespace SquareRt.Tests.ValueConverters
{
    [TestFixture]
    public class DoubleToStringValueConverter
    {
        DoubleToStringValueConverter _subject;

        [SetUp]
        public void SetUp ()
        {
            _subject = new DoubleToStringValueConverter ();
        }

        [Test]
        public void Convert_DoubleToString ()
        {
            var subject = new DoubleToStringValueConverter ();
        }
    }
}