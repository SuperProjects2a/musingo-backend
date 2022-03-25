namespace musingo_backend.Dtos
{
    public class UserCommentCreateDto
    {
        public int TransactionId { get; set; }
        public double? Rating { get; set; }
        public string? CommentText { get; set; }
    }
}
