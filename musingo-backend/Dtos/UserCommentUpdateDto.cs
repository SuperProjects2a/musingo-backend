using System.ComponentModel.DataAnnotations;

namespace musingo_backend.Dtos
{
    public class UserCommentUpdateDto
    {
        public int Id { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public double? Rating { get; set; }
        [MaxLength(300, ErrorMessage = "Max length 300 characters")]
        public string? CommentText { get; set; }
    }
}
