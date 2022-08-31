
namespace Ibdb.Shared.Application
{
    public static class EventNames
    {
        public static class Books
        {
            public static string BookCreated => "Books_BookCreated";

            public static string BookEdited => "Books_BookEdited";

            public static string BookDeleted => "Books_BookDeleted";

            public static string BookReviewed => "Books_BookReviewed";

            public static string BookReviewDeleted => "Books_BookReviewDeleted";
        }

        public static class Reviews
        {
            public static string ReviewCreated => "Reviews_ReviewCreated";

            public static string ReviewEdited => "Reviews_ReviewEdited";

            public static string ReviewDeleted => "Reviews_ReviewDeleted";

            public static string BookCreated => "Reviews_BookCreated";

            public static string BookEdited => "Reviews_BookEdited";

            public static string BookDeleted => "Reviews_BookDeleted";

            public static string ReviewBookEdited => "Reviews_ReviewBookEdited";
        }
    }
}
