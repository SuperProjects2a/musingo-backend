namespace musingo_backend.Dtos
{
    public class UserCommentUpdateDto
    {
        public int Id { get; set; }
        public double? Rating { get; set; }
        public string? CommentText { get; set; }
    }
}
